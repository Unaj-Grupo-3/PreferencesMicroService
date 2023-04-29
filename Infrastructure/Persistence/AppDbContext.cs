using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<InterestCategory> InterestCategoryDb { get; set; }
        public DbSet<Interest> InterestDb { get; set; }
        public DbSet<Preference> PreferenceDb { get; set; }
        public DbSet<OverallPreference> OverallPreferenceDb { get; set; }
        public DbSet<GenderPreference> GenderPreferenceDb { get; set; }

        //CONSTRUCTOR
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //MODELADO
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //CATEGORIA INTERES
            modelBuilder.Entity<InterestCategory>(entity =>
            {
                entity.ToTable("InterestCategory");
                entity.HasKey(ic => ic.InterestCategoryId);
                entity.Property(ic => ic.InterestCategoryId).ValueGeneratedOnAdd();
                entity.Property(ic => ic.Description).HasColumnType("nvarchar(50)");

            });

            //INTERES
            modelBuilder.Entity<Interest>(entity =>
            {
                entity.ToTable("Interest");
                entity.HasKey(i => i.InterestId);
                entity.Property(i => i.InterestId).ValueGeneratedOnAdd();
                entity.Property(i => i.Description).HasColumnType("nvarchar(50)");

                //RELACION
                entity
                    .HasOne<InterestCategory>(interest => interest.InterestCategory)
                    .WithMany(interest => interest.Interests)
                    .HasForeignKey(s => s.InterestCategoryId);
            });

            //PREFERENCIA
            modelBuilder.Entity<Preference>(entity =>
            {
                entity.ToTable("Preference");
                entity.HasKey(p => new { p.UserId, p.InterestId } );

                //RELACION
                entity
                    .HasOne<Interest>(preference => preference.Interest)
                    .WithMany(interest => interest.Preferences)
                    .HasForeignKey(preference => preference.InterestId);
                //FK p.UserId

            });

            //PREFERENCIA GENERAL
            modelBuilder.Entity<OverallPreference>(entity =>
            {
                entity.ToTable("OverallPreference");
                entity.HasKey(op => op.OverallPreferenceId);
                entity.Property(op => op.OverallPreferenceId).ValueGeneratedOnAdd();

                //FK p.UserId

            });

            //PREFERENCIA DE GENERO
            modelBuilder.Entity<GenderPreference>(entity =>
            {
                entity.ToTable("GenderPreference");
                entity.HasKey(p => new { p.UserId, p.GenderId });

                //FK p.UserId y p.GenderId
            });
        }
    }
}

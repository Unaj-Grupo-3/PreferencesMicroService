using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Interest
    {
        [Key]
        public int InterestId { get; set; }
        public string InterestCategoryId { get; set; } //FK varchar(50)
        public string Description { get; set; } //varchar(50)
        
        //RELACION
        public List<InterestCategory> InterestCategories { get; set; }
        public List<Preference> Preferences { get; set; }
    }
}

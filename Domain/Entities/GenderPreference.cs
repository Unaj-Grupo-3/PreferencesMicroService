using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class GenderPreference
    {
        [Key]
        public int GenderPreferenceId { get; set; }
        public int UserId { get; set; } //FK
        public int GenderId { get; set; } //FK
    }
}

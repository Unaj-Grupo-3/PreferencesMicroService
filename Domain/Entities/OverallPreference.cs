using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class OverallPreference
    {
        public int OverallPreferenceId { get; set; }
        public int UserId { get; set; } //FK
        public int SinceAge { get; set; }
        public int UntilAge { get; set; }
        public int Distance { get; set; }
    }
}

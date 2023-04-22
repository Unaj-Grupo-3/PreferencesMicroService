using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Preference
    {
        public int UserId { get; set; } //FK
        public int InterestId { get; set; } //FK
        public bool OwnInterest { get; set; }
        public bool Like { get; set; }
        
        //RELACION
        public virtual Interest Interest { get; set; }
    }
}

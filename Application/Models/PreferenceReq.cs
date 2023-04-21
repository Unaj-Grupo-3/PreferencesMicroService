using Domain.Entities;

namespace Application.Models
{
    public class PreferenceReq
    {
        public int UserId { get; set; } //FK
        public int InterestId { get; set; } //FK
        public bool OwnInterest { get; set; }
        public bool Like { get; set; }
    }
}

using Domain.Entities;

namespace Application.Models
{
    public class PreferenceResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public InterestResponse Interest { get; set; }
        public bool OwnInterest { get; set; }
        public bool Like { get; set; }
    }
}

using Domain.Entities;

namespace Application.Models
{
    public class PreferenceResponse
    {
        public Guid UserId { get; set; }
        public InterestResponse Interest { get; set; }
        public bool OwnInterest { get; set; }
        public bool Like { get; set; }
    }
}

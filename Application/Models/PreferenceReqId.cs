using Domain.Entities;

namespace Application.Models
{
    public class PreferenceReqId
    {
        public Guid UserId { get; set; }
        public int InterestId { get; set; }
    }
}

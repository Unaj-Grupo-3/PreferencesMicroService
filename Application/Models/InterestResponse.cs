using Domain.Entities;

namespace Application.Models
{
    public class InterestResponse
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public InterestCategoryResponse InterestCategory { get; set; }
    }
}

namespace Domain.Entities
{
    public class InterestCategory
    {
        public int InterestCategoryId { get; set; }
        public string Description { get; set; }
        public IList<Interest>? Interests { get; set; }
    }
}

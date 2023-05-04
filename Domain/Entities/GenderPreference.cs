namespace Domain.Entities
{
    public class GenderPreference
    {
        public Guid UserId { get; set; } //FK
        public int GenderId { get; set; } //FK
    }
}

namespace Domain.Entities
{
    public class GenderPreference
    {
        public int UserId { get; set; } //FK
        public int GenderId { get; set; } //FK
    }
}

namespace Application.Models
{
    public class OverallPreferenceResponse
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int SinceAge { get; set; }
        public int UntilAge { get; set; }
        public int Distance { get; set; }
    }
}

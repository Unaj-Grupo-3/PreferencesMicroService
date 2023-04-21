namespace Application.Models
{
    public class OverallPreferenceResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SinceAge { get; set; }
        public int UntilAge { get; set; }
        public int Distance { get; set; }
    }
}

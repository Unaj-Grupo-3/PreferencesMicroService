namespace Application.Models
{
    public class OverallPreferenceReq
    {
        public Guid UserId { get; set; }
        public int SinceAge { get; set; }
        public int UntilAge { get; set; }
        public int Distance { get; set; }
    }
}

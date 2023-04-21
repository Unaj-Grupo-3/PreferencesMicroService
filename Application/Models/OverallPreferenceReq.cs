namespace Application.Models
{
    public class OverallPreferenceReq
    {
        public int UserId { get; set; }
        public int SinceAge { get; set; }
        public int UntilAge { get; set; }
        public int Distance { get; set; }
    }
}

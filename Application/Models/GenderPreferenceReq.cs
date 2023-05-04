namespace Application.Models
{
    public class GenderPreferenceReq
    {
        public Guid UserId { get; set; }
        public int GenderId { get; set; }
    }
}

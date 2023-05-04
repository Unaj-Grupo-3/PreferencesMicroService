namespace Application.Models
{
    public class GenderPreferenceResponse
    {
        public Guid UserId { get; set; }
        public int GenderId { get; set; }
        public string GenderName { get; set;}
    }
}

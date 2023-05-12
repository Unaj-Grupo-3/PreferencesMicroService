
namespace Application.Models
{
    public class UserPreferencesResponseFull
    {
        public int UserId { get; set; }
        public int SinceAge { get; set; } //OVERALL
        public int UntilAge { get; set; } //OVERALL
        public int Distance { get; set; } //OVERALL
        public IEnumerable<GenderPreferenceResponse>? GendersPreferencesId { get; set; } //GENDERS
        public IEnumerable<PreferenceResponseFull>? InterestPreferencesId { get; set; } //PREFERENCES -> Where Like=1
    }
}

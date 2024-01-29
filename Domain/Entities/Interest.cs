namespace Domain.Entities
{
    public class Interest
    {
        public int InterestId { get; set; }
        public int InterestCategoryId { get; set; } //FK 
        public string Description { get; set; } 

        //RELACION
        public virtual InterestCategory InterestCategory { get; set; }
        public IList<Preference> Preferences { get; set; }
    }
}

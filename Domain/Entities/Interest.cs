namespace Domain.Entities
{
    public class Interest
    {
        public int InterestId { get; set; }
        public int InterestCategoryId { get; set; } //FK varchar(50)
        public string Description { get; set; } //varchar(50)
        
        //RELACION
        public virtual InterestCategory InterestCategory { get; set; }
        public IList<Preference> Preferences { get; set; }
    }
}

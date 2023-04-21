using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class InterestCategory
    {
        [Key]
        public int InterestCategoryId { get; set; } 
        public string Description { get; set; } //varchar(50)
        
        //RELACION
        public Interest Interest { get; set; }
    }
}

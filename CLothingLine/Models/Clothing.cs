using System.ComponentModel.DataAnnotations;

namespace CLothingLine.Models
{
    public class Clothing
    {

        public int Id { get; set; }
        [Required]
        public string ?Name { get; set; }
       
        public string ?Color { get; set; }
        [Required]
        public string? Type { get; set; }
        [Required]
        public string ?CustomerServiceReport { get; set; }   

       
        
    }
}

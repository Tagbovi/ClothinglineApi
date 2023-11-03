using System.ComponentModel.DataAnnotations;

namespace CLothingLine.Dtos
{
    public class ReadClothsDto
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string ?Color { get; set; }
        [Required]
        public string ?Type { get; set; }

    }
}

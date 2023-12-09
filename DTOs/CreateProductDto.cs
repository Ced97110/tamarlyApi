using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CreateProductDto
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(100, Double.PositiveInfinity)]
        public long Price { get; set; }

        [Required]
        public IFormFile File { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        public int PageCount { get; set; }

        [Required]
        public int FileSize { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string FileFormat { get; set; }


    }
}
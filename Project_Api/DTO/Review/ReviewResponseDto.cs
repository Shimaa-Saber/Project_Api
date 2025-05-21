using System.ComponentModel.DataAnnotations;

namespace Project_Api.DTO.Review
{
    public class ReviewResponseDto
    {
        [Required]
        [StringLength(500)]
        public string Response { get; set; }
    }
}

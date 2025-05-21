using System.ComponentModel.DataAnnotations;

namespace Project_Api.DTO.Review
{
    public class CreateReviewDto
    {
        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(500)]
        public string Comment { get; set; }

        [Required]
        public string TherapistId { get; set; }
        [Required]
        public int SessionId { get; set; }
    }
}

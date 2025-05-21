using System.ComponentModel.DataAnnotations;

namespace Project_Api.DTO.Messageing
{
    public class SendMessageDto
    {
        [Required]
        [StringLength(2000)]
        public string Content { get; set; }
    }
}

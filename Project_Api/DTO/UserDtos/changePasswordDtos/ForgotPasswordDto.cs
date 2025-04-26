using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Project_Api.DTO.UserDtos.changePasswordDtos
{
    public class ForgotPasswordDto
    {
       
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}

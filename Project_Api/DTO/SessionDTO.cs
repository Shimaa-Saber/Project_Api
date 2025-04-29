using ProjectApi.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Api.DTO
{
    public class SessionDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        //public DateTime CreatedAt { get; set; }
    }
}

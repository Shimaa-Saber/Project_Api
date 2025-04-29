namespace Project_Api.DTO.Notifications
{
    public class MarkReadRequest
    {
        public List<int> NotificationIds { get; set; }  
        public bool MarkAll { get; set; } = false;
    }
}

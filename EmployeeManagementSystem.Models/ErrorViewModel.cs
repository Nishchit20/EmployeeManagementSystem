namespace EmployeeManagementSystem.Models
{
    /// <summary>
    /// This is Entity model is used to for the Error handling
    /// </summary>
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}

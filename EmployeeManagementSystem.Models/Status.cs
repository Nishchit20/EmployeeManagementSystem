namespace EmployeeManagementSystem.Models
{
    /// <summary>
    /// This Entity table will define in which state the process is in and will return us with the state message
    /// </summary>
    public class Status
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}

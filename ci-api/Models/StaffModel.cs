namespace CIManagement.Models
{
    public class StaffLoginModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public class StaffInformationModel
    {
        public int? Id { get; set; }
        public string? StaffName { get; set; }
        public string? Department { get; set; }
        public bool? CreateAccess { get; set; }
        public bool? EditAccess { get; set; }
        public bool? ReadAccess { get; set; }
        public bool? DeleteAccess { get; set; }
        public string? StatusMessage { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace CIManagement.Entities
{
    public partial class TblStaff
    {
        public int Id { get; set; }
        public string? StaffName { get; set; }
        public string? Department { get; set; }
        public bool? CreateAccess { get; set; }
        public bool? EditAccess { get; set; }
        public bool? ReadAccess { get; set; }
        public bool? DeleteAccess { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}

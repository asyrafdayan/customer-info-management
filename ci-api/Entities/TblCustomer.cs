using System;
using System.Collections.Generic;

namespace CIManagement.Entities
{
    public partial class TblCustomer
    {
        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerStreet { get; set; }
        public string? CustomerPosCode { get; set; }
        public string? CustomerCity { get; set; }
        public string? CustomerState { get; set; }
        public string? CustomerCountry { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}

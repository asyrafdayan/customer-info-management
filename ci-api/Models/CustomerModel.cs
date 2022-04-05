namespace CIManagement.Models
{

    public class CustomersListModel
    {
        public int TotalResults { get; set; } = 0;
        public string? StatusMessage { get; set; }
        public List<CustomerModel> Customers { get; set; } = new List<CustomerModel>();
    }

    public class CustomerModel
    {
        public int? Id { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerStreet { get; set; }
        public string? CustomerPosCode { get; set; }
        public string? CustomerCity { get; set; }
        public string? CustomerState { get; set; }
        public string? CustomerCountry { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}

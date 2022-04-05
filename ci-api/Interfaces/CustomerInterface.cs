using CIManagement.Models;

namespace CIManagement.Interfaces
{
    public interface ICustomerInterface
    {

        /// <summary>
        /// Inserts a new Customer to tblCustomer
        /// Limit insertion to 10 customers only
        /// Limitations of a free database
        /// </summary>
        void RegisterNewCustomer(CustomerModel customer);

        /// <summary>
        /// Returns a list of customers
        /// </summary>
        CustomersListModel FetchCustomersList();

        /// <summary>
        /// Fetches Customer information from their Id
        /// </summary>
        /// <param name="Id"></param>
        CustomerModel FetchCustomerInformation(string Id);

        /// <summary>
        /// Deletes Customer from Database based on supplied Id
        /// </summary>
        /// <param name="Id"></param>
        void DeleteCustomer(string Id);
    }
}

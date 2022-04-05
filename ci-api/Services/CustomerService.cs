using CIManagement.Entities;
using CIManagement.Interfaces;
using CIManagement.Models;

namespace CIManagement.Services
{
    public class CustomerService : ICustomerInterface
    {
        private readonly CI_Context _ciContext;
        public CustomerService(CI_Context ciContext)
        {
            _ciContext = ciContext;
        }

        public void DeleteCustomer(string Id)
        {
            try
            {
                var c = _ciContext.TblCustomers.Where(c => c.Id == int.Parse(Id.Trim())).FirstOrDefault();

                if (c != null)
                {
                    _ciContext.TblCustomers.Remove(c);
                    _ciContext.SaveChanges();
                }
                else
                    throw new Exception("404");
            }
            catch (Exception ex)
            {
                if (ex.Message != "404")
                {
                    throw new Exception("500");
                }
            }
        }

        public CustomerModel FetchCustomerInformation(string Id)
        {
            try
            {
                var customer = _ciContext.TblCustomers.Where(c => c.Id == int.Parse(Id.Trim())).FirstOrDefault();

                if (customer == null)
                {
                    throw new Exception("404");
                }

                CustomerModel cust = new()
                {
                    Id = customer.Id,
                    CustomerName = customer.CustomerName,
                    CustomerCity = customer.CustomerCity,
                    CustomerCountry = customer.CustomerCountry,
                    CustomerPosCode = customer.CustomerPosCode,
                    CustomerState = customer.CustomerState,
                    CustomerStreet = customer.CustomerStreet,
                    DateOfBirth = customer.DateOfBirth
                };

                return cust;
            }
            catch (Exception ex)
            {
                if (ex.Message == "404")
                {
                    throw new Exception(ex.Message);
                }
                throw new Exception("500");
            }
        }

        public CustomersListModel FetchCustomersList()
        {
            try
            {
                CustomersListModel customersListModel = new();
                var customers = _ciContext.TblCustomers.ToList();

                // Return an empty list
                if (customers.Count == 0)
                {
                    return customersListModel;
                }

                customersListModel.StatusMessage = "Success";
                customersListModel.TotalResults = customers.Count;
                foreach (var c in customers)
                {
                    CustomerModel customer = new()
                    {
                        Id = c.Id,
                        CustomerName = c.CustomerName,
                        CustomerCity = c.CustomerCity,
                        CustomerCountry = c.CustomerCountry,
                        CustomerPosCode = c.CustomerPosCode,
                        CustomerStreet = c.CustomerStreet,
                        CustomerState = c.CustomerState,
                        DateOfBirth = c.DateOfBirth
                    };
                    customersListModel.Customers.Add(customer);
                }
                return customersListModel;
            }
            catch (Exception ex)
            {
                if (ex is TimeoutException)
                {
                    throw new Exception("408", new Exception("Timeout Occurred"));
                }
                else
                {
                    throw new Exception("500", new Exception("Server Exception"));
                }
            }
        }

        public void RegisterNewCustomer(CustomerModel customer)
        {
            try
            {
                CustomersListModel customers = FetchCustomersList();

                if (customers.TotalResults == 10)
                {
                    throw new Exception("507");
                }

                TblCustomer tc = new()
                {
                    CustomerName = customer.CustomerName,
                    CustomerCity = customer.CustomerCity,
                    CustomerCountry = customer.CustomerCountry,
                    CustomerPosCode = customer.CustomerPosCode,
                    CustomerState = customer.CustomerState,
                    CustomerStreet = customer.CustomerStreet,
                    DateOfBirth = customer.DateOfBirth
                };

                _ciContext.TblCustomers.Add(tc);
                _ciContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Limit has exceeded
                if (ex.Message == "507")
                {
                    throw new Exception(ex.Message);
                }
                throw new Exception("500");
            }
        }
    }
}

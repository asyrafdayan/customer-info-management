using CIManagement.Interfaces;
using CIManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CIManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerInformationController : Controller
    {
        private readonly IStaffInterface _staffService;
        private readonly ICustomerInterface _customerService;

        public CustomerInformationController(IStaffInterface staffService, ICustomerInterface customerService)
        {
            _staffService = staffService;
            _customerService = customerService;
        }

        /// <summary>
        /// Login User
        /// </summary>
        /// <returns></returns>
        [HttpPost("Login")]
        public IActionResult Login(StaffLoginModel login)
        {
            StaffInformationModel staff = new();
            try
            {
                staff = _staffService.Login(login);
                return Ok(staff);
            }
            catch (Exception ex)
            {
                staff.StatusMessage = ex.InnerException?.Message;
                return StatusCode(int.Parse(ex.Message), staff);
            }
        }

        /// <summary>
        /// Returns the Logged in User information
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetStaffInfo")]
        public IActionResult GetStaffInfo(string id)
        {
            StaffInformationModel staff = new();
            try
            {
                staff = _staffService.GetStaffInformation(id);
                return Ok(staff);
            }
            catch (Exception ex)
            {
                staff.StatusMessage = ex.InnerException?.Message;
                return StatusCode(int.Parse(ex.Message), staff);
            }
        }

        /// <summary>
        /// Registers a new customer to the system
        /// </summary>
        /// <returns></returns>
        [HttpPost("RegisterCustomer")]
        public IActionResult RegisterNewCustomer(CustomerModel newCustomer)
        {
            try
            {
                _customerService.RegisterNewCustomer(newCustomer);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(int.Parse(ex.Message));
            }
        }

        /// <summary>
        /// Returns a list of customers
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCustomersList")]
        public IActionResult GetCustomerListing()
        {
            CustomersListModel customersListModel = new();

            try
            {
                customersListModel = _customerService.FetchCustomersList();

                return Ok(customersListModel);
            }
            catch (Exception ex)
            {
                customersListModel.StatusMessage = ex.InnerException?.Message;
                return StatusCode(int.Parse(ex.Message), customersListModel);
            }
        }

        /// <summary>
        /// Fetches customer information using customer id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetCustomerInformation")]
        public IActionResult GetCustomerInformation(string id)
        {
            CustomerModel customer = new();
            try
            {
                customer = _customerService.FetchCustomerInformation(id);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(int.Parse(ex.Message), customer);
            }
        }

        /// <summary>
        /// Deletes customer based on given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteCustomer")]
        public IActionResult DeleteCustomer(string id)
        {
            try
            {
                _customerService.DeleteCustomer(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(int.Parse(ex.Message));
            }
        }
    }
}

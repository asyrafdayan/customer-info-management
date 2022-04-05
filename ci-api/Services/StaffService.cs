using CIManagement.Entities;
using CIManagement.Interfaces;
using CIManagement.Models;

namespace CIManagement.Services
{
    public class StaffService : IStaffInterface
    {
        private readonly CI_Context _cicontext;

        public StaffService(CI_Context cicontext)
        {
            _cicontext = cicontext;
        }

        public StaffInformationModel GetStaffInformation(string Id)
        {
            try
            {
                var s = _cicontext.TblStaffs.Where(s => s.Id == int.Parse(Id.Trim())).FirstOrDefault();

                // No user with the supplied ID exists
                if (s == null)
                {
                    throw new Exception("404");
                }

                StaffInformationModel staff = new()
                {
                    Id = s.Id,
                    StaffName = s.StaffName,
                    Department = s.Department,
                    CreateAccess = s.CreateAccess,
                    EditAccess = s.EditAccess,
                    ReadAccess = s.ReadAccess,
                    DeleteAccess = s.DeleteAccess,
                    StatusMessage = "Success"
                };

                return staff;
            }
            catch (Exception ex)
            {
                if (ex.Message == "404")
                {
                    throw new Exception("404", new Exception("User not found"));
                }
                if (ex is TimeoutException)
                {
                    throw new Exception("408", new Exception("Request timeout"));
                }
                else
                {
                    throw new Exception("500", new Exception("Server error"));
                }
            }
        }

        public StaffInformationModel Login(StaffLoginModel staff)
        {
            var s = _cicontext.TblStaffs.Where(s => s.Username == staff.Username).FirstOrDefault();

            // Username does not exist
            if (s == null)
            {
                throw new Exception("404", new Exception("User not found"));
            }

            // Wrong password
            if (s.Password != staff.Password)
            {
                throw new Exception("403", new Exception("Invalid Credentials"));
            }

            // Return staff information on successful login
            StaffInformationModel st = new()
            {
                Id = s.Id,
                StaffName = s.StaffName,
                StatusMessage = "Successful Login"
            };

            return st;
        }
    }
}

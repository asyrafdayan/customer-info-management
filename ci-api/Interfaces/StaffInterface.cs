using CIManagement.Models;

namespace CIManagement.Interfaces
{
    public interface IStaffInterface
    {
        /// <summary>
        /// Interface for staff login
        /// </summary>
        /// <param name="staff"></param>
        /// <returns>
        /// StaffInformationModel
        /// </returns>
        StaffInformationModel Login(StaffLoginModel staff);
        
        /// <summary>
        /// Interface to fetch Staff Information by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>
        /// StaffInformationModel
        /// </returns>
        StaffInformationModel GetStaffInformation(string Id);
    }
}

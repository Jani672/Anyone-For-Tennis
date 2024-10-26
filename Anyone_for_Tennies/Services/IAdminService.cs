using Anyone_for_Tennies.Models;
using System.Threading.Tasks;

namespace Anyone_for_Tennies.Services
{
    public interface IAdminService
    {
        // Method to register a new admin
        Task<Admin> RegisterAsync(AdminRegisterViewModel model);

        // Method to log in an admin
        Task<Admin> LoginAsync(AdminLoginViewModel model);

        // Method to edit an admin's information
        Task<Admin> EditAsync(int id, AdminEditViewModel model);

        // Method to delete an admin by id
        Task<bool> DeleteAsync(int id);
    }
}

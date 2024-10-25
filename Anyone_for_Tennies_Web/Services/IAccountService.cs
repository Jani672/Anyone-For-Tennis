using Anyone_for_Tennies.Models;

namespace Anyone_for_Tennies.Services
{
    public interface IAccountService
    {
        Task<Member> RegisterAsync(RegisterViewModel model);
        Task<Member> LoginAsync(LoginViewModel model);
        Task<Member> EditAsync(int id, EditViewModel model);
        Task<bool> DeleteAsync(int id);
    }

}

using Anyone_for_Tennies.Models;
using System.Threading.Tasks;

namespace Anyone_for_Tennies.Services
{
    public interface ICoachService
    {
        Task<Coach> RegisterAsync(CoachRegisterViewModel model);
        Task<Coach> LoginAsync(CoachLoginViewModel model);
        Task<Coach> EditAsync(int id, CoachEditViewModel model);
        Task<bool> DeleteAsync(int id);
    }
}

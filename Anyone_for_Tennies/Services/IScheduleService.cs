using Anyone_for_Tennies.Models;
using System.Threading.Tasks;

namespace Anyone_for_Tennies.Services
{
    public interface IScheduleService
    {
        Task<Schedule> CreateAsync(ScheduleCreateViewModel model);
        Task<Schedule> GetByIdAsync(int id);
        Task<Schedule> EditAsync(int id, ScheduleEditViewModel model);
        Task<bool> DeleteAsync(int id);
    }
}

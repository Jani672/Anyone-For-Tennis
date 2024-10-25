// Services/IEnrollmentService.cs
using Anyone_for_Tennies.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Anyone_for_Tennies.Services
{
    public interface IEnrollmentService
    {
        Task<Enrollment> CreateAsync(EnrollmentCreateViewModel model);
        Task<IEnumerable<Enrollment>> GetByMemberIdAsync(int memberId);
        Task<bool> DeleteAsync(int id);
    }
}

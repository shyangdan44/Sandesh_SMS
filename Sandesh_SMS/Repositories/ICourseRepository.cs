using Sandesh_SMS.ViewModels;
using System.Linq.Expressions;

namespace Sandesh_SMS.Repositories
{
    public interface ICourseRepository
    {
        Task<CourseViewModel> GetByIdAsync(int id);
        Task<List<CourseViewModel>> GetAllAsync();
        Task AddAsync(CourseViewModel course);
        Task UpdateAsync(CourseViewModel course);
        Task DeleteAsync(int Id);
    }
}

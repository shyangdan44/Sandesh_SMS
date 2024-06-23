using Sandesh_SMS.Models;
using Sandesh_SMS.ViewModels;
using System.Linq.Expressions;

namespace Sandesh_SMS.Repositories
{
    public interface IStudentRepository
    {
        Task<StudentViewModel> GetByIdAsync(int id);
        IQueryable<StudentViewModel> GetAllAsync();
        Task AddAsync(StudentViewModel student);
        Task UpdateAsync(StudentViewModel student);
        Task<StudentViewModel> DetailsByIdAsync(int id);
        Task DeleteAsync(int Id);
        Task<List<Class>> GetAllClasses();
        Task<List<Course>> GetAllCourses();
    }
}

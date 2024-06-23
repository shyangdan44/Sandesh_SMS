using Sandesh_SMS.ViewModels;
using System.Linq.Expressions;

namespace Sandesh_SMS.Repositories
{
    public interface IClassRepository
    {
        Task<ClassViewModel> GetByIdAsync(int id);
        Task<List<ClassViewModel>> GetAllAsync();
        Task AddAsync(ClassViewModel clas);
        Task UpdateAsync(ClassViewModel clas);
        Task DeleteAsync(int Id);
    }
}

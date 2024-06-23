using Microsoft.EntityFrameworkCore;
using Sandesh_SMS.Data;
using Sandesh_SMS.Models;
using Sandesh_SMS.ViewModels;
using System.Linq.Expressions;

namespace Sandesh_SMS.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly AppDbContext _dbContext;

        //Dependency Injection
        public ClassRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(ClassViewModel clas)
        {
            var newClass = new Class()
            {
                ClassName = clas.ClassName
            };
            await _dbContext.Classes.AddAsync(newClass);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            Class clas = await _dbContext.Classes.FindAsync(Id);
            _dbContext.Classes.Remove(clas);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ClassViewModel>> GetAllAsync()
        {
            var clas = await _dbContext.Classes.ToListAsync();
            List<ClassViewModel> classViewModels = new List<ClassViewModel>();
            foreach (var Clas in clas)
            {
                var classViewModel = new ClassViewModel
                {
                    ClassId = Clas.ClassId,
                    ClassName = Clas.ClassName
                };

                classViewModels.Add(classViewModel);
            }

            return classViewModels;
        }

        public async Task<ClassViewModel> GetByIdAsync(int id)
        {
            var Clas = await _dbContext.Classes.FindAsync(id);
            var classViewModel = new ClassViewModel
            {
                ClassId = Clas.ClassId,
                ClassName = Clas.ClassName
            };
            return classViewModel;
        }

        public async Task UpdateAsync(ClassViewModel clasUpdated)
        {
            var Clas = await _dbContext.Classes.FindAsync(clasUpdated.ClassId);
            Clas.ClassName = clasUpdated.ClassName;

            _dbContext.Classes.Update(Clas);
            await _dbContext.SaveChangesAsync();
        }
    }
}

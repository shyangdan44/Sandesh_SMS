using Microsoft.EntityFrameworkCore;
using Sandesh_SMS.Data;
using Sandesh_SMS.Models;
using Sandesh_SMS.ViewModels;
using System.Linq.Expressions;


namespace Sandesh_SMS.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _dbContext;

        //Dependency Injection
        public CourseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(CourseViewModel course)
        {
            var newCourse = new Course()
            {
                CourseName = course.CourseName
            };
            await _dbContext.Courses.AddAsync(newCourse);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            Course course = await _dbContext.Courses.FindAsync(Id);
            _dbContext.Courses.Remove(course);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<CourseViewModel>> GetAllAsync()
        {
            var cources = await _dbContext.Courses.ToListAsync();
            List<CourseViewModel> courseViewModels = new List<CourseViewModel>();
            foreach (var department in cources)
            {
                var courseViewModel = new CourseViewModel
                {
                    CourseId = department.CourseId,
                    CourseName = department.CourseName
                };

                courseViewModels.Add(courseViewModel);
            }

            return courseViewModels;
        }

        public async Task<CourseViewModel> GetByIdAsync(int id)
        {
            var course = await _dbContext.Courses.FindAsync(id);
            var courseViewModel = new CourseViewModel
            {
                CourseId = course.CourseId,
                CourseName = course.CourseName
            };
            return courseViewModel;
        }

        public async Task UpdateAsync(CourseViewModel courseUpdated)
        {
            var course = await _dbContext.Courses.FindAsync(courseUpdated.CourseId);
            course.CourseName = courseUpdated.CourseName;

            _dbContext.Courses.Update(course);
            await _dbContext.SaveChangesAsync();
        }
    }
}

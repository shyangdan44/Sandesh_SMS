using Microsoft.EntityFrameworkCore;
using Sandesh_SMS.Data;
using Sandesh_SMS.Models;
using Sandesh_SMS.ViewModels;
using System.Linq.Expressions;

namespace Sandesh_SMS.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _dbContext;
        public StudentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(StudentViewModel student)
        {
            var newStudent = new Student()
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                DateOfBirth = student.DateOfBirth,
                PhoneNumber = student.PhoneNumber,
                Gender = student.Gender,
                Email = student.Email,
                Address = student.Address,
                IsActive = student.IsActive,
                
                ClassId = student.ClassId,
                CourseId = student.CourseId,
            };
            await _dbContext.Students.AddAsync(newStudent);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            Student student = await _dbContext.Students.FindAsync(Id);
            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<StudentViewModel> DetailsByIdAsync(int id)
        {
            var student = await _dbContext.Students
                .Include(s => s.Class)
                .Include(s => s.Course)
                .FirstOrDefaultAsync(s => s.StudentId == id);

            if (student == null)
            {
                return null;
            }

            return new StudentViewModel
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                DateOfBirth = student.DateOfBirth,
                Gender = student.Gender,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                Address = student.Address,
                IsActive = student.IsActive,
                ClassId = student.ClassId,
                CourseId = student.CourseId,

            };
        }

        public IQueryable<StudentViewModel> GetAllAsync()
        {
            var students = _dbContext.Students
            .Select(s => new StudentViewModel
            {
                StudentId = s.StudentId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                DateOfBirth = s.DateOfBirth,
                Gender = s.Gender,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                Address = s.Address,
                IsActive = s.IsActive,
                
                ClassId = s.ClassId,
                CourseId = s.CourseId
            });

            return students;
        }

        public async Task<List<Class>> GetAllClasses()
        {
            return await _dbContext.Classes.ToListAsync();
        }

        public async Task<List<Course>> GetAllCourses()
        {
            return await _dbContext.Courses.ToListAsync();
        }

        public async Task<StudentViewModel> GetByIdAsync(int id)
        {
            var student = await _dbContext.Students.FindAsync(id);
            var studentViewModel = new StudentViewModel
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                DateOfBirth = student.DateOfBirth,
                Gender = student.Gender,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                Address = student.Address,
                IsActive = student.IsActive,
                
                ClassId = student.ClassId,
                CourseId = student.CourseId
            };
            return studentViewModel;
        }

        public async Task UpdateAsync(StudentViewModel studentUpdated)
        {
            var student = await _dbContext.Students.FindAsync(studentUpdated.StudentId);
            student.FirstName = studentUpdated.FirstName;
            student.LastName = studentUpdated.LastName;
            student.Email = studentUpdated.Email;
            student.DateOfBirth = studentUpdated.DateOfBirth;
            student.PhoneNumber = studentUpdated.PhoneNumber;
            student.Address = studentUpdated.Address;
            
            student.ClassId = studentUpdated.ClassId;
            student.CourseId = studentUpdated.CourseId;
            student.IsActive = studentUpdated.IsActive;
            _dbContext.Students.Update(student);
            await _dbContext.SaveChangesAsync();
        }
    }
}

namespace Sandesh_SMS.ViewModels
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

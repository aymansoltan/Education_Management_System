using FinalProjectApi.Models;

using Microsoft.EntityFrameworkCore;

namespace FinalProjectMVC.Repository
{
    public class CourseRepository :GenericRepository<Course> , ICourseRepository
    {
        private readonly ITIContext Context;
        public CourseRepository(ITIContext context) : base(context)
        {
            Context = context;
        }

        public List<Course> getCourseWithDept()
        {
            return Context.Courses.Include(d=>d.Department).ToList();
        }
        public Course? CourseWithDeptTraineeInstractor(int id)
        {
            return Context.Courses.Include(d => d.Department)
                .Include(d => d.Instructors)
                .Include(d => d.CourseResults)
                .ThenInclude(cr => cr.Trainee)
                .AsNoTracking()
                .FirstOrDefault(c => c.Id==id);
        }

        public List<Course> GetAllll()
        {
            return Context.Courses
                .Select(c => new Course { Id = c.Id, Name = c.Name }) // Ã·» «·‹ Id Ê«·‹ Name ›ﬁÿ
                .ToList();
        }

     
    }
}

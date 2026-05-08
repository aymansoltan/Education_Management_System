using FinalProjectApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMVC.Repository
{
    public class InstractorRepository :GenericRepository<Instructor> , IInstractorRepository
    {
        private readonly ITIContext Context;
        public InstractorRepository(ITIContext context) : base(context)
        {
            Context = context;
        }


        public Instructor? GetInstructorWithCourseDeptTrainee(int id)
        {
            return Context.Instructors
                .Include(d => d.Department)
                .Include(c => c.Course)
                .ThenInclude(cr=>cr.CourseResults)
                .ThenInclude(c=>c.Trainee)
                .FirstOrDefault(i => i.Id==id);
        }  
        public Instructor? GetInstructorWithCourseDept(int id)
        {
            return Context.Instructors
                .Include(d => d.Department)
                .Include(c => c.Course)
                .FirstOrDefault(i => i.Id==id);
        }  
    }
}

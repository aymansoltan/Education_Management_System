using FinalProjectApi.Models;

using Microsoft.EntityFrameworkCore;

namespace FinalProjectMVC.Repository
{
    public class TraineeRepository :GenericRepository<Trainee> , ITraineeRepository
    {
        private readonly ITIContext Context;
        public TraineeRepository(ITIContext context) : base(context)
        {
            Context = context;
        }

  
        public List<Trainee> AlltarinWithDept()
        {
            return Context.Trainees
                .Include(t => t.Department)
                .ToList();
        }

        public Trainee? GetTraineeWithDeptCourseInstructors(int id)
        {
            return Context.Trainees
                .AsNoTracking()
                .Include(t => t.Department)
                .Include(t => t.CourseResults)
                    .ThenInclude(cr => cr.Course)
                        .ThenInclude(c => c.Instructors)
                .FirstOrDefault(t => t.Id == id);
        }


        public List<Trainee> GetAllll()
        {
            return Context.Trainees
                .Select(t => new Trainee { Id = t.Id, Name = t.Name }) // Ã·» «·‹ Id Ê«·‹ Name ›ﬁÿ
                .ToList();
        }

    }
}

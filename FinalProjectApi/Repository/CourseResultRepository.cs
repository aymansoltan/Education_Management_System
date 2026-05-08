using FinalProjectApi.Models;

using Microsoft.EntityFrameworkCore;

namespace FinalProjectMVC.Repository
{
    public class CourseResultRepository :GenericRepository<CourseResult> , ICourseResultRepository
    {
        private readonly ITIContext _context;
        public CourseResultRepository(ITIContext context) : base(context)
        {
            _context = context;
        }
        public List<CourseResult> GetAllWithTrainingAndCourse()
        {
            return _context.CourseResults
                .Include(c => c.Course)
                .Include(c => c.Trainee)
                .ToList();

        }


        public CourseResult? GetByIDWithTrainingAndCourse(int trainingId, int courseId)
        {
            return _context.CourseResults
                .Include(c => c.Course)
                .Include(c => c.Trainee)
                .FirstOrDefault(c => c.TraineeId==trainingId && c.CourseId==courseId);
         
        }
    }
}

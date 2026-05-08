using FinalProjectApi.Models;

namespace FinalProjectMVC.Repository
{
    public interface ICourseResultRepository :IGenericRepository<CourseResult>
    {
        List<CourseResult> GetAllWithTrainingAndCourse();
        CourseResult? GetByIDWithTrainingAndCourse(int trainingId, int courseId);
    }
}

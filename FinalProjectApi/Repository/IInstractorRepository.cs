

using FinalProjectApi.Models;

namespace FinalProjectMVC.Repository
{
    public interface IInstractorRepository : IGenericRepository<Instructor>
    {
        Instructor? GetInstructorWithCourseDeptTrainee(int id);
        Instructor? GetInstructorWithCourseDept(int id);
    }
}

using FinalProjectApi.Models;


namespace FinalProjectMVC.Repository
{
    public interface ICourseRepository :IGenericRepository<Course>
    {
        List<Course> getCourseWithDept();
        Course? CourseWithDeptTraineeInstractor(int id);
        List<Course> GetAllll();


    }
}

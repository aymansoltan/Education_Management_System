using FinalProjectApi.Models;


namespace FinalProjectMVC.Repository
{
    public interface ITraineeRepository : IGenericRepository<Trainee>
    {

        List<Trainee> AlltarinWithDept();
        Trainee? GetTraineeWithDeptCourseInstructors(int id);
      
        List<Trainee> GetAllll();

    }
}

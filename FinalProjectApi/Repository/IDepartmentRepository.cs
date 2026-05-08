using FinalProjectApi.Dto.DepartmentDto;
using FinalProjectApi.Models;

namespace FinalProjectMVC.Repository
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        DepartmentWithTraineesDto? DepartmentWithTrainee(int id );
        DepartmentWithCourseDto? GetDepartmentWithCourse(int id);
        DepartmentWithInstructorDto? GetDepartmentWithInstructor(int id);
        GetByIdWithTraineeCourseInstructorDto? GetByIdWithTraineeCourseInstructor(int id);
    }
}

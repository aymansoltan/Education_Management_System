using FinalProjectApi.Models;

namespace FinalProjectApi.Dto.DepartmentDto
{
    public class GetByIdWithTraineeCourseInstructorDto
    {
        public  int  Id { get; set; }
        public string Name { get; set; }
        public string Manager { get; set; }
        public List<string> courses { get; set; } = new List<string>();
        public List<string> instructors { get; set; } = new List<string>();
        public List<string> trainees { get; set; } = new List<string>();
    }
}

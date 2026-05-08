using FinalProjectApi.Models;

namespace FinalProjectApi.Dto.DepartmentDto
{
    public class DepartmentWithInstructorDto
    {
        public  int  Id { get; set; }
        public string Name { get; set; }
        public string Manager { get; set; }
        public List<string> Instructors { get; set; } = new List<string>();
    }
}

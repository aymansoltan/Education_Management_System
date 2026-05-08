namespace FinalProjectApi.Dto.TraineeDto
{
    public class TraineeWithDeptCourseInstructorsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Grade { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }

      
        public List<CourseWithInstructorsDto> Courses { get; set; } = new List<CourseWithInstructorsDto>();
    }

    public class CourseWithInstructorsDto
    {
        public string CourseName { get; set; }
        public List<string> Instructors { get; set; } = new List<string>();
    }
}

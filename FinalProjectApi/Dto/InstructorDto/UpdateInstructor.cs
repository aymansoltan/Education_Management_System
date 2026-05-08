namespace FinalProjectApi.Dto.InstructorDto
{
    public class UpdateInstructor
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
        public int CourseId { get; set; }
    }
}

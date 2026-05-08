namespace FinalProjectApi.Dto.courseDto
{
    public class createCourseDto
    {
        public string Name { get; set; }
        public int Hours { get; set; }
        public int Degree { get; set; }
        public int MinDegree { get; set; }
        public int DepartmentId { get; set; }
    }
}

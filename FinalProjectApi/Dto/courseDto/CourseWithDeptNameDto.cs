namespace FinalProjectApi.Dto.courseDto
{
    public class CourseWithDeptNameDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int Hours { get; set; }
        public int Degree { get; set; }
        public int MinDegree { get; set; }
        public string Department { get; set; }

    }
}

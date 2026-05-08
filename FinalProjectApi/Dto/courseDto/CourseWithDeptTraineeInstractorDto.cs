namespace FinalProjectApi.Dto.courseDto
{
    public class CourseWithDeptTraineeInstractorDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int Hours { get; set; }
        public int Degree { get; set; }
        public int MinDegree { get; set; }
        public string Department { get; set; }
        public List<string> Instructors { get; set; } = new List<string>();
        public List<string> Trainees { get; set; } = new List<string>();

    }
}

namespace FinalProjectApi.Dto.DepartmentDto
{
    public class DepartmentWithTraineesDto
    {
        public  int  Id { get; set; }
        public string Name { get; set; }
        public string Manager { get; set; }
        public List<string> Trainees { get; set; } = new List<string>();
    }
}

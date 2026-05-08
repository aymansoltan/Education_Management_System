using FinalProjectApi.Dto.courseDto;
using FinalProjectApi.Models;
using FinalProjectMVC.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {

        private readonly ICourseRepository courseRepo;
        private readonly IDepartmentRepository departmentRepo;

        public CourseController(ICourseRepository courseRepository, IDepartmentRepository departmentRepository)
        {
            this.courseRepo=courseRepository;
            this.departmentRepo=departmentRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            var course = courseRepo.getCourseWithDept().Select(c => new CourseWithDeptNameDto
            {
                Id=c.Id,
                Name=c.Name,
                Hours=c.Hours,
                Degree=c.Degree,
                MinDegree=c.MinDegree,
                Department=c.Department?.Name ?? "N/A"
            });
            return Ok(course);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetByID(int id )
        {

            Course course = courseRepo.getCourseWithDept().FirstOrDefault(c=>c.Id== id);
            if (course == null)
                return NotFound(new { message = "this course is not found" });

            CourseWithDeptNameDto model = new CourseWithDeptNameDto
            {
                Id=course.Id,
                Name=course.Name,
                Hours=course.Hours,
                Degree=course.Degree,
                MinDegree=course.MinDegree,
                Department=course.Department?.Name ?? "N/A"
            };
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var course = courseRepo.GetById(id);
            if (course == null)
                return NotFound(new { message = "this course is not found" });

            courseRepo.Delete(id);
            courseRepo.Save();
            return NoContent();

        }

        [HttpGet("departments")]
        public IActionResult GetDepartments()
        {
            var departments = departmentRepo.GetAll()
                .Select(d => new { d.Id, d.Name })
                .ToList();

            return Ok(departments);
        }

        [HttpPost]
        public IActionResult Add([FromBody]createCourseDto courseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var course = new Course
            {
                Name=courseDto.Name,
                Hours=courseDto.Hours,
                Degree=courseDto.Degree,
                MinDegree=courseDto.MinDegree,
                DepartmentId=courseDto.DepartmentId

            };
            courseRepo.Add(course);
            courseRepo.Save();
            return CreatedAtAction(nameof(GetByID), new { id = course.Id }, course);

        }

        [HttpPut("{id:int}")]
        public IActionResult Edit( int id , [FromBody] UpdateCourseDto courseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dept = departmentRepo.GetById(courseDto.DepartmentId);
            if (dept == null)
                return BadRequest(new { message = "Invalid department ID" });


            var OldCourse = courseRepo.GetById(id);

            if (OldCourse == null)
                return NotFound(new { message = "course not found" });

            OldCourse.Name=courseDto.Name;
            OldCourse.Degree=courseDto.Degree;
            OldCourse.MinDegree=courseDto.MinDegree;
            OldCourse.Hours=courseDto.Hours;
            OldCourse.DepartmentId=courseDto.DepartmentId;

            courseRepo.Update(OldCourse);
            courseRepo.Save();

            return NoContent();
        }




        [HttpGet("{id:int}/Details")]
        public IActionResult Details(int id)
        {

            var course = courseRepo.CourseWithDeptTraineeInstractor(id);

            if (course == null)
                return NotFound(new { message = "course not found" });

            var dto = new CourseWithDeptTraineeInstractorDto
            {
                Id=course.Id,
                Name = course.Name,
                Hours=course.Hours,
                Degree = course.Degree,
                MinDegree = course.MinDegree,
                Department = course.Department?.Name,
                Instructors = course.Instructors?.Select(i => i.Name).ToList(),
                Trainees = course.CourseResults?.Select(cr=>cr.Trainee.Name).ToList(),
            };
            return Ok(dto);
        }
    }
}

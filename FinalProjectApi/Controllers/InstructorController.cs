using FinalProjectApi.Dto.InstructorDto;
using FinalProjectApi.Models;
using FinalProjectMVC.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {

        private readonly IInstractorRepository instractorRepo;
        private readonly ICourseRepository courseRepo;
        private readonly IDepartmentRepository departmentRepo;

        public InstructorController(IInstractorRepository instractorRepository, IDepartmentRepository departmentRepository, ICourseRepository courseRepository)
        {
            this.instractorRepo=instractorRepository;
            this.courseRepo=courseRepository;
            this.departmentRepo = departmentRepository;

        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var instructor = instractorRepo.GetAll().Select(item => (new InstructorDto
            {
                Id = item.Id,
                Name = item.Name,
                Address = item.Address,
            })).ToList();

            return Ok(instructor);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var instructor = instractorRepo.GetInstructorWithCourseDept(id);
            if (instructor == null)
                return NotFound(new { message = "Instructor not found" });
            var model = new InstructorDetailsDto
            {
                Id = instructor.Id,
                Name = instructor.Name,
                Address = instructor.Address,
                Salary=instructor.Salary,
                Department=instructor.Department?.Name ??"N/A",
                Course = instructor.Course?.Name??"N/A"

            };
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var instructor = instractorRepo.GetById(id);
            if (instructor == null)
                return NotFound(new { message = "instractor is not found" });

            instractorRepo.Delete(id);
            instractorRepo.Save();
            return NoContent();


        }

        [HttpGet("dropdown-data")]
        public IActionResult GetDropdownData()
        {
            var departments = departmentRepo.GetAll()
                                .Select(d => new DropdownItemDto
                                {
                                    Id = d.Id,
                                    Name = d.Name
                                }).ToList();

            var courses = courseRepo.GetAll()
                                .Select(c => new DropdownItemDto
                                {
                                    Id = c.Id,
                                    Name = c.Name
                                }).ToList();

            return Ok(new
            {
                Departments = departments,
                Courses = courses
            });
        }

        [HttpPost]
        public IActionResult Add([FromBody] createInstructor InsDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dept = departmentRepo.GetById(InsDto.DepartmentId);
            if (dept == null)
                return NotFound(new { message = $"department id {InsDto.DepartmentId} is not valid " });


            var course = courseRepo.GetById(InsDto.CourseId);
            if (course == null)
                return NotFound(new { message = $"course id {InsDto.CourseId} is not valid " });

            var instructor = new Instructor
            {
                Name=InsDto.Name,
                ImageUrl=InsDto.ImageUrl,
                Salary=InsDto.Salary,
                Address= InsDto.Address,
                DepartmentId=InsDto.DepartmentId,
                CourseId=InsDto.CourseId,

            };

            instractorRepo.Add(instructor);
            instractorRepo.Save();

            return CreatedAtAction(nameof(GetById), new{id = instructor.Id }, instructor);
        }


        [HttpPut("{id:int}")]
        public IActionResult Edit(int id,[FromBody] UpdateInstructor InsDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dept = departmentRepo.GetById(InsDto.DepartmentId);
            if (dept == null)
                return NotFound(new { message = $"department id {InsDto.DepartmentId} is not valid " });


            var course = courseRepo.GetById(InsDto.CourseId);
            if (course == null)
                return NotFound(new { message = $"course id {InsDto.CourseId} is not valid " });


            var OldInstructor = instractorRepo.GetById(id);
            if (OldInstructor == null)
                return NotFound();

            OldInstructor.Name=InsDto.Name;
            OldInstructor.ImageUrl=InsDto.ImageUrl;
            OldInstructor.Salary= InsDto.Salary;
            OldInstructor.Address=InsDto.Address;
            OldInstructor.DepartmentId = InsDto.DepartmentId;
            OldInstructor.CourseId = InsDto.CourseId;
    

            instractorRepo.Update(OldInstructor);
            instractorRepo.Save();

            return NoContent();
        }



    }
}





//        [HttpGet]
//        public IActionResult GetCoursesByDepartment(int departmentId)
//        {
//            var courses = courseRepo.GetAll()
//                                    .Where(c => c.DepartmentId == departmentId)
//                                    .Select(c => new { c.Id, c.Name })
//                                    .ToList();

//            return Json(courses);
//        }
//        [HttpGet]
//        public IActionResult GetDepartmentByCourse(int courseId)
//        {
//            var course = courseRepo.GetById(courseId);
//            if (course == null)
//                return NotFound();

//            return Json(new { departmentId = course.DepartmentId });
//        }
//    }
//}
//    }
//}

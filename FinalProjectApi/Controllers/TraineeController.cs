using FinalProjectApi.Dto.TraineeDto;
using FinalProjectApi.Models;
using FinalProjectMVC.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraineeController : ControllerBase
    {
        private readonly ITraineeRepository traineeRepo;
        private readonly IDepartmentRepository departmentRepo;

        public TraineeController(ITraineeRepository traineeRepository, IDepartmentRepository departmentRepository)
        {
            traineeRepo = traineeRepository;
            departmentRepo = departmentRepository;
        }

        // GET: api/trainee
        [HttpGet]
        public IActionResult GetAll()
        {
            var trainees = traineeRepo.AlltarinWithDept();

            var model = trainees.Select(t => new TraineeDto
            {
                Id = t.Id,
                Name = t.Name,
                Grade = t.Grade,
                Address = t.Address,
                Department = t.Department?.Name ?? "N/A",
            }).ToList();

            return Ok(model);
        }

        // GET: api/trainee/5
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var trainee = traineeRepo.GetById(id);
            if (trainee == null)
                return NotFound();

            var traineeDto = new TraineeDto
            {
                Id = trainee.Id,
                Name = trainee.Name,
                Grade = trainee.Grade,
                Address = trainee.Address,
                Department = trainee.Department?.Name ?? "N/A",
            };

            return Ok(traineeDto);
        }

        // GET: api/trainee/5/details
        [HttpGet("{id:int}/details")]
        public IActionResult GetTrainee(int id)
        {
            var trainee = traineeRepo.GetTraineeWithDeptCourseInstructors(id);

            if (trainee == null)
                return NotFound(new { message = $"Trainee with ID {id} not found." });

            var traineeDto = new TraineeWithDeptCourseInstructorsDto
            {
                Id = trainee.Id,
                Name = trainee.Name,
                Grade = trainee.Grade,
                Address = trainee.Address,
                Department = trainee.Department?.Name ?? "N/A",
                Courses = trainee.CourseResults.Select(cr => new CourseWithInstructorsDto
                {
                    CourseName = cr.Course?.Name,
                    Instructors = cr.Course?.Instructors?.Select(i => i.Name).ToList() ?? new List<string>()
                }).ToList()
            };

            return Ok(traineeDto);
        }



        // GET: api/trainee/departments
        [HttpGet("departments")]
        public IActionResult GetDepartments()
        {
            var departments = departmentRepo.GetAll()
                .Select(d => new { d.Id, d.Name })
                .ToList();

            return Ok(departments);
        }

        // POST: api/trainee
        [HttpPost]
        public IActionResult Add([FromBody] CraeteUpdateTraineeDto traineeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var trainee = new Trainee
            {
                Name = traineeDto.Name,
           
                Grade = traineeDto.Grade,
                Address = traineeDto.Address,
                DepartmentId = traineeDto.DepartmentId
            };

            traineeRepo.Add(trainee);
            traineeRepo.Save();

            return CreatedAtAction(nameof(GetById), new { id = trainee.Id }, new
            {
                message = "Trainee created successfully",
                trainee = new
                {
                    trainee.Id,
                    trainee.Name,
                    trainee.Grade,
                    trainee.Address,
                    trainee.DepartmentId
                }
            });
        }

        // PUT: api/trainee/5
        [HttpPut("{id:int}")]
        public IActionResult Edit(int id, CraeteUpdateTraineeDto trainee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var oldTrainee = traineeRepo.GetById(id);
            if (oldTrainee == null)
                return NotFound(new { message = "This trainee was not found." });

            oldTrainee.Name = trainee.Name;
       
            oldTrainee.Address = trainee.Address;
            oldTrainee.Grade = trainee.Grade;
            oldTrainee.DepartmentId = trainee.DepartmentId;

            traineeRepo.Update(oldTrainee);
            traineeRepo.Save();

            return Ok(new { message = "Trainee updated successfully" });
        }

        // DELETE: api/trainee/5
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var trainee = traineeRepo.GetById(id);
            if (trainee == null)
                return NotFound(new { message = $"Trainee with ID {id} was not found." });

            traineeRepo.Delete(id);
            traineeRepo.Save();

            return NoContent();
        }
    }
} 
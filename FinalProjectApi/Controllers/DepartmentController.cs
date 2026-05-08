using FinalProjectApi.Dto;
using FinalProjectApi.Dto.DepartmentDto;
using FinalProjectApi.Models;
using FinalProjectMVC.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository departmentRepo;
   
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            this.departmentRepo=departmentRepository;
        }

        [HttpGet]
        public IActionResult GetAllDepartment()
        {
          var departments = departmentRepo.GetAll()
                .Select(item=> new GetDepartmentDto
            {
                Id=item.Id,
                Name=item.Name,
                Manager = item.Manager
            }).ToList();
         
            return Ok(departments);
        }


        [HttpGet("{id:int}")]
        public IActionResult GetDepartment(int id)
        {
            var department = departmentRepo.GetById(id);

            if (department == null)
                return NotFound(new { Message = "This department was not found." });

            var departmentDto = new GetDepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                Manager = department.Manager
            };

            return Ok(departmentDto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var department = departmentRepo.GetById(id);
            if (department == null)
                return NotFound("This department is not found");
            departmentRepo.Delete(id);
            departmentRepo.Save();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Edit( int id ,[FromBody] UpdateAndCreateDepartmentDto department)
        {

            if (!ModelState.IsValid)            
                return BadRequest(ModelState);    
            
            var oldDepartment = departmentRepo.GetById(id);

            if (oldDepartment == null)            
                return NotFound();
            
            oldDepartment.Name=department.Name;
            oldDepartment.Manager=department.Manager;
            departmentRepo.Update(oldDepartment);
            departmentRepo.Save();
            return NoContent();
        }

        [HttpPost]
        public IActionResult add([FromBody] UpdateAndCreateDepartmentDto departmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var department = new Department
            {
                Name = departmentDto.Name,
                Manager = departmentDto.Manager

            };
            departmentRepo.Add(department);
            departmentRepo.Save();
            var result = new GetDepartmentDto
            {
                Id=department.Id,
                Name= department.Name,
                Manager = department.Manager
            };
            return CreatedAtAction(nameof(GetDepartment) , new {id=department.Id} , result);

        }





        [HttpGet("DeptTrainee/{id}")]
        public IActionResult GetDepartmentWithTrainee(int id)
        {
            var department = departmentRepo.DepartmentWithTrainee(id);
            if (department == null)
                return NotFound(new { message = "Department not found!" });
         
            return Ok(department);
        }








        [HttpGet("DeptCourse/{id}")]
        public IActionResult GetDepartmentWithCourse(int id)
        {
            var department = departmentRepo.GetDepartmentWithCourse(id);
            if (department == null)
                return NotFound(new { message = "Department not found!" });
      
         
            return Ok(department);
        }






        [HttpGet("DeptInstrutor/{id}")]
        public IActionResult GetDepartmentWithInstructor(int id)
        {
            var department = departmentRepo.GetDepartmentWithInstructor(id);
            if (department == null)
                return NotFound(new { message = "Department not found!" });

            return Ok(department);
        }





        [HttpGet("DeptTraineeInsCourse/{id}")]
        public IActionResult GetByIdWithTraineeCourseInstructor(int id)
        {
            var department = departmentRepo.GetByIdWithTraineeCourseInstructor(id);
            if (department == null)
                return NotFound(new { message = "Department not found!" });

            return Ok(department);
        }
    }
}

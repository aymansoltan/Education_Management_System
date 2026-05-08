using FinalProjectApi.Dto.DepartmentDto;
using FinalProjectApi.Models;

using Microsoft.EntityFrameworkCore;

namespace FinalProjectMVC.Repository
{
    public class DepartmentRepository :GenericRepository<Department> , IDepartmentRepository
    {
        private readonly ITIContext Context;
        public DepartmentRepository(ITIContext context) :base(context)
        {
            Context = context;
        }

        public DepartmentWithTraineesDto? DepartmentWithTrainee(int id )
        {
            return Context.Departments
                .Where(d => d.Id==id)
                .Select(d => new DepartmentWithTraineesDto
                {
                    Id= d.Id,
                    Name= d.Name,
                    Manager=    d.Manager,
                    Trainees=d.Trainees.Select(t => t.Name).ToList(),
                }).FirstOrDefault();

        } 


        public DepartmentWithCourseDto? GetDepartmentWithCourse(int id )
        {
            return Context.Departments
            .Where(d => d.Id==id)
            .Select(d => new DepartmentWithCourseDto
            {
                Id= d.Id,
                Name= d.Name,
                Manager=d.Manager,
                courses=d.Courses.Select(t => t.Name).ToList(),
            }).FirstOrDefault();
        }


        public DepartmentWithInstructorDto? GetDepartmentWithInstructor(int id )
        {
            return Context.Departments
                .Where(d => d.Id==id)
                .Select(d => new DepartmentWithInstructorDto
                {
                    Id= d.Id,
                    Name= d.Name,
                    Manager=d.Manager,
                    Instructors=d.Instructors.Select(t => t.Name).ToList(),
                }).FirstOrDefault();

        }



        public GetByIdWithTraineeCourseInstructorDto? GetByIdWithTraineeCourseInstructor(int id )
        {
            return Context.Departments
                            .Where(d => d.Id==id)
                            .Select(d => new GetByIdWithTraineeCourseInstructorDto
                            {
                                Id= d.Id,
                                Name= d.Name,
                                Manager=d.Manager,
                                instructors=d.Instructors.Select(t => t.Name).ToList(),
                                courses=d.Courses.Select(t => t.Name).ToList(),
                                trainees=d.Trainees.Select(t => t.Name).ToList(),

                            }).FirstOrDefault();
        }
    }
}

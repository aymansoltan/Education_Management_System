using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectApi.Models;

[Index("DepartmentId", Name = "IX_Courses_DepartmentId")]
public partial class Course
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public int Hours { get; set; }

    public int Degree { get; set; }

    public int MinDegree { get; set; }

    public int DepartmentId { get; set; }

    [InverseProperty("Course")]
    public virtual ICollection<CourseResult> CourseResults { get; set; } = new List<CourseResult>();

    [ForeignKey("DepartmentId")]
    [InverseProperty("Courses")]
    public virtual Department Department { get; set; } = null!;

    [InverseProperty("Course")]
    public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
}

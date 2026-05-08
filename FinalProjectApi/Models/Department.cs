using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectApi.Models;

public partial class Department
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    public string Manager { get; set; } = null!;

    [InverseProperty("Department")]
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    [InverseProperty("Department")]
    public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();

    [InverseProperty("Department")]
    public virtual ICollection<Trainee> Trainees { get; set; } = new List<Trainee>();
}

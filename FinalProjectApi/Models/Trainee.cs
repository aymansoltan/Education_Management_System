using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectApi.Models;

[Index("DepartmentId", Name = "IX_Trainees_DepartmentId")]
public partial class Trainee
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(250)]
    public string ImageUrl { get; set; } = null!;

    [StringLength(100)]
    public string Address { get; set; } = null!;

    public string Grade { get; set; } = null!;

    public int DepartmentId { get; set; }

    [InverseProperty("Trainee")]
    public virtual ICollection<CourseResult> CourseResults { get; set; } = new List<CourseResult>();

    [ForeignKey("DepartmentId")]
    [InverseProperty("Trainees")]
    public virtual Department Department { get; set; } = null!;
}

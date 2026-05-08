using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectApi.Models;

[Index("CourseId", Name = "IX_Instructors_CourseId")]
[Index("DepartmentId", Name = "IX_Instructors_DepartmentId")]
public partial class Instructor
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(250)]
    public string ImageUrl { get; set; } = null!;

    [StringLength(100)]
    public string Address { get; set; } = null!;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Salary { get; set; }

    public int DepartmentId { get; set; }

    public int CourseId { get; set; }

    [ForeignKey("CourseId")]
    [InverseProperty("Instructors")]
    public virtual Course Course { get; set; } = null!;

    [ForeignKey("DepartmentId")]
    [InverseProperty("Instructors")]
    public virtual Department Department { get; set; } = null!;
}

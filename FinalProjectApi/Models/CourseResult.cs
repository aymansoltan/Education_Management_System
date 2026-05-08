using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectApi.Models;

[Index("CourseId", Name = "IX_CourseResults_CourseId")]
[Index("TraineeId", Name = "IX_CourseResults_TraineeId")]
public partial class CourseResult
{
    [Key]
    public int Id { get; set; }

    [Column("degree")]
    public int Degree { get; set; }

    public int TraineeId { get; set; }

    public int CourseId { get; set; }

    [ForeignKey("CourseId")]
    [InverseProperty("CourseResults")]
    public virtual Course Course { get; set; } = null!;

    [ForeignKey("TraineeId")]
    [InverseProperty("CourseResults")]
    public virtual Trainee Trainee { get; set; } = null!;
}

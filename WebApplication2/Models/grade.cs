using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Model;

[PrimaryKey("coursecode", "studentid")]
public partial class grade
{
    [Key]
    public int studentid { get; set; }

    [Key]
    [StringLength(6)]
    [Unicode(false)]
    public string coursecode { get; set; } = null!;

    [Column("grade")]
    [StringLength(1)]
    [Unicode(false)]
    public char grade1 { get; set; }

    [ForeignKey("coursecode")]
    [InverseProperty("grade")]
    public virtual course coursecodeNavigation { get; set; } = null!;

    [ForeignKey("studentid")]
   [InverseProperty("grade")]
    public virtual student student { get; set; } = null!;
}

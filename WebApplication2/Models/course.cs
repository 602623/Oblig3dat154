using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Model;

namespace WebApplication2.Models;

public partial class course
{
    [Key]
    [StringLength(6)]
    [Unicode(false)]
    public string coursecode { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string coursename { get; set; } = null!;

    [StringLength(1)]
    [Unicode(false)]
    public string semester { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string teacher { get; set; } = null!;

    [InverseProperty("coursecodeNavigation")]
    public virtual ICollection<grade> grade { get; set; } = new List<grade>();
}

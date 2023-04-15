using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Model;

namespace WebApplication2.Models;

public partial class student
{
    [Key]
    public int id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string studentname { get; set; } = null!;

    public int studentage { get; set; }

    [InverseProperty("student")]
    public virtual ICollection<grade> grade { get; set; } = new List<grade>();
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task2.Models;

public class Test01
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [StringLength(100)]
    public string Name { get; set; }
    public short Status { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
}
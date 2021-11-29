using System;
using System.ComponentModel.DataAnnotations;

namespace TCD0302API.Models
{
  public class Park
  {
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string State { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime Established { get; set; }
  }
}

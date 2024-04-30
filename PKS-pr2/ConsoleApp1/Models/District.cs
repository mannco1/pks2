using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1.Models;

public class District
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
}
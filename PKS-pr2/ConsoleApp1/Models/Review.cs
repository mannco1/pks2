using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace ConsoleApp1.Models;

public class Review
{
    [Key]
    public int Id { get; set; }
    public Estate Estate { get; set; }
    public int EstateId { get; set; }
    public long Date { get; set; }
    public Standard Standard { get; set; }
    public int StandardId { get; set; }
    public int Grade { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace ConsoleApp1.Models;

public class Estate
{
    [Key]
    public int Id { get; set; }
    public District District { get; set; }
    public int DistrictId { get; set; }
    public string Address { get; set; }
    public int Floor { get; set; }
    public int Rooms { get; set; }
    public Type Type { get; set; }
    public int TypeId { get; set; }
    public bool Status { get; set; }
    public double Cost { get; set; }
    public string Description { get; set; }
    public Material Material { get; set; }
    public int MaterialId { get; set; }
    public double Square { get; set; }
    public long Date { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace ConsoleApp1.Models;

public class Sell
{
    [Key]
    public int Id { get; set; }
    public Estate Estate { get; set; }
    public int EstateId { get; set; }
    public long Date { get; set; }
    public Agent Agent{ get; set; }
    public int AgentId { get; set; }
    public int Cost { get; set; }
}
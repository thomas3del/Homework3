using System.ComponentModel.DataAnnotations;

namespace Homework3.Models;

/// <summary>
/// Склад (справочная таблица, сторона "один")
/// </summary>
public class Warehouse
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = "";

    public ICollection<Batch> Batches { get; set; } = new List<Batch>();
}
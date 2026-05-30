using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework3.Models;

/// <summary>
/// Партия товаров (основная таблица, сторона "много")
/// </summary>
public class Batch
{
    public int Id { get; set; }

    [ForeignKey(nameof(Warehouse))]
    public int WarehouseId { get; set; }

    public Warehouse? Warehouse { get; set; }

    [Required]
    public string ProductName { get; set; } = "";

    [Range(0, int.MaxValue, ErrorMessage = "Количество не может быть отрицательным")]
    public int Quantity { get; set; }
}
using Homework3.Models;
using Microsoft.EntityFrameworkCore;

namespace Homework3.Data;

/// <summary>
/// Контекст базы данных
/// </summary>
public class AppDbContext : DbContext
{
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Batch> Batches { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=warehouse.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Начальные данные: 4 склада
        modelBuilder.Entity<Warehouse>().HasData(
            new Warehouse { Id = 1, Name = "Центральный склад" },
            new Warehouse { Id = 2, Name = "Северный склад" },
            new Warehouse { Id = 3, Name = "Южный склад" },
            new Warehouse { Id = 4, Name = "Восточный склад" }
        );

        // Начальные данные: 14 партий товаров
        modelBuilder.Entity<Batch>().HasData(
            new Batch { Id = 1, WarehouseId = 1, ProductName = "Белый рис", Quantity = 500 },
            new Batch { Id = 2, WarehouseId = 1, ProductName = "Сахар", Quantity = 300 },
            new Batch { Id = 3, WarehouseId = 2, ProductName = "Оливковое масло", Quantity = 150 },
            new Batch { Id = 4, WarehouseId = 3, ProductName = "Мука", Quantity = 200 },
            new Batch { Id = 5, WarehouseId = 4, ProductName = "Чечевица", Quantity = 120 },
            new Batch { Id = 6, WarehouseId = 2, ProductName = "Финики", Quantity = 250 },
            new Batch { Id = 7, WarehouseId = 1, ProductName = "Чай", Quantity = 400 },
            new Batch { Id = 8, WarehouseId = 3, ProductName = "Кофе", Quantity = 180 },
            new Batch { Id = 9, WarehouseId = 4, ProductName = "Сухое молоко", Quantity = 90 },
            new Batch { Id = 10, WarehouseId = 2, ProductName = "Соки", Quantity = 600 },
            new Batch { Id = 11, WarehouseId = 1, ProductName = "Консервы", Quantity = 350 },
            new Batch { Id = 12, WarehouseId = 3, ProductName = "Специи", Quantity = 75 },
            new Batch { Id = 13, WarehouseId = 4, ProductName = "Рис басмати", Quantity = 220 },
            new Batch { Id = 14, WarehouseId = 2, ProductName = "Подсолнечное масло", Quantity = 310 }
        );
    }
}
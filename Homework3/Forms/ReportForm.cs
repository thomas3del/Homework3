using Homework3.Data;
using Microsoft.EntityFrameworkCore;

namespace Homework3.Forms;

public partial class ReportForm : Form
{
    private DataGridView dgvReport1 = new DataGridView(), dgvReport2 = new DataGridView(), dgvReport3 = new DataGridView();
    private Button btnBack = new Button();

    public ReportForm()
    {
        this.Text = "Отчёты";
        this.Size = new System.Drawing.Size(800, 700);
        LoadReports();
        SetupUI();
    }

    private void SetupUI()
    {
        Label lbl1 = new Label { Text = "Отчёт 1: Список партий", Location = new System.Drawing.Point(20, 10), Size = new System.Drawing.Size(300, 30) };
        dgvReport1.Location = new System.Drawing.Point(20, 40); dgvReport1.Size = new System.Drawing.Size(740, 150);

        Label lbl2 = new Label { Text = "Отчёт 2: Количество по складам", Location = new System.Drawing.Point(20, 210), Size = new System.Drawing.Size(300, 30) };
        dgvReport2.Location = new System.Drawing.Point(20, 240); dgvReport2.Size = new System.Drawing.Size(740, 120);

        Label lbl3 = new Label { Text = "Отчёт 3: Среднее количество", Location = new System.Drawing.Point(20, 380), Size = new System.Drawing.Size(300, 30) };
        dgvReport3.Location = new System.Drawing.Point(20, 410); dgvReport3.Size = new System.Drawing.Size(740, 120);

        btnBack.Text = "Назад"; btnBack.Location = new System.Drawing.Point(300, 560); btnBack.Size = new System.Drawing.Size(150, 50);
        btnBack.Click += (s, e) => this.Close();

        this.Controls.Add(lbl1); this.Controls.Add(dgvReport1);
        this.Controls.Add(lbl2); this.Controls.Add(dgvReport2);
        this.Controls.Add(lbl3); this.Controls.Add(dgvReport3);
        this.Controls.Add(btnBack);
    }

    private void LoadReports()
    {
        using var context = new AppDbContext();

        // Отчёт 1: Полный список партий с названиями складов
        var report1 = context.Batches.Include(b => b.Warehouse).OrderBy(b => b.ProductName)
            .Select(b => new { b.ProductName, WarehouseName = b.Warehouse != null ? b.Warehouse.Name : "", b.Quantity }).ToList();
        dgvReport1.DataSource = report1;

        // Отчёт 2: Количество партий по складам
        var report2 = context.Batches.Include(b => b.Warehouse)
            .GroupBy(b => b.Warehouse != null ? b.Warehouse.Name : "Без склада")
            .Select(g => new { Warehouse = g.Key, Count = g.Count() }).OrderBy(r => r.Warehouse).ToList();
        dgvReport2.DataSource = report2;

        // Отчёт 3: Среднее количество по складам
        var report3 = context.Batches.Include(b => b.Warehouse)
            .GroupBy(b => b.Warehouse != null ? b.Warehouse.Name : "Без склада")
            .Select(g => new { Warehouse = g.Key, AvgQuantity = g.Average(b => b.Quantity) }).OrderByDescending(r => r.AvgQuantity).ToList();
        dgvReport3.DataSource = report3;
    }
}
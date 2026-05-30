using Homework3.Data;
using Homework3.Models;
using Microsoft.EntityFrameworkCore;

namespace Homework3.Forms;

public partial class BatchesForm : Form
{
    private DataGridView dgvBatches = new DataGridView();
    private Button btnAdd = new Button(), btnEdit = new Button(), btnDelete = new Button(), btnRefresh = new Button(), btnBack = new Button();

    public BatchesForm()
    {
        this.Text = "Управление партиями товаров";
        this.Size = new System.Drawing.Size(700, 500);
        LoadData();
        SetupButtons();
    }

    private void SetupButtons()
    {
        dgvBatches.Location = new System.Drawing.Point(20, 20);
        dgvBatches.Size = new System.Drawing.Size(640, 300);
        btnAdd.Text = "Добавить"; btnAdd.Location = new System.Drawing.Point(20, 340);
        btnEdit.Text = "Редактировать"; btnEdit.Location = new System.Drawing.Point(120, 340);
        btnDelete.Text = "Удалить"; btnDelete.Location = new System.Drawing.Point(220, 340);
        btnRefresh.Text = "Обновить"; btnRefresh.Location = new System.Drawing.Point(320, 340);
        btnBack.Text = "Назад"; btnBack.Location = new System.Drawing.Point(420, 340);

        btnAdd.Click += BtnAdd_Click;
        btnEdit.Click += BtnEdit_Click;
        btnDelete.Click += BtnDelete_Click;
        btnRefresh.Click += (s, e) => LoadData();
        btnBack.Click += (s, e) => this.Close();

        this.Controls.Add(dgvBatches);
        this.Controls.Add(btnAdd);
        this.Controls.Add(btnEdit);
        this.Controls.Add(btnDelete);
        this.Controls.Add(btnRefresh);
        this.Controls.Add(btnBack);
    }

    private void LoadData()
    {
        using var context = new AppDbContext();
        var batches = context.Batches.Include(b => b.Warehouse).Select(b => new { b.Id, b.ProductName, WarehouseName = b.Warehouse != null ? b.Warehouse.Name : "", b.Quantity }).ToList();
        dgvBatches.DataSource = batches;
    }

    private void BtnAdd_Click(object? sender, EventArgs e)
    {
        using var context = new AppDbContext();
        var warehouses = context.Warehouses.ToList();
        var form = new BatchEditForm(null, warehouses);
        if (form.ShowDialog() == DialogResult.OK)
        {
            context.Batches.Add(new Batch { ProductName = form.ProductName, WarehouseId = form.WarehouseId, Quantity = form.Quantity });
            context.SaveChanges();
            LoadData();
            MessageBox.Show("Партия добавлена!", "Успех");
        }
    }

    private void BtnEdit_Click(object? sender, EventArgs e)
    {
        if (dgvBatches.CurrentRow == null) return;
        int id = (int)dgvBatches.CurrentRow.Cells["Id"].Value;
        using var context = new AppDbContext();
        var batch = context.Batches.Find(id);
        var warehouses = context.Warehouses.ToList();
        if (batch != null)
        {
            var form = new BatchEditForm(batch, warehouses);
            if (form.ShowDialog() == DialogResult.OK)
            {
                batch.ProductName = form.ProductName;
                batch.WarehouseId = form.WarehouseId;
                batch.Quantity = form.Quantity;
                context.SaveChanges();
                LoadData();
                MessageBox.Show("Партия обновлена!", "Успех");
            }
        }
    }

    private void BtnDelete_Click(object? sender, EventArgs e)
    {
        if (dgvBatches.CurrentRow == null) return;
        int id = (int)dgvBatches.CurrentRow.Cells["Id"].Value;
        string name = dgvBatches.CurrentRow.Cells["ProductName"].Value.ToString() ?? "";
        if (MessageBox.Show($"Удалить партию \"{name}\"?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
            using var context = new AppDbContext();
            var batch = context.Batches.Find(id);
            if (batch != null) { context.Batches.Remove(batch); context.SaveChanges(); LoadData(); MessageBox.Show("Партия удалена!", "Успех"); }
        }
    }
}
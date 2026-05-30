using Homework3.Data;
using Homework3.Models;
using Microsoft.EntityFrameworkCore;

namespace Homework3.Forms;

public partial class WarehousesForm : Form
{
    private DataGridView dgvWarehouses = new DataGridView();
    private Button btnAdd = new Button(), btnEdit = new Button(), btnDelete = new Button(), btnRefresh = new Button(), btnBack = new Button();

    public WarehousesForm()
    {
        this.Text = "Управление складами";
        this.Size = new System.Drawing.Size(600, 450);
        LoadData();
        SetupButtons();
    }

    private void SetupButtons()
    {
        dgvWarehouses.Location = new System.Drawing.Point(20, 20);
        dgvWarehouses.Size = new System.Drawing.Size(540, 250);

        btnAdd.Text = "Добавить"; btnAdd.Location = new System.Drawing.Point(20, 290);
        btnEdit.Text = "Редактировать"; btnEdit.Location = new System.Drawing.Point(120, 290);
        btnDelete.Text = "Удалить"; btnDelete.Location = new System.Drawing.Point(220, 290);
        btnRefresh.Text = "Обновить"; btnRefresh.Location = new System.Drawing.Point(320, 290);
        btnBack.Text = "Назад"; btnBack.Location = new System.Drawing.Point(420, 290);

        btnAdd.Click += BtnAdd_Click;
        btnEdit.Click += BtnEdit_Click;
        btnDelete.Click += BtnDelete_Click;
        btnRefresh.Click += (s, e) => LoadData();
        btnBack.Click += (s, e) => this.Close();

        this.Controls.Add(dgvWarehouses);
        this.Controls.Add(btnAdd);
        this.Controls.Add(btnEdit);
        this.Controls.Add(btnDelete);
        this.Controls.Add(btnRefresh);
        this.Controls.Add(btnBack);
    }

    private void LoadData()
    {
        using var context = new AppDbContext();
        var warehouses = context.Warehouses.OrderBy(w => w.Name).ToList();
        dgvWarehouses.DataSource = warehouses;
        if (dgvWarehouses.Columns.Contains("Batches")) dgvWarehouses.Columns["Batches"].Visible = false;
    }

    private void BtnAdd_Click(object? sender, EventArgs e)
    {
        string name = Microsoft.VisualBasic.Interaction.InputBox("Введите название склада:", "Добавление склада", "");
        if (!string.IsNullOrWhiteSpace(name))
        {
            using var context = new AppDbContext();
            context.Warehouses.Add(new Warehouse { Name = name });
            context.SaveChanges();
            LoadData();
            MessageBox.Show("Склад добавлен!", "Успех");
        }
    }

    private void BtnEdit_Click(object? sender, EventArgs e)
    {
        if (dgvWarehouses.CurrentRow == null) return;
        int id = (int)dgvWarehouses.CurrentRow.Cells["Id"].Value;
        string oldName = dgvWarehouses.CurrentRow.Cells["Name"].Value.ToString() ?? "";
        string newName = Microsoft.VisualBasic.Interaction.InputBox("Введите новое название:", "Редактирование склада", oldName);
        if (!string.IsNullOrWhiteSpace(newName))
        {
            using var context = new AppDbContext();
            var warehouse = context.Warehouses.Find(id);
            if (warehouse != null) { warehouse.Name = newName; context.SaveChanges(); LoadData(); MessageBox.Show("Склад обновлён!", "Успех"); }
        }
    }

    private void BtnDelete_Click(object? sender, EventArgs e)
    {
        if (dgvWarehouses.CurrentRow == null) return;
        int id = (int)dgvWarehouses.CurrentRow.Cells["Id"].Value;
        string name = dgvWarehouses.CurrentRow.Cells["Name"].Value.ToString() ?? "";
        using var context = new AppDbContext();
        if (context.Batches.Any(b => b.WarehouseId == id))
        {
            MessageBox.Show($"Невозможно удалить склад \"{name}\", так как на нём есть товары!", "Ошибка");
            return;
        }
        if (MessageBox.Show($"Удалить склад \"{name}\"?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
            var warehouse = context.Warehouses.Find(id);
            if (warehouse != null) { context.Warehouses.Remove(warehouse); context.SaveChanges(); LoadData(); MessageBox.Show("Склад удалён!", "Успех"); }
        }
    }
}
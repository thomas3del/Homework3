using Homework3.Models;

namespace Homework3.Forms;

public partial class BatchEditForm : Form
{
    private TextBox txtProductName = new TextBox();
    private ComboBox cmbWarehouse = new ComboBox();
    private NumericUpDown numQuantity = new NumericUpDown();
    private Button btnSave = new Button(), btnCancel = new Button();

    private Batch? _batch;
    private List<Warehouse> _warehouses;

    public string ProductName => txtProductName.Text.Trim();
    public int WarehouseId => (int)cmbWarehouse.SelectedValue;
    public int Quantity => (int)numQuantity.Value;

    public BatchEditForm(Batch? batch, List<Warehouse> warehouses)
    {
        _batch = batch;
        _warehouses = warehouses;
        this.Text = batch == null ? "Добавление партии" : "Редактирование партии";
        this.Size = new System.Drawing.Size(400, 250);
        SetupControls();
        LoadValues();
    }

    private void SetupControls()
    {
        Label lblProduct = new Label { Text = "Товар:", Location = new System.Drawing.Point(20, 20), Size = new System.Drawing.Size(100, 30) };
        txtProductName.Location = new System.Drawing.Point(120, 20); txtProductName.Size = new System.Drawing.Size(200, 30);

        Label lblWarehouse = new Label { Text = "Склад:", Location = new System.Drawing.Point(20, 60), Size = new System.Drawing.Size(100, 30) };
        cmbWarehouse.Location = new System.Drawing.Point(120, 60); cmbWarehouse.Size = new System.Drawing.Size(200, 30);
        cmbWarehouse.DataSource = _warehouses;
        cmbWarehouse.DisplayMember = "Name";
        cmbWarehouse.ValueMember = "Id";

        Label lblQuantity = new Label { Text = "Количество:", Location = new System.Drawing.Point(20, 100), Size = new System.Drawing.Size(100, 30) };
        numQuantity.Location = new System.Drawing.Point(120, 100); numQuantity.Size = new System.Drawing.Size(100, 30); numQuantity.Minimum = 0; numQuantity.Maximum = 1000000;

        btnSave.Text = "Сохранить"; btnSave.Location = new System.Drawing.Point(80, 150); btnSave.Size = new System.Drawing.Size(100, 40);
        btnCancel.Text = "Отмена"; btnCancel.Location = new System.Drawing.Point(200, 150); btnCancel.Size = new System.Drawing.Size(100, 40);

        btnSave.Click += (s, e) => { if (ValidateForm()) { DialogResult = DialogResult.OK; Close(); } };
        btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

        this.Controls.Add(lblProduct); this.Controls.Add(txtProductName);
        this.Controls.Add(lblWarehouse); this.Controls.Add(cmbWarehouse);
        this.Controls.Add(lblQuantity); this.Controls.Add(numQuantity);
        this.Controls.Add(btnSave); this.Controls.Add(btnCancel);
    }

    private void LoadValues()
    {
        if (_batch != null)
        {
            txtProductName.Text = _batch.ProductName;
            cmbWarehouse.SelectedValue = _batch.WarehouseId;
            numQuantity.Value = _batch.Quantity;
        }
    }

    private bool ValidateForm()
    {
        if (string.IsNullOrWhiteSpace(txtProductName.Text))
        {
            MessageBox.Show("Введите название товара!", "Ошибка");
            return false;
        }
        if (numQuantity.Value < 0)
        {
            MessageBox.Show("Количество не может быть отрицательным!", "Ошибка");
            return false;
        }
        return true;
    }
}
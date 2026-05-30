using Homework3.Forms;

namespace Homework3.Forms;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        SetupButtons();
    }

    private void SetupButtons()
    {
        this.Text = "Управление складами и товарами";
        this.Size = new System.Drawing.Size(400, 300);

        var btnWarehouses = new Button
        {
            Text = "Склады (Warehouses)",
            Location = new System.Drawing.Point(100, 50),
            Size = new System.Drawing.Size(200, 40)
        };

        var btnBatches = new Button
        {
            Text = "Товары (Batches)",
            Location = new System.Drawing.Point(100, 110),
            Size = new System.Drawing.Size(200, 40)
        };

        var btnReport = new Button
        {
            Text = "Отчёт (Report)",
            Location = new System.Drawing.Point(100, 170),
            Size = new System.Drawing.Size(200, 40)
        };

        var btnExit = new Button
        {
            Text = "Выход",
            Location = new System.Drawing.Point(100, 230),
            Size = new System.Drawing.Size(200, 40)
        };

        btnWarehouses.Click += (s, e) => { new WarehousesForm().ShowDialog(); };
        btnBatches.Click += (s, e) => { new BatchesForm().ShowDialog(); };
        btnReport.Click += (s, e) => { new ReportForm().ShowDialog(); };
        btnExit.Click += (s, e) => Application.Exit();

        this.Controls.Add(btnWarehouses);
        this.Controls.Add(btnBatches);
        this.Controls.Add(btnReport);
        this.Controls.Add(btnExit);
    }
}
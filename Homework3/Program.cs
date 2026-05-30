using Homework3.Forms;

namespace Homework3;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        using (var context = new Data.AppDbContext())
        {
            context.Database.EnsureCreated();
        }

        Application.Run(new MainForm());
    }
}
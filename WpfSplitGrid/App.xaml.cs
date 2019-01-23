using System.Windows;
using WpfSplitGrid.ViewModels;
using WpfSplitGrid.Views;

namespace WpfSplitGrid
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainVm = new MainVm();
            new MainWindow { DataContext = mainVm }.Show();
        }
    }
}

using System.Windows;
using Prism.DryIoc;
using Prism.Ioc;
using Socket_Debugger.Views;

namespace Socket_Debugger
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
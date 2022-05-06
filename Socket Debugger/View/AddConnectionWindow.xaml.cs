using System.Windows;
using Socket_Debugger.ViewModel;

namespace Socket_Debugger.View
{
    /// <summary>
    /// AddConnectionWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddConnectionWindow : Window
    {
        public AddConnectionWindow()
        {
            InitializeComponent();
            this.DataContext = new AddConnectionViewModel(this);
        }
    }
}
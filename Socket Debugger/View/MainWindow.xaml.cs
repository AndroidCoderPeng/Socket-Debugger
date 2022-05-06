using System.Windows;
using Socket_Debugger.Utils;
using Socket_Debugger.ViewModel;

namespace Socket_Debugger.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // 初始化数据库
            SqLiteHelper.GetInstance().InitDataBase();

            this.DataContext = new MainViewModel();
        }
    }
}
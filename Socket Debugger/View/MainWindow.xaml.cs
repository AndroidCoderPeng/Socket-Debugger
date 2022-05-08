using System.Windows;
using System.Windows.Controls;
using Socket_Debugger.Model;
using Socket_Debugger.Utils;

namespace Socket_Debugger.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _selectedType = "TCP客户端";
        private ConnectionModel selectedModel;

        public MainWindow()
        {
            InitializeComponent();
            // 初始化数据库
            SqLiteHelper.GetInstance().InitDataBase();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = (ListView) sender;
            ListViewItem selectedItemView = (ListViewItem) listView.SelectedItem;
            StackPanel stackPanel = (StackPanel) selectedItemView.Content;
            TextBlock textBlock = (TextBlock) stackPanel.Children[1];
            _selectedType = textBlock.Text.Contains("\r") ? textBlock.Text.Replace("\r", "") : textBlock.Text;
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddConnectionWindow connectionWindow = new AddConnectionWindow(_selectedType);
            connectionWindow.ShowDialog();
        }

        private void DelButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (selectedModel != null)
            {
                SqLiteHelper.GetInstance().DeleteConnectionModel(selectedModel);
            }
            else
            {
                MessageBoxHelper.ShowError("删除失败！");
            }
        }
    }
}
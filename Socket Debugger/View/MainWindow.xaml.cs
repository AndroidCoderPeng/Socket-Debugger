using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using Socket_Debugger.Model;
using Socket_Debugger.Utils;

namespace Socket_Debugger.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SqLiteHelper _sqLiteHelper;
        private string _selectedType = "TCP客户端";
        private List<ConnectionModel> _connectionModels;
        private ConnectionModel _selectedModel;

        public MainWindow()
        {
            InitializeComponent();
            // 初始化数据库
            _sqLiteHelper = SqLiteHelper.GetInstance();
            _sqLiteHelper.InitDataBase();
            _connectionModels = _sqLiteHelper.QueryConnectionModelsByType(_selectedType);
            if (_connectionModels == null)
            {
                CenterListView.Visibility = Visibility.Collapsed;
                EmptyPanel.Visibility = Visibility.Visible;
            }
            else
            {
                CenterListView.Visibility = Visibility.Visible;
                EmptyPanel.Visibility = Visibility.Collapsed;
            }

            //绑定数据
            CenterListView.ItemsSource = _connectionModels;
        }

        private void LeftListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = (ListView) sender;
            ListViewItem selectedItemView = (ListViewItem) listView.SelectedItem;
            StackPanel stackPanel = (StackPanel) selectedItemView.Content;
            TextBlock textBlock = (TextBlock) stackPanel.Children[1];
            _selectedType = textBlock.Text.Contains("\r") ? textBlock.Text.Replace("\r", "") : textBlock.Text;
        }

        private void CenterListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(CenterListView.SelectedItem is ConnectionModel model)) return;
            _selectedModel = model;
            Trace.WriteLine(JsonConvert.SerializeObject(model));
            // 绑定右边面板
            
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddConnectionWindow connectionWindow = new AddConnectionWindow(_selectedType);
            connectionWindow.ShowDialog();
        }

        private void DelButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_selectedModel != null)
            {
                _sqLiteHelper.DeleteConnectionModel(_selectedModel);
            }
            else
            {
                MessageBoxHelper.ShowError("删除失败！");
            }
        }
    }
}
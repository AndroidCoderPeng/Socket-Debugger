using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private readonly SqLiteHelper _sqLiteHelper;
        private string _selectedType = "TCP客户端";
        private ObservableCollection<ConnectionModel> _connectionModels;
        private ConnectionModel _selectedModel;

        public MainWindow()
        {
            InitializeComponent();
            // 初始化左边功能列表
            List<LeftFunctionModel> leftFunctionModels = new List<LeftFunctionModel>
            {
                new LeftFunctionModel {FunctionIcon = "CableData", FunctionName = "TCP客户端"},
                new LeftFunctionModel {FunctionIcon = "CableData", FunctionName = "TCP服务端"},
                new LeftFunctionModel {FunctionIcon = "AudioInputRca", FunctionName = "UDP客户端"},
                new LeftFunctionModel {FunctionIcon = "AudioInputRca", FunctionName = "UDP服务端"},
                new LeftFunctionModel {FunctionIcon = "LanConnect", FunctionName = "WebSocket\r客户端"},
                new LeftFunctionModel {FunctionIcon = "LanConnect", FunctionName = "WebSocket\r服务端"}
            };
            //绑定数据
            LeftListView.ItemsSource = leftFunctionModels;

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
            if (!(LeftListView.SelectedItem is LeftFunctionModel model)) return;
            _selectedType = model.FunctionName.Contains("\r")
                ? model.FunctionName.Replace("\r", "")
                : model.FunctionName;
        }

        private void CenterListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(CenterListView.SelectedItem is ConnectionModel model)) return;
            _selectedModel = model;
            // 绑定右边面板
            ServerTextBlock.Text = _selectedModel.Comment;
            HostTextBlock.Text = _selectedModel.ConnHost + ":" + _selectedModel.ConnPort;
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
                _connectionModels.Remove(_selectedModel);
                // 删除数据之后重新绑定数据源
                CenterListView.ItemsSource = _connectionModels;
            }
            else
            {
                MessageBoxHelper.ShowError("删除失败！");
            }
        }
    }
}
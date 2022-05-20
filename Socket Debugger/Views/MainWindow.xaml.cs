using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Socket_Debugger.Model;
using Socket_Debugger.Utils;

namespace Socket_Debugger.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _selectedType = "TCP客户端";
        private ObservableCollection<ConnectionModel> _connectionModels;
        private ConnectionModel _selectedModel;

        public MainWindow()
        {
            InitializeComponent();
            // 初始化数据库
            SqLiteHelper.GetInstance().InitDataBase();
            _connectionModels = SqLiteHelper.GetInstance().QueryConnectionModelsByType(_selectedType);
            InitView();

            //绑定数据
            CenterListView.ItemsSource = _connectionModels;
        }

        private void InitView()
        {
            if (_connectionModels.Count == 0)
            {
                CenterListView.Visibility = Visibility.Collapsed;
                EmptyPanel.Visibility = Visibility.Visible;
                RightGridView.Visibility = Visibility.Collapsed;
                RightEmptyPanel.Visibility = Visibility.Visible;
            }
            else
            {
                CenterListView.Visibility = Visibility.Visible;
                EmptyPanel.Visibility = Visibility.Collapsed;
                RightGridView.Visibility = Visibility.Visible;
                RightEmptyPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("MainWindow_OnLoaded");
        }

        private void LeftListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(LeftListBox.SelectedItem is LeftFunctionModel model)) return;
            _selectedType = model.FunctionName;
            // _connectionModels =
            // SqLiteHelper.GetInstance().QueryConnectionModelsByType(_selectedType);
            // CenterListView.ItemsSource = _connectionModels;
            LeftListBox.SelectedItem = null;
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
            AddConnectionWindow connectionWindow = new AddConnectionWindow
            {
                SelectedType = _selectedType
            };
            connectionWindow.ShowDialog();
            if (!connectionWindow.IsSaved) return;
            _connectionModels = SqLiteHelper.GetInstance().QueryConnectionModelsByType(_selectedType);
            CenterListView.ItemsSource = _connectionModels;
            InitView();
        }

        private void DelButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_connectionModels.Count == 0)
            {
                MessageBoxHelper.ShowError("无数据，无法删除！");
                return;
            }

            if (_selectedModel != null)
            {
                SqLiteHelper.GetInstance().DeleteConnectionModel(_selectedModel);
                _connectionModels.Remove(_selectedModel);
                // 删除数据之后重新绑定数据源
                CenterListView.ItemsSource = _connectionModels;
                InitView();
            }
            else
            {
                MessageBoxHelper.ShowError("删除失败！");
            }
        }
    }
}
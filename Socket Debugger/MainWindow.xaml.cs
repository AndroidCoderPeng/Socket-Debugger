using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Socket_Debugger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly string[] _itemImages =
        {
            @"Resources/Image/Tcp.png",
            @"Resources/Image/Tcp.png",
            @"Resources/Image/Udp.png",
            @"Resources/Image/Udp.png",
            @"Resources/Image/WebSocket.png",
            @"Resources/Image/WebSocket.png"
        };

        private readonly string[] _itemNames = {"TCP客户端", "TCP服务端", "UDP客户端", "UDP服务端", "WS客户端", "WS服务端"};

        private ObservableCollection<FunctionItem> FunctionItems { get; } = new ObservableCollection<FunctionItem>();

        public MainWindow()
        {
            InitializeComponent();

            //设置左边ListView数据
            for (int i = 0; i < 6; i++)
            {
                FunctionItems.Add(new FunctionItem
                {
                    ItemImage = new BitmapImage(new Uri(_itemImages[i], UriKind.Relative)),
                    ItemTitle = _itemNames[i]
                });
            }

            FuncListView.ItemsSource = FunctionItems;
            FuncListView.SelectedIndex = 0;
            //设置中间Panel数据
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // 添加配置
            FunctionItem selectedItem = (FunctionItem) FuncListView.SelectedItem;
            AddConfigDialog addConfig = new AddConfigDialog(selectedItem.ItemTitle);
            addConfig.sendMessage = RecevieResult;
            addConfig.ShowDialog();
            // Accept the dialog and return the dialog result
            // this.DialogResult = true;
        }

        public void RecevieResult(string value)
        {
            Debug.WriteLine(value);
        }
        
        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
        }

        public class FunctionItem
        {
            public BitmapImage ItemImage { get; set; }

            public string ItemTitle { get; set; }
        }
    }
}
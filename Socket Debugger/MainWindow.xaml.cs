using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            @"Resources/Tcp.png",
            @"Resources/Tcp.png",
            @"Resources/Udp.png",
            @"Resources/Udp.png",
            @"Resources/WebSocket.png",
            @"Resources/WebSocket.png"
        };

        private readonly string[] _itemNames = {"TCP客户端", "TCP服务端", "UDP客户端", "UDP服务端", "WST客户端", "WST服务端"};

        public MainWindow()
        {
            InitializeComponent();

            //设置左边ListView数据
            var functionItems = new ObservableCollection<FunctionItem>();
            for (int i = 0; i < 6; i++)
            {
                functionItems.Add(new FunctionItem
                {
                    ItemImage = new BitmapImage(new Uri(_itemImages[i], UriKind.Relative)),
                    ItemTitle = _itemNames[i]
                });
            }

            FuncListView.ItemsSource = functionItems;
        }

        public class FunctionItem
        {
            public BitmapImage ItemImage { get; set; }

            public string ItemTitle { get; set; }
        }
    }
}
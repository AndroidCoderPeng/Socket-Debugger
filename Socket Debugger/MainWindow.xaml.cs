using System;
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

        private readonly string[] _itemNames = {"TCP客户端", "TCP服务端", "UDP客户端", "UDP服务端", "WebSocket客户端", "WebSocket服务端"};

        private readonly ObservableCollection<FunctionItem> _functionItems = new ObservableCollection<FunctionItem>();

        public MainWindow()
        {
            InitializeComponent();

            //设置左边ListView数据
            _functionItems.Add(new FunctionItem(new BitmapImage(new Uri(_itemImages[0], UriKind.Relative)),
                _itemNames[0]));
            _functionItems.Add(new FunctionItem(new BitmapImage(new Uri(_itemImages[1], UriKind.Relative)),
                _itemNames[1]));
            _functionItems.Add(new FunctionItem(new BitmapImage(new Uri(_itemImages[2], UriKind.Relative)),
                _itemNames[2]));
            _functionItems.Add(new FunctionItem(new BitmapImage(new Uri(_itemImages[3], UriKind.Relative)),
                _itemNames[3]));
            _functionItems.Add(new FunctionItem(new BitmapImage(new Uri(_itemImages[4], UriKind.Relative)),
                _itemNames[4]));
            _functionItems.Add(new FunctionItem(new BitmapImage(new Uri(_itemImages[5], UriKind.Relative)),
                _itemNames[5]));

            FuncListView.ItemsSource = _functionItems;
        }

        public class FunctionItem
        {
            private BitmapImage ItemImage { get; set; }

            private string ItemTitle { get; set; }

            public FunctionItem(BitmapImage itemImage, string itemTitle)
            {
                this.ItemImage = itemImage;
                this.ItemTitle = itemTitle;
            }
        }
    }
}
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

        public MainWindow()
        {
            InitializeComponent();

            //设置左边ListView数据
            for (int i = 0; i < _itemNames.Length; i++)
            {
                ItemInfoCollection.Add(new ItemInfo()
                {
                    ItemImage = new BitmapImage(new Uri(_itemImages[i], UriKind.Relative)),
                    ItemTitle = _itemNames[i]
                });
            }

            FirstListView.DataContext = ItemInfoCollection;
        }

        private ObservableCollection<ItemInfo> ItemInfoCollection { get; set; } = new ObservableCollection<ItemInfo>();

        public class ItemInfo
        {
            public BitmapImage ItemImage { get; set; }
            public string ItemTitle { get; set; }
        }
    }
}
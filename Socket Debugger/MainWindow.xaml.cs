using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Media;
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

        private readonly Color _topBackground = Color.FromRgb(244, 238, 238);

        public MainWindow()
        {
            InitializeComponent();

            //中间底部按钮
            AddButton.BorderBrush = new SolidColorBrush(Colors.White);
            DelButton.BorderBrush = new SolidColorBrush(Colors.White);

            //右边顶部Panel
            TopDockPanel.Background = new SolidColorBrush(_topBackground);

            //右边底部SelectPanel
            SelectPanel.Background = new SolidColorBrush(Color.FromRgb(236, 236, 236));

            //设置左边ListView背景和数据
            SetupLeftListView();
        }

        private void SetupLeftListView()
        {
            LinearGradientBrush brush = new LinearGradientBrush();
            brush.GradientStops.Add(new GradientStop
            {
                Offset = 0.20,
                Color = Color.FromRgb(232, 213, 209)
            });
            brush.GradientStops.Add(new GradientStop
            {
                Offset = 0.80,
                Color = Color.FromRgb(227, 208, 227)
            });
            FirstListView.Background = brush;
            //绑定数据
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
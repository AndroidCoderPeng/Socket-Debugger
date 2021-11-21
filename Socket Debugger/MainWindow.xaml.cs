using System.ComponentModel;
using System.Windows.Media;

namespace Socket_Debugger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly string[] _itemImages =
        {
            "pack://application:,,,/Resource/IMG_TCP.png", "pack://application:,,,/Resource/IMG_TCP.png",
            "pack://application:,,,/Resource/IMG_TCP.png", "pack://application:,,,/Resource/IMG_TCP.png",
            "pack://application:,,,/Resource/IMG_TCP.png", "pack://application:,,,/Resource/IMG_TCP.png"
        };

        private readonly string[] _itemNames = {"TCP客户端", "TCP服务端", "UDP客户端", "UDP服务端", "WebSocket客户端", "WebSocket服务端"};

        private readonly Color _topBackground = Color.FromRgb(244, 238, 238);

        public MainWindow()
        {
            InitializeComponent();

            //设置左边ListView背景和数据
            SetupLeftListView();

            //中间底部按钮
            AddButton.BorderBrush = new SolidColorBrush(Colors.White);
            DelButton.BorderBrush = new SolidColorBrush(Colors.White);

            //右边顶部Panel
            TopDockPanel.Background = new SolidColorBrush(_topBackground);
            ServerLabel.Background = new SolidColorBrush(_topBackground);
            ServerLabel.BorderBrush = new SolidColorBrush(_topBackground);
            HostLabel.Background = new SolidColorBrush(_topBackground);
            HostLabel.BorderBrush = new SolidColorBrush(_topBackground);

            //右边底部SelectPanel
            SelectPanel.Background = new SolidColorBrush(Color.FromRgb(236, 236, 236));
        }

        private void SetupLeftListView()
        {
            var brush = new LinearGradientBrush();
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
            // var dataObj = new ObservableCollection<object>();
            // for (var i = 0; i < _itemNames.Length; i++)
            // {
            //     dataObj.Add(new
            //     {
            //         Title = _itemNames[i]
            //     });
            // }
            //
            // FirstListView.DataContext = dataObj;
        }
    }

    public class Item : INotifyPropertyChanged
    {
        public string ImageUrl
        {
            get { return ImageUrl; }
            set
            {
                ImageUrl = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ImageUrl"));
            }
        }

        public string ItemName
        {
            get { return ItemName; }
            set
            {
                ItemName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ItemName"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using Socket_Debugger.Model;
using Socket_Debugger.Utils;

namespace Socket_Debugger.View
{
    /// <summary>
    /// AddConnectionxaml 的交互逻辑
    /// </summary>
    public partial class AddConnectionWindow : Window
    {
        public string SelectedType { set; get; }

        // 是否保存成功
        public bool IsSaved { private set; get; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SelectedTypeTextBox.Text = SelectedType;
            if (SelectedType.Contains("WebSocket"))
            {
                PortTextBox.Text = "/";
                PortTextBox.IsEnabled = false;
            }
            else
            {
                IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
                foreach (IPAddress address in addressList)
                {
                    if (address.AddressFamily.ToString() == "InterNetwork")
                    {
                        ConnectionAddressTextBox.Text = address.ToString();
                    }
                }

                PortTextBox.Text = "8080";
                PortTextBox.IsEnabled = true;
            }
        }

        public AddConnectionWindow()
        {
            InitializeComponent();
        }

        private void RepeatCheckBox_OnClick(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            MessageTextBox.IsEnabled = checkBox.IsChecked == true;
            TimeTextBox.IsEnabled = checkBox.IsChecked == true;
        }

        // 保存连接配置事件
        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            string remark = RemarkTextBox.Text;
            if (remark.Equals(""))
            {
                MessageBoxHelper.ShowError("操作失败！请输入备注信息");
                return;
            }

            string connectionAddress = ConnectionAddressTextBox.Text;
            if (connectionAddress.Equals(""))
            {
                MessageBoxHelper.ShowError("操作失败！请输入连接地址");
                return;
            }

            string port = PortTextBox.Text;
            if (port.Equals(""))
            {
                MessageBoxHelper.ShowError("操作失败！请输入端口信息");
                return;
            }

            string repeatMessage = "";
            // 用int32最大值间接代替不重复发送数据
            int time = int.MaxValue;
            if (RepeatCheckBox.IsChecked == true)
            {
                string messageStr = MessageTextBox.Text;
                if (messageStr.Equals(""))
                {
                    MessageBoxHelper.ShowError("操作失败！请输入需要重复发送的数据内容");
                    return;
                }

                repeatMessage = messageStr;

                string timeStr = TimeTextBox.Text;
                if (timeStr.Equals(""))
                {
                    MessageBoxHelper.ShowError("操作失败！请输入时间间隔");
                    return;
                }

                time = int.Parse(TimeTextBox.Text);
            }

            ConnectionModel model = new ConnectionModel
            {
                Uuid = IdHelper.Generate(),
                Comment = remark,
                ConnType = SelectedTypeTextBox.Text,
                ConnHost = connectionAddress,
                ConnPort = port,
                MsgType = MsgTypeComboBox.Text,
                Message = repeatMessage,
                TimePeriod = time
            };
            SqLiteHelper.GetInstance().AddConfig(model);
            Close();
        }

        // 关闭窗口
        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            IsSaved = true;
        }
    }
}
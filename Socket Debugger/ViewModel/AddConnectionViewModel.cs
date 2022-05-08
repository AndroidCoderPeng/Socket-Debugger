using System.Net;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Socket_Debugger.Model;
using Socket_Debugger.Utils;
using Socket_Debugger.View;

namespace Socket_Debugger.ViewModel
{
    internal class AddConnectionViewModel : ObservableObject
    {
        // 保存连接配置事件
        public RelayCommand ConfirmDialogCommand { set; private get; }

        // 关闭窗口事件
        public RelayCommand DismissDialogCommand { set; private get; }

        // 连接类型
        private string _selectedType;

        // 连接地址
        private string _connectionAddress;

        public string SelectedType
        {
            get => _selectedType;
            private set => SetProperty(ref _selectedType, value);
        }

        public string ConnectionAddress
        {
            get
            {
                // 获取本机IP地址
                if (_selectedType.Contains("WebSocket")) return _connectionAddress;
                IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
                foreach (IPAddress address in addressList)
                {
                    if (address.AddressFamily.ToString() == "InterNetwork")
                    {
                        _connectionAddress = address.ToString();
                    }
                }

                return _connectionAddress;
            }
            private set => SetProperty(ref _connectionAddress, value);
        }

        public AddConnectionViewModel(AddConnectionWindow window, string selectedType)
        {
            _selectedType = selectedType;
            if (_selectedType.Contains("WebSocket"))
            {
                window.PortTextBox.Text = "/";
                window.PortTextBox.IsEnabled = false;
            }
            else
            {
                window.PortTextBox.Text = "8080";
                window.PortTextBox.IsEnabled = true;
            }

            // 保存连接数据
            ConfirmDialogCommand = new RelayCommand(() =>
            {
                string remark = window.RemarkTextBox.Text;
                if (remark.Equals(""))
                {
                    MessageBoxHelper.ShowError("操作失败！请输入备注信息");
                    return;
                }

                if (_connectionAddress == null)
                {
                    MessageBoxHelper.ShowError("操作失败！请输入连接地址");
                    return;
                }

                string port = window.PortTextBox.Text;
                if (port.Equals(""))
                {
                    MessageBoxHelper.ShowError("操作失败！请输入端口信息");
                    return;
                }

                string repeatMessage = "";
                // 用int32最大值间接代替不重复发送数据
                int time = int.MaxValue;
                if (window.RepeatCheckBox.IsChecked == true)
                {
                    string messageStr = window.MessageTextBox.Text;
                    if (messageStr.Equals(""))
                    {
                        MessageBoxHelper.ShowError("操作失败！请输入需要重复发送的数据内容");
                        return;
                    }

                    repeatMessage = messageStr;

                    string timeStr = window.TimeTextBox.Text;
                    if (timeStr.Equals(""))
                    {
                        MessageBoxHelper.ShowError("操作失败！请输入时间间隔");
                        return;
                    }

                    time = int.Parse(window.TimeTextBox.Text);
                }

                ConnectionModel model = new ConnectionModel
                {
                    Uuid = IdHelper.Generate(),
                    Comment = remark,
                    ConnType = _selectedType,
                    ConnHost = _connectionAddress,
                    ConnPort = port,
                    MsgType = window.MsgTypeComboBox.Text,
                    Message = repeatMessage,
                    TimePeriod = time
                };
                SqLiteHelper.GetInstance().AddConfig(model);
                window.Close();
            });

            // 关闭窗口
            DismissDialogCommand = new RelayCommand(window.Close);
        }
    }
}
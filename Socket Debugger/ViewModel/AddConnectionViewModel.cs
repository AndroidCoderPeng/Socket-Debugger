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
            window.PortTextBox.IsEnabled = !_selectedType.Contains("WebSocket");

            // 保存连接数据
            ConfirmDialogCommand = new RelayCommand(() =>
            {
                string remark = window.RemarkTextBox.Text;
                if (remark.Equals(""))
                {
                    return;
                }
                string port = window.PortTextBox.Text;
                if (port.Equals(""))
                {
                    return;
                }

                int time = 0;
                if (!window.TimeTextBox.Text.Equals(""))
                {
                    time = int.Parse(window.TimeTextBox.Text);
                }

                ConnectionModel model = new ConnectionModel();
                model.Uuid = IdHelper.Generat();
                model.Comment = remark;
                model.ConnType = _selectedType;
                model.ConnHost = _connectionAddress;
                model.ConnPort = port;
                model.MsgType = window.MsgTypeComboBox.Text;
                model.Message = window.MessageTextBox.Text;
                model.TimePeriod = time;
                SqLiteHelper.GetInstance().AddConfig(model);
                window.Close();
            });

            // 关闭窗口
            DismissDialogCommand = new RelayCommand(window.Close);
        }
    }
}
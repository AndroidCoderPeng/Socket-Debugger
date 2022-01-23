using System.Windows;
using Socket_Debugger.Utils;

namespace Socket_Debugger.View
{
    /// <summary>
    /// AddConfigDialog.xaml 的交互逻辑
    /// </summary>
    public partial class AddConfigDialog : Window
    {
        public AddConfigDialog(string itemTitle)
        {
            InitializeComponent();
            ConnTypeTextBlock.Text = itemTitle;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            string comment = CommentTextBox.Text.Trim();
            if ("".Equals(comment))
            {
                MessageBox.Show("备注不能为空");
                return;
            }

            string connHost = ConnHostTextBox.Text.Trim();
            if ("".Equals(connHost))
            {
                MessageBox.Show("主机地址不能为空");
                return;
            }

            string connPort = ConnPortTextBox.Text.Trim();
            if ("".Equals(connPort))
            {
                MessageBox.Show("端口不能为空");
                return;
            }

            string message = "";
            if (ReSendCheckBox.IsChecked.Value)
            {
                string msg = MessageTextBox.Text.Trim();
                if ("".Equals(msg))
                {
                    MessageBox.Show("发送内容不能为空");
                    return;
                }

                message = msg;
            }

            //存入数据库
            SqLiteHelper.GetInstance()
                .AddConfig(IdHelper.Generat(), ConnTypeTextBlock.Text, comment, connHost, connPort, message);
            Close();
        }
    }
}
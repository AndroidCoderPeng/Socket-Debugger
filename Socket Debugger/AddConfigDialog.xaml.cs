using System.Windows;

namespace Socket_Debugger
{
    /// <summary>
    /// AddConfigDialog.xaml 的交互逻辑
    /// </summary>
    public partial class AddConfigDialog : Window
    {
        public AddConfigDialog(string itemTitle)
        {
            InitializeComponent();
            connTypeTextBlock.Text = itemTitle;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            string comment = commentTextBox.Text.Trim();
            if (!"".Equals(comment)) return;
            MessageBox.Show("备注不能为空");
            return;
        }
    }
}
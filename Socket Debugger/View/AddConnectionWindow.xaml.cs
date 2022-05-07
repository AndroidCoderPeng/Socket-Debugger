using System.Windows;
using System.Windows.Controls;
using Socket_Debugger.ViewModel;

namespace Socket_Debugger.View
{
    /// <summary>
    /// AddConnectionWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddConnectionWindow : Window
    {
        public AddConnectionWindow(string selectedType)
        {
            InitializeComponent();
            this.DataContext = new AddConnectionViewModel(this, selectedType);
        }

        private void OnChecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox) sender;
            if (checkBox.IsChecked == true)
            {
                TimeTextBox.IsEnabled = true;
            }
        }

        private void OnUnchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox) sender;
            if (checkBox.IsChecked == false)
            {
                TimeTextBox.IsEnabled = false;
            }
        }
    }
}
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Socket_Debugger.View;

namespace Socket_Debugger.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public RelayCommand AddConfigCommand { set; private get; }
        private ListViewItem _selectedItemView;
        private string _selectedType = "TCP客户端";

        public ListViewItem SelectedItemView
        {
            get => _selectedItemView;
            set
            {
                _selectedItemView = value;
                StackPanel stackPanel = (StackPanel) _selectedItemView.Content;
                TextBlock textBlock = (TextBlock) stackPanel.Children[1];
                _selectedType = textBlock.Text.Contains("\r") ? textBlock.Text.Replace("\r", "") : textBlock.Text;
            }
        }

        public MainViewModel()
        {
            AddConfigCommand = new RelayCommand(() =>
            {
                AddConnectionWindow connectionWindow = new AddConnectionWindow(_selectedType);
                connectionWindow.ShowDialog();
            });
        }
    }
}
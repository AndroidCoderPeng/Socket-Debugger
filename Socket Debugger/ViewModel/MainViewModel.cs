using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Socket_Debugger.View;

namespace Socket_Debugger.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public RelayCommand AddConfigCommand { set; private get; }

        public MainViewModel()
        {
            AddConfigCommand = new RelayCommand(() =>
            {
                AddConnectionWindow connectionWindow = new AddConnectionWindow();
                connectionWindow.ShowDialog();
            });
        }
    }
}
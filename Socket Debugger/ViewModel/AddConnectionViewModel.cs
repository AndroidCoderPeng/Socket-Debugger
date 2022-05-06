using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Socket_Debugger.ViewModel
{
    internal class AddConnectionViewModel : ObservableObject
    {
        public RelayCommand DismissDialogCommand { set; private get; }

        public AddConnectionViewModel(Window window)
        {
            DismissDialogCommand = new RelayCommand(window.Close);
        }
    }
}
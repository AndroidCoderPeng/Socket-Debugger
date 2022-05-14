using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Socket_Debugger.View;

namespace Socket_Debugger.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public ICommand LeftListViewItemCommand { get; }
        public ICommand AddButtonCommand { get; }
        public ICommand DeleteButtonCommand { get; }

        public MainViewModel()
        {
            // 左边ListView点击事件
            LeftListViewItemCommand = new RelayCommand(() => { });

            // 添加连接按钮事件
            AddButtonCommand = new RelayCommand(() => { });

            // 删除连接按钮事件
            DeleteButtonCommand = new RelayCommand(() => { });
        }
    }
}
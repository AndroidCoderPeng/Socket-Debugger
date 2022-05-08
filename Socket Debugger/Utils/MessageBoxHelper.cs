using System.Reflection;
using System.Windows;

namespace Socket_Debugger.Utils
{
    public static class MessageBoxHelper
    {
        public static void ShowError(string messageStr)
        {
            // 应用名
            string applicationName = Assembly.GetExecutingAssembly().GetName().Name;
            MessageBox.Show(messageStr, applicationName, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
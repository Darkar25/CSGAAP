using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CSGAAP.GUI
{
    /// <summary>
    /// Логика взаимодействия для AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e) => Close();

        private void URL_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock tb)
            {
                try
                {
                    Process.Start(tb.Text);
                }
                catch
                {
                    // hack because of this: https://github.com/dotnet/corefx/issues/10361
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        var url = tb.Text.Replace("&", "^&");
                        Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                    }
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        Process.Start("xdg-open", tb.Text);
                    }
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    {
                        Process.Start("open", tb.Text);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }
    }
}

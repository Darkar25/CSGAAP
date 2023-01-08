using CSGAAP.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для ProcessingProgressWindow.xaml
    /// </summary>
    public partial class ProcessingProgressWindow : Window
    {
        private readonly API Instance;

        public ProcessingProgressWindow(API inst)
        {
            Instance = inst;
            InitializeComponent();
        }

        public async void Run()
        {
            DialogResult = await Instance.Execute();
            Close();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Instance.Cancellation?.Cancel();
            cancel.Content = "Cancelling...";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = Instance.Cancellation is not null;
            Instance.Cancellation?.Cancel();
            cancel.Content = "Cancelling...";
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            Run();
        }
    }
}

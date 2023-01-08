using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace CSGAAP.GUI
{
    /// <summary>
    /// Логика взаимодействия для ResultsWindow.xaml
    /// </summary>
    public partial class ResultsWindow : Window
    {
        public Dictionary<DateTime, string> Results { get; }

        public ResultsWindow(Dictionary<DateTime, string> results)
        {
            Results = results;
            InitializeComponent();
            tabs.SelectedIndex = Results.Count - 1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}

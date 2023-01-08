using CSGAAP.Util;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// Логика взаимодействия для AuthorEditWindow.xaml
    /// </summary>
    public partial class AuthorEditWindow : Window
    {
        public ObservableCollection<Document> Documents { get; }
        public string Author { get; set; }

        public AuthorEditWindow(string author, IEnumerable<Document> documents)
        {
            Author = author;
            Documents = new(documents);
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Author)) MessageBox.Show("You need to specify an Author.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (Documents.Count == 0) MessageBox.Show("You need to include documents.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                DialogResult = true;
                Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new();
            if (ofd.ShowDialog() == true)
            {
                Document d = new(ofd.FileName, string.Empty);
                Documents.Add(d);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var d = (Document)unknowndocuments.SelectedItem;
            Documents.Remove(d);
        }
    }
}

using CSGAAP.Backend;
using CSGAAP.Generics;
using CSGAAP.Util;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks.Sources;
using System.Windows;
using System.Windows.Data;

namespace CSGAAP.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public API Instance => API.Instance;

        public string[] notes = new string[] { "", "", "", "", "" };
        public Dictionary<DateTime, string> results = new();

        public CollectionViewSource UnknownDocumentsList { get; } = new() { Source = API.Instance.Documents };

        public MainWindow()
        {
            UnknownDocumentsList.Filter += (s, e) => { e.Accepted = !((Document)e.Item).IsKnownAuthor; };
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tabs.SelectedIndex = tabs.Items.Count - 1;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (tabs.SelectedIndex < tabs.Items.Count - 1) tabs.SelectedIndex++;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var a = (Canonicizer)((Parameterizable)canonicizerlist.SelectedItem).NewInstanceWithParams();
            Instance.Canonicizers.Add(a);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Instance.Canonicizers.Remove((Canonicizer)selectedcanonicizers.SelectedItem);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Instance.Canonicizers.Clear();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var a = (EventDriver)((Parameterizable)eventdriverlist.SelectedItem).NewInstanceWithParams();
            Instance.EventDrivers.Add(a);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Instance.EventDrivers.Remove((EventDriver)selectedeventdrivers.SelectedItem);
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            foreach (var a in CSGAAP.Backend.EventDrivers.List)
                Instance.EventDrivers.Add((EventDriver)a.NewInstanceWithParams());
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            Instance.EventDrivers.Clear();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            var a = (EventCuller)((Parameterizable)eventcullerlist.SelectedItem).NewInstanceWithParams();
            Instance.EventCullers.Add(a);
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            Instance.EventCullers.Remove((EventCuller)selectedeventcullers.SelectedItem);
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            foreach (var a in CSGAAP.Backend.EventCullers.List)
                Instance.EventCullers.Add((EventCuller)a.NewInstanceWithParams());
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            Instance.EventCullers.Clear();
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            var a = (AnalysisDriver)((Parameterizable)analysisdriverlist.SelectedItem).NewInstanceWithParams();
            a.Distance = (DistanceFunction)distancefunctionlist.SelectedItem;
            if(!a.SupportsDistance ^ a.Distance is not null) Instance.AnalysisDrivers.Add(a);
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            Instance.AnalysisDrivers.Remove((AnalysisDriver)selectedanalysisdrivers.SelectedItem);
        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            if (distancefunctionlist.SelectedItem is null) return;
            foreach (var a in CSGAAP.Backend.AnalysisDrivers.List)
            {
                var ad = (AnalysisDriver)a.NewInstanceWithParams();
                if (ad.SupportsDistance)
                    ad.Distance = (DistanceFunction)distancefunctionlist.SelectedItem;
                Instance.AnalysisDrivers.Add(ad);
            }
        }

        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
            Instance.AnalysisDrivers.Clear();
        }

        private void Button_Click_17(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new();
            if (ofd.ShowDialog() == true) Instance.Documents.Add(new(ofd.FileName, string.Empty));
        }

        private void Button_Click_18(object sender, RoutedEventArgs e)
        {
            Instance.Documents.Remove((Document)unknowndocuments.SelectedItem);
        }

        private void Button_Click_19(object sender, RoutedEventArgs e)
        {
            if (authors.SelectedItem is IGrouping<string, Document> entry) foreach (var doc in entry) Instance.Documents.Remove(doc);
            else if (authors.SelectedItem is Document doc) foreach (var d in Instance.DocumentsByAuthor[doc.Author!]) Instance.Documents.Remove(d);
        }

        private void CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_20(object sender, RoutedEventArgs e)
        {
            AuthorEditWindow aed = new("", Array.Empty<Document>());
            if(aed.ShowDialog() == true)
            {
                foreach (var doc in aed.Documents)
                {
                    doc.Author = aed.Author;
                    Instance.Documents.Add(doc);
                }
            }
        }

        private void Button_Click_21(object sender, RoutedEventArgs e)
        {
            IGrouping<string, Document> a;
            if (authors.SelectedItem is IGrouping<string, Document> entry) a = entry;
            else if (authors.SelectedItem is Document doc) a = Instance.DocumentsByAuthor[doc.Author!].GroupBy(x => x.Author!).First();
            else return;
            AuthorEditWindow aed = new(a.Key, a);
            if (aed.ShowDialog() == true)
            {
                foreach(var doc in Instance.DocumentsByAuthor[a.Key])
                    Instance.Documents.Remove(doc);
                foreach (var doc in aed.Documents)
                {
                    doc.Author = aed.Author;
                    Instance.Documents.Add(doc);
                }
            }
        }

        private void Button_Click_22(object sender, RoutedEventArgs e)
        {
            ProcessingProgressWindow ppd = new(Instance);
            ppd.Owner = this;
            if (ppd.ShowDialog() == true)
            {
                results[DateTime.Now] = Instance.FormattedResult();
                ResultsWindow rd = new(results);
                if (rd.ShowDialog() == true) results.Clear();
            }
        }

        private void CommandBinding_Executed_1(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            SaveFileDialog sfd = new();
            if (sfd.ShowDialog() == true) File.WriteAllLines(sfd.FileName, Instance.Documents.Select(x => $"{x.Author};{x.URI};{x.Title}"));
        }

        private void CommandBinding_Executed_2(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            OpenFileDialog ofd = new();
            if (ofd.ShowDialog() == true)
                foreach(var entry in File.ReadAllLines(ofd.FileName).Select(x => x.Split(',')).Where(x => x.Length >= 3))
                    Instance.Documents.Add(new(entry[1], entry[0], entry[2]));
        }

        private void Button_Click_23(object sender, RoutedEventArgs e)
        {
            NotesWindow nw = new(notes[tabs.SelectedIndex]);
            if (nw.ShowDialog() == true) notes[tabs.SelectedIndex] = nw.Text;
        }

        private void CommandBinding_Executed_3(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            new AboutWindow().ShowDialog();
        }
    }
}

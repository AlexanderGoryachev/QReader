using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Storage;
using System.IO.IsolatedStorage;
using System.Collections.ObjectModel;

namespace QReader
{
    public partial class MainPage : PhoneApplicationPage
    {
        private IsolatedStorageSettings localSettings;
        private ObservableCollection<History> settingsCollection = new ObservableCollection<History>();
        public class History
        {
            public string Key {get; set;}
            public string Value {get; set;}
        }

        public MainPage()
        {
            InitializeComponent();
            if (localSettings != null) settingsCollection = (ObservableCollection<History>)localSettings.Values;
            HistoryListBox.ItemsSource = settingsCollection;
        }

        private void QRScaner_ScanComplete(object sender, JeffWilcox.Controls.ScanCompleteEventArgs e)
        {
            MessageBox.Show(e.Result);
            var counter = HistoryListBox.Items.Count;
            settingsCollection.Add(new History { Key = counter.ToString(), Value = e.Result });
            QRScaner.StartScanning();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            settingsCollection.Add(new History { Key = "1", Value = "qwerty" });
        }

        private void HistoryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HistoryListBox.SelectedIndex >= 0)
            {
                Clipboard.SetText(((History)HistoryListBox.SelectedItem).Value);
                MessageBox.Show("Text copied to clipboard");
            }
        }
    }
}
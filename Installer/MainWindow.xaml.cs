using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Net;
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
using System.Windows.Navigation;
using System.ComponentModel;

namespace Installer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Button_Click();
        }

        private void Button_Click()
        {
            WebClient webClient = new WebClient();

            UriBuilder uriBuilder = new UriBuilder("");
            Uri uri = uriBuilder.Uri;

            webClient.DownloadFileAsync(uri, Path.GetTempPath() + "\\test.exe");
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
        }

        void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        void Completed(object sender, AsyncCompletedEventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = Path.GetTempPath() + "\\test.exe";
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
//            p.StartInfo.UseShellExecute = true;
//            p.StartInfo.Verb = "runas";
            p.Start();
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to cancel installation?", "Confirm", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.OK)
            {
                this.Close();
            }
            else
                return;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

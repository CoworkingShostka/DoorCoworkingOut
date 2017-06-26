using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Threading;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Text;
using Windows.Devices.SerialCommunication;
using Windows.Devices.Enumeration;
using Windows.Storage.Streams;
using Windows.Web.Http;

//using Models;
using ViewModels;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace DoorCoworkingOut
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage Current;

        MainViewModel connection = new MainViewModel();
        

        public MainPage()
        {
            this.InitializeComponent();

            // This is a static public property that allows downstream pages to get a handle to the MainPage instance
            // in order to call methods that are in this class.
            Current = this;

            MyFrame.Navigate(typeof(Connection));
            TitleText.Text = "Connection";
            BackButton.Visibility = Visibility.Collapsed;
            //StatusTextBlock.Text = "Starting";
            
            
        }

        

        //декодируем данные (обязательно)
        //public string decodeMqttMess()
        //{
        //    Encoding enc = Encoding.GetEncoding(1251);
        //    return enc.GetString(mqttMessage);
        //}

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //connection.MqttConnect();
            //connection.SerialConnection();

        }

        private void IconsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ConnectListBox.IsSelected)
            {
                MyFrame.Navigate(typeof(Connection));
                TitleText.Text = "Connection";
                BackButton.Visibility = Visibility.Collapsed;
            }
            else if (SecondListBoxItem.IsSelected)
            {
                MyFrame.Navigate(typeof(Page2));
                TitleText.Text = "Page 2";
                BackButton.Visibility = Visibility.Visible;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MyFrame.CanGoBack)
            {
                MyFrame.GoBack();
                ConnectListBox.IsSelected = true;
            }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            //CancelReadTask();
            
            //connection.CancelReadTask();

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            ;
        //    try
        //    {
        //        // string ver = new WebClient().DownloadString("https://drive.google.com/uc?export=download&id=0B1PRhPmv7AwwZFFDM2VzYXJ3cFU");
        //        new HttpClient().DownloadFile("https://drive.google.com/uc?export=download&id=0B1PRhPmv7AwweldpMTduQm9CVEk", Application.StartupPath + "\\update.bat");


        //        new WebClient().DownloadFile("https://drive.google.com/uc?export=download&id=0B1PRhPmv7AwwbFBqNjNBLTlKMjQ", Application.StartupPath + "\\zamokServ_new.exe");
        //        new WebClient().DownloadFile("https://drive.google.com/uc?export=download&id=0B1PRhPmv7AwwTG1hVkxOTHRWVE0", Application.StartupPath + "\\M2Mqtt.Net.dlll");

        //        ProcessStartInfo upd = new ProcessStartInfo("cmd.exe");

        //        Process.Start(Application.StartupPath + "\\update.bat");

        //        this.Close();


        //    }

        //    catch (Exception) { MessageBox.Show("В процесі оновлення сталась помилка\nзверніться до розробника", "Сталась помилка"); }
        }
    }
}

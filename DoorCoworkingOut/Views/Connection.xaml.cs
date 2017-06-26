using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using ViewModels;

//using Models;
// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace DoorCoworkingOut
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class Connection : Page
    {
        //MainPage mainPage = new MainPage();
        ConnectionViewModel CVM = new ConnectionViewModel();
       
        public Connection()
        {
            this.InitializeComponent();
            
            //CVM.MqttConnect();
            //connection.SerialConnection();
                
                     
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ;
            //CVM.MqttConnect();
            
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            CVM.MqttConnect();
            //connection.SerialConnection();
        }

    }
}

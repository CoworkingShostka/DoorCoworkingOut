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
using System.Net;
using ViewModels;

namespace Models
{
    public class ConnectionModel : NotificationBase
    {
        public MqttClient client;
        public CancellationTokenSource ReadCancellationTokenSource;
        private SerialDevice serialPort = null;
        DataWriter dataWriteObject = null;
        DataReader dataReaderObject = null;

        private string _mqttStatus;
        public string mqttStatus
        {
            get { return _mqttStatus; }
            set
            {
                SetProperty(ref _mqttStatus, value);
            }
        }

        public async void MqttConnect()
        {

            //client = new MqttClient("test.mosquitto.org");

            client = new MqttClient("192.168.1.2");
            client.Connect(Guid.NewGuid().ToString());

            if (client.IsConnected)
            {
                mqttStatus = "I`m Connected";
            }
            //string clientId = Guid.NewGuid().ToString();
            //client.Connect(clientId);
            //client.Publish("Test/Connection", Encoding.UTF8.GetBytes("I'm Connected"));
            // register to message received
            //client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            // subscribe to the topic "/home/temperature" with QoS 2
            //client.Subscribe(new string[] { "AS/DoorCoworcingOut/server_response" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

        }

        byte[] mqttMessage;
        //string mqttTopic;
        string mqttMessageConv;

        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            //mqttMessage = e.Message;
            //mqttTopic = e.Topic;
            //mqttMessageConv = decodeMqttMess();
            //StatusTextBlock.Text = Encoding.UTF8.GetString(mqttMessage);
            //sendToPort(Encoding.UTF8.GetString(mqttMessage));
        }


        public async void SerialConnection()
        {
            string qFilter = SerialDevice.GetDeviceSelector("COM3");
            DeviceInformationCollection devices = await DeviceInformation.FindAllAsync(qFilter);

            if (devices.Any())
            {
                string deviceId = devices.First().Id;

                await OpenPort(deviceId);
            }

            ReadCancellationTokenSource = new CancellationTokenSource();

            while (true)
            {
                await Listen();
            }
        }

        public void CancelReadTask()
        {
            if (ReadCancellationTokenSource != null)
            {
                if (!ReadCancellationTokenSource.IsCancellationRequested)
                {
                    ReadCancellationTokenSource.Cancel();
                }
            }

            if (serialPort != null)
            {
                serialPort.Dispose();
            }
            serialPort = null;
        }

        private async Task OpenPort(string deviceId)
        {
            serialPort = await SerialDevice.FromIdAsync(deviceId);

            if (serialPort != null)
            {
                serialPort.WriteTimeout = TimeSpan.FromMilliseconds(100);
                serialPort.ReadTimeout = TimeSpan.FromMilliseconds(100);
                serialPort.BaudRate = 9600;
                serialPort.Parity = SerialParity.None;
                serialPort.StopBits = SerialStopBitCount.One;
                serialPort.DataBits = 8;
                serialPort.Handshake = SerialHandshake.None;
                //Connection.txtStatus.Text = "Serial port configured successfully";
                //StatusTextBlock.Text = "Serial port configured successfully";
            }
        }

        private async Task Listen()
        {
            try
            {
                if (serialPort != null)
                {
                    dataReaderObject = new DataReader(serialPort.InputStream);
                    await ReadAsync(ReadCancellationTokenSource.Token);
                }
            }
            catch (Exception ex)
            {
                //txtStatus.Text = ex.Message;
            }
            finally
            {
                if (dataReaderObject != null)    // Cleanup once complete
                {
                    dataReaderObject.DetachStream();
                    dataReaderObject = null;
                }
            }
        }

        private async Task ReadAsync(CancellationToken cancellationToken)
        {
            Task<UInt32> loadAsyncTask;

            uint ReadBufferLength = 256;  // only when this buffer would be full next code would be executed

            dataReaderObject.InputStreamOptions = InputStreamOptions.Partial;

            loadAsyncTask = dataReaderObject.LoadAsync(ReadBufferLength).AsTask(cancellationToken);   // Create a task object

            UInt32 bytesRead = await loadAsyncTask;    // Launch the task and wait until buffer would be full

            if (bytesRead > 0)
            {
                string strFromPort = dataReaderObject.ReadString(bytesRead);
                int fstLetter = strFromPort.IndexOf("Info");
                int lstLetter = strFromPort.IndexOf("Info", fstLetter + 1);
                if ((fstLetter >= 0) && (lstLetter > 0)) strFromPort = strFromPort.Substring(fstLetter, lstLetter - fstLetter);

                this.client.Publish("AS/DoorCoworkingOut/cardID", Encoding.UTF8.GetBytes(strFromPort));
                //StatusTextBlock.Text = strFromPort;

                //txtPortData.Text = strFromPort;
                //txtStatus.Text = "Read at " + DateTime.Now.ToString(System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.LongTimePattern);
            }
        }

        private async Task WriteAsync(string text2write)
        {
            Task<UInt32> storeAsyncTask;

            if (text2write.Length != 0)
            {
                dataWriteObject.WriteString(text2write);

                storeAsyncTask = dataWriteObject.StoreAsync().AsTask();  // Create a task object

                UInt32 bytesWritten = await storeAsyncTask;   // Launch the task and wait
                if (bytesWritten > 0)
                {
                    //txtStatus.Text = bytesWritten + " bytes written at " + DateTime.Now.ToString(System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.LongTimePattern);
                }
            }
            else { }
        }


        private async Task sendToPort(string sometext)
        {
            try
            {
                if (serialPort != null)
                {
                    dataWriteObject = new DataWriter(serialPort.OutputStream);

                    await WriteAsync(sometext);
                }
                else { }
            }
            catch (Exception ex)
            {
                // txtStatus.Text = ex.Message;
            }
            finally
            {
                if (dataWriteObject != null)   // Cleanup once complete
                {
                    dataWriteObject.DetachStream();
                    dataWriteObject = null;
                }
            }
        }

    }
}

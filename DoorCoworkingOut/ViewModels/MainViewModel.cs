using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using Models;

namespace ViewModels
{
    public class MainViewModel : NotificationBase
    {

        //private ConnectionModel _connectionModel;
        //public ConnectionModel connectionModel
        //{
        //    get { return _connectionModel; }
        //    set
        //    {
        //        connectionModel.MqttConnect();
        //    }

        //}
        public ConnectionModel connectionModel = new ConnectionModel();

        public MainViewModel()
        {
            
            connectionModel.MqttConnect();
            //connectionModel.SerialConnection();
        }


    }
}

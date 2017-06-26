using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using Models;

namespace ViewModels
{
    class MainViewModel : NotificationBase
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

        public MainViewModel()
        {
            ConnectionModel connectionModel = new ConnectionModel();
            connectionModel.MqttConnect();
            //connectionModel.SerialConnection();
        }


    }
}

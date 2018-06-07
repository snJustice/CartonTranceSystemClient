using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigRead;
using Communications.DSocket;
using Communications.OPC;
using System.Threading.Tasks;
using System.Threading;

namespace CartonTranceSystemClient.CustomApplication
{
    public class BrainHeart
    {
        OPCClient opcClient;
        TCPCommunication tcpClient;
        ConfigString config;
        Task ScanTask;

        bool lastSignal;
        bool currentSignal;

        public BrainHeart()
        {
            lastSignal = currentSignal = false;
            config = ConfigString.GetInstance();
            config.GetALLNeedDataFromIni();
            opcClient = new OPCClient(config.OPC_Read_Address);
            tcpClient = new TCPCommunication(config.IP);
            ScanTask = new Task(ScanReadImageSignal);

        }

        public async void ConnectDevice()
        {
            await tcpClient.ConnectAsync();
            ScanTask.Start();
        }

        private void ScanReadImageSignal()
        {
            while (true)
            {
                currentSignal = opcClient.ReadFlag();
                Thread.Sleep(50);
                if (currentSignal == true && lastSignal == false)
                {
                    tcpClient.Send("1");
                }

                lastSignal = currentSignal;
            } 
        }


        public void Close()
        {
            if(tcpClient!=null&&tcpClient.IsConnected)
            {
                tcpClient.Close();
            }

            if (opcClient != null)
            {
                opcClient.Close();
            }
        }
    }
}

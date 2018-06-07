using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OPCAutomation;

using ConfigRead;



namespace Communications.OPC
{
    public class OPCClient
    {
        #region
        OPCServer opcServer;
        OPCGroups opcGroups;
        /// <summary>
        /// OPCGroup Object
        /// </summary>
        OPCGroup opcGroup;
        /// <summary>
        /// OPCItems Object
        /// </summary>
        OPCItems opcItems;
        /// <summary>
        /// OPCItem Object
        /// </summary>
        OPCItem[] opcItem;

        #endregion

        List<string> opc_address;
        private readonly string OPCServerString;

        ConfigString config;

        public OPCClient(List<string> _readAddress)
        {
            config = ConfigString.GetInstance();
            opc_address = _readAddress;

            opcItem = new OPCItem[opc_address.Count];
                      
            OPCServerString = config.OPC_Name;
          
            opcServer = new OPCServer();
         

            ConnectOPCServer();
            CreateReadGroup();
        }

        

        

        void ConnectOPCServer()
        {
            try
            {
                opcServer.Connect(OPCServerString);
                
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

      
        void CreateReadGroup()
        {
            opcGroups = opcServer.OPCGroups;
            opcGroup = opcGroups.Add("PLCReadAddress"); 
            SetGroupProperty();

            opcItems = opcGroup.OPCItems;
            int i = 0;
            foreach (string address in opc_address)
            {
               
                opcItem[i] = opcItems.AddItem(address, i);
                i++;
            }
            //opcGroup.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(opcGroup_DataChange);

        }

        private void opcGroup_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            Console.WriteLine(ReadFlag());
            

        }

       

        public bool ReadFlag()
        {
            object ItemValues = new object();
            object Qualities = new object();
            object TimeStamps = new object();//同步读的临时变量：值、质量、时间戳

            
            opcItem[0].Read(1, out ItemValues, out Qualities, out TimeStamps);//同步读，第一个参数只能为1或2
            return Convert.ToBoolean(ItemValues);
        }

        /// <summary>
        /// 设置组属性
        /// </summary>
        private void SetGroupProperty()
        {
            opcServer.OPCGroups.DefaultGroupIsActive = true;
            opcServer.OPCGroups.DefaultGroupDeadband = 0;
            opcGroup.UpdateRate = 50;
            Console.WriteLine(opcGroup.UpdateRate);
            opcGroup.IsActive = true;
            opcGroup.IsSubscribed = true;
        }

        public void Close()
        {
            if(opcServer!=null)
            {
                opcServer.Disconnect();
            }
                
        }


    }
}

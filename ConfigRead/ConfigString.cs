using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IniParser;
using IniParser.Model;

namespace ConfigRead
{
    public   class ConfigString
    {
        //public  string Remote_SQLServer_Connect_String;
        //public  string Root_Path;
        public  List<string> OPC_Read_Address;
        public  string OPC_Name;
        public string IP;
        

        private static readonly object locker = new object();
        private static ConfigString configString;

        private ConfigString()
        {
            OPC_Read_Address = new List<string>();
        }

        public static ConfigString GetInstance()
        {
            if(configString==null)
            {
                lock(locker)
                {
                    if (configString == null)
                    {
                        configString = new ConfigString();
                    }
                }
            }

            
            return configString;
        }

        private string ReadIniFileByDirectory(string _field1,string _field2)
        {
            IniParser.FileIniDataParser sss = new IniParser.FileIniDataParser();
            IniData data = sss.ReadFile("config.ini", ASCIIEncoding.Default);
            string needData = data[_field1][_field2];
            return needData;

            


        }

        private void GetRemoteSQLServerConnectString()
        {
            string IP = ReadIniFileByDirectory("MISDataBase","Server");
            string DBName = ReadIniFileByDirectory("MISDataBase", "DBName");
            string UserID = ReadIniFileByDirectory("MISDataBase","UserID");
            string PWD = ReadIniFileByDirectory("MISDataBase","PWD");

            /*Remote_SQLServer_Connect_String = @"Data Source=" + IP +
                ";Initial Catalog=" + DBName +
                ";User ID=" + UserID +
                ";Password=" + PWD +
                "; Integrated Security = false; ";*/
        }

        private void GetFilePathRoot()
        {
            //Root_Path = ReadIniFileByDirectory("FilePath","Root");
        }

        private void GetOPCName()
        {
            OPC_Name = ReadIniFileByDirectory("OPCName", "Name");
        }

        private void GetOPCReadAddress()
        {
            IniParser.FileIniDataParser sss = new IniParser.FileIniDataParser();
            IniData data = sss.ReadFile("config.ini", ASCIIEncoding.Default);
            foreach(var dd in data["OPCRead"])
            {
                OPC_Read_Address.Add(dd.Value);
            }
        }

        public void GetALLNeedDataFromIni()
        {
            //GetRemoteSQLServerConnectString();
            //GetFilePathRoot();
            GetOPCName();
            GetOPCReadAddress();
            GetIPAddress();

        }

        private void GetIPAddress()
        {
            IP = ReadIniFileByDirectory("TCP","IP");
        }
    }
}

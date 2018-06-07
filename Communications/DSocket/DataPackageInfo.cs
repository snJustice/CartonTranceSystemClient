﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.ProtoBase;

namespace Communications.DSocket
{
    public class DataPackageInfo : IPackageInfo
    {
        public string CodeData { get; set; }

        public DataPackageInfo(string _codeData)
        {
            CodeData = _codeData;
        }
        


    }
}

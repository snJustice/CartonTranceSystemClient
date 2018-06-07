using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Reflection;

namespace Loger
{
    public class LogManager
    {
        ILog log;//日志
        public LogManager()
        {
            log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        }

        public void WriteInfo(string _message)
        {
            log.Info(_message);
        }
    }
}

using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DianDianClient.Utils
{
    public class LogHelper
    {
        private static ILog log = null;
        public static ILog Log
        {
            get
            {
                if (log == null)
                {
                    //log4.config表示log4的配置文件
                    string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "log.config");
                    log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(fileName));
                    //Log4Name表示配置文件中的日志名称
                    log = LogManager.GetLogger("Log4Name");
                }
                return log;
            }
        }
    }
}

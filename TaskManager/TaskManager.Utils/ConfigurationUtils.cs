using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Utils
{
    public class ConfigurationUtils
    {
        public static string GetSetting(string key, string defValue = null)
        {
            try
            {
                return ConfigurationManager.AppSettings[key];
            }
            catch
            {
                return defValue;
            }
        }
    }
}

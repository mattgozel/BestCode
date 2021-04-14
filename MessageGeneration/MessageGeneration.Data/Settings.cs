using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGeneration.Data
{
    public class Settings
    {
        private static string _mode;
        public static string GetMode()
        {
            if (string.IsNullOrEmpty(_mode))
                _mode = ConfigurationManager.AppSettings["Mode"].ToString();

            return _mode;
        }
    }
}

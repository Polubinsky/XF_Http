using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UkrGo.Helpers;

namespace UkrGo.Model
{
    public class SettingsData
    {
        public bool RemoveDuplicate
        {
            get
            {
                return Settings.RemoveDublicate;
            }
            set
            {
                Settings.RemoveDublicate = value;
            }
        }
        public bool AskPinCode
        {
            get
            {
                return Settings.AskPinCode;
            }
            set
            {
                Settings.AskPinCode = value;
            }
        }
    }
}

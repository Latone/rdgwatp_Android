using System;
using System.Collections.Generic;
using System.Text;

namespace rdgwatp_Android
{
     static class FastForwardChangeXMLObjects
    {
        public delegate void UpdateLabel(string value);
        public static event UpdateLabel OnLabelUpdate;
        public static void Process(string str)
        {
            if (OnLabelUpdate != null)
                OnLabelUpdate(str);
        }
    }
}

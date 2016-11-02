using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckBridges.Core.Models
{
    public class HMCItem
    {
        public string Caption { get; private set; }
        public string ApiValue { get; private set; }

        public HMCItem(string caption)
        {
            Caption = caption;
            ApiValue = "";
        }
        public HMCItem(string caption, string apiValue)
        {
            Caption = caption;
            ApiValue = apiValue;
        }
    }
}

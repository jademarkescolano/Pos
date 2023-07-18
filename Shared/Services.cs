using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Shared
{
    public class xservices
    {
        public int serviceid { get; set; }
        public string service { get; set; }
        public double _fee { get; set; }
        public string fee
        {
            get { return _fee.ToString("0.00"); }
            set { _fee = double.Parse(value); }
        }
    }
}

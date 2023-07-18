using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Shared
{
    public class clientservices
    {

        public int clientserviceid { get; set; }
        public int clientid { get; set; }
        public string name { get; set; }
		public string receiptno { get; set; }
		public int empid { get; set; }
        public string employee { get; set; }
        public string service { get; set; }
        public double _fee { get; set; }
        public string? fee
        {
            get { return _fee.ToString("0.00"); }
            set { _fee = double.Parse(value); }
        }
        public DateTime date { get; set; } = DateTime.Now;  
       
    }
}

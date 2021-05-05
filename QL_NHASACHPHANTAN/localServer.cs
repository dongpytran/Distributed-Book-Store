using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_NHASACHPHANTAN
{
    class localServer
    {
        public string svname { get; set; }
        public string macn { get; set; }

        public localServer() { 
        }
        public localServer(string s, string cn) {
            this.svname = s;
            this.macn = cn;
        }
    }
}

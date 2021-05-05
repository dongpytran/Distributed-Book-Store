using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_NHASACHPHANTAN
{
    class ChiTietHd
    {
        public string mash { get; set; }
        public string tensh { get; set; }
        public int soluong { get; set; }
        public int giaban { get; set; }
        public int tongtien { get; set; }

        public ChiTietHd() { 
        }

        public ChiTietHd(string ms, string tens, int sl, int gia)
        {
            this.mash = ms;
            this.tensh = tens;
            this.soluong = sl;
            this.giaban = gia;
            this.tongtien = sl * gia;
        }

    }
}

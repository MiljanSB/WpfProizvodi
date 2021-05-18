using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfProizvodi
{
    class Kategorija
    {
        public int KategorijaId { get; set; }
        public string NazivKategorije { get; set; }
        public string OpisKategorije { get; set; }

        public override string ToString()
        {
            return NazivKategorije;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfProizvodi
{
    class Proizvod
    {
        public int ProizvodId { get; set; }
        public int KategorijaId { get; set; }
        public string NazivProizvoda { get; set; }
        public decimal Cena { get; set; }
        public int KolicinaNaLageru { get; set; }

        public override string ToString()
        {
            return NazivProizvoda;
        }
    }
}

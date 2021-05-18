using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfProizvodi
{
    static class StringEkstenzija
    {
        public static bool Contains(this string s, string deoStringa, StringComparison comp)
        {
            int? indeks = s?.IndexOf(deoStringa, comp);
            return indeks > -1;
        }
    }
}

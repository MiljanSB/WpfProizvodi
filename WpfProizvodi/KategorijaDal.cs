using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WpfProizvodi
{
    static class KategorijaDal
    {
        public static List<Kategorija> VratiKategorije()
        {
            List<Kategorija> listaKategorija = new List<Kategorija>();


            using (SqlConnection konekcija = new SqlConnection(Konekcija.cnnMagacin))
            {

                using (SqlCommand komanda = new SqlCommand("SELECT * FROM Kategorija ORDER BY NazivKategorije",konekcija))
                {

                    try
                    {
                        konekcija.Open();
                        SqlDataReader dr = komanda.ExecuteReader();
                        while (dr.Read())
                        {
                            Kategorija k = new Kategorija {
                                KategorijaId = dr.GetInt32(0),
                                NazivKategorije = dr.GetString(1),
                                OpisKategorije = dr.GetString(2)
                            };

                            listaKategorija.Add(k);
                        }
                        return listaKategorija;
                    }
                    catch (Exception)
                    {

                        return null;
                    }
                }
            }
        }
    }
}

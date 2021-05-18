using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WpfProizvodi
{
    static class ProizvodDal
    {
        public static List<Proizvod> VratiProizvode()
        {

            List<Proizvod> listaProizvoda = new List<Proizvod>();

            using (SqlConnection konekcija = new SqlConnection(Konekcija.cnnMagacin))
            {
                using (SqlCommand komanda = new SqlCommand("SELECT * FROM Proizvod ORDER BY NazivProizvoda",konekcija))
                {

                    try
                    {
                        konekcija.Open();
                        SqlDataReader dr = komanda.ExecuteReader();
                        while (dr.Read())
                        {
                            Proizvod p = new Proizvod {
                                ProizvodId = dr.GetInt32(0),
                                KategorijaId = dr.GetInt32(1),
                                NazivProizvoda = dr.GetString(2),
                                Cena = dr.GetDecimal(3),
                                KolicinaNaLageru = dr.GetInt32(4)
                            };
                            listaProizvoda.Add(p);
                        }
                        return listaProizvoda;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
        }

        public static int UbaciProizvod(Proizvod p)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO Proizvod VALUES(@KategorijaId,@NazivProizvoda,@Cena,@KolicinaNaLageru)");
            sb.AppendLine("SELECT CAST(SCOPE_IDENTITY() AS int)");
            int ID = 0;
            using (SqlConnection konekcija = new SqlConnection(Konekcija.cnnMagacin))
            {
                using (SqlCommand komanda = new SqlCommand(sb.ToString(),konekcija))
                {

                    try
                    {
                        komanda.Parameters.AddWithValue("@KategorijaId",p.KategorijaId);
                        komanda.Parameters.AddWithValue("@NazivProizvoda", p.NazivProizvoda);
                        komanda.Parameters.AddWithValue("@Cena", p.Cena);
                        komanda.Parameters.AddWithValue("@KolicinaNaLageru", p.KolicinaNaLageru);
                        konekcija.Open();
                        ID = (int)komanda.ExecuteScalar();

                        return ID;
                    }
                    catch (Exception)
                    {
                        return -1;
                    }
                }
            }
        }

        public static int PromeniProizvod(Proizvod p)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE Proizvod");
            sb.AppendLine("SET KategorijaId =@KategorijaId,NazivProizvoda=@NazivProizvoda,Cena=@Cena,KolicinaNaLageru=@KolicinaNaLageru");
            sb.AppendLine("WHERE ProizvodId = @ProizvodId");

            using (SqlConnection konekcija = new SqlConnection(Konekcija.cnnMagacin))
            {
                using (SqlCommand komanda=new SqlCommand(sb.ToString(),konekcija))
                {
                    try
                    {
                        komanda.Parameters.AddWithValue("@KategorijaId", p.KategorijaId);
                        komanda.Parameters.AddWithValue("@NazivProizvoda", p.NazivProizvoda);
                        komanda.Parameters.AddWithValue("@Cena", p.Cena);
                        komanda.Parameters.AddWithValue("@KolicinaNaLageru", p.KolicinaNaLageru);
                        komanda.Parameters.AddWithValue("@ProizvodId", p.ProizvodId);

                        konekcija.Open();
                        komanda.ExecuteNonQuery();

                        return 0;
                    }
                    catch (Exception)
                    {
                        return -1;
                    }
                }
            }
        }

        public static int ObrisiProizvod(int id)
        {
            using (SqlConnection konekcija = new SqlConnection(Konekcija.cnnMagacin))
            {
                using (SqlCommand komanda = new SqlCommand("DELETE FROM Proizvod WHERE ProizvodId = @ProizvodId", konekcija))
                {

                    try
                    {
                        komanda.Parameters.AddWithValue("ProizvodId", id);
                        konekcija.Open();
                        komanda.ExecuteNonQuery();

                        return 0;
                    }
                    catch (Exception)
                    {
                        return -1;
                    }
                }
            }
        }
    }
}

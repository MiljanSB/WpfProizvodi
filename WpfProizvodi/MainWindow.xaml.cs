using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfProizvodi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //KATEGORIJE
        private void PrikaziKategorije()
        {
            ComboBoxKategorija.Items.Clear();
            List<Kategorija> listaKategorija = KategorijaDal.VratiKategorije();

            if (listaKategorija != null)
            {
                foreach (Kategorija k in listaKategorija)
                {
                    ComboBoxKategorija.Items.Add(k);
                }
            }


        }

        //PROIZVODI
        private void PrikaziProizvode()
        {
            ComboBoxProizvod.Items.Clear();
            List<Proizvod> listaProizvoda = ProizvodDal.VratiProizvode();
            if (listaProizvoda!= null)
            {
                foreach (Proizvod p in listaProizvoda)
                {
                    ComboBoxProizvod.Items.Add(p);
                }
            }
        }

        //RESETUJ
        private void Resetuj()
        {
            TextBoxPretraga.Clear();
            TextBoxID.Clear();
            TextBoxNaziv.Clear();
            TextBoxCena.Clear();
            TextBoxKolicina.Clear();
            ComboBoxProizvod.SelectedIndex = -1;
            ComboBoxKategorija.SelectedIndex = -1;
        }

        //FIND 
        private Kategorija NadjiKategoriju(int id)
        {
            foreach (Kategorija k in ComboBoxKategorija.Items)
            {
                if (k.KategorijaId==id)
                {
                    return k;
                }
            }
            return null;
        }

        //FIND

        private Proizvod NadjiProizvod(int id)
        {
            foreach (Proizvod p in ComboBoxProizvod.Items)
            {
                if (p.ProizvodId == id)
                {
                    return p;
                }
            }
            return null;
        }

        //PRETRAGA PO INDEKSU
        private int NadjiIndeks(string pretraga)
        {
            foreach (Proizvod p in ComboBoxProizvod.Items)
            {
                if (p.NazivProizvoda.Contains(pretraga,StringComparison.CurrentCultureIgnoreCase))
                {
                    return ComboBoxProizvod.Items.IndexOf(p);
                }
            }

            return -1;
        }

        //VALIDACIJA
        private bool Validacija()
        {
            if (ComboBoxKategorija.SelectedIndex < 0) 
            {
                MessageBox.Show("Odaberi kategoriju");
                return false;
            }

            if (string.IsNullOrWhiteSpace(TextBoxNaziv.Text) || TextBoxNaziv.Text.Length <2)
            {
                MessageBox.Show("Unesite naziv proizvoda");
                TextBoxNaziv.Focus();
                return false;
            }

            if (!decimal.TryParse(TextBoxCena.Text,out decimal cena))
            {
                MessageBox.Show("Unesite broj");
                TextBoxCena.Clear();
                TextBoxCena.Focus();
                return false;
            }

            if (!int.TryParse(TextBoxKolicina.Text,out int kolicina))
            {
                MessageBox.Show("Unesite broj");
                TextBoxKolicina.Clear();
                TextBoxKolicina.Focus();
                return false;
            }

            return true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PrikaziKategorije();
            PrikaziProizvode();
        }

        private void ButtonResetuj_Click(object sender, RoutedEventArgs e)
        {
            Resetuj();
        }

        private void ComboBoxProizvod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxProizvod.SelectedIndex > -1)
            {
                Proizvod p = ComboBoxProizvod.SelectedItem as Proizvod;

                TextBoxID.Text = p.ProizvodId.ToString();
                TextBoxNaziv.Text = p.NazivProizvoda;
                TextBoxCena.Text = p.Cena.ToString();
                TextBoxKolicina.Text = p.KolicinaNaLageru.ToString();

                ComboBoxKategorija.SelectedItem = NadjiKategoriju(p.KategorijaId); 
            }
        }

        private void TextBoxPretraga_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Enter)
            {
                string pretraga = TextBoxPretraga.Text.Trim();

                ComboBoxProizvod.SelectedIndex = NadjiIndeks(pretraga);
            }
        }

        private void ButtonUbaci_Click(object sender, RoutedEventArgs e)
        {
            if (Validacija())
            {
                Kategorija k = ComboBoxKategorija.SelectedItem as Kategorija;

                Proizvod p = new Proizvod {
                    KategorijaId = k.KategorijaId,
                    NazivProizvoda = TextBoxNaziv.Text,
                    Cena = decimal.Parse(TextBoxCena.Text),
                    KolicinaNaLageru = int.Parse(TextBoxKolicina.Text)                    
                };

                int id = ProizvodDal.UbaciProizvod(p);

                if (id > 0)
                {
                    PrikaziProizvode();
                    ComboBoxProizvod.SelectedItem = NadjiProizvod(id);
                    MessageBox.Show("Proizvod ubacen");
                }
                else
                {
                    MessageBox.Show("Greska pri ubacivanju");
                }
            }
        }

        private void ButtonPromeni_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxProizvod.SelectedIndex>-1)
            {
                if (Validacija())
                {
                    Proizvod p = ComboBoxProizvod.SelectedItem as Proizvod;
                    Kategorija k = ComboBoxKategorija.SelectedItem as Kategorija;
                    p.KategorijaId = k.KategorijaId;
                    p.NazivProizvoda = TextBoxNaziv.Text;
                    p.Cena = decimal.Parse(TextBoxCena.Text);
                    p.KolicinaNaLageru = int.Parse(TextBoxKolicina.Text);

                    int rez = ProizvodDal.PromeniProizvod(p);

                    if (rez == 0)
                    {
                        PrikaziProizvode();
                        ComboBoxProizvod.SelectedItem = NadjiProizvod(p.ProizvodId);
                        MessageBox.Show("Podaci promenjeni");
                    }
                    else
                    {
                        MessageBox.Show("Greska pri promeni podataka");
                    }
                }
            }
            else
            {
                MessageBox.Show("Odaberite proizvod");
            }
        }

        private void ButtonObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxProizvod.SelectedIndex > -1)
            {
                    Proizvod p = ComboBoxProizvod.SelectedItem as Proizvod;
                MessageBoxResult rez = MessageBox.Show("Da li ste sigurni?", p.NazivProizvoda, MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (rez == MessageBoxResult.OK)
                {

                    int id = ProizvodDal.ObrisiProizvod(p.ProizvodId);

                    if (id == 0)
                    {
                        PrikaziProizvode();
                        Resetuj();
                        MessageBox.Show("Podaci obrisani");
                    }
                    else
                    {
                        MessageBox.Show("Greska pri brisanju");
                    }
                }
            }
            else
            {
                MessageBox.Show("Odaberite proizvod");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BasicBlue.SzachyBasicBlue;
using SzachyBasicBlue;

namespace BasicBlue
{
    public partial class Form1 : Form
    {
        private Color bialy = Color.Beige;
        private Color czarny = Color.Brown;
        private Bierka wybrana;
        private Gra g = new Gra();
        private bool uzywajSI = false;
        private int ileLosowac = 3;

        public Form1()
        {
            // wykonanie kilku niezbędnych na początku operacji
            InitializeComponent();
            podpiszSzachownice();
            RysujSiatke(bialy, czarny);
            kompletujPiony();
            Bierka.przeliczWszystieRuchy();
            odswiezLabelKolejka();
        }


        private void podpiszSzachownice()
        {
            string odsteplitery = "              ";
            string odstepcyfry = "\n\n\n\n";
            string litery = "A" + odsteplitery + "B" + odsteplitery + "C" + odsteplitery + "D" + odsteplitery + "E" + odsteplitery + "F" + odsteplitery + "G" + odsteplitery + "H";
            string cyfry = "8" + odstepcyfry + "7" + odstepcyfry + "6" + odstepcyfry + "5" + odstepcyfry + "4" + odstepcyfry + "3" + odstepcyfry + "2" + odstepcyfry + "1";
            lblPodpisSzachownicyLitery1.Text = lblPodpisSzachownicyLitery2.Text = litery;
            lblcyfry1.Text = lblcyfry2.Text = cyfry;
        }
        // ustawianie bierki, czyli narysowanie odpowiedniego obrazka na szachownicy (dokładniej to na panelu)
        private void ustawBierke(Bierka b)
        {
            Panel p = tableLayoutPanel1.GetControlFromPosition(b.pozycjaX, b.pozycjaY) as Panel;
            if (p != null)
            {
                p.BackgroundImage = b.grafika;
            }
        }

        private void kompletujPiony()
        {
            foreach (Bierka b in Gra.bierkiBiale.Union(Gra.bierkiCzarne))
            {
                ustawBierke(b);
            }
        }
        // rysowanie czarno-białej siatki.
        // cały myk polega na tym, że panele są ułożone w kolejności, więc nieparzysty
        // jest zawsze obok parzystego, więc można sprtawdzać, że jeżeli parzysty, to taki kolor,
        // a jeśli nieparzysty, to inny
        private void RysujSiatke(Color biale, Color czarne)
        {
            foreach (Control c in tableLayoutPanel1.Controls)
            {
                if (c is Panel)
                {
                    int numer = int.Parse(c.Name.Replace("panel", string.Empty));
                    if (numer % 2 == 0) c.BackColor = biale;
                    else c.BackColor = czarne;
                }
            }
        }

        private void WyczyscPanele()
        {
            foreach (Control c in tableLayoutPanel1.Controls)
            {
                if (c is Panel)
                {
                    c.BackgroundImage = null;
                }
            }
        }

        private void odswiezLabelKolejka()
        {
            lblCzyjRuch.Text = "Oczekiwanie na ruch gracza: " + Gra.kolejka.ToString();
        }



        private void panel_Click(object sender, EventArgs e)
        {
            if (Gra.kolejka == Enums.czyjaKolej.Osoba)
            {
                TableLayoutPanelCellPosition pos = Tools.GetCellPosotion(tableLayoutPanel1);
                Bierka b = Bierka.getBierkaByPos(pos.Column, pos.Row, Gra.bierkiBiale, Gra.bierkiCzarne);


                if (wybrana == null && b != null && b.kolor == Enums.Kolor_pionków.Czarne) return;


                if (wybrana != null && b == null)
                {
                    if (!wybrana.przesun(pos.Column, pos.Row, true))
                    {
                        MessageBox.Show("ruch niedozwolony");
                    }
                    else
                    {
                        WyczyscPanele();
                        foreach (Bierka bi in Gra.bierkiBiale.Union(Gra.bierkiCzarne))
                        {
                            ustawBierke(bi);
                        }
                        Gra.kolejka = Enums.czyjaKolej.Komputer;
                        odswiezLabelKolejka();
                        Bierka.przeliczWszystieRuchy();

                        wykonajRuchPC();

                      
                        WyczyscPanele();
                        // Bierka.przeliczWszystieRuchy();  // moze niepotrzebne
                        foreach (Bierka bi in Gra.bierkiBiale.Union(Gra.bierkiCzarne))
                        {
                            ustawBierke(bi);
                        }
                        odswiezLabelKolejka();
                    }
                }
                else if (wybrana != null && b != null && wybrana.kolor != b.kolor)   // bicie
                {
                    if (!wybrana.zbij(b))
                    {
                        MessageBox.Show("bicie niedozwolone");
                    }
                    else
                    {
                        WyczyscPanele();
                        foreach (Bierka bi in Gra.bierkiBiale.Union(Gra.bierkiCzarne))
                        {
                            ustawBierke(bi);
                        }
                        Gra.kolejka = Enums.czyjaKolej.Komputer;
                        odswiezLabelKolejka();
                        Bierka.przeliczWszystieRuchy();
                      
                        wykonajRuchPC();
                        WyczyscPanele();
                        Bierka.przeliczWszystieRuchy();  // moze niepotrzebne
                        foreach (Bierka bi in Gra.bierkiBiale.Union(Gra.bierkiCzarne))
                        {
                            ustawBierke(bi);
                        }
                        odswiezLabelKolejka();
                    }
                }
                else wybrana = b;


                if (b == null)
                    label3.Text = "Puste pole " + pos.Column + " " + pos.Row;
                else
                {
                    label3.Text = b.ToString() + "  kolor: " + b.kolor + "(X,Y)=(" + b.pozycjaX + "," + b.pozycjaY + ")";
                    //b.generujMozliweRuchy();
                    Bierka.przeliczWszystieRuchy();
                    label4.Text = "możliwe ruchy: " + b.mozliweRuchy.Count() + Environment.NewLine + "możliwe bicia: " + b.mozliweBicia.Count();
                }
            }
            txtPgn.Text = Gra.pgnString;
            sprawdzMozliwoscRoszady();

          
        }

        private void radioColor_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNiebieska.Checked)
            {
                czarny = Color.Blue;
                bialy = Color.White;
            }
            else if (rbCzarna.Checked)
            {
                czarny = Color.Black;
                bialy = Color.White;
            }
            else if (rbbezowa.Checked)
            {
                czarny = Color.Brown;
                bialy = Color.Beige;
            }

            RysujSiatke(bialy, czarny);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            TableLayoutPanelCellPosition pos = Tools.GetCellPosotion(tableLayoutPanel1);
            label1.Text = Gra.tlumaczNazwePola(pos.Row, pos.Column) + "     " + pos.Column + " " + pos.Row;

            RysujSiatke(bialy, czarny);

            Panel p = tableLayoutPanel1.GetControlFromPosition(pos.Column, pos.Row) as Panel;
            if (p != null)
            {
                p.BackColor = Color.Red;
            }
        }

        private void btnZapiszGre_Click(object sender, EventArgs e)
        {
            ZapisOdczyt.zapiszGre("d:\\szachySave.chess", Gra.bierkiBiale, Gra.bierkiCzarne);
        }

        private void btnWczytaj_Click(object sender, EventArgs e)
        {
            // wczytanie gry
            ZapisOdczyt.wczytajGre();

            // wykonanie podstawowych operacji po wczytaniu - wyczyszczenie planszy, ustawienie bierek 
            WyczyscPanele();
            foreach (Bierka bi in Gra.bierkiBiale.Union(Gra.bierkiCzarne))
            {
                ustawBierke(bi);
            }
            odswiezLabelKolejka();
            txtPgn.Text = Gra.pgnString;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // otworzenie podglądu partii
            string pgn = "pgnString: '" + txtPgn.Text + "',";
            Podglad p = new Podglad(pgn);
            p.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Bierka> listaBiale1 = new List<Bierka>();
            listaBiale1 = Tools.klonujBierki(Gra.bierkiBiale);
            List<Bierka> listaCzarne1 = new List<Bierka>();
            listaCzarne1 = Tools.klonujBierki(Gra.bierkiCzarne);

            RuchDrzewo rd = Gracz_Czlowiek.wykonajNajlepszyRuch(listaBiale1, listaCzarne1);

            string wiadomosc = "Komputer proponuje następujący ruch: ";
            string operacja = " przesunięcie ";
            if (rd.czyBicie) operacja = " bicie ";
            string punktDocelowy = Gra.tlumaczNazwePolaPGN(rd.punkt.X, rd.punkt.Y);
            string punktZrodlowy = Gra.tlumaczNazwePolaPGN(rd.bierka.pozycjaX, rd.bierka.pozycjaY);

            MessageBox.Show(wiadomosc + operacja + rd.bierka + " z miejsca " + punktZrodlowy + " na miejsce " + punktDocelowy);
        }

        private void radioSI_CheckedChanged(object sender, EventArgs e)
        {
            // zmiana sposoby wykonywania uchów przez PC
            if (radioSI.Checked)
            {
                textBox1.Enabled = true;
                textBox1.Text = ileLosowac.ToString();
                uzywajSI = true;
            }

            else
            {
                textBox1.Enabled = false;
                uzywajSI = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int ile = 3;   // domyślna wartość

            if (!int.TryParse(textBox1.Text, out ile))
            {
                MessageBox.Show("Niepoprawna wartość. Ustawiam domyślną");
                textBox1.Text = "3";
            }
            else ileLosowac = ile;
        }

        private void sprawdzMozliwoscRoszady()
        {
            List<Enums.Roszada> listaRoszad = Krol.zwrocDostepneRoszady();
            // otrzymujemy listę roszad. Jeśli jest pusta, to nei można wykonać roszady
            if (listaRoszad == null)
            {
                btnMalaRoszada.Enabled = btnDuzaRoszada.Enabled = false;
            }
            else
            {
                // aktywowanie konkretnych przycisków do roszady
                if (listaRoszad.Contains(Enums.Roszada.Dluga)) btnDuzaRoszada.Enabled = true;
                else btnDuzaRoszada.Enabled = false;

                if (listaRoszad.Contains(Enums.Roszada.Krotka)) btnMalaRoszada.Enabled = true;
                else btnMalaRoszada.Enabled = false;
            }
        }

        private void btnDuzaRoszada_Click(object sender, EventArgs e)
        {

            btnDuzaRoszada.Enabled = false;
            // szukanie odpowiednich figur
            Bierka wieza = Bierka.getBierkaByPos(0, 7, Gra.bierkiBiale, Gra.bierkiCzarne);
            Bierka krol = Bierka.getBierkaByPos(4, 7, Gra.bierkiBiale, Gra.bierkiCzarne);
            // wykonanie roszady - zostało wcześniej sprawdzone, czy można to zrobić.
            Bierka.roszada(wieza, krol, Enums.Roszada.Krotka);
            zrobResztePoRoszadzie();
          
        }
        
        private void btnMalaRoszada_Click(object sender, EventArgs e)
        {
            btnMalaRoszada.Enabled = false;
            // szukanie odpowiednich figur
            Bierka wieza = Bierka.getBierkaByPos(7, 7, Gra.bierkiBiale, Gra.bierkiCzarne);
            Bierka krol = Bierka.getBierkaByPos(4, 7, Gra.bierkiBiale, Gra.bierkiCzarne);

            // wykonanie roszady - zostało wcześniej sprawdzone, czy można to zrobić.
            Bierka.roszada(wieza, krol, Enums.Roszada.Krotka);
            zrobResztePoRoszadzie();
        }


        // jedna funkcja uzywana przy obu roszadach - bo po kazdej trzeba przegenerowac ruchy i zmienic kolejke
        private void zrobResztePoRoszadzie()
        {
              WyczyscPanele();
            foreach (Bierka bi in Gra.bierkiBiale.Union(Gra.bierkiCzarne))
            {
                ustawBierke(bi);
            }
            odswiezLabelKolejka();
            Bierka.przeliczWszystieRuchy();
            Gra.kolejka = Enums.czyjaKolej.Komputer;
            wykonajRuchPC();

            WyczyscPanele();
            Bierka.przeliczWszystieRuchy();  // moze niepotrzebne
            foreach (Bierka bi in Gra.bierkiBiale.Union(Gra.bierkiCzarne))
            {
                ustawBierke(bi);
            }
            odswiezLabelKolejka();
        }

        // zbiór funkcji do obsługi ruchu wykonywanego przez komputer
        private void wykonajRuchPC()
        {
            if (uzywajSI)
            {

                RuchDrzewo rd = new RuchDrzewo();
                do
                {
                    // mechanizm losujący bierki przekazywane do funkcji obliczającej
                    List<Bierka> listaBiale = new List<Bierka>();
                    listaBiale = Tools.klonujBierki(Gra.bierkiBiale);

                    List<Bierka> listaCzarne = new List<Bierka>();
                    listaCzarne = Tools.klonujBierki(Gra.bierkiCzarne);

                    listaBiale = LosujBierki.zwrocListe(ileLosowac, listaBiale);
                    listaCzarne = LosujBierki.zwrocListe(ileLosowac, listaCzarne);
                   
                    // zwrócenie najlepszego ruchu
                    rd = Gracz_Komputer.obliczRuchAI(listaBiale, listaCzarne);
                }
                while (rd.bierka == null);

                Bierka bi = Bierka.getBierkaByPos(rd.bierka.pozycjaX, rd.bierka.pozycjaY, Gra.bierkiBiale, Gra.bierkiCzarne);
                if (rd.czyBicie)
                {
                    try  // tutaj niestety czasem coś się psuje.
                    {  // komputer ma w którymś miejscu nieaktualną listę pionów (prawdopodobnie)
                        bi.zbij(Bierka.getBierkaByPos(bi.mozliweBicia[0].X, bi.mozliweBicia[0].Y, Gra.bierkiBiale, Gra.bierkiCzarne));
                    }
                    catch { MessageBox.Show("Wystąpił problem z mechanizmem SI. PC Oddaje ruch graczowi"); }
                    Gra.kolejka = Enums.czyjaKolej.Osoba;
                }
                else
                {
                    bi.przesun(rd.punkt.X, rd.punkt.Y, true);
                    Gra.kolejka = Enums.czyjaKolej.Osoba;
                }
            }
            else
            {
                if (!Gracz_Komputer.Zrob_RuchLosowy())
                {
                    MessageBox.Show("Wygrałeś !!");
                }
            }
        }

    }
}

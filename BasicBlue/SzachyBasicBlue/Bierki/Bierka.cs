using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BasicBlue.SzachyBasicBlue;
using SzachyBasicBlue;

namespace SzachyBasicBlue
{
    /// <summary>
    /// Klasa służy do pokazania użytkownikowi dostępnych ruchów figur i obliczenie najlepszego ruchu. Klasa pokazuje nam również ilość dostępnych poruszeń figur. Wybiera najlepszy ruch.
    /// </summary>
    public class Bierka : ICloneable
    {
        public Image grafika;
        public int pozycjaY;
        public int pozycjaX;
        public int zasieg;
        public Enums.Kolor_pionków kolor;

        public List<Enum> kierunkiRuchu = new List<Enum>();
        public List<Enum> kierunekBicia = new List<Enum>();
        public List<Point> mozliweRuchy = new List<Point>();
        public List<Point> mozliweBicia = new List<Point>();

        public int punkty;
        public string litera;
        public bool bylRuch;



        // funkcja uzywana przy klonowaniu listy - wygooglane
        public object Clone()
        {
            return this.MemberwiseClone();
        }



        public bool przesun(int x, int y, bool pgn)
        {
            bool ret = false;

            if (this.mozliweRuchy.Contains(new Point(x, y)))
            {
                //if (this.kolor == Enums.Kolor_pionków.Biale && Gracz_Czlowiek.szach && this.GetType() != typeof(Krol))
                //{
                //    MessageBox.Show("Jest szach. Należy ruszyć się królem.");
                //    return false;
                //}


                this.pozycjaX = x;
                this.pozycjaY = y;
                ret = true;

                // funkcja podzielona na dwie, ponieważ ta sama funkcja uzywana jest 
                // do symulowania zachowania gracza i do przestawiania bierek przez mechanizm sztucznej inteligencji
                // a gdy algorytm coś oblicza, to nie chcemy sumować liczby wykonanych ruchów. chcemy 
                // jedynie zmieniać współrzędne bierek
                if (pgn)
                {

                    this.generujMozliweRuchy(Gra.bierkiBiale, Gra.bierkiCzarne);
                    this.wyczyscMozliweRuchyZeSmieci();
                    this.bylRuch = true;

                    string numer = string.Empty;
                    Gra.ileRuchow++;
                    int ktoryNumer = Gra.ileRuchow / 2;
                    ktoryNumer++;
                    if (Gra.ileRuchow % 2 == 1) numer = " " + ktoryNumer + ". ";
                    else numer = " ";
                    Gra.pgnString += numer + this.litera + Gra.tlumaczNazwePolaPGN(x, y).ToLower().Trim();



                    Gra.ustawSzach(Gra.bierkiBiale, Gra.bierkiCzarne);
                    if (Gracz_Czlowiek.szach)
                    {
                        MessageBox.Show("Zaszachowano gracza");
                        Bierka.wygenerujMozliweRuchyKrola();
                    }
                    if (Gracz_Komputer.szach)
                    {
                        MessageBox.Show("Zaszachowano komputer");
                        Bierka.wygenerujMozliweRuchyKrola();
                    }

                }
            }

            return ret;
        }


        static public void roszada(Bierka wieza, Bierka krol, Enums.Roszada typRoszady)
        {
            int pozycjaXKrola = 0;  // docelowa pozycja krola
            int pozycjaXWiezy = 0;   // docelowa pozycja wieży
            string PGN = "0-0";   // dla krotkiej roszady

            if (typRoszady == Enums.Roszada.Dluga)
            {
                pozycjaXKrola = 2;
                pozycjaXWiezy = 3;
                PGN += "-0";
            }
            else
            {
                pozycjaXKrola = 6;
                pozycjaXWiezy = 5;
            }

            // pozycje Y się nie zmieniają.
            wieza.pozycjaX = pozycjaXWiezy;
            krol.pozycjaX = pozycjaXKrola;

            // wygenerowanie możliwych ruchów dla biurerk, które biorą udział w roszadzie
            wieza.generujMozliweRuchy(Gra.bierkiBiale, Gra.bierkiCzarne);
            wieza.wyczyscMozliweRuchyZeSmieci();
            wieza.bylRuch = true;
            krol.generujMozliweRuchy(Gra.bierkiBiale, Gra.bierkiCzarne);
            krol.wyczyscMozliweRuchyZeSmieci();
            krol.bylRuch = true;

            string numer = string.Empty;
            Gra.ileRuchow++;
            int ktoryNumer = Gra.ileRuchow / 2;
            ktoryNumer++;
            if (Gra.ileRuchow % 2 == 1) numer = " " + ktoryNumer + ". ";
            else numer = " ";
            Gra.pgnString += numer + PGN;


            Gra.ustawSzach(Gra.bierkiBiale, Gra.bierkiCzarne);
            if (Gracz_Czlowiek.szach)
            {
                MessageBox.Show("Zaszachowano gracza");
                Bierka.wygenerujMozliweRuchyKrola();
            }
            if (Gracz_Komputer.szach)
            {
                MessageBox.Show("Zaszachowano komputer");
                Bierka.wygenerujMozliweRuchyKrola();
            }
        }

        public bool zbij(Bierka ofiara)
        {
            bool ret = false;
            if (ofiara != null)
            {
                if (this.mozliweBicia.Contains(new Point(ofiara.pozycjaX, ofiara.pozycjaY)))
                {
                    //  if (ofiara.ToString() == "Krol") MessageBox.Show("Nie można bić króla");
                    //  else
                    //   {

                    int pozycjaTMPX = this.pozycjaX;

                    this.pozycjaX = ofiara.pozycjaX;
                    this.pozycjaY = ofiara.pozycjaY;
                    this.bylRuch = true;
                    ret = true;

                    if (ofiara.kolor == Enums.Kolor_pionków.Biale) Gra.bierkiBiale.Remove(ofiara);
                    else Gra.bierkiCzarne.Remove(ofiara);

                    this.generujMozliweRuchy(Gra.bierkiBiale, Gra.bierkiCzarne);
                    this.wyczyscMozliweRuchyZeSmieci();

                    Gra.ileRuchow++;
                    string numer = string.Empty;
                    string pionek = string.Empty;

                    if (this.GetType() == typeof(Pionek))
                    {
                        pionek = Gra.tlumaczNazweKolumny(pozycjaTMPX).ToLower();
                    }

                    int ktoryNumer = Gra.ileRuchow / 2;
                    ktoryNumer++;
                    if (Gra.ileRuchow % 2 == 1) numer = " " + ktoryNumer + ". ";
                    else numer = " ";
                    Gra.pgnString += numer + pionek + this.litera + "x" + Gra.tlumaczNazwePolaPGN(ofiara.pozycjaX, ofiara.pozycjaY).ToLower().Trim();
                }
                // }
            }
            else
            {
                MessageBox.Show("coś się popsuło");
                Bierka.przeliczWszystieRuchy();
            }

            return ret;
        }

        public override string ToString()
        {
            return this.GetType().ToString().Replace("SzachyBasicBlue.", string.Empty);
        }


        public void generujMozliweRuchy(List<Bierka> biale, List<Bierka> czarne)
        {
            this.mozliweRuchy.Clear();
            this.mozliweBicia.Clear();

            if (this.kierunkiRuchu.Contains(Enums.KierunekRuchu.Gora))
            {
                if (this.kolor == Enums.Kolor_pionków.Czarne)
                {
                    for (int i = 1; i <= zasieg; i++)
                    {
                        if (getBierkaByPos(this.pozycjaX, this.pozycjaY + i, biale, czarne) == null)
                        {
                            if (this.pozycjaY + i <= 7)
                                this.mozliweRuchy.Add(new Point(this.pozycjaX, this.pozycjaY + i));
                        }
                        else
                        {
                            if (getBierkaByPos(this.pozycjaX, this.pozycjaY + i, biale, czarne).kolor == Enums.Kolor_pionków.Biale)
                                this.mozliweBicia.Add(new Point(this.pozycjaX, this.pozycjaY + i));
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 1; i <= zasieg; i++)
                    {
                        if (getBierkaByPos(this.pozycjaX, this.pozycjaY - i, biale, czarne) == null)
                        {
                            if (this.pozycjaY - i >= 0)
                                this.mozliweRuchy.Add(new Point(this.pozycjaX, this.pozycjaY - i));
                        }
                        else
                        {
                            if (getBierkaByPos(this.pozycjaX, this.pozycjaY - i, biale, czarne).kolor == Enums.Kolor_pionków.Czarne)
                                this.mozliweBicia.Add(new Point(this.pozycjaX, this.pozycjaY - i));
                            break;
                        }
                    }
                }
            }



            if (this.kierunkiRuchu.Contains(Enums.KierunekRuchu.Dol))
            {
                if (this.kolor == Enums.Kolor_pionków.Czarne)
                {
                    for (int i = 1; i <= zasieg; i++)
                    {
                        if (getBierkaByPos(this.pozycjaX, this.pozycjaY - i, biale, czarne) == null)
                        {
                            if (this.pozycjaY - i > 0)
                                this.mozliweRuchy.Add(new Point(this.pozycjaX, this.pozycjaY - i));
                        }
                        else
                        {
                            if (getBierkaByPos(this.pozycjaX, this.pozycjaY - i, biale, czarne).kolor == Enums.Kolor_pionków.Biale)
                                this.mozliweBicia.Add(new Point(this.pozycjaX, this.pozycjaY - i));
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 1; i <= zasieg; i++)
                    {
                        if (getBierkaByPos(this.pozycjaX, this.pozycjaY + i, biale, czarne) == null)
                        {
                            if (this.pozycjaY + i <= 7)
                                this.mozliweRuchy.Add(new Point(this.pozycjaX, this.pozycjaY + i));
                        }
                        else
                        {
                            if (getBierkaByPos(this.pozycjaX, this.pozycjaY + i, biale, czarne).kolor == Enums.Kolor_pionków.Czarne)
                                this.mozliweBicia.Add(new Point(this.pozycjaX, this.pozycjaY + i));
                            break;
                        }
                    }
                }
            }

            if (this.kierunkiRuchu.Contains(Enums.KierunekRuchu.ProstoSkos))
            {
                Point p1 = new Point(this.pozycjaX - 1, this.pozycjaY - 2);
                Point p2 = new Point(this.pozycjaX + 1, this.pozycjaY - 2);
                Point p3 = new Point(this.pozycjaX - 2, this.pozycjaY - 1);
                Point p4 = new Point(this.pozycjaX - 2, this.pozycjaY + 1);
                Point p5 = new Point(this.pozycjaX - 1, this.pozycjaY + 2);
                Point p6 = new Point(this.pozycjaX + 1, this.pozycjaY + 2);
                Point p7 = new Point(this.pozycjaX + 2, this.pozycjaY - 1);
                Point p8 = new Point(this.pozycjaX + 2, this.pozycjaY + 1);

                if (getBierkaByPos(p1.X, p1.Y, biale, czarne) == null) this.mozliweRuchy.Add(p1);
                if (getBierkaByPos(p2.X, p2.Y, biale, czarne) == null) this.mozliweRuchy.Add(p2);
                if (getBierkaByPos(p3.X, p3.Y, biale, czarne) == null) this.mozliweRuchy.Add(p3);
                if (getBierkaByPos(p4.X, p4.Y, biale, czarne) == null) this.mozliweRuchy.Add(p4);
                if (getBierkaByPos(p5.X, p5.Y, biale, czarne) == null) this.mozliweRuchy.Add(p5);
                if (getBierkaByPos(p6.X, p6.Y, biale, czarne) == null) this.mozliweRuchy.Add(p6);
                if (getBierkaByPos(p7.X, p7.Y, biale, czarne) == null) this.mozliweRuchy.Add(p7);
                if (getBierkaByPos(p8.X, p8.Y, biale, czarne) == null) this.mozliweRuchy.Add(p8);

                if (getBierkaByPos(p1.X, p1.Y, biale, czarne) != null && getBierkaByPos(p1.X, p1.Y, biale, czarne).kolor != this.kolor)
                    this.mozliweBicia.Add(p1);

                if (getBierkaByPos(p2.X, p2.Y, biale, czarne) != null && getBierkaByPos(p2.X, p2.Y, biale, czarne).kolor != this.kolor)
                    this.mozliweBicia.Add(p2);

                if (getBierkaByPos(p3.X, p3.Y, biale, czarne) != null && getBierkaByPos(p3.X, p3.Y, biale, czarne).kolor != this.kolor)
                    this.mozliweBicia.Add(p3);

                if (getBierkaByPos(p4.X, p4.Y, biale, czarne) != null && getBierkaByPos(p4.X, p4.Y, biale, czarne).kolor != this.kolor)
                    this.mozliweBicia.Add(p4);

                if (getBierkaByPos(p5.X, p5.Y, biale, czarne) != null && getBierkaByPos(p5.X, p5.Y, biale, czarne).kolor != this.kolor)
                    this.mozliweBicia.Add(p5);

                if (getBierkaByPos(p6.X, p6.Y, biale, czarne) != null && getBierkaByPos(p6.X, p6.Y, biale, czarne).kolor != this.kolor)
                    this.mozliweBicia.Add(p6);

                if (getBierkaByPos(p7.X, p7.Y, biale, czarne) != null && getBierkaByPos(p7.X, p7.Y, biale, czarne).kolor != this.kolor)
                    this.mozliweBicia.Add(p7);

                if (getBierkaByPos(p8.X, p8.Y, biale, czarne) != null && getBierkaByPos(p8.X, p8.Y, biale, czarne).kolor != this.kolor)
                    this.mozliweBicia.Add(p8);
            }


            if (this.kierunkiRuchu.Contains(Enums.KierunekRuchu.Skos))
            {
                // ruch w jedna strone
                for (int i = this.pozycjaX - 1; i <= zasieg && i >= 0 && i <= 7; i--)
                {
                    if (getBierkaByPos(i, this.pozycjaY - (this.pozycjaX - i), biale, czarne) == null)
                    {
                        this.mozliweRuchy.Add(new Point(i, this.pozycjaY - (this.pozycjaX - i)));
                    }
                    else
                    {
                        if (getBierkaByPos(i, this.pozycjaY - (this.pozycjaX - i), biale, czarne).kolor != this.kolor)
                            this.mozliweBicia.Add(new Point(i, this.pozycjaY - (this.pozycjaX - i)));
                        break;
                    }
                }

                // w druga...
                for (int i = this.pozycjaX + 1; i <= zasieg && i >= 0 && i <= 7; i++)
                {
                    if (getBierkaByPos(i, this.pozycjaY + (this.pozycjaX - i), biale, czarne) == null)
                    {
                        this.mozliweRuchy.Add(new Point(i, this.pozycjaY + (this.pozycjaX - i)));
                    }
                    else
                    {
                        if (getBierkaByPos(i, this.pozycjaY + (this.pozycjaX - i), biale, czarne).kolor != this.kolor)
                            this.mozliweBicia.Add(new Point(i, this.pozycjaY + (this.pozycjaX - i)));
                        break;
                    }
                }


                // ruch w jedna strone
                for (int i = this.pozycjaX - 1; i <= zasieg && i >= 0 && i <= 7; i--)
                {
                    if (getBierkaByPos(i, this.pozycjaY + (this.pozycjaX - i), biale, czarne) == null)
                    {
                        this.mozliweRuchy.Add(new Point(i, this.pozycjaY + (this.pozycjaX - i)));
                    }
                    else
                    {
                        if (getBierkaByPos(i, this.pozycjaY + (this.pozycjaX - i), biale, czarne).kolor != this.kolor)
                            this.mozliweBicia.Add(new Point(i, this.pozycjaY + (this.pozycjaX - i)));
                        break;
                    }
                }

                // w druga...
                for (int i = this.pozycjaX + 1; i <= zasieg && i >= 0 && i <= 7; i++)
                {
                    if (getBierkaByPos(i, this.pozycjaY - (this.pozycjaX - i), biale, czarne) == null)
                    {
                        this.mozliweRuchy.Add(new Point(i, this.pozycjaY - (this.pozycjaX - i)));
                    }
                    else
                    {
                        if (getBierkaByPos(i, this.pozycjaY - (this.pozycjaX - i), biale, czarne).kolor != this.kolor)
                            this.mozliweBicia.Add(new Point(i, this.pozycjaY - (this.pozycjaX - i)));
                        break;
                    }
                }
            }


            if (this.kierunkiRuchu.Contains(Enums.KierunekRuchu.Bok))
            {
                // ruch w jedna strone
                for (int i = this.pozycjaX - 1; i <= zasieg && i >= 0 && i <= 7; i--)
                {
                    if (getBierkaByPos(i, this.pozycjaY, biale, czarne) == null)
                    {
                        this.mozliweRuchy.Add(new Point(i, this.pozycjaY));
                    }
                    else
                    {
                        if (getBierkaByPos(i, this.pozycjaY, biale, czarne).kolor != this.kolor)
                            this.mozliweBicia.Add(new Point(i, this.pozycjaY));
                        break;
                    }
                }

                // w druga...
                for (int i = this.pozycjaX + 1; i <= zasieg && i >= 0 && i <= 7; i++)
                {
                    if (getBierkaByPos(i, this.pozycjaY, biale, czarne) == null)
                    {
                        this.mozliweRuchy.Add(new Point(i, this.pozycjaY));
                    }
                    else
                    {
                        if (getBierkaByPos(i, this.pozycjaY, biale, czarne).kolor != this.kolor)
                            this.mozliweBicia.Add(new Point(i, this.pozycjaY));
                        break;
                    }
                }
            }


            if (this.GetType() == typeof(Pionek))    // pion jest nietypowy i musi byc obsluzony oddzielnie
            {
                this.mozliweBicia.Clear();
                Point p1;
                Point p2;


                if (this.kolor == Enums.Kolor_pionków.Biale)
                {
                    p1 = new Point(this.pozycjaX - 1, this.pozycjaY - 1);
                    p2 = new Point(this.pozycjaX + 1, this.pozycjaY - 1);
                }
                else
                {
                    p1 = new Point(this.pozycjaX - 1, this.pozycjaY + 1);
                    p2 = new Point(this.pozycjaX + 1, this.pozycjaY + 1);

                }
                if (getBierkaByPos(p1.X, p1.Y, biale, czarne) != null && getBierkaByPos(p1.X, p1.Y, biale, czarne).kolor != this.kolor)
                    this.mozliweBicia.Add(p1);

                if (getBierkaByPos(p2.X, p2.Y, biale, czarne) != null && getBierkaByPos(p2.X, p2.Y, biale, czarne).kolor != this.kolor)
                    this.mozliweBicia.Add(p2);
            }


            if (this.GetType() == typeof(Krol))    // Krol też zachowuje się inaczej niż inne piony, dlatego jest traktowany inaczej
            {
                this.mozliweBicia.Clear();
                this.mozliweRuchy.Clear();
                Point p1, p2, p3, p4, p5, p6, p7, p8;

                p1 = new Point(this.pozycjaX - 1, this.pozycjaY);
                p2 = new Point(this.pozycjaX - 1, this.pozycjaY + 1);
                p3 = new Point(this.pozycjaX - 1, this.pozycjaY - 1);
                p4 = new Point(this.pozycjaX + 1, this.pozycjaY);
                p5 = new Point(this.pozycjaX + 1, this.pozycjaY + 1);
                p6 = new Point(this.pozycjaX + 1, this.pozycjaY - 1);
                p7 = new Point(this.pozycjaX, this.pozycjaY + 1);
                p8 = new Point(this.pozycjaX, this.pozycjaY - 1);

                // sposób na dodanie większej ilościo ręcznie stworzonych obiektów w jednej linii
                this.mozliweRuchy.AddRange(new Point[] { p1, p2, p3, p4, p5, p6, p7, p8 });


                List<Point> doUsuniecia = new List<Point>();

                foreach (Point p in this.mozliweRuchy)
                {
                    Bierka b = getBierkaByPos(p.X, p.Y, biale, czarne);
                    if (b != null)
                    {
                        // jeżeli jakaś bierka zajmuje pole na liście ruchów, to musimy usunąć ten ruch
                        doUsuniecia.Add(p);
                        // za to jeżeli ma inny kolor, to dodajemy to do bić.
                        if (b.kolor != this.kolor) this.mozliweBicia.Add(p);
                    }
                }

                // taki zabieg, bo nie można usuwać obiektów z listy w momencie, kiedy po tej liście idzie pętla foreach
                // logiczne, ale potrafi zaskoczyć :)
                foreach (Point p in doUsuniecia)
                {
                    this.mozliweRuchy.Remove(p);
                }

            }
        }






        public static Bierka getBierkaByPos(int x, int y, List<Bierka> biale, List<Bierka> czarne)
        {
            foreach (Bierka b in biale.Union(czarne))
            {
                if (b.pozycjaX == x && b.pozycjaY == y) return b;
            }
            return null;
        }


        public static void przeliczWszystieRuchy()
        {
            foreach (Bierka b in Gra.bierkiBiale.Union(Gra.bierkiCzarne))
            {
                b.generujMozliweRuchy(Gra.bierkiBiale, Gra.bierkiCzarne);
                b.wyczyscMozliweRuchyZeSmieci();
            }
        }


        public static void przeliczWszystieRuchy(List<Bierka> biale, List<Bierka> czarne)
        {
            foreach (Bierka b in biale.Union(czarne))
            {
                b.generujMozliweRuchy(biale, czarne);

                b.wyczyscMozliweRuchyZeSmieci();
            }
        }

        // funkcja sprawdzająca, czy każda z bierek ma same dobre ruchy
        // jeżeli lista ruchów którejś z bierek wychodzi poza obszar szachownicy
        // lub jeżeli na torze ruchu bierki znajduje się inna bierka, ruch jkest usuwany
        private void wyczyscMozliweRuchyZeSmieci()
        {
            List<Point> ruchyDoUsuniecia = new List<Point>();
            List<Point> biciaDoUsuniecia = new List<Point>();

            foreach (Point p in this.mozliweRuchy)
            {
                if (p.X < 0 || p.X > 7) ruchyDoUsuniecia.Add(p);
                else if (p.Y < 0 || p.Y > 7) ruchyDoUsuniecia.Add(p);
            }


            foreach (Point p in this.mozliweBicia)
            {
                if (p.X < 0 || p.X > 7) biciaDoUsuniecia.Add(p);
                else if (p.Y < 0 || p.Y > 7) biciaDoUsuniecia.Add(p);
            }


            foreach (Point p in ruchyDoUsuniecia)
            {
                try
                {
                    this.mozliweRuchy.Remove(p);
                }
                catch { }
            }

            foreach (Point p in biciaDoUsuniecia)
            {
                try
                {
                    this.mozliweBicia.Remove(p);
                }
                catch { }
            }
        }
        // Paweł Potera & Krzysztof Sakowicz
        static public int getPunktacjaListyBierek(List<Bierka> lista, List<Bierka> biale, List<Bierka> czarne) 
        {
            int ret = 0;

            foreach (Bierka b in lista)// Paweł Potera & Krzysztof Sakowicz
            {
                ret += b.punkty;

                foreach (Point p in b.mozliweBicia)
                {
                    Bierka b1 = getBierkaByPos(p.X, p.Y, biale, czarne);
                    // dodatkowe, w zasadzie oba zbędne warunki - tak w razie czego :)
                    if (b1 != null && b1.kolor != b.kolor) ret += b1.punkty; 
                }
            }
            return ret;
        }


        static public void wygenerujMozliweRuchyKrola()
        {
            // funkcja wywoływana po wykryciue szacha. Odświeża ona możliwe do poruszania się pola dla szachowanego króla.
            Krol k;
            List<Point> doUsuniecia = new List<Point>();

            if (Gracz_Czlowiek.szach)
            {
                foreach (Bierka kr in Gra.bierkiCzarne)
                {
                    if (kr.GetType() == typeof(Krol))
                    {
                        kr.generujMozliweRuchy(Gra.bierkiBiale, Gra.bierkiCzarne);
                        kr.wyczyscMozliweRuchyZeSmieci();

                        foreach (Point pkt in kr.mozliweRuchy)
                        {
                            // brakuje sprawdzenia po biciach krola
                            foreach (Bierka bierkaPrzeciwnika in Gra.bierkiCzarne)
                            {
                                if (bierkaPrzeciwnika.mozliweRuchy.Union(bierkaPrzeciwnika.mozliweBicia).Contains(pkt))
                                {
                                    doUsuniecia.Add(pkt);
                                }
                            }
                        }
                        foreach (Point pointDel in doUsuniecia)
                        {
                            foreach (Bierka kro in Gra.bierkiBiale)
                            {
                                if (kro.GetType() == typeof(Krol)) kro.mozliweRuchy.Remove(pointDel);
                            }
                        }
                        if (kr.mozliweRuchy.Union(kr.mozliweBicia).Count() == 0)
                        {
                            Gracz_Czlowiek.szachMat = true;
                            MessageBox.Show("Mat");
                        }
                    }
                }
            }

            else if (Gracz_Komputer.szach)
            {
                foreach (Bierka kr in Gra.bierkiCzarne)
                {
                    if (kr.GetType() == typeof(Krol))
                    {
                        kr.generujMozliweRuchy(Gra.bierkiBiale, Gra.bierkiCzarne);
                        kr.wyczyscMozliweRuchyZeSmieci();

                        foreach (Point pkt in kr.mozliweRuchy)
                        {
                            foreach (Bierka bierkaPrzeciwnika in Gra.bierkiBiale)
                            {
                                if (bierkaPrzeciwnika.mozliweRuchy.Union(bierkaPrzeciwnika.mozliweBicia).Contains(pkt))
                                {
                                    doUsuniecia.Add(pkt);
                                }
                            }
                        }

                        foreach (Point pointDel in doUsuniecia)
                        {
                            foreach (Bierka kro in Gra.bierkiCzarne)
                            {
                                if (kro.GetType() == typeof(Krol)) kro.mozliweRuchy.Remove(pointDel);
                            }
                        }

                        if (kr.mozliweRuchy.Union(kr.mozliweBicia).Count() == 0)
                        {
                            Gracz_Komputer.szachMat = true;
                            MessageBox.Show("Mat");
                        }
                    }
                }
            }
        }
    }
}

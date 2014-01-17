using System;
using System.Collections.Generic;
using System.Drawing;
using BasicBlue.SzachyBasicBlue;
namespace SzachyBasicBlue {
	public class Gra  {
        public Gra()
        {
            stworzUstawBierki();
            kolejka = Enums.czyjaKolej.Osoba;
        }

        public string Wyniki;
        public string Status;
        public int Wykonane_Ruchy;

        public static string pgnString;
        public static int ileRuchow;


        public static Enums.czyjaKolej kolejka;

        public static List<Bierka> bierkiBiale = new List<Bierka>();
        public static List<Bierka> bierkiCzarne = new List<Bierka>();

        // funkcja tworz�ca dwa zestawy bierek (wywo�ywana na pocz�tku)
        public void stworzUstawBierki()
        {
            for (int i = 0; i < 8; i++)
            {
                Pionek p = new Pionek(Enums.Kolor_pionk�w.Biale, i, 6, false);
                bierkiBiale.Add(p);
                p = new Pionek(Enums.Kolor_pionk�w.Czarne, i, 1, false);
                bierkiCzarne.Add(p);
            }

            Wieza w = new Wieza(Enums.Kolor_pionk�w.Biale, 0, 7, false);
            bierkiBiale.Add(w);
            w = new Wieza(Enums.Kolor_pionk�w.Biale, 7, 7, false);
            bierkiBiale.Add(w);

            w = new Wieza(Enums.Kolor_pionk�w.Czarne, 0, 0, false);
            bierkiCzarne.Add(w);
            w = new Wieza(Enums.Kolor_pionk�w.Czarne, 7, 0, false);
            bierkiCzarne.Add(w);

            Skoczek s = new Skoczek(Enums.Kolor_pionk�w.Biale, 1, 7, false);
            bierkiBiale.Add(s);
            s = new Skoczek(Enums.Kolor_pionk�w.Biale, 6, 7, false);
            bierkiBiale.Add(s);

            s = new Skoczek(Enums.Kolor_pionk�w.Czarne, 1, 0, false);
            bierkiCzarne.Add(s);
            s = new Skoczek(Enums.Kolor_pionk�w.Czarne, 6, 0, false);
            bierkiCzarne.Add(s);

            Goniec g = new Goniec(Enums.Kolor_pionk�w.Biale, 2, 7, false);
            bierkiBiale.Add(g);
            g = new Goniec(Enums.Kolor_pionk�w.Biale, 5, 7, false);
            bierkiBiale.Add(g);

            g = new Goniec(Enums.Kolor_pionk�w.Czarne, 2, 0, false);
            bierkiCzarne.Add(g);
            g = new Goniec(Enums.Kolor_pionk�w.Czarne, 5, 0, false);
            bierkiCzarne.Add(g);

            Hetman h = new Hetman(Enums.Kolor_pionk�w.Biale, 3, 7, false);
            bierkiBiale.Add(h);
            h = new Hetman(Enums.Kolor_pionk�w.Czarne, 3, 0, false);
            bierkiCzarne.Add(h);

            Krol k = new Krol(Enums.Kolor_pionk�w.Biale, 4, 7, false);
            bierkiBiale.Add(k);
            k = new Krol(Enums.Kolor_pionk�w.Czarne, 4, 0, false);
            bierkiCzarne.Add(k);
        }

        // trzy poni�sze funkcje wspomagaj� prze�o�enie zapisu wsp�rz�dnych X-Y na zapis A7, B2 itd.
        static public string tlumaczNazwePola(int x, int y)
        {
            string ret = (8 - x).ToString() + tlumaczNazweKolumny(y);
            return ret;
        }

        static public string tlumaczNazwePolaPGN(int x, int y)
        {
            string ret = tlumaczNazweKolumny(x) + (8 - y).ToString();
            return ret;
        }

        public static string tlumaczNazweKolumny(int y)
        {
            if (y == 0) return "A";
            else if (y == 1) return "B";
            else if (y == 2) return "C";
            else if (y == 3) return "D";
            else if (y == 4) return "E";
            else if (y == 5) return "F";
            else if (y == 6) return "G";
            else return "H";
        }

        public static void ustawSzach(List<Bierka> biale, List<Bierka> czarne)
        {
            Bierka.przeliczWszystieRuchy(biale, czarne);

            // sprawdzamy, czy komputer zaszachowa� osob�
            foreach (Bierka bi in biale)
            {
                foreach (Point poi in bi.mozliweBicia)
                {
                    Bierka dozbicia = Bierka.getBierkaByPos(poi.X, poi.Y, biale, czarne);
                    if (dozbicia.GetType() == typeof(Krol))
                    {
                        Gracz_Komputer.szach = true;
                    }
                }
            }

            // czy osoba zaszachowa�a PC ?
            foreach (Bierka bi in czarne)
            {
                foreach (Point poi in bi.mozliweBicia)
                {
                    Bierka dozbicia = Bierka.getBierkaByPos(poi.X, poi.Y, biale, czarne);
                    if (dozbicia.GetType() == typeof(Krol))
                    {
                        Gracz_Czlowiek.szach = true;
                    }
                }

            }
        }

	}

}

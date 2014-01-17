using System;
using System.Linq;
using System.Collections.Generic;
using BasicBlue.SzachyBasicBlue;
using System.Drawing;
namespace SzachyBasicBlue
{
    public class Gracz_Komputer     //klasa opisujπca zachowanie komputera
    {

        public static bool szach = false;       //na poczπtku musimy oprogramowaÊ sytuacje specjalne szach i mat, ktÛre oczywiúcie na poczπtku gry nie majπ miejsca
        public static bool szachMat = false;    

        public static bool Zrob_RuchLosowy()
        {
          
            if (Gra.bierkiCzarne.Count == 0) return false;      //jeøeli komputer nie ma bierek to nie wykona ruchu losowego


            if (Gracz_Komputer.szach)       //najpierw sprawdzamy czy zaistnia≥ szach - jest on obs≥ugiwany priorytetowo przed jakimkolwiek innym ruchem i gdy zaistnieje prubujemy przesuniπÊ krÛla
            {
                foreach (Bierka krol in Gra.bierkiCzarne)
                {
                    if (krol.GetType() == typeof(Krol))
                    {
                        krol.przesun(krol.mozliweRuchy[0].X, krol.mozliweRuchy[0].Y, true);
                        Gracz_Komputer.szach = false;
                    }
                }
            }
                                                             //jeøeli nie zaistnia≥ szach to szukamy moøliwoúci bicia

            Bierka bi = czyMoznaBic(Enums.Gracz.Komputer);   //najpierw sprawdzamy czy bicie jest moøliwe
            if (bi != null)     //jeøeli jest to bijemy
            {
                bi.zbij(Bierka.getBierkaByPos(bi.mozliweBicia[0].X, bi.mozliweBicia[0].Y, Gra.bierkiBiale, Gra.bierkiCzarne));
                Gra.kolejka = Enums.czyjaKolej.Osoba;
                Bierka.przeliczWszystieRuchy();
            }
            else
            {                   //jeøeli nie to prÛbujemy wykonaÊ ruch dla losowej bierki
                bool udany = false;
                Random r = new Random();
                while (!udany)  //ale oczywiúcie ten ruch musi byÊ moøliwy
                {
                    Bierka b = Gra.bierkiCzarne[r.Next(Gra.bierkiCzarne.Count)];
                    if (b.mozliweRuchy.Count > 0)
                    {
                        int rnd = r.Next(b.mozliweRuchy.Count);
                        if (b.przesun(b.mozliweRuchy[rnd].X, b.mozliweRuchy[rnd].Y, true))
                        {
                            Gra.kolejka = Enums.czyjaKolej.Osoba;
                            Bierka.przeliczWszystieRuchy();
                            udany = true;
                            break;
                        }
                    }
                }
            }
            return true;
        }

        // czy gracz jest w posiadaniu bierki, ktÛra moøe wykonaÊ bicie
        public static Bierka czyMoznaBic(Enums.Gracz g)
        {
            if (g == Enums.Gracz.Komputer)
            {

                foreach (Bierka b in Gra.bierkiCzarne)
                {
                    if (b.mozliweBicia.Count > 0) return b;
                }
            }
            else
            {
                foreach (Bierka b in Gra.bierkiBiale)
                {
                    if (b.mozliweBicia.Count > 0) return b;
                }
            }
            return null;
        }


        public static RuchDrzewo obliczRuchAI(List<Bierka> GraBiale, List<Bierka> GraCzarne)
        {
            List<RuchDrzewo> listaRuchow = new List<RuchDrzewo>();
            List<Bierka> listaCzarneNieRuszac = new List<Bierka>();
            listaCzarneNieRuszac = Tools.klonujBierki(GraCzarne);

            foreach (Bierka b1 in listaCzarneNieRuszac)
            {
                List<Point> listaP = new List<Point>();
                listaP = Tools.klonujPointy(b1.mozliweRuchy);

                List<Point> listaP1 = new List<Point>();
                listaP1 = Tools.klonujPointy(b1.mozliweBicia);

                foreach (Point p1 in listaP.Union(listaP1))
                {
                    bool biciePierwsze = false;
                    Bierka zbita = new Bierka();

                    List<Bierka> listaBiale1 = new List<Bierka>();
                    listaBiale1 = Tools.klonujBierki(GraBiale);
                    List<Bierka> listaCzarne1 = new List<Bierka>();
                    listaCzarne1 = Tools.klonujBierki(GraCzarne);




                    int xPoprzednie = b1.pozycjaX;
                    int yPoprzednie = b1.pozycjaY;


                    if (b1.mozliweBicia.Contains(p1))
                    {
                        zbita = Bierka.getBierkaByPos(p1.X, p1.Y, listaBiale1, listaCzarne1);
                        listaBiale1.Remove(zbita);
                        biciePierwsze = true;
                    }
                    else b1.przesun(p1.X, p1.Y, false);

             
                    Bierka.przeliczWszystieRuchy(listaBiale1, listaCzarne1);
                    RuchDrzewo rd = Gracz_Czlowiek.wykonajNajlepszyRuch(listaBiale1, listaCzarne1);
                    Bierka.przeliczWszystieRuchy(listaBiale1, listaCzarne1);

                   




                    List<Bierka> listaBiale2 = new List<Bierka>();
                    listaBiale2 = Tools.klonujBierki(listaBiale1);
                    List<Bierka> listaCzarne2 = new List<Bierka>();
                    listaCzarne2 = Tools.klonujBierki(listaCzarne1);


                    foreach (Bierka b2 in listaCzarne2)
                    {
                        List<Point> listaP2 = new List<Point>();
                        listaP2 = Tools.klonujPointy(b2.mozliweRuchy);

                        List<Point> listaP3 = new List<Point>();
                        listaP3 = Tools.klonujPointy(b2.mozliweBicia);

                        foreach (Point p2 in listaP2.Union(listaP3))
                        {
                            bool bicie = false;
                            zbita = new Bierka();

                            int x = b2.pozycjaX;
                            int y = b2.pozycjaY;

                            if (b2.mozliweBicia.Contains(p2))
                            {
                                zbita = Bierka.getBierkaByPos(p2.X, p2.Y, listaBiale2, listaCzarne2);
                                listaBiale2.Remove(zbita);
                                bicie = true;
                            }
                            else b2.przesun(p2.X, p2.Y, false);

                            
                            Bierka.przeliczWszystieRuchy(listaBiale2, listaCzarne2);


                            List<Bierka> listaBialeOstatnia = new List<Bierka>();
                            listaBialeOstatnia = Tools.klonujBierki(listaBiale2);
                            List<Bierka> listaCzarneOstatnia = new List<Bierka>();
                            listaCzarneOstatnia = Tools.klonujBierki(listaCzarne2);


                            rd = Gracz_Czlowiek.wykonajNajlepszyRuch(listaBialeOstatnia, listaCzarneOstatnia);
                            Bierka.przeliczWszystieRuchy(listaBialeOstatnia, listaCzarneOstatnia);


                            int bialePkt = Bierka.getPunktacjaListyBierek(listaBialeOstatnia, listaBialeOstatnia, listaCzarneOstatnia);
                            int czarnePkt = Bierka.getPunktacjaListyBierek(listaCzarneOstatnia, listaBialeOstatnia, listaCzarneOstatnia);

                            b1.pozycjaX = xPoprzednie;
                            b1.pozycjaY = yPoprzednie;
                            listaRuchow.Add(new RuchDrzewo(b1, p1, czarnePkt - bialePkt, biciePierwsze));


                            if (bicie) listaBiale2.Add(zbita);
                            else
                            {
                                b2.pozycjaX = x;
                                b2.pozycjaY = y;
                            }
                        }
                    }
                }
            }

            int max = 0;
            RuchDrzewo wybranyRuch = new RuchDrzewo(); // = listaRuchow[0];   // gdy jest remis

            if (listaRuchow.Count > 0) wybranyRuch = listaRuchow[0];  // gdy remis punktowy

            foreach (RuchDrzewo ruch in listaRuchow)
            {
                if (ruch.ile > max)
                {
                    max = ruch.ile;
                    wybranyRuch = ruch;
                }
            }

            // jak wszystko skonczone to przejrzyj liste elementow i znajdü max.
            return wybranyRuch;
        }
    }
}
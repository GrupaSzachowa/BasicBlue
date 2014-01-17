using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using BasicBlue.SzachyBasicBlue;
namespace SzachyBasicBlue {
    public class Gracz_Czlowiek
    {

        public static bool szach = false;
        public static bool szachMat = false;

        static public RuchDrzewo wykonajNajlepszyRuch(List<Bierka> listaBiale, List<Bierka> listaCzarne)
        {
            int punktyMax = 0;
            Bierka wybrana = new Bierka();
            Point wybranyRuch = new Point();
            bool czyBicie = false;

          //  List<Bierka> listaBiale = new List<Bierka>();
          //  listaBiale = Tools.klonujBierki(Gra.bierkiBiale);

            foreach (Bierka b in listaBiale)
            {
                List<Point> listaP = new List<Point>();
                listaP = Tools.klonujPointy(b.mozliweRuchy);

                List<Point> listaP1 = new List<Point>();
                listaP1 = Tools.klonujPointy(b.mozliweBicia);
                
                int x = b.pozycjaX;
                int y = b.pozycjaY;

                foreach (Point p in listaP.Union(listaP1))
                {
                    bool bicie = false;
                   // List<Bierka> listaczarne = new List<Bierka>();
                   // listaczarne = Tools.klonujBierki(Gra.bierkiCzarne);
                    Bierka zbita = null;
                    bicie = false;

                    if (b.mozliweBicia.Contains(p))
                    {
                       zbita = Bierka.getBierkaByPos(p.X, p.Y, listaBiale, listaCzarne);
                       listaCzarne.Remove(zbita);
                       bicie = true;
                    }
                    else b.przesun(p.X, p.Y, false);

                    Bierka.przeliczWszystieRuchy(listaBiale, listaCzarne);
                    
                    int wynikBiale = Bierka.getPunktacjaListyBierek(listaBiale, listaBiale, listaCzarne);
                    int wynikCzarne = Bierka.getPunktacjaListyBierek(listaCzarne, listaBiale, listaCzarne);

                    int roznica = wynikBiale - wynikCzarne;

                    if (punktyMax < roznica)
                    {
                        punktyMax = roznica;
                        wybrana = b;
                        wybranyRuch = p;
                        czyBicie = bicie;
                    }

                    // przywracam zbit¹ bierkê
                    if (zbita != null) listaCzarne.Add(zbita);

                    // cofam bierkê
                    b.pozycjaX = x;
                    b.pozycjaY = y;
                    Bierka.przeliczWszystieRuchy(listaBiale, listaCzarne);
                }
            }

            string pozycjaWybranej = Gra.tlumaczNazwePolaPGN(wybrana.pozycjaX, wybrana.pozycjaY);
            string pozycjadocelowa = Gra.tlumaczNazwePolaPGN(wybranyRuch.X, wybranyRuch.Y);
           
            RuchDrzewo ruch = new RuchDrzewo(wybrana, wybranyRuch, punktyMax, czyBicie);
            
            
            return ruch;


        }



	}

}

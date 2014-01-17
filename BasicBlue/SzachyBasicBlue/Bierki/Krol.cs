using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BasicBlue.SzachyBasicBlue;
namespace SzachyBasicBlue {
	public class Krol : Bierka  {

        public Krol(Enums.Kolor_pionków kolor, int x, int y, bool ruch)
        {
            if (kolor == Enums.Kolor_pionków.Biale)
            {
                this.grafika = BasicBlue.Properties.Resources.krolWhite;
                this.kolor = Enums.Kolor_pionków.Biale;
            }
            else
            {
                this.grafika = BasicBlue.Properties.Resources.krolBlack;
                this.kolor = Enums.Kolor_pionków.Czarne;
            }
            this.pozycjaX = x;
            this.pozycjaY = y;
            this.zasieg = 1;
            this.punkty = 0;
            this.litera = "K";
            this.bylRuch = ruch;

            kierunkiRuchu.Add(Enums.KierunekRuchu.Skos);
            kierunkiRuchu.Add(Enums.KierunekRuchu.Gora);
            kierunkiRuchu.Add(Enums.KierunekRuchu.Dol);
            kierunkiRuchu.Add(Enums.KierunekRuchu.Bok);

            kierunekBicia.Add(Enums.KierunekRuchu.Gora);
            kierunekBicia.Add(Enums.KierunekRuchu.Dol);
            kierunekBicia.Add(Enums.KierunekRuchu.Bok);
            kierunekBicia.Add(Enums.KierunekRuchu.Skos);
        }
        
        public Krol()
        {
           
        }


           static public List<Enums.Roszada> zwrocDostepneRoszady()
        {
               List<Enums.Roszada> lista = new List<Enums.Roszada>();
               List<Bierka> listaWiez = new List<Bierka>();
               bool czyRuchKrola = false;
               bool lewa = false;
               bool prawa = false;

            foreach (Bierka b in Gra.bierkiBiale)
            {
                // je¿eli ruszyl sie krol, to na pewno nie mo¿na zrobiæ roszady
                if (b.GetType() == typeof(Krol))
                {
                    if (b.bylRuch) czyRuchKrola = true;
                }
            }
            if (czyRuchKrola) return null;
            else
            {
                foreach (Bierka b in Gra.bierkiBiale)
                {
                    // je¿eli ruszyl sie krol, to na pewno nie mo¿na zrobiæ roszady
                    if (b.GetType() == typeof(Wieza))
                    {
                        if (!b.bylRuch) 
                        {
                            listaWiez.Add(b);
                        }
                    }
                }
            }


            foreach (Bierka wie in listaWiez)
            {
                if (wie.pozycjaX == 0) lewa = true;
                else if (wie.pozycjaY == 7) prawa = true;
            }



               // bierki przeszkadzajace w duzej roszadzie
            Bierka b1 = getBierkaByPos(1, 7, Gra.bierkiBiale, Gra.bierkiCzarne);
            Bierka b2 = getBierkaByPos(2, 7, Gra.bierkiBiale, Gra.bierkiCzarne);
            Bierka b3 = getBierkaByPos(3, 7, Gra.bierkiBiale, Gra.bierkiCzarne);

               // bierki przeszkadzajace w malej roszadzie
            Bierka b4 = getBierkaByPos(5, 7, Gra.bierkiBiale, Gra.bierkiCzarne);
            Bierka b5 = getBierkaByPos(6, 7, Gra.bierkiBiale, Gra.bierkiCzarne);


            if (b1 == null && b2 == null && b3 == null && lewa)
            {
                // mozna zrobic dluga roszade
                lista.Add(Enums.Roszada.Dluga);
            }

            if (b4 == null && b5 == null && prawa)
            {
                // mozna zrobic krotka roszade
                lista.Add(Enums.Roszada.Krotka);
            }

            return lista;
               
       
        }

	}

}

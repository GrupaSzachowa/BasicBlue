using System;
using System.Drawing;
using BasicBlue.SzachyBasicBlue;
namespace SzachyBasicBlue {
	public class Hetman : Bierka  {



     

        public Hetman(Enums.Kolor_pionków kolor, int x, int y, bool ruch)
        {
            if (kolor == Enums.Kolor_pionków.Biale)
            {
                this.grafika = BasicBlue.Properties.Resources.HetmanWhite;
                this.kolor = Enums.Kolor_pionków.Biale;
            }
            else
            {
                this.grafika = BasicBlue.Properties.Resources.hetmanBlack;
                this.kolor = Enums.Kolor_pionków.Czarne;
            }
            this.pozycjaX = x;
            this.pozycjaY = y;
            this.zasieg = 8;
            this.punkty = 400;
            this.litera = "Q";
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
           public Hetman()
        {
           
        }
	}

}

using System;
using BasicBlue.SzachyBasicBlue;
namespace SzachyBasicBlue {
	public class Goniec : Bierka  {


        public Goniec(Enums.Kolor_pionk�w kolor, int x, int y, bool ruch)
        {
            if (kolor == Enums.Kolor_pionk�w.Biale)
            {
                this.grafika = BasicBlue.Properties.Resources.goniecWhite;
                this.kolor = Enums.Kolor_pionk�w.Biale;
            }
            else
            {
                this.grafika = BasicBlue.Properties.Resources.goniecBlack;
                this.kolor = Enums.Kolor_pionk�w.Czarne;
            }
            this.pozycjaX = x;
            this.pozycjaY = y;
            this.zasieg = 8;
            this.punkty = 50;
            this.litera = "B";
            this.bylRuch = ruch;

            kierunkiRuchu.Add(Enums.KierunekRuchu.Skos);
            kierunekBicia.Add(Enums.KierunekRuchu.Skos);
        }

           public Goniec()
        {
           
        }
	}
}

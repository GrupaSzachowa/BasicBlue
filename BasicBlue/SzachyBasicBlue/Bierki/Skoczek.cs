using System;
using BasicBlue.SzachyBasicBlue;
namespace SzachyBasicBlue {
	public class Skoczek : Bierka  {
        public Skoczek()
        {
           
        }

        public Skoczek(Enums.Kolor_pionk�w kolor, int x, int y,bool ruch)
        {
            if (kolor == Enums.Kolor_pionk�w.Biale)
            {
                this.grafika = BasicBlue.Properties.Resources.skoczekWhite;
                this.kolor = Enums.Kolor_pionk�w.Biale;
            }
            else
            {
                this.grafika = BasicBlue.Properties.Resources.skoczekBlack;
                this.kolor = Enums.Kolor_pionk�w.Czarne;
            }


            this.pozycjaX = x;
            this.pozycjaY = y;
            this.zasieg = 3;
            this.punkty = 50;
            this.litera = "N";
            this.bylRuch = ruch;

            kierunkiRuchu.Add(Enums.KierunekRuchu.ProstoSkos);
            kierunekBicia.Add(Enums.KierunekRuchu.ProstoSkos);
        }

	}

}

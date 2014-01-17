using System;
using BasicBlue.SzachyBasicBlue;
namespace SzachyBasicBlue {
	public class Wieza : Bierka  {


        public Wieza()
        {
           
        }


        public Wieza(Enums.Kolor_pionk�w kolor, int x, int y, bool ruch)
        {
            if (kolor == Enums.Kolor_pionk�w.Biale)
            {
                this.grafika = BasicBlue.Properties.Resources.wiezaWhite;
                this.kolor = Enums.Kolor_pionk�w.Biale;
            }
            else
            {
                this.grafika = BasicBlue.Properties.Resources.wiezaBlack;
                this.kolor = Enums.Kolor_pionk�w.Czarne;
            }
            this.pozycjaX = x;
            this.pozycjaY = y;
            this.zasieg = 8;
            this.punkty = 100;
            this.litera = "R";
            this.bylRuch = ruch;

            kierunkiRuchu.Add(Enums.KierunekRuchu.Gora);
            kierunkiRuchu.Add(Enums.KierunekRuchu.Dol);
            kierunkiRuchu.Add(Enums.KierunekRuchu.Bok);
            kierunekBicia.Add(Enums.KierunekRuchu.Gora);
            kierunekBicia.Add(Enums.KierunekRuchu.Dol);
            kierunekBicia.Add(Enums.KierunekRuchu.Bok);
        }



	}

}

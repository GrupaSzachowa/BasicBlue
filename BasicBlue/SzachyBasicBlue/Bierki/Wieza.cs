using System;
using BasicBlue.SzachyBasicBlue;
namespace SzachyBasicBlue
{
    public class Wieza : Bierka
    {


        public Wieza()
        {

        }


        public Wieza(Enums.Kolor_pionków kolor, int x, int y, bool ruch)
        {
            if (kolor == Enums.Kolor_pionków.Biale)
            {
                this.grafika = BasicBlue.Properties.Resources.wiezaWhite;
                this.kolor = Enums.Kolor_pionków.Biale;
            }
            else
            {
                this.grafika = BasicBlue.Properties.Resources.wiezaBlack;
                this.kolor = Enums.Kolor_pionków.Czarne;
            }
            this.pozycjaX = x;
            this.pozycjaY = y;
            this.zasieg = 8;
            this.punkty = 100;// Paweł Potera & Krzysztof Sakowicz
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

using System;
using BasicBlue.SzachyBasicBlue;
namespace SzachyBasicBlue
{
    public class Goniec : Bierka
    {


        public Goniec(Enums.Kolor_pionków kolor, int x, int y, bool ruch)
        {
            if (kolor == Enums.Kolor_pionków.Biale)
            {
                this.grafika = BasicBlue.Properties.Resources.goniecWhite;
                this.kolor = Enums.Kolor_pionków.Biale;
            }
            else
            {
                this.grafika = BasicBlue.Properties.Resources.goniecBlack;
                this.kolor = Enums.Kolor_pionków.Czarne;
            }
            this.pozycjaX = x;
            this.pozycjaY = y;
            this.zasieg = 8;
            this.punkty = 50;// Paweł Potera & Krzysztof Sakowicz
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

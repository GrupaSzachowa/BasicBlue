using System;
using BasicBlue.SzachyBasicBlue;
namespace SzachyBasicBlue
{
    public class Skoczek : Bierka
    {
        public Skoczek()
        {

        }

        public Skoczek(Enums.Kolor_pionków kolor, int x, int y, bool ruch)
        {
            if (kolor == Enums.Kolor_pionków.Biale)
            {
                this.grafika = BasicBlue.Properties.Resources.skoczekWhite;
                this.kolor = Enums.Kolor_pionków.Biale;
            }
            else
            {
                this.grafika = BasicBlue.Properties.Resources.skoczekBlack;
                this.kolor = Enums.Kolor_pionków.Czarne;
            }


            this.pozycjaX = x;
            this.pozycjaY = y;
            this.zasieg = 3;
            this.punkty = 50;// Paweł Potera & Krzysztof Sakowicz
            this.litera = "N";
            this.bylRuch = ruch;

            kierunkiRuchu.Add(Enums.KierunekRuchu.ProstoSkos);
            kierunekBicia.Add(Enums.KierunekRuchu.ProstoSkos);
        }

    }

}

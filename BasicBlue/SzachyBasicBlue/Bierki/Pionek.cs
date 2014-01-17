using System;
using BasicBlue.SzachyBasicBlue;
namespace SzachyBasicBlue
{
    public class Pionek : Bierka
    {
        public Enums.KierunekRuchu Kierunek_Ruchu;


        public Pionek(Enums.Kolor_pionków kolor, int x, int y, bool ruch)
        {
            if (kolor == Enums.Kolor_pionków.Biale)
            {
                this.grafika = BasicBlue.Properties.Resources.pionWhite;
                this.kolor = Enums.Kolor_pionków.Biale;
            }
            else
            {
                this.grafika = BasicBlue.Properties.Resources.pionBlack;
                this.kolor = Enums.Kolor_pionków.Czarne;
            }
            this.pozycjaX = x;
            this.pozycjaY = y;
            this.zasieg = 1;
            this.punkty = 10;// Paweł Potera & Krzysztof Sakowicz
            this.bylRuch = ruch;

            kierunkiRuchu.Add(Enums.KierunekRuchu.Gora);
            kierunekBicia.Add(Enums.KierunekRuchu.Skos);
        }
        public Pionek()
        {

        }
    }
}

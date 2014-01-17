using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicBlue.SzachyBasicBlue
{
    public class Enums
    {
        public enum Gracz
        {
            Osoba,
            Komputer
        }

        public enum czyjaKolej
        {
            Osoba,
            Komputer
        }

        public enum KierunekRuchu
        {
            Gora,
            Dol,
            Skos,
            Bok,
            ProstoSkos     // skoczek 
        }

        public enum Roszada
        {
            Krotka,
            Dluga
        }


        public enum Kolor_Gracza
        {
            Czarny,
            Biały,
        }



        public enum Kolor_pionków
        {
            Czarne,
            Biale,
        }

        public enum WynikGry
        {
            CzarneWygrały,
            BiałeWygrały,
            Remis,

        }

    }
}

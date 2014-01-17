using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzachyBasicBlue;

namespace BasicBlue.SzachyBasicBlue
{
    class LosujBierki//Artur Zajączkowski + Adam Sieradzki + Michał Łuczyński
    {



        public static List<Bierka> zwrocListe(int ile, List<Bierka> lista)
        {
            while (lista.Count > ile)
            {
                Random rnd = new Random();
                int r = rnd.Next(lista.Count);
                lista.RemoveAt(r);
            }
            return lista;
        }



    }
}

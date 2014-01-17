using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SzachyBasicBlue;

namespace BasicBlue.SzachyBasicBlue
{
    class ZapisOdczyt
    {

        public static void zapiszGre(string sciezka, List<Bierka> biale, List<Bierka> czarne)
        {
            try
            {
                string linie = string.Empty;
                foreach (Bierka b in Gra.bierkiCzarne.Union(Gra.bierkiBiale))
                {
                    linie += b.kolor.ToString() + ";" + b.ToString() + ";" + b.pozycjaX + ";" + b.pozycjaY + ";" + b.bylRuch + "\r\n";
                }
                linie += Gra.pgnString;

                StreamWriter file = new System.IO.StreamWriter("d:\\szachySave.chess");
                file.WriteLine(linie);

                file.Close();
                MessageBox.Show("Zapis się powiódł.");
            }
            catch
            {
                MessageBox.Show("Zapis się nie powiódł.");
            }
        }




        public static void wczytajGre()
        {
            string linijka = string.Empty;
            Gra.bierkiBiale.Clear();
            Gra.bierkiCzarne.Clear();

            System.IO.StreamReader file = new System.IO.StreamReader("d:\\szachySave.chess");

            // odczyt i przetwarzanie każdej linii pliku osobno
            while ((linijka = file.ReadLine()) != null)
            {
                string[] tab = linijka.Split(';');

                if (tab.Length == 5)
                {
                    string typBierki = tab[1];
                    string kolor = tab[0];
                    int x = int.Parse(tab[2]);
                    int y = int.Parse(tab[3]);
                    string ruch = tab[4];
                    Enums.Kolor_pionków k = Enums.Kolor_pionków.Biale;
                    bool bylRuch = false;

                    if (ruch == "True") bylRuch = true;


                    if (kolor == "Czarne") k = Enums.Kolor_pionków.Czarne;
                    else if (kolor == "Biale") k = Enums.Kolor_pionków.Biale;

                    if (typBierki == "Pionek")
                    {
                        if (k == Enums.Kolor_pionków.Biale) Gra.bierkiBiale.Add(new Pionek(k, x, y, bylRuch));
                        else Gra.bierkiCzarne.Add(new Pionek(k, x, y, bylRuch));
                    }
                    else if (typBierki == "Wieza")
                    {
                        if (k == Enums.Kolor_pionków.Biale) Gra.bierkiBiale.Add(new Wieza(k, x, y, bylRuch));
                        else Gra.bierkiCzarne.Add(new Wieza(k, x, y, bylRuch));
                    }
                    else if (typBierki == "Skoczek")
                    {
                        if (k == Enums.Kolor_pionków.Biale) Gra.bierkiBiale.Add(new Skoczek(k, x, y, bylRuch));
                        else Gra.bierkiCzarne.Add(new Skoczek(k, x, y, bylRuch));
                    }
                    else if (typBierki == "Goniec")
                    {
                        if (k == Enums.Kolor_pionków.Biale) Gra.bierkiBiale.Add(new Goniec(k, x, y, bylRuch));
                        else Gra.bierkiCzarne.Add(new Goniec(k, x, y, bylRuch));
                    }
                    else if (typBierki == "Hetman")
                    {
                        if (k == Enums.Kolor_pionków.Biale) Gra.bierkiBiale.Add(new Hetman(k, x, y, bylRuch));
                        else Gra.bierkiCzarne.Add(new Hetman(k, x, y, bylRuch));
                    }

                    else if (typBierki == "Krol")
                    {
                        if (k == Enums.Kolor_pionków.Biale) Gra.bierkiBiale.Add(new Krol(k, x, y, bylRuch));
                        else Gra.bierkiCzarne.Add(new Krol(k, x, y, bylRuch));
                    }
                }
                else if (linijka.Contains("1."))
                {
                    Gra.pgnString = linijka;
                }

            }
            file.Close();
            Bierka.przeliczWszystieRuchy();
            Gra.kolejka = Enums.czyjaKolej.Osoba;
        }
    }
}

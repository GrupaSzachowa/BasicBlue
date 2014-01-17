using System;
using System.Collections.Generic;
using BasicBlue.SzachyBasicBlue;
namespace SzachyBasicBlue {
	public class Gra  {
		public string Wyniki;
		public string Status;
		public int Wykonane_Ruchy;
		/// <summary>
		/// Nazwa gracza
		/// </summary>
		public Gracz[] Gracz;
		public bool Zapis;
		public bool Odczyt;

        public static Enums.czyjaKolej kolejka;

        public static List<Bierka> bierkiBiale = new List<Bierka>();
        public static List<Bierka> bierkiCzarne = new List<Bierka>();
        // funkcja tworz¹ca dwa zestawy bierek (wywyo³ywana na pocz¹tku)
        
        public void stworzUstawBierki()
        {
            for (int i = 0; i < 8; i++)
            {
                Pionek p = new Pionek(Enums.Kolor_pionków.Biale, i, 6, false);
                bierkiBiale.Add(p);
                p = new Pionek(Enums.Kolor_pionków.Czarne, i, 1, false);
                bierkiCzarne.Add(p);
            }

            Wieza w = new Wieza(Enums.Kolor_pionków.Biale, 0, 7, false);
            bierkiBiale.Add(w);
            w = new Wieza(Enums.Kolor_pionków.Biale, 7, 7, false);
            bierkiBiale.Add(w);

            w = new Wieza(Enums.Kolor_pionków.Czarne, 0, 0, false);
            bierkiCzarne.Add(w);
            w = new Wieza(Enums.Kolor_pionków.Czarne, 7, 0, false);
            bierkiCzarne.Add(w);

            Skoczek s = new Skoczek(Enums.Kolor_pionków.Biale, 1, 7, false);
            bierkiBiale.Add(s);
            s = new Skoczek(Enums.Kolor_pionków.Biale, 6, 7, false);
            bierkiBiale.Add(s);

            s = new Skoczek(Enums.Kolor_pionków.Czarne, 1, 0, false);
            bierkiCzarne.Add(s);
            s = new Skoczek(Enums.Kolor_pionków.Czarne, 6, 0, false);
            bierkiCzarne.Add(s);

            Goniec g = new Goniec(Enums.Kolor_pionków.Biale, 2, 7, false);
            bierkiBiale.Add(g);
            g = new Goniec(Enums.Kolor_pionków.Biale, 5, 7, false);
            bierkiBiale.Add(g);

            g = new Goniec(Enums.Kolor_pionków.Czarne, 2, 0, false);
            bierkiCzarne.Add(g);
            g = new Goniec(Enums.Kolor_pionków.Czarne, 5, 0, false);
            bierkiCzarne.Add(g);

            Hetman h = new Hetman(Enums.Kolor_pionków.Biale, 3, 7, false);
            bierkiBiale.Add(h);
            h = new Hetman(Enums.Kolor_pionków.Czarne, 3, 0, false);
            bierkiCzarne.Add(h);

            Krol k = new Krol(Enums.Kolor_pionków.Biale, 4, 7, false);
            bierkiBiale.Add(k);
            k = new Krol(Enums.Kolor_pionków.Czarne, 4, 0, false);
            bierkiCzarne.Add(k);
        }
		public void Dodaj_Ruch() {
			throw new System.Exception("Not implemented");
		}
		public void Zakoñczona() {
			throw new System.Exception("Not implemented");
		}
		public void JestSzachMat() {
			throw new System.Exception("Not implemented");
		}
		public void Operacje() {
			throw new System.Exception("Not implemented");
		}
		public void Jest_Pat() {
			throw new System.Exception("Not implemented");
		}
		public void Jest_Szach() {
			throw new System.Exception("Not implemented");
		}

        private Historia_Ruchow historia;
		private ZapisGry zapisGry;
		private OdczytGry odczytGry;
		private Gracz gracz;

		private Bierka bierka;

	}

}

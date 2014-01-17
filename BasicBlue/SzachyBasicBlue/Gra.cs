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

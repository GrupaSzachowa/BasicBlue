using System;
namespace SzachyBasicBlue {
	public class Gra : WynikGry  {
		public string Wyniki;
		public string Status;
		public int Wykonane_Ruchy;
		/// <summary>
		/// Nazwa gracza
		/// </summary>
		public Gracz[] Gracz;
		public bool Zapis;
		public bool Odczyt;

		public void Dodaj_Ruch() {
			throw new System.Exception("Not implemented");
		}
		public void Zako�czona() {
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

		private Historia_Ruch�w historia Ruch�w;
		private ZapisGry zapisGry;
		private OdczytGry odczytGry;
		private Gracz gracz;

		private Bierka bierka;

	}

}

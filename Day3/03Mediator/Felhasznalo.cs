using System;

namespace _03Mediator
{
    public class Felhasznalo : IMegfigyelo
    {
        public string Nev { get; internal set; }
        public Felhasznalo(string nev)
        {
            this.Nev = nev;
        }

        public UzenoSzoba UzenetKozpont { get; set; }

        public void UzenetKuldes(string cimzett, string uzenet)
        {
            UzenetKozpont.Uzenet(kuldo: Nev, cimzett: cimzett, uzenet: uzenet);
        }

        public void Uzenet(string kuldo, string uzenet)
        {
            Console.WriteLine("küldő: {0}, címzett: {1}, uzenet: {2}", kuldo, Nev, uzenet);
        }
    }

    public interface IMegfigyelo
    {
        /// <summary>
        /// Csak lekérdezni akarjuk a kereséshez a címzésnél, így
        /// setter nem kell
        /// </summary>
        string Nev { get; }
        UzenoSzoba UzenetKozpont { get; set; }
        void Uzenet(string kuldo, string uzenet);
    }
}
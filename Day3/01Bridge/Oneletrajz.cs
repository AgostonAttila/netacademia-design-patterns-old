using System;

namespace _01Bridge
{
    public class Oneletrajz : Dokumentum
    {
        public Oneletrajz(IFormazo formazo) 
            : base(formazo)
        { }

        public string[] Munkahelyek { get; set; }
        public string Nev { get; set; }
        public string[] Tanulmanyok { get; set; }

        public override void Print()
        {
            Console.WriteLine(formazo.Formazas("Név", Nev));
            foreach (var tanulmany in Tanulmanyok)
            {
                Console.WriteLine(formazo.Formazas("  Iskola", tanulmany));
            }
        }
    }

    //public class Oneletrajz2 : Oneletrajz
    //{ }

    //public class Oneletrajz3 : Oneletrajz
    //{ }
}
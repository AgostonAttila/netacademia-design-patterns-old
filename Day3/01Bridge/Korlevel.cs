using System;

namespace _01Bridge
{
    public class Korlevel : Dokumentum
    {
        public Korlevel(IFormazo formazo) 
            : base(formazo)
        { }

        public DateTime Dátum { get; set; }
        public string Elkoszones { get; set; }
        public string Megszolitas { get; set; }

        public override void Print()
        {
            Console.WriteLine(formazo.Formazas("Megszólítas", Megszolitas));
            Console.WriteLine(formazo.Formazas("Elköszönes", Elkoszones));
        }
    }

    //public class Korlevel2 : Korlevel
    //{ }

    //public class Korlevel3 : Korlevel
    //{ }
}
using System;
using System.Linq;

namespace _01Bridge
{
    public class Jelentes : Dokumentum
    {
        public Jelentes(IFormazo formazo) 
            : base(formazo)
        { }

        public string Bevezetes { get; set; }
        public string TeljesJelentes { get; set; }
        public string Kivonat { get; set; }
        public string Kovetkeztetes { get; set; }
        public string Keszitoje { get; set; }

        public override void Print()
        {
            //Nyugdíjba megy, mert formázót használunk helyette
            //Console.WriteLine("Készítő: {0}", Keszitoje);
            //Console.WriteLine("Kivonat: {0}", Kivonat);

            Console.WriteLine(formazo.Formazas("Készítő", Keszitoje));
            Console.WriteLine(formazo.Formazas("Kivonat", Kivonat));
        }
    }

    //public class Jelentes2 : Jelentes
    //{
    //    public override void Print()
    //    {
    //        Console.WriteLine("Készítő: {0}", new string(Keszitoje.Reverse().ToArray()));
    //        Console.WriteLine("Kivonat: {0}", new string(Kivonat.Reverse().ToArray()));
    //    }
    //}

    //public class Jelentes3 : Jelentes
    //{
    //    public override void Print()
    //    {
    //        Console.WriteLine("Készítő: =={0}==", Keszitoje);
    //        Console.WriteLine("Kivonat: =={0}==", Kivonat);
    //    }
    //}

}
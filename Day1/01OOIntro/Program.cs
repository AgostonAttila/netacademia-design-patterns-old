using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01OOIntro
{

    /// <summary>
    /// DP röviden
    /// - Christopher Alexander, 1977
    /// - 1987, OOPSLA
    /// - 1994, GoF (Gang of Four)
    /// 
    /// OO alapelvei
    ///  - Elvonatkoztatás
    ///  - Egységbezárás
    ///  - Modularitás
    ///  - Hierarchia
    ///  
    /// OO fogalmak
    ///  - Objektum
    ///    - Azonosítható
    ///    - van állapota
    ///    - van működése
    ///  - Osztály
    ///    - Azonos típusú objektumokat fog össze
    ///    - Az általa meghatározott objektumok definiciójára, létrehozására szolgál
    ///  
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var haziallat = new Haziallat();
            Console.WriteLine(haziallat.Nev);

            haziallat.Nev = "Bubu";
            Console.WriteLine(haziallat.Nev);

            haziallat.Koszon();

            var macska = new Macska();
            Console.WriteLine(macska.Nev);
            macska.Koszon();

            haziallat.Enekel();
            macska.Enekel();

            Console.WriteLine();

            //elkérem a macskától a benne/mellé példányosodott háziállat ősosztályt
            var macskabolHaziallat = (Haziallat)macska;
            Console.WriteLine(macskabolHaziallat.Nev);
            macskabolHaziallat.Koszon();
            macskabolHaziallat.Enekel();

            macska.Beszel();
            macskabolHaziallat.Beszel();

            Console.WriteLine();

            var sziamiMacska = new SziamiMacska();
            Console.WriteLine(sziamiMacska.Nev);
            sziamiMacska.Koszon();
            sziamiMacska.Beszel();
            sziamiMacska.Enekel();

            var csakMacska = (Macska)sziamiMacska;
            Console.WriteLine();
            Console.WriteLine(csakMacska.Nev);
            csakMacska.Koszon();
            csakMacska.Beszel();
            csakMacska.Enekel();

            var csakHaziallat = (Haziallat)sziamiMacska;
            Console.WriteLine();
            Console.WriteLine(csakHaziallat.Nev);
            csakHaziallat.Koszon();
            csakHaziallat.Beszel();
            csakHaziallat.Enekel();

            Console.ReadLine();

        }
    }

    public class Haziallat
    {
        public Haziallat()
        {
            Nev = "Haziallat";
            Console.WriteLine("Haziallat konstruktor");
        }

        public string Nev { get; set; }

        public void Koszon()
        {
            Console.WriteLine("Én vagyok a háziállat");
        }

        /// <summary>
        /// A virtual kulcsszóval az ősosztály engedélyezi, hogy 
        /// át lehessen írni a működését
        /// </summary>
        public virtual void Enekel()
        {
            Console.WriteLine("Háziállat vagyok, nem tudok énekelni");
        }

        public void Beszel()
        {
            Console.WriteLine("Én, mint háziállat természetesen tudok beszélni");
        }

    }

    /// <summary>
    /// Leszármaztatjuk a Macskát az ősosztályból
    /// ezzel elérjük azt, hogy a Macska mindazt tudja, 
    /// amit a háziállat tud
    /// </summary>
    public class Macska : Haziallat
    {
        public Macska()
        {
            Nev = "Macska";
            Console.WriteLine("Macska kunstruktor");
        }

        /// <summary>
        /// Ez ugyanaz, mintha new kulcsszóval hoztuk volna létre:
        /// public new void Koszon()
        /// 
        /// Ezzel a Haziallat Koszon fuggvenye felé a kapcsolat
        /// megszűnik, annak a helyét átveszi ez a függvény
        /// </summary>
        public void Koszon()
        {
            Console.WriteLine("Én a macska vagyok");
        }


        /// <summary>
        /// Az override kulcsszóval felülírom az ősosztály működését
        /// </summary>
        public override void Enekel()
        {
            Console.WriteLine("Miaú (macska énekel)");
        }

    }

    public class SziamiMacska : Macska
    {
        public SziamiMacska()
        {
            Nev = "Sziami macska";
            Console.WriteLine("SziamiMacska konstruktor");
        }

        public new void Koszon()
        {
            Console.WriteLine("Én a sziami macska vagyok");
        }

        public override void Enekel()
        {
            Console.WriteLine("Miaiaú (a sziámi macska énekel)");
        }

    }

}

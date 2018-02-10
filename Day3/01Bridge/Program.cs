using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01Bridge
{
    class Program
    {
        ///1. feladat: valahogy egységesen kellene tárolni a dokumentumokat
        ///            és ha szükség van rá, akkor nyomtatni.
        ///2. feladat: megváltozott módon kell tudni nyomtatni.
        ///
        ///3. Ez nagyon sok új osztályt eredményezhet, a számuk az (algoritmusok száma) x (alaposztályok száma)
        ///   feladat: hogy lehetne ezt "robbanást" elkerülni
        ///   
        /// 4. Megoldás: az ősosztályba stratégiát injektálunk, amit a leszármaztatott osztályok 
        ///              látnak és használhatnak. Így nem a fa alján leszármaztatással teszünk különbséget
        ///              az algoritmusok szerint, hanem a fa gyökerétől eleve az algoritmus rendelkezésre áll
        ///              és cserélhető.
        ///              
        /// Megjegyzések
        /// 
        /// - Az adaptert akkor használjuk, ha meglévő osztályok között szeretnénk kapcsolatot teremteni,
        ///   a bridge pedig akkor hasznos, ha egy új megoldást keresünk a kialakuló leszármaztatási 
        ///   problémára.
        /// 
        static void Main(string[] args)
        {
            IFormazo formazo = new Formazo();
            formazo = new VisszafeleFormazo();
            formazo = new MasfeleFormazo();

            var jelentes = new Jelentes(formazo)
            {
                Bevezetes = "Ez a jelentés azért készült, hogy",
                Keszitoje = "A jelentésosztály",
                Kivonat = "A lényeg, amit ebben szerepel, az, hogy...",
                TeljesJelentes = "bla, bla, bla",
                Kovetkeztetes = "A továbbiakban azt lehet mondani, ...."
            };

            var oneletrajz = new Oneletrajz(formazo)
            {
                Nev = "Gipsz Jakab",
                Tanulmanyok = new [] { "I. Rákóczi Ferenc középiskola", "Egyetem"},
                Munkahelyek = new [] {"Gyakornok", "Beosztott", "Munkatárs" }
            };

            var korlevel = new Korlevel(formazo)
            {
                Megszolitas = "Kedves Kolléga!",
                Elkoszones = "Üdvözlettel: Marketingesek",
                Dátum = DateTime.Now
            };

            var dokumentumok = new List<Dokumentum>(new Dokumentum[] { jelentes, oneletrajz, korlevel });

            foreach (var dokumentum in dokumentumok)
            {
                Console.WriteLine();
                dokumentum.Print();
            }

            Console.ReadLine();

        }
    }
}

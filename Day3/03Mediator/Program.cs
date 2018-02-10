using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03Mediator
{
    /// <summary>
    /// Megjegyzések
    /// 
    /// A közvetítő minta feladata, hogy példányok közötti 
    /// több-a-többhöz kommunikációt megvalósítson
    /// 
    /// A kommunikációt egységbe zárjuk, így elérjük, hogy a résztvevő
    /// példányok és osztályok közötti csatolás gyenge lesz.
    /// 
    /// Ha az üzenetküldés működésén módosítani kell, akkor ez egy 
    /// helyen történik, az Uzenoszoba osztályban
    /// 
    /// a több-a-többhöz kapcsolatot felváltja az egy-a-többhöz kapcsolat
    /// ezeket a kapcsolatokat könnyebb kezelni és átlátni.
    /// 
    /// Központosítja a működést, így a központi rész akár bonyolulttá is válhat
    /// </summary>

    class Program
    {
        static void Main(string[] args)
        {
            var szoba = new UzenoSzoba();

            var felhasznalo1 = new Felhasznalo("f1");
            var felhasznalo2 = new Felhasznalo("f2");
            var felhasznalo3 = new Felhasznalo("f3");
            var felhasznalo4 = new Felhasznalo("f4");

            szoba.Regisztracio(felhasznalo1);
            szoba.Regisztracio(felhasznalo2);
            szoba.Regisztracio(felhasznalo3);
            szoba.Regisztracio(felhasznalo4);

            felhasznalo1.UzenetKuldes(felhasznalo2.Nev, "Üzenet: 1->2");
            felhasznalo2.UzenetKuldes(felhasznalo3.Nev, "Üzenet: 2->3");
            felhasznalo3.UzenetKuldes(felhasznalo4.Nev, "Üzenet: 3->4");
            felhasznalo4.UzenetKuldes(felhasznalo1.Nev, "Üzenet: 4->1");
            felhasznalo1.UzenetKuldes(felhasznalo3.Nev, "Üzenet: 1->3");
            felhasznalo2.UzenetKuldes(felhasznalo4.Nev, "Üzenet: 2->4");

            //küldő: f1, címzett: f2, uzenet: Üzenet: 1->2
            //küldő: f2, címzett: f3, uzenet: Üzenet: 2->3
            //küldő: f3, címzett: f4, uzenet: Üzenet: 3->4
            //küldő: f4, címzett: f1, uzenet: Üzenet: 4->1
            //küldő: f1, címzett: f3, uzenet: Üzenet: 1->3
            //küldő: f2, címzett: f4, uzenet: Üzenet: 2->4

            Console.WriteLine();

            felhasznalo1.UzenetKuldes(string.Format("{0},{1}",felhasznalo2.Nev, felhasznalo4.Nev), "Üzenet: 1->2");

            Console.ReadLine();
        }
    }
}

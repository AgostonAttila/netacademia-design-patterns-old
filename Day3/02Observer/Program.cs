using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02Observer
{
    /// <summary>
    /// Feladat: készítünk egy programot, ami importál nagy állományokat.
    ///          és közben szeretnénk értesülni a folyamatról
    ///          
    ///          egy a többhöz kommunikáció, amikor nem kell előre 
    ///          definiálni, hogy kik vesznek részt a kommunikációban.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var betolto = new Betolto();

            var felulet = new FelhasznaloiFelulet();
            var naplo = new Naplozo();

            ///un. megfigyelőket csatlakoztatok a 
            ///végrehajtó osztályhoz

            betolto.Ertesites += (sender, folyamatJelzo) => felulet.Ertesites(folyamatJelzo);
            betolto.Ertesites += (sender, folyamatJelzo) => naplo.Ertesites(folyamatJelzo);

            betolto.Start();

            //betolto.Ertesites += Betolto_Ertesites;
            //betolto.Ertesites -= Betolto_Ertesites;

            //EventHandler<int> fe = (sender, folyamatJelzo) => felulet.Ertesites(folyamatJelzo);
            //EventHandler<int> ne = (sender, folyamatJelzo) => felulet.Ertesites(folyamatJelzo);

            //betolto.Ertesites += fe;
            //betolto.Ertesites += ne;

            //betolto.Ertesites -= fe;
            //betolto.Ertesites -= ne;

            Console.ReadKey();

        }

        //private static void Betolto_Ertesites(object sender, int e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

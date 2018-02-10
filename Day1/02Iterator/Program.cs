using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02Iterator
{
    class Program
    {
        static void Main(string[] args)
        {
            var ugyfel = new Ugyfel();

            ugyfel.Name = "Gipsz Jakab";
            //ugyfel.Address = "Utca, hsz";

            var vegeredmeny = true;
            foreach (bool eredmeny in Validalas(ugyfel))
            {
                if (!eredmeny)
                {
                    vegeredmeny = false;
                    break;
                }
            }

            Console.WriteLine("Validálás sikeres: {0}", vegeredmeny);

            var lista = new List<string>(new string[] { "Egy", "Ketto", "Harom" });

            var gyujtemeny = new BejarhatoGyujtemeny(lista);
            foreach (var item in gyujtemeny)
            {
                Console.WriteLine("+++{0}", item);
            }

            ///A foreach ciklus működése a következő
            ///
            ///Bejaro bejaro = BejarhatoGyujtemeny.GetEnumerator()
            ///do
            ///{
            ///   var leszkovetkezo = Bejaro.MoveNext();
            ///   var aktualis = Bejaro.Current();
            ///   (tennivaló elvégzése az aktuális elemen)
            ///} while (leszkovetkezo)
            ///

            ///Figyelni kell arra, hogy a bejárás indulása után az eredeti
            ///osztály módosulhat, és ilyen esetekben ezzel
            ///foglalkozni kell. Például a Lista osztály bejáró implementációja
            ///ezt nem engedi:
            //foreach (var item in lista)
            //{
            //    lista.Remove(item);
            //}
            ///Ha bejárás közben akarom módosítani, akkor
            ///kivételt dob:
            ///An unhandled exception of type 'System.InvalidOperationException' occurred in mscorlib.dll
            ///Additional information: Collection was modified; enumeration operation may not execute.

            Console.ReadLine();
        }

        /// <summary>
        /// Ez a függvény demonstrálja, hogy egy ciklusból is meg tudunk hívni összetett
        /// műveletet. Az IEnumerable visszatérési típussal és a 
        /// yield return visszatéréssel a függvényünk
        /// részekben fut, egyszerre mindig csak a következő 
        /// yield return-ig, majd a következő végrehajtás
        /// innen, ebből az állapotból folytatja, és így tovább, 
        /// amíg a függvény véget nem ér
        /// </summary>
        /// <param name="ugyfel"></param>
        /// <returns></returns>
        private static IEnumerable<bool> Validalas(Ugyfel ugyfel)
        {
            yield return !string.IsNullOrWhiteSpace(ugyfel.Name);
            yield return !string.IsNullOrWhiteSpace(ugyfel.Address);

            ///Es itt folytathatnánk a sort bármeddig
            ///

            //A foreach ciklus egészen addig hívja újra, amíg yield return-nel térünk vissza
            //ha nem, akkor nem hív többé
        }
    }

    public class Ugyfel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    //Készítünk egy olyan osztályt, amit ciklusban lehet meghívni, 
    //és az egyes összetevőin végig lehet lépkedni.

    //annyi csalást elkövetünk, hogy beléül listát használunk,
    //így csak a felület implementációját és működést vizsgáljuk.

    /// <summary>
    /// A bejárható gyűjtemény lehetőséget ad arra, hogy egy 
    /// ciklust használva bejárjuk
    /// </summary>
    public class BejarhatoGyujtemeny : IEnumerable
    {
        private List<string> lista;

        public BejarhatoGyujtemeny(List<string> lista)
        {
            this.lista = lista;
        }

        public IEnumerator GetEnumerator()
        {
            Console.WriteLine("   BejarhatoGyujtemeny.GetEnumerator");
            //return new Bejaro(lista);
            return new ForditottBejaro(lista);
        }
    }

    /// <summary>
    /// A bejáró osztály végigmegy az egyes elemeken, és 
    /// a mindig az aktuális elemet szolgáltatja
    /// </summary>
    public class Bejaro : IEnumerator
    {
        private List<string> lista;
        private int position = -1;

        public Bejaro(List<string> lista)
        {
            this.lista = lista;
        }

        public object Current
        {
            get
            {
                var current = lista[position];
                Console.WriteLine("   Bejaro.Current: {0}: {1}", position, current);
                return current;
            }
        }

        public bool MoveNext()
        {
            position++;
            var ujraHivhato = position < lista.Count;
            Console.WriteLine("   Bejaro.MoveNext: {0}, ujrahivhato: {1}", position, ujraHivhato);
            return ujraHivhato;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }

    public class ForditottBejaro : IEnumerator
    {
        private List<string> lista;
        private int position = -1;

        public ForditottBejaro(List<string> lista)
        {
            this.lista = lista.OrderBy(x=>x)
                              .ToList();
        }

        public object Current
        {
            get
            {
                var current = lista[position];
                Console.WriteLine("   ForditottBejaro.Current: {0}: {1}", position, current);
                return current;
            }
        }

        public bool MoveNext()
        {
            position++;
            var ujraHivhato = position < lista.Count;
            Console.WriteLine("   ForditottBejaro.MoveNext: {0}, ujrahivhato: {1}", position, ujraHivhato);
            return ujraHivhato;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }

}

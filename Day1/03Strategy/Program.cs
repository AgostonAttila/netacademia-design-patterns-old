using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03Strategy
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<int>(new int[] { 14, 2, 4, 27, 3, 5, 2, 9 });

            var kornyezet = new Kornyezet(list);

            ///amikor szükségünk van egy műveletre, akkor implementálunk
            int eredmeny1 = kornyezet.Osszeg1();
            int eredmeny2 = kornyezet.Osszeg2();
            ///Ebből akárhányat létre tudok hozni
            ///...
            ///

            Console.WriteLine("eredmény1: {0}, eredmény2: {1}", eredmeny1, eredmeny2);

            //Kisebb rendszerezés:
            eredmeny1 = kornyezet.Osszeg(1);
            eredmeny2 = kornyezet.Osszeg(2);
            Console.WriteLine("eredmény1: {0}, eredmény2: {1}", eredmeny1, eredmeny2);

            //Kisebb refactoring:
            eredmeny1 = kornyezet.OsszegJav(Kornyezet.Algoritmusok.SajatOsszeg);
            eredmeny2 = kornyezet.OsszegJav(Kornyezet.Algoritmusok.BeepitettOsszeg);
            Console.WriteLine("eredmény1: {0}, eredmény2: {1}", eredmeny1, eredmeny2);

            //Virtualis függvénnyel elkészítjük az alapértelmezett algoritmust
            eredmeny1 = kornyezet.OsszegAlapertelmezettAlg();
            //majd override-oljuk a leszármaztatott osztályban, így elérjük, hogy
            //a megváltozott algoritmust érjük el.
            var javKornyezet = new JavitottKornyezet(list);
            eredmeny2 = javKornyezet.OsszegAlapertelmezettAlg();
            Console.WriteLine("eredmény1: {0}, eredmény2: {1}", eredmeny1, eredmeny2);

            ///Az eddigi megoldások nem igazán jók:
            ///ahogy nő az algoritmusok száma és ahogy nő
            ///a felhasználási terület úgy fognak sokszorozódni 
            ///a leszármaztatások csak azért, hogy az újabb algoritmusokat
            ///be tudjuk szorítani az alkalmazásba.
            ///

            ///Ehelyett a Stratégia minta azt tanácsolja,
            ///hogy zárjuk egységbe az algoritmust, és 
            ///építsünk külön egy osztályt, ami csak az algoritmus 
            ///implementációkkal foglalkozik
            ///

            //Kell egy környezet
            var strategiaKornyezet = new StrategiaKornyezet(list);
            //kell egy strategia
            strategiaKornyezet.Strategiabeallitas(new SimaOsszeg());
            eredmeny1 = strategiaKornyezet.Osszeg();

            //De használhatunk másik stratégiát is
            strategiaKornyezet.Strategiabeallitas(new ParatlanokOsszeg());
            eredmeny1 = strategiaKornyezet.Osszeg();

            //De használhatunk másik stratégiát is
            strategiaKornyezet.Strategiabeallitas(new ParosokOsszeg());
            eredmeny2 = strategiaKornyezet.Osszeg();
            Console.WriteLine("eredmény1: {0}, eredmény2: {1}", eredmeny1, eredmeny2);

            var delegateKornyezet = new DelegateKornyezet(list);

            //sima összegzés
            eredmeny1 = delegateKornyezet.Osszeg(x => x.Sum());
            //Páros összegzés
            eredmeny2 = delegateKornyezet.Osszeg(x => { return x.Where(y => y % 2 == 0).Sum(); });
            //Páratlan összegzés
            var eredmeny3 = delegateKornyezet.Osszeg(x => { return x.Where(y => y % 2 == 1).Sum(); });

            Console.WriteLine("eredmény1: {0}, eredmény2: {1}, eredmény3: {2}", eredmeny1, eredmeny2, eredmeny3);

            using (var db = new MyDb())
            {
                db.Customers.Add(new Customer() { Name = "egy", Description = "Ez az egy" });
                db.Customers.Add(new Customer() { Name = "kettő", Description = "Ez a kettő" });
                db.Customers.Add(new Customer() { Name = "három", Description = "Ez a három" });
                db.SaveChanges();
            }

            Console.WriteLine("Mentés kész");

            Console.ReadLine();
        }
    }

    public class Kornyezet
    {
        protected List<int> list;

        /// <summary>
        /// A paraméterezett konstruktor
        /// Ha ilyet implementálunk, akkor 
        /// a default konstruktort a fordító nem hozza létre.
        /// tehát többé ilyet nem lehet csinálni:
        /// var k = new Kornyezet();
        /// ugyanis nincs ilyen konstruktor
        /// </summary>
        /// <param name="list"></param>
        public Kornyezet(List<int> list)
        {
            this.list = list;
        }

        /// <summary>
        /// Összegzőfüggvény egyik implementációval
        /// </summary>
        /// <returns></returns>
        public int Osszeg1()
        {
            var sum = 0;
            foreach (var elem in list)
            {
                sum += elem;
            }
            return sum;
        }

        /// <summary>
        /// Összegzőfüggvény másik implementációval
        /// </summary>
        /// <returns></returns>
        public int Osszeg2()
        {
            return list.Sum();
        }

        //Első szervező lépés
        public int Osszeg(int algoritmus)
        {
            if (algoritmus == 1)
            {
                return Osszeg1();
            }

            if (algoritmus == 2)
            {
                return Osszeg2();
            }

            throw new ArgumentOutOfRangeException("algoritmus");
        }
        public enum Algoritmusok
        {
            SajatOsszeg, BeepitettOsszeg
        }

        public int OsszegJav(Algoritmusok algoritmus)
        {
            switch (algoritmus)
            {
                case Algoritmusok.SajatOsszeg:
                    return Osszeg1();
                case Algoritmusok.BeepitettOsszeg:
                    return Osszeg2();
                default:
                    throw new ArgumentOutOfRangeException("algoritmus");
            }
        }

        /// <summary>
        /// Virtuális függvénnyel lehetővé teszem, 
        /// hogy később az algoritmust változtatni tudjuk.
        /// </summary>
        /// <returns></returns>
        public virtual int OsszegAlapertelmezettAlg()
        {
            var sum = 0;
            foreach (var elem in list)
            {
                sum += elem;
            }
            return sum;
        }
    }

    /// <summary>
    /// A leszármaztatott osztályban felül tudjuk definiálni az eredeti algoritmust.
    /// </summary>
    public class JavitottKornyezet : Kornyezet
    {
        /// <summary>
        /// Ezt kötelező implementálni,
        /// mert máshogy nem tudjuk példányosítani a 
        /// az alaposztályt.
        /// </summary>
        /// <param name="list"></param>
        public JavitottKornyezet(List<int> list)
            : base(list)
        {
        }

        public override int OsszegAlapertelmezettAlg()
        {
            return list.Sum();
        }
    }




}

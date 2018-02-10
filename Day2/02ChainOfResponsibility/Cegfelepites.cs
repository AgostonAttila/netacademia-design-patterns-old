using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02ChainOfResponsibility
{
    /// <summary>
    /// A cégünknél a szabadságok engedélyezésének a menete a következő,
    /// a management-ben alá-fölé rendeltségi viszonyban van hierarchia
    /// hogy egy-egy vezető, hány nap szabdságolást engedélyezhet.
    /// Ha a napok túlmegy az ő hatáskörén, akkor a főnökének kell 
    /// továbbadnia, aki több napot engedélyezhet.
    /// 
    /// 1. lépés: kezdeti implementáció
    /// 2. lépés: a döntési művelet beépítése a munkatárs osztályba (egységbezárás)
    /// 3. lépés: a döntések sorrendjének felépítése, és a ciklusből való áthelyezése
    /// 4. lépés: Felelősségi lánc építése
    /// 
    /// Megjegyzések
    /// - Ha a kérést elindító nem feltétlenül ismeri és a kiszolgáló példányt
    /// - a CoR mintával el tudjuk választani a kérőt a kiszolgálótól, 
    ///   a fogadó példányokat láncba állítjuk, és a kérés addig halad,
    ///   amíg valaki kiszolgálja.
    ///   
    /// - Nem biztos, hogy a kérést valaki is kiszolgálja
    /// - a kérést egyszerre több is kiszolgálhatja, ki kell 
    ///   jelölni automatikusan a megfelelőt
    /// - a kérelemre válaszoló lehetséges példányok halmazát 
    ///   is ki kell tudni automatikusan menet közben jelölni
    ///   
    /// 
    /// </summary>
    public class Cegfelepites
    {
        public bool SzabadsagEngedelyezese(int engedelyezendoNap)
        {
            //var engedelyezok = new List<Munkatars>(new Munkatars[]
            //{
            //    new Munkatars("Gipsz Jakab", 1),
            //    new Munkatars("Nagy Zoltán", 2),
            //    new Munkatars("Kis Sándor", 4),
            //    new Munkatars("Tóth Ferenc", 6),
            //    new Munkatars("Sándor Péter",10)
            //});

            //foreach (var munkatars in engedelyezok)
            //{
            //    var eredmeny = munkatars.SzabadsagEngedelyezese(engedelyezendoNap);
            //    if (eredmeny
            //        != Munkatars.EngedelyezesEredmenye.NincsRaFelhatalmazas)
            //    {
            //        //Itt eredmény született, ezt kiírjuk
            //        Console.WriteLine("{0} napot {1}: {2}", engedelyezendoNap, eredmeny, munkatars.Nev);
            //        return true;
            //    }
            //}
            //Console.WriteLine("Nincs aki ezt engedélyezni tudja");
            //return false;

            //létrehozunk egy új hierarchiát, ami leírja a döntéshozatal módját.
            var jakab = new SzabadsagEngedelyezes(new Munkatars("Gipsz Jakab", 1));
            var zoltan = new SzabadsagEngedelyezes(new Munkatars("Nagy Zoltán", 2));
            var sandor = new SzabadsagEngedelyezes(new Munkatars("Kis Sándor", 4));
            var ferenc = new SzabadsagEngedelyezes(new Munkatars("Tóth Ferenc", 6));
            var peter = new SzabadsagEngedelyezes(new Munkatars("Sándor Péter", 10));

            jakab.AKovetkezoASorban(zoltan);
            zoltan.AKovetkezoASorban(sandor);
            sandor.AKovetkezoASorban(ferenc);
            ferenc.AKovetkezoASorban(peter);

            //nem vizsgáljuk, hogy a korábbi elem szerepel-e a láncban (ez körhöz vezetne, ami végtelen ciklust okozna)
            //nem vizsgáljuk, hogy valakit saját magához szeretnénk adni (szintén)

            //vizsgáljuk, hogy ne lehessen nullal hívni

            EngedelyezesEredmenye eredmeny = jakab.SzabadsagEngedelyezese(engedelyezendoNap);

            return eredmeny != EngedelyezesEredmenye.NincsRaFelhatalmazas;
        }
    }

    public interface ISzabadsagEngedelyezes
    {
        void AKovetkezoASorban(SzabadsagEngedelyezes kovetkezo);
        EngedelyezesEredmenye SzabadsagEngedelyezese(int engedelyezendoNap);
    }

    public class SzabadsagEngedelyezes : ISzabadsagEngedelyezes
    {
        //Itt is a felületet használom, hogy polimorfizmussal két működést implementáljak
        private ISzabadsagEngedelyezes kovetkezo = SzabadsagEngedelyezes_NincsFelhatalmazasSemmire.Peldany;
        private Munkatars munkatars=null;

        public SzabadsagEngedelyezes(Munkatars munkatars)
        {
            if (munkatars == null) { throw new ArgumentNullException("munkatars"); }
            this.munkatars = munkatars;
        }
        public void AKovetkezoASorban(SzabadsagEngedelyezes kovetkezo)
        {
            if (kovetkezo == null) { throw new ArgumentNullException("kovetkezo"); }
            this.kovetkezo = kovetkezo;
        }

        public EngedelyezesEredmenye SzabadsagEngedelyezese(int engedelyezendoNap)
        {
            var valasz = munkatars.SzabadsagEngedelyezese(engedelyezendoNap);
            if (valasz==EngedelyezesEredmenye.NincsRaFelhatalmazas)
            {
                //Ezt az if-et kiírtjuk: null object pattern
                //vagyis nem feltételvizsgálattal döntjük el, hogy 
                //megyünk-e tovább, hanem logikán belül maradunk,
                //és a null-t helyettesítjük egy olyan példánnyal, 
                //ami azt végzi, amit most itt.
                //if (kovetkezo == null)
                //{
                //    return EngedelyezesEredmenye.NincsRaFelhatalmazas;
                //}
                return kovetkezo.SzabadsagEngedelyezese(engedelyezendoNap);
            }
            return valasz;
        }
    }

    /// <summary>
    /// Ez az osztály arra való, hogy jelezze a lánc végét.
    /// A lánc végét pedig egy példány is jelezheti, ezért 
    /// a legjobb, ha megoldjuk, hogy ez az osztály ne tudjon több 
    /// példányban létezni.
    /// Például úgy, hogy SINGLETON-ná tesszük:)
    /// </summary>
    class SzabadsagEngedelyezes_NincsFelhatalmazasSemmire : ISzabadsagEngedelyezes
    {
        public void AKovetkezoASorban(SzabadsagEngedelyezes kovetkezo)
        {
            throw new NotImplementedException();
        }
        public EngedelyezesEredmenye SzabadsagEngedelyezese(int engedelyezendoNap)
        {
            return EngedelyezesEredmenye.NincsRaFelhatalmazas;
        }

        #region Singleton implementáció
        private SzabadsagEngedelyezes_NincsFelhatalmazasSemmire() { }
        public static SzabadsagEngedelyezes_NincsFelhatalmazasSemmire Peldany { get { return Beagyazott.Peldany; } }
        private class Beagyazott
        {
            private static readonly SzabadsagEngedelyezes_NincsFelhatalmazasSemmire peldany = new SzabadsagEngedelyezes_NincsFelhatalmazasSemmire();
            static Beagyazott() {  }

            internal static SzabadsagEngedelyezes_NincsFelhatalmazasSemmire Peldany { get { return peldany; }  }
        }
        #endregion Singleton implementáció
    }

    public class Munkatars
    {
        public Munkatars(string nev, int maxNapok)
        {
            Nev = nev;
            MaximumNapok = maxNapok;
        }

        public EngedelyezesEredmenye SzabadsagEngedelyezese(int napokSzama)
        {
            if (MaximumNapok>=napokSzama)
            {
                //Itt van a tényleges döntés, 
                //de most mindig kedvezően
                //fog a főnök dönteni
                return EngedelyezesEredmenye.Engedelyezve;
            }
            return EngedelyezesEredmenye.NincsRaFelhatalmazas;
        }

        public string Nev { get; set; }
        //Ezt nem használjuk már kívülről
        private int MaximumNapok { get; }

    }
    public enum EngedelyezesEredmenye
    {
        Engedelyezve, Visszautasitva, NincsRaFelhatalmazas
    }
}

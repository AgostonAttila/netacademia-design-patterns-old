using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace _03Command
{
    [TestClass]
    public class CommandTests
    {
        [TestMethod]
        public void HaNemAdunkParametertVisszakapunkNegySort()
        {
            var alkalmazas = new Alkalmazas();
            var eredmeny = alkalmazas.Bevitel(new string[] { });
            Console.WriteLine(eredmeny);
            var sorok = eredmeny.Count(x => x == '\n');
            Assert.AreEqual(4, sorok);
        }

        [TestMethod]
        public void HaUjParancsotAdunkAkkorValaszol()
        {
            var alkalmazas = new Alkalmazas();
            var eredmeny = alkalmazas.Bevitel(new string[] { "uj" });

            Console.WriteLine(eredmeny);
            Assert.AreEqual("hozzáadva", eredmeny);
        }

        [TestMethod]
        public void HaModositasParancsotAdunkAkkorValaszol()
        {
            var alkalmazas = new Alkalmazas();
            var eredmeny = alkalmazas.Bevitel(new string[] { "modositas", "2" });

            Console.WriteLine(eredmeny);
            Assert.AreEqual(string.Format("módosítás kész: {0}", 2), eredmeny);
        }

        [TestMethod]
        public void HaTorlesParancsotAdunkAkkorValaszol()
        {
            var alkalmazas = new Alkalmazas();
            var eredmeny = alkalmazas.Bevitel(new string[] { "torles", "5" });

            Console.WriteLine(eredmeny);
            Assert.AreEqual(string.Format("törlés kész: {0}", 5), eredmeny);
        }

        [TestMethod]
        public void HaIsmeretlenParancsotAdunkAkkorValaszol()
        {
            var alkalmazas = new Alkalmazas();
            var eredmeny = alkalmazas.Bevitel(new string[] { "ismeretlen", "5" });

            Console.WriteLine(eredmeny);
            Assert.AreEqual(string.Format("Ismeretlen parancs: {0}", "ismeretlen"), eredmeny);
        }

    }

    public interface IParancs
    {
        string Hivas { get; }
        string Hasznalat { get; }
        string Vegrehajtas();

        void ParameterMegadas(string[] parameterek);
    }

    public class UjParancs : IParancs
    {
        public string Hasznalat { get { return "uj"; } }
        public string Hivas { get { return "uj"; } }

        public void ParameterMegadas(string[] parameterek)
        {
            
        }

        public string Vegrehajtas() { return "hozzáadva"; }
    }

    public class ModositasParancs : IParancs
    {
        string Azonosito;
        public string Hasznalat { get { return "modositas azonosito"; } }
        public string Hivas { get { return "modositas"; } }

        public void ParameterMegadas(string[] parameterek)
        {
            Azonosito = parameterek[1];
        }

        public string Vegrehajtas() { return string.Format("módosítás kész: {0}",Azonosito); ; }
    }

    public class TorlesParancs : IParancs
    {
        string Azonosito;
        public string Hasznalat { get { return "torles azonosito"; } }
        public string Hivas { get { return "torles"; } }
        public string Vegrehajtas() { return string.Format("törlés kész: {0}", Azonosito); ; }
        public void ParameterMegadas(string[] parameterek)
        {
            Azonosito = parameterek[1];
        }
    }

    class NincsIlyenParancs : IParancs
    {
        private string v;

        public NincsIlyenParancs(string v)
        {
            this.v = v;
        }
        public string Hasznalat { get { throw new NotImplementedException(); } }
        public string Hivas { get { throw new NotImplementedException(); } }

        public string Vegrehajtas()
        {
            return string.Format("Ismeretlen parancs: {0}", v);
        }
        public void ParameterMegadas(string[] parameterek)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// Alkalmazásunk egy konzol alkalmazás beviteli felületéről kér adatokat, 
    /// a kapott feladatokat értelmezi, végrehajtja, válaszol.
    /// </summary>
    public class Alkalmazas
    {
        public string Bevitel(string[] parameterek)
        {
            //A régi módszer nyugdíjba megy
            //if (parameterek.Length==0)
            //{
            //    return HasznalatModja();
            //}

            //var feldolgozo = new ParancsFeldolgozo();
            //return feldolgozo.Vegrehajtas(parameterek);

            //Az új módszer menete: lesz egy listánk, és mindent ez a lista tartalmaz
            var parancsok = new List<IParancs>(
                new IParancs[] {
                    new UjParancs(),
                    new ModositasParancs(),
                    new TorlesParancs()
                });

            if (parameterek.Length == 0)
            {
                return HasznalatModja2(parancsok);
            }

            var ertelmezo = new ParancsErtelmezo(parancsok);
            IParancs parancs = ertelmezo.ParancsValasztasa(parameterek);
            return parancs.Vegrehajtas();
        }
        private string HasznalatModja2(IEnumerable<IParancs> parancsok)
        {
            var valasz = string.Format("Használat: alkalmazas[.exe] parancs paraméterek{0}", Environment.NewLine);
            foreach (var parancs in parancsok)
            {
                valasz = string.Format("{0}{1}{2}",valasz, parancs.Hasznalat, Environment.NewLine);
            }
            return valasz;
        }

        private string HasznalatModja()
        {
            var valasz = string.Format("Használat: alkalmazas[.exe] parancs paraméterek{0}", Environment.NewLine);
            valasz = string.Format("{0}uj{1}", valasz, Environment.NewLine);
            valasz = string.Format("{0}modositas azonosito{1}", valasz, Environment.NewLine);
            valasz = string.Format("{0}torles azonosito{1}", valasz, Environment.NewLine);
            return valasz;
        }
    }

    public class ParancsFeldolgozo
    {
        public ParancsFeldolgozo()
        {
        }
        public string Vegrehajtas(string[] parameterek)
        {
            var parancsszoveg = parameterek[0];
            Parancsok parancs;
            if (!Enum.TryParse<Parancsok>(parancsszoveg, out parancs))
            {
                //throw new ArgumentOutOfRangeException(string.Format("parancs: {0}", parancsszoveg));
                return string.Format("Ismeretlen parancs: {0}", parancsszoveg);
            }

            switch (parancs)
            {
                case Parancsok.uj:
                    return AddNew();
                case Parancsok.modositas:
                    return Modify(parameterek[1]);
                case Parancsok.torles:
                    return Delete(parameterek[1]);
                default:
                    return string.Format("Erre a parancsra nem vagyunk felkészülve: {0}", parancsszoveg);
            }
        }

        private string Delete(string v)
        {
            return string.Format("törlés kész: {0}", v);
        }

        private string Modify(string v)
        {
            return string.Format("módosítás kész: {0}", v);
        }

        private string AddNew()
        {
            return "hozzáadva";
        }
    }

    public enum Parancsok
    {
        uj, modositas, torles
    }
}

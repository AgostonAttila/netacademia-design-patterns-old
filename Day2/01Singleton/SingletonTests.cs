using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using Ninject;

namespace _01Singleton
{
    /// <summary>
    /// Megjegyzések
    /// 
    /// Olyan osztály, ami 
    /// - csak egy példányban létezhet
    /// - Az életciklusáról saját maga gondoskodik
    /// - Csak akkor jön létre a példány, amikor szükség van rá.
    /// 
    /// Kérdések
    /// - nehéz tesztelni
    /// - ha ez egy olyan osztály ami az életciklusáról saját maga gondoskodik, 
    ///   a többi meg nem ilyen, akkor ez zavart okozhat
    ///   (Ennek az egyik lehetséges megoldása DI Containerek használata)  
    /// 
    /// - Ez csak singleton példányosítását oldja meg MultiThread környezetben a működését nem
    ///    tehát a cache osztályt építünk akkor a cache műveleteknél szintén kell figyelni a tread-safe 
    ///    viselkedésre.
    /// 
    /// </summary>
    [TestClass]
    public class SingletonTests
    {
        /// <summary>
        /// Első implementáció, ez nem thread-safe
        /// </summary>
        [TestMethod]
        public void AKetPeldanyUgyanaz1()
        {
            var peldany1 = Singleton1.Instance;
            var peldany2 = Singleton1.Instance;

            //Ez referencia alapján hasonlítja össze az objektumokat
            //egyezőség szempontjából
            Assert.AreEqual(peldany1, peldany2);
        }

        /// <summary>
        /// Első thread-safe implementáció egyszerű lock-kal
        /// </summary>
        [TestMethod]
        public void AKetPeldanyUgyanaz2()
        {
            var peldany1 = Singleton2.Instance;
            var peldany2 = Singleton2.Instance;

            //Ez referencia alapján hasonlítja össze az objektumokat
            //egyezőség szempontjából
            Assert.AreEqual(peldany1, peldany2);
        }

        [TestMethod]
        public void AKetPeldanyUgyanaz3()
        {
            var peldany1 = Singleton3.Instance;
            var peldany2 = Singleton3.Instance;
            Assert.AreEqual(peldany1, peldany2);
        }

        [TestMethod]
        public void AKetPeldanyUgyanaz4()
        {
            var peldany1 = Singleton4.Instance;
            var peldany2 = Singleton4.Instance;
            Assert.AreEqual(peldany1, peldany2);
        }

        [TestMethod]
        public void AKetPeldanyUgyanaz5()
        {
            var peldany1 = Singleton5.Instance;
            var peldany2 = Singleton5.Instance;
            Assert.AreEqual(peldany1, peldany2);
        }

        [TestMethod]
        public void AKetPeldanyUgyanaz6()
        {
            var peldany1 = Singleton6.Instance;
            var peldany2 = Singleton6.Instance;
            Assert.AreEqual(peldany1, peldany2);
        }

        [TestMethod]
        public void AKetPeldanyUgyanaz7()
        {
            var peldany1 = Singleton7.Instance;
            var peldany2 = Singleton7.Instance;
            Assert.AreEqual(peldany1, peldany2);
        }

        ///Di Conténerek példa
        ///

        ///Unity
        ///
        [TestMethod]
        public void AKetPeldanyUgyanaz8()
        {
            var container = new UnityContainer();
            container.RegisterType<AkarmiBarmi, AkarmiBarmi>(new ExternallyControlledLifetimeManager());

            var peldany1 = container.Resolve<AkarmiBarmi>();
            var peldany2 = container.Resolve<AkarmiBarmi>();

            Assert.AreEqual(peldany1, peldany2);
        }

        ///Ninject
        ///
        [TestMethod]
        public void AKetPeldanyUgyanaz9()
        {
            var kernel = new StandardKernel();
            kernel.Bind<AkarmiBarmi>().To<AkarmiBarmi>().InSingletonScope();

            var peldany1 = kernel.Get<AkarmiBarmi>();
            var peldany2 = kernel.Get<AkarmiBarmi>();

            Assert.AreEqual(peldany1, peldany2);

            //Ellenteszt
            var peldany3 = new AkarmiBarmi();
            Assert.AreNotEqual(peldany1, peldany3);
        }
    }
}

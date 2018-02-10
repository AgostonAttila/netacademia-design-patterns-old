using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _02ChainOfResponsibility
{
    [TestClass]
    public class CoRTests
    {
        [TestMethod]
        public void EgyszeruSikeresEngedelyezes()
        {
            var ceg = new Cegfelepites();
            var eredmeny = ceg.SzabadsagEngedelyezese(7);
            Assert.IsTrue(eredmeny);
        }

        [TestMethod]
        public void EgyszeruSikertelenEngedelyezes()
        {
            var ceg = new Cegfelepites();
            var eredmeny = ceg.SzabadsagEngedelyezese(11);
            Assert.IsFalse(eredmeny);
        }
    }
}

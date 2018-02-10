using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Data.OleDb;

namespace _04Adapter
{

    ///Az adapter mintával példáulkét egymással "nem kompatibilis"
    ///osztályt tudunk együtműködésre bírni.
    ///
    ///Vagy, ha ismerjük a felületet, akkor tudunk fejleszteni egy másik osztály használatához,
    ///amit majd a már elkészült megoldásba illesztünk.
    ///
    ///A példában IDbDataAdapter felületet használjuk demonstrációra

    [TestClass]
    public class AdapterTests
    {
        [TestMethod]
        public void HaDublorrelHivomAkkor()
        {
            var megjelenito = new AdatMegjelenito(new DublorAdapter());

            var writer = new StringWriter();

            megjelenito.Megjelenites(writer);

            var result = writer.ToString();
            Console.WriteLine(result);

            var sorok = result.Count(x => x == '\n');

            Assert.AreEqual(3, sorok);
        }

        [TestMethod]
        public void HaDbreHivomAkkor()
        {
            var data = new OleDbDataAdapter();
            data.SelectCommand = new OleDbCommand("SELECT * FROM Customers");
            data.SelectCommand.Connection 
                = new OleDbConnection("Provider=sqloledb;Data Source=2-00\\SQLEXPRESS;Initial Catalog=_03Strategy.MyDb;Integrated Security = SSPI;");

            var megjelenito = new AdatMegjelenito(data);

            var writer = new StringWriter();

            megjelenito.Megjelenites(writer);

            var result = writer.ToString();
            Console.WriteLine(result);

            var sorok = result.Count(x => x == '\n');

            Assert.AreEqual(17, sorok);

        }

    }
}

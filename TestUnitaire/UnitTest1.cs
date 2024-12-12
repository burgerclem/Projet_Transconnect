using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MyTests")]

namespace ProjetC_A3
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            NoeudN kevin = null;
            Salarié a = null;
            ArbreNAire b = new ArbreNAire();
            Assert.AreEqual(null,b.FindNode(kevin,a));
        }
        [TestMethod]
        public void TestMethod2()
        {
            Commande c = new Commande();
            string temp = "3h15";
            Assert.AreEqual(195, c.ConvertTimeToMinutes(temp));
        }
        [TestMethod]
        public void TestMethod3()
        {
            Noeud n = new Noeud(144,null,null);
            bool gyat = false;
            Assert.AreNotEqual(gyat,n.AssocierNoeudFilsGauche(n));
        }
        [TestMethod]
        public void TestMethod4()
        {
            Client p = new Client(6, "dddd", "caca", DateTime.Parse("2020-01-02"), "4", "@", "066345");
            string pull = "6 dddd caca 02/01/2020 4 @ 066345";
            Assert.AreEqual(pull, p.ToString());
        }
        [TestMethod]
        public void TestMethod5()
        {
            Salarié sigma = new Salarié(4563, "Nympho", "Ciprine", DateTime.Parse("2023-01-04"), "666", "csharp", "dzqfe", DateTime.Parse("2023-01-06"), "Responsable caca", 1);
            int pollo = 1;
            Assert.AreNotEqual(Salarié.findSalarié(pollo), sigma);
        }
    }
}
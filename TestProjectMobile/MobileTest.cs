using MobileLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProjectMobile
{
    /// <summary>
    ///This is a test class for MobileTest and is intended
    ///to contain all MobileTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MobileTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes

        //
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //

        #endregion Additional test attributes

        /// <summary>
        /// Mobile mit Nummer erzeugen
        /// </summary>
        [TestMethod()]
        public void T01_CreateSimpleMobileTest()
        {
            Mobile mobile = new Mobile("0123456");
            Assert.AreEqual("0123456", mobile.PhoneNumber);
        }

        /// <summary>
        /// Mobile mit Nummer und Namen erzeugen
        /// </summary>
        [TestMethod()]
        public void T02_CreateSimpleMobileTest()
        {
            Mobile mobile = new Mobile("0123456", "Huber");
            Assert.AreEqual("0123456", mobile.PhoneNumber);
            Assert.AreEqual("Huber", mobile.Name);
        }

        /// <summary>
        /// Mobile mit Nummer und fehlerhaftem Namen erzeugen
        /// </summary>
        [TestMethod()]
        public void T03_CreateErrorMobileTest()
        {
            Mobile mobile = new Mobile("0123456", null);
            Assert.AreEqual("0123456", mobile.PhoneNumber);
            Assert.AreEqual("ERROR", mobile.Name);
            mobile = new Mobile("0123456", "");
            Assert.AreEqual("0123456", mobile.PhoneNumber);
            Assert.AreEqual("ERROR", mobile.Name);
            mobile = new Mobile("0123456", "A");
            Assert.AreEqual("0123456", mobile.PhoneNumber);
            Assert.AreEqual("ERROR", mobile.Name);
            mobile = new Mobile("0123456", "1234");
            Assert.AreEqual("0123456", mobile.PhoneNumber);
            Assert.AreEqual("ERROR", mobile.Name);
            mobile = new Mobile("0123456", "A1");
            Assert.AreEqual("0123456", mobile.PhoneNumber);
            Assert.AreEqual("A1", mobile.Name);
        }

        /// <summary>
        /// Ersten Call erzeugen
        /// </summary>
        [TestMethod()]
        public void T04_SimpleCallTest()
        {
            Mobile active = new Mobile("0123456", "Active");
            Mobile passive = new Mobile("9876543", "Passive");
            active.StartCallTo(passive);
            System.Threading.Thread.Sleep(3000);
            Assert.IsTrue(active.StopCall());
            Assert.AreEqual("9876543", active.LastCalledNumber, "LastCalledNumber");
            Assert.AreEqual(60, active.SecondsActive, "Active Seconds");
            Assert.AreEqual(60, passive.SecondsPassive, "Passive Seconds");
            Assert.AreEqual(8, active.CentsToPay, "60 seconds ==> 1 minute ==> 8 cents");
        }

        /// <summary>
        /// Ersten Call verrechnen
        /// </summary>
        [TestMethod()]
        public void T05_SimpleToPayTest()
        {
            Mobile active = new Mobile("0123456", "Active");
            Mobile passive = new Mobile("9876543", "Passive");
            active.StartCallTo(passive);
            System.Threading.Thread.Sleep(3000);
            Assert.IsTrue(active.StopCall());
            Assert.AreEqual(8, active.CentsToPay, "60 seconds ==> 1 minute ==> 8 cents");
        }

        /// <summary>
        /// Call knapp über 1 Minute verrechnen
        /// </summary>
        [TestMethod()]
        public void T06_ToPayTest()
        {
            Mobile active = new Mobile("0123456", "Active");
            Mobile passive = new Mobile("9876543", "Passive");
            active.StartCallTo(passive);
            System.Threading.Thread.Sleep(3100);
            Assert.IsTrue(active.StopCall());
            Assert.AreEqual(12, active.CentsToPay, "62 seconds > 1 minute ==> 12 cents");
        }

        /// <summary>
        /// Mehrere Calls verrechnen
        /// </summary>
        [TestMethod()]
        public void T07_MoreCallsToPayTest()
        {
            Mobile active = new Mobile("0123456", "Active");
            Mobile passive1 = new Mobile("1111111", "Passive1");
            Mobile passive2 = new Mobile("2222222", "Passive2");
            active.StartCallTo(passive1);
            System.Threading.Thread.Sleep(3000);
            Assert.IsTrue(active.StopCall(), "Stop Call passive1");
            active.StartCallTo(passive2);
            System.Threading.Thread.Sleep(2200);
            Assert.IsTrue(active.StopCall(), "Stop Call passive2");
            Assert.AreEqual(16, active.CentsToPay);
            Assert.AreEqual(60, passive1.SecondsPassive, 1, "Passive 1 Sekunden");
            Assert.AreEqual(44, passive2.SecondsPassive, 1, "Passive 2 Sekunden");
        }

        /// <summary>
        /// Mehrere Calls verrechnen
        /// </summary>
        [TestMethod()]
        public void T09_StopPassive()
        {
            Mobile active = new Mobile("0123456", "Active");
            Mobile passive = new Mobile("9876543", "Passive");
            active.StartCallTo(passive);
            System.Threading.Thread.Sleep(3100);
            Assert.IsTrue(passive.StopCall());
            Assert.AreEqual(12, active.CentsToPay, "62 seconds > 1 minute ==> 12 cents");
            Assert.AreEqual(0, passive.CentsToPay);
            Assert.AreEqual(62, passive.SecondsPassive, 1);
            Assert.AreEqual(62, active.SecondsActive, 1);
        }

        /// <summary>
        /// StopCall ohne StartCall testen
        /// </summary>
        [TestMethod()]
        public void T08_StopCallErrorTest()
        {
            Mobile active = new Mobile("0123456", "Active");
            Assert.IsFalse(active.StopCall());
        }

        [TestMethod()]
        public void T10_BusyPhoneTest()
        {
            Mobile active = new Mobile("0123456", "Active");
            Mobile passive = new Mobile("9876543", "Passive");
            Mobile active2 = new Mobile("01234562", "Active2");
            Mobile passive2 = new Mobile("01234563", "Passive2");
            bool result = active.StartCallTo(passive);
            Assert.AreEqual(true, result, "First call should be ok!");
            result = active.StartCallTo(passive2);
            Assert.AreEqual(false, result, "Active phone already busy! Returnvalue false expected!");
            result = active2.StartCallTo(passive);
            Assert.AreEqual(false, result, "Passive phone already busy! Returnvalue false expected!");
            System.Threading.Thread.Sleep(1000);
            Assert.IsTrue(active.StopCall());
            result = active.StartCallTo(passive2);
            Assert.AreEqual(true, result, "After call has ended, a new call shall be available");
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PS_UnitTests.PersonClasses;

namespace PS_UnitTests.Test
{
    [TestClass]
    public class AssertClass
    {
        #region Are Equal / Not Equal
        [TestMethod]
        [Owner("aVentura")]
        public void AreEqualTest()
        {
            string str1 = "Paul";
            string str2 = "Paul";

            Assert.AreEqual(str1, str2);
        }

        [TestMethod]
        [Owner("aVentura")]
        [ExpectedException(typeof(AssertFailedException))]
        public void AreEqualCaseSensitiveTest()
        {
            string str1 = "Paul";
            string str2 = "paul";

            Assert.AreEqual(str1, str2, false);
        }

        [TestMethod]
        [Owner("aVentura")]
        public void AreNotEqualTest()
        {
            string str1 = "Paul";
            string str2 = "John";

            Assert.AreNotEqual(str1, str2);
        }
        #endregion

        #region Are same/not same

        [TestMethod]
        [Owner("aVentura")]
        public void AreSameTest()
        {
            FileProcess x = new FileProcess();
            FileProcess y = x;

            Assert.AreSame(x, y);
        }

        [TestMethod]
        [Owner("aVentura")]
        public void AreNotSameTest()
        {
            FileProcess x = new FileProcess();
            FileProcess y = new FileProcess();

            Assert.AreNotSame(x, y);
        }

        #endregion

        #region IsInstanceOffType
        [TestMethod]
        public void IsInstanceOffTypeTest()
        {
            PersonManager mgr = new PersonManager();
            Person per;

            per = mgr.CreatePerson("Paul", "Sherrif", true);

            Assert.IsInstanceOfType(per, typeof(Supervisor));
        }

        [TestMethod]
        public void IsNotInstanceOffTypeTest()
        {
            PersonManager mgr = new PersonManager();
            Person per;

            per = mgr.CreatePerson("Paul", "Sherrif", false);

            Assert.IsNotInstanceOfType(per, typeof(Supervisor));
        }
        #endregion

        #region IsNull Test
        [TestMethod]
        public void IsNullTest()
        {
            PersonManager mgr = new PersonManager();
            Person per;

            per = mgr.CreatePerson("", "Sherrif", true);

            Assert.IsNull(per);
        }
        #endregion


    }
}

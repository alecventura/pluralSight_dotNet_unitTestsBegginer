using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PS_UnitTests;
using System.Configuration;
using System.IO;

namespace PS_UnitTests.Test
{
    [TestClass]
    public class FileProcessTest
    {
        private const string BAD_FILE_NAME = @"C:\BadFileName.bad";
        private string _GoodFileName;

        #region Class Initialize and Cleanup
        [ClassInitialize]
        public static void ClassInitialize(TestContext tc)
        {
            tc.WriteLine("In the class initilize.");
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
        }
        #endregion

        #region Test Initialize and Cleanup
        [TestInitialize]
        public void TestInitialize()
        {
            if (TestContext.TestName == "FileNameDoesExists")
            {
                SetGoodFileName();
                File.AppendAllText(_GoodFileName, "Some Text");
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (TestContext.TestName == "FileNameDoesExists")
            {
                if (!string.IsNullOrEmpty(_GoodFileName))
                {
                    File.Delete(_GoodFileName);
                }
            }
        }
        #endregion

        public TestContext TestContext { get; set; }

        public void SetGoodFileName()
        {
            _GoodFileName = ConfigurationManager.AppSettings["GoodFileName"];
            if (_GoodFileName.Contains("[AppPath]"))
            {
                _GoodFileName = _GoodFileName.Replace("[AppPath]",
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            }
        }

        [TestMethod]
        [Description("Check if file name exists")]
        [Owner("aVentura")]
        [Priority(0)]
        [TestCategory("NoException")]
        public void FileNameDoesExists()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;
            
            fromCall = fp.FileExists(_GoodFileName);

            Assert.IsTrue(fromCall);
        }

        [TestMethod]
        [Description("Check if file name does NOT exists")]
        [Owner("aVentura")]
        [Priority(0)]
        [TestCategory("NoException")]
        public void FileNameDoesNotExists()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            fromCall = fp.FileExists(BAD_FILE_NAME);

            Assert.IsFalse(fromCall);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [Owner("aVentura")]
        [Priority(1)]
        [TestCategory("Exception")]
        public void FileNameNullOrEmpty_ThrowsArgumentNullException()
        {
            FileProcess fp = new FileProcess();

            fp.FileExists("");
        }

        [TestMethod]
        [Owner("aVentura")]
        [Priority(1)]
        [TestCategory("Exception")]
        public void FileNameNullOrEmpty_ThrowsArgumentNullExceptionUsingTryCatch()
        {
            FileProcess fp = new FileProcess();
            try
            {
                fp.FileExists("");
            }
            catch (ArgumentNullException e)
            {
                // The test was a success
                return;
            }

            Assert.Fail("Should return exception");
        }

        [TestMethod]
        [Ignore]
        public void IgnoreThisOne()
        {
            Assert.IsTrue(true);
        }

        [TestMethod]
        [Timeout(1000)]
        public void SimulateTimeOut()
        {
            System.Threading.Thread.Sleep(500);
        }

        private const string FILE_NAME = @"FileToDeploy.txt";

        [TestMethod]
        [Owner("aVentura")]
        [DeploymentItem(FILE_NAME)]
        public void FileNameDoesExistUsingDeploymentItem()
        {
            FileProcess fp = new FileProcess();
            string fileName;
            bool fromCall;

            fileName = TestContext.DeploymentDirectory + @"\" + FILE_NAME;
            TestContext.WriteLine("Checking file: " + fileName);

            fromCall = fp.FileExists(fileName);

            Assert.IsTrue(fromCall);
        }
    }
}

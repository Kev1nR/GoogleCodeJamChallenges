using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject1
{
    /// <summary>
    /// Summary description for GoogleCodeCampGeneralTests
    /// </summary>
    [TestClass]
    public class GoogleCodeCampGeneralTests
    {
        public GoogleCodeCampGeneralTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestFileReading()
        {
            var lines = GoogleCodeJam.Africa2010_Qualification.FileReaders.readFileToEnd (@"C:\Dropfolder\GoogleBootcampFiles\Africa201_qual_StoreCredit_small.in");

            Assert.IsInstanceOfType(lines, typeof(IEnumerable<string>));
        }

        [TestMethod]
        public void TestFileWriting()
        {
            var lines = 
                Microsoft.FSharp.Collections.FSharpList<string>.Cons("Line1", 
                    Microsoft.FSharp.Collections.FSharpList<string>.Cons("Line2",
                        Microsoft.FSharp.Collections.FSharpList<string>.Cons("Line3",
                            Microsoft.FSharp.Collections.FSharpList<string>.Cons("Line4", 
                                Microsoft.FSharp.Collections.FSharpList<string>.Empty))));
            
            GoogleCodeJam.Africa2010_Qualification.FileReaders.writeListToFile (@"C:\Dropfolder\GoogleBootcampFiles\Africa201_qual_StoreCredit_large.out", lines);

            Assert.IsInstanceOfType(lines, typeof(IEnumerable<string>));
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChmodConverterLib;

namespace ChmodConverterTests
{
    [TestClass]
    public class ChmodConverterTests
    {
        [TestMethod]
        public void SymbolicToNumericTest_ValidInputs()
        {
            Assert.AreEqual("777", ChmodConverter.SymbolicToNumeric("rwxrwxrwx"));
            Assert.AreEqual("755", ChmodConverter.SymbolicToNumeric("rwxr-xr-x"));
            Assert.AreEqual("644", ChmodConverter.SymbolicToNumeric("rw-r--r--"));
            Assert.AreEqual("000", ChmodConverter.SymbolicToNumeric("---------"));
            Assert.AreEqual("770", ChmodConverter.SymbolicToNumeric("rwxrwx---"));
            Assert.AreEqual("700", ChmodConverter.SymbolicToNumeric("rwx------"));
            Assert.AreEqual("400", ChmodConverter.SymbolicToNumeric("r--------"));
            Assert.AreEqual("444", ChmodConverter.SymbolicToNumeric("r--r--r--"));
            Assert.AreEqual("111", ChmodConverter.SymbolicToNumeric("--x--x--x"));
            Assert.AreEqual("222", ChmodConverter.SymbolicToNumeric("-w--w--w-"));
            Assert.AreEqual("333", ChmodConverter.SymbolicToNumeric("-wx-wx-wx"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SymbolicToNumericTest_NullInput()
        {
            ChmodConverter.SymbolicToNumeric(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SymbolicToNumericTest_EmptyInput()
        {
            ChmodConverter.SymbolicToNumeric("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SymbolicToNumericTest_InvalidLength()
        {
            ChmodConverter.SymbolicToNumeric("rwxrwx");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SymbolicToNumericTest_InvalidCharacters()
        {
            ChmodConverter.SymbolicToNumeric("rwzrwxrwx");
        }

        [TestMethod]
        public void NumericToSymbolicTest_ValidInputs()
        {
            Assert.AreEqual("rwxrwxrwx", ChmodConverter.NumericToSymbolic("777"));
            Assert.AreEqual("rwxr-xr-x", ChmodConverter.NumericToSymbolic("755"));
            Assert.AreEqual("rw-r--r--", ChmodConverter.NumericToSymbolic("644"));
            Assert.AreEqual("---------", ChmodConverter.NumericToSymbolic("000"));
            Assert.AreEqual("rwxrwx---", ChmodConverter.NumericToSymbolic("770"));
            Assert.AreEqual("rwx------", ChmodConverter.NumericToSymbolic("700"));
            Assert.AreEqual("r--------", ChmodConverter.NumericToSymbolic("400"));
            Assert.AreEqual("r--r--r--", ChmodConverter.NumericToSymbolic("444"));
            Assert.AreEqual("--x--x--x", ChmodConverter.NumericToSymbolic("111"));
            Assert.AreEqual("-w--w--w-", ChmodConverter.NumericToSymbolic("222"));
            Assert.AreEqual("-wx-wx-wx", ChmodConverter.NumericToSymbolic("333"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NumericToSymbolicTest_NullInput()
        {
            ChmodConverter.NumericToSymbolic(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NumericToSymbolicTest_EmptyInput()
        {
            ChmodConverter.NumericToSymbolic("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NumericToSymbolicTest_InvalidLength()
        {
            ChmodConverter.NumericToSymbolic("77");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NumericToSymbolicTest_InvalidDigit()
        {
            ChmodConverter.NumericToSymbolic("788");
        }
    }
}
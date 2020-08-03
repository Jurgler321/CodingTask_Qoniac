using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CurrencyConversion.Tests
{
    [TestClass]
    public class ConversionTests
    {
        [TestMethod]
        public void DollarConversionTest1()
        {
            var conversion = new Service.CurrencyConversion();
            var result = conversion.Convert(0m);
            Assert.AreEqual("zero dollars", result);
        }
        [TestMethod]
        public void DollarConversionTest2()
        {
            var conversion = new Service.CurrencyConversion();
            var result = conversion.Convert(1m);
            Assert.AreEqual(result, "one dollar");
        }
        [TestMethod]
        public void DollarConversionTest3()
        {
            var conversion = new Service.CurrencyConversion();
            var result = conversion.Convert(25.1m);
            Assert.AreEqual(result, "twenty-five dollars and ten cents");
        }
        [TestMethod]
        public void DollarConversionTest4()
        {
            var conversion = new Service.CurrencyConversion();
            var result = conversion.Convert(0.01m);
            Assert.AreEqual(result, "zero dollars and one cent");
        }
        [TestMethod]
        public void DollarConversionTest5()
        {
            var conversion = new Service.CurrencyConversion();
            var result = conversion.Convert(45100m);
            Assert.AreEqual(result, "forty-five thousand one hundred dollars");
        }
        [TestMethod]
        public void DollarConversionTest6()
        {
            var conversion = new Service.CurrencyConversion();
            var result = conversion.Convert(999999999.99m);
            Assert.AreEqual(result, "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents");
        }
        [TestMethod]
        public void DollarConversionTestCustom()
        {
            var conversion = new Service.CurrencyConversion();
            var result = conversion.Convert(100001.01m);
            Assert.AreEqual(result, "one hundred thousand one dollars and one cent");
        }
    }
}

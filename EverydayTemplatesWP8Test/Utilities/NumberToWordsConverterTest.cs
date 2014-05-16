using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace EverydayTemplatesWP8.Utilities
{
    [TestClass]
    public class NumberToWordsConverterTest
    {
        [TestMethod]
        public void TestConvert()
        {
            double number = 23123456.123456789;
            string strNumber = NumberToWordsConverter.Convert(number);
            Console.WriteLine(strNumber);
        }
    }
}

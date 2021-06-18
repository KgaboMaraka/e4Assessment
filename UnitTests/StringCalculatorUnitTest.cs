using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class StringCalculatorUnitTest
    {
        [TestMethod]
        public void Emptystring()
        {
            string numbers = "";
            var expectedOutput = "0";

            var actualOutput = StringCalculator.Program.AddCaller(numbers);
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void UnknownAmountOfNumbers()
        {
            string numbers = "3,5,8,4,6,21,4,7,4,9,5,12,34,87,5,2,9,7,34,7,2";
            var expectedOutput = "275";

            var actualOutput = StringCalculator.Program.AddCaller(numbers);
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void NewLines()
        {
            string numbers = "1" + Environment.NewLine + "4" + Environment.NewLine + "3";
            var expectedOutput = "8";

            var actualOutput = StringCalculator.Program.AddCaller(numbers);
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void NewLinesAndDelimiter()
        {
            string numbers = "1,5" + Environment.NewLine + "4" + Environment.NewLine + "3";
            var expectedOutput = "13";

            var actualOutput = StringCalculator.Program.AddCaller(numbers);
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void DifferentDelimiters1()
        {
            string numbers = "3$5$9$5";
            var expectedOutput = "22";

            var actualOutput = StringCalculator.Program.AddCaller(numbers);
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void DifferentDelimiters2()
        {
            string numbers = "4%9%32%21%9";
            var expectedOutput = "75";

            var actualOutput = StringCalculator.Program.AddCaller(numbers);
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void ChangeDelimiter()
        {
            string numbers = "//;" + Environment.NewLine + "4;3;6;8";
            var expectedOutput = "21";

            var actualOutput = StringCalculator.Program.AddCaller(numbers);
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void OneNegativeNumber()
        {
            string numbers = "-1,2,3,9";
            var expectedOutput = "StringCalculator.NegativesNotAllowedException: negatives not allowed: -1";

            var actualOutput = StringCalculator.Program.AddCaller(numbers);
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void MultipleNegativeNumbers()
        {
            string numbers = "-1,2,-3,9,-4,-7";
            var expectedOutput = "StringCalculator.NegativesNotAllowedException: negatives not allowed: -1,-3,-4,-7";

            var actualOutput = StringCalculator.Program.AddCaller(numbers);
            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}

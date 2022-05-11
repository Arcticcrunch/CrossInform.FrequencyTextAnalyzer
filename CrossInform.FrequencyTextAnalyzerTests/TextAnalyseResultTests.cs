using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrossInform.FrequencyTextAnalyzer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossInform.FrequencyTextAnalyzer.Tests
{
    [TestClass()]
    public class TextAnalyseResultTests
    {
        [TestMethod()]
        [DataRow(null, null)]
        public void BothArgumentsNullCheck_MergeResultsTest(Dictionary<string, int> dictionary1, Dictionary<string, int> dictionary2)
        {
            // arrange

            // act

            // assert
            Assert.ThrowsException<NullReferenceException>(() => TextAnalyseResult.MergeResults(dictionary1, dictionary2));
        }

        [TestMethod()]
        public void FirstArgumentNullCheck_MergeResultsTest()
        {
            // arrange
            Dictionary<string, int> dictionary1 = new Dictionary<string, int>();
            Dictionary<string, int> dictionary2 = null;
            // act

            // assert
            Assert.ThrowsException<NullReferenceException>(() => TextAnalyseResult.MergeResults(dictionary1, dictionary2));
        }
        [TestMethod()]
        public void SecondArgumentNullCheck_MergeResultsTest()
        {
            // arrange
            Dictionary<string, int> dictionary1 = null;
            Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
            // act

            // assert
            Assert.ThrowsException<NullReferenceException>(() => TextAnalyseResult.MergeResults(dictionary1, dictionary2));
        }

        [TestMethod()]
        public void FirstArgumentEmptyCheck_MergeResultsTest()
        {
            // arrange
            string str1 = "asdf";
            string str2 = "af";

            Dictionary<string, int> dictionary1 = new Dictionary<string, int>();
            Dictionary<string, int> dictionary2 = new Dictionary<string, int>();

            dictionary2.Add(str1, 3);
            dictionary2.Add(str2, 5);
            // act

            TextAnalyseResult.MergeResults(dictionary1, dictionary2);

            // assert
            Assert.AreEqual(3, dictionary1[str1]);
            Assert.AreEqual(5, dictionary1[str2]);
        }
        [TestMethod()]
        public void SecondArgumentEmptyCheck_MergeResultsTest()
        {
            // arrange
            string str1 = "asdf";
            string str2 = "af";

            Dictionary<string, int> dictionary1 = new Dictionary<string, int>();
            Dictionary<string, int> dictionary2 = new Dictionary<string, int>();

            dictionary1.Add(str1, 3);
            dictionary1.Add(str2, 5);
            // act

            TextAnalyseResult.MergeResults(dictionary1, dictionary2);

            // assert
            Assert.AreEqual(3, dictionary1[str1]);
            Assert.AreEqual(5, dictionary1[str2]);
        }
        [TestMethod()]
        public void BothArgumentsContainsValuesCheck_MergeResultsTest()
        {
            // arrange
            string str1 = "asdf";
            string str2 = "af";
            string str3 = "fztaf";

            Dictionary<string, int> dictionary1 = new Dictionary<string, int>();
            Dictionary<string, int> dictionary2 = new Dictionary<string, int>();

            dictionary1.Add(str3, 11);
            dictionary2.Add(str1, 3);
            dictionary2.Add(str2, 5);
            // act

            TextAnalyseResult.MergeResults(dictionary1, dictionary2);

            // assert
            Assert.AreEqual(3, dictionary1[str1]);
            Assert.AreEqual(5, dictionary1[str2]);
            Assert.AreEqual(11, dictionary1[str3]);
        }
    }
}
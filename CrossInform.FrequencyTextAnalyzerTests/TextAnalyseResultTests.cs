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
        public void BothArgumentsNullCheck_MergeResultsTest(Dictionary<char[], int> dictionary1, Dictionary<char[], int> dictionary2)
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
            Dictionary<char[], int> dictionary1 = new Dictionary<char[], int>();
            Dictionary<char[], int> dictionary2 = null;
            // act

            // assert
            Assert.ThrowsException<NullReferenceException>(() => TextAnalyseResult.MergeResults(dictionary1, dictionary2));
        }
        [TestMethod()]
        public void SecondArgumentNullCheck_MergeResultsTest()
        {
            // arrange
            Dictionary<char[], int> dictionary1 = null;
            Dictionary<char[], int> dictionary2 = new Dictionary<char[], int>();
            // act

            // assert
            Assert.ThrowsException<NullReferenceException>(() => TextAnalyseResult.MergeResults(dictionary1, dictionary2));
        }

        [TestMethod()]
        public void FirstArgumentEmptyCheck_MergeResultsTest()
        {
            // arrange
            char[] arr1 = new char[] { 'a', 's', 'd', 'f' };
            char[] arr2 = new char[] { 'a', 'f' };

            Dictionary<char[], int> dictionary1 = new Dictionary<char[], int>();
            Dictionary<char[], int> dictionary2 = new Dictionary<char[], int>();

            dictionary2.Add(arr1, 3);
            dictionary2.Add(arr2, 5);
            // act

            TextAnalyseResult.MergeResults(dictionary1, dictionary2);

            // assert
            Assert.AreEqual(3, dictionary1[arr1]);
            Assert.AreEqual(5, dictionary1[arr2]);
        }
        [TestMethod()]
        public void SecondArgumentEmptyCheck_MergeResultsTest()
        {
            // arrange
            char[] arr1 = new char[] { 'a', 's', 'd', 'f' };
            char[] arr2 = new char[] { 'a', 'f' };

            Dictionary<char[], int> dictionary1 = new Dictionary<char[], int>();
            Dictionary<char[], int> dictionary2 = new Dictionary<char[], int>();

            dictionary1.Add(arr1, 3);
            dictionary1.Add(arr2, 5);
            // act

            TextAnalyseResult.MergeResults(dictionary1, dictionary2);

            // assert
            Assert.AreEqual(3, dictionary1[arr1]);
            Assert.AreEqual(5, dictionary1[arr2]);
        }
        [TestMethod()]
        public void BothArgumentsContainsValuesCheck_MergeResultsTest()
        {
            // arrange
            char[] arr1 = new char[] { 'a', 's', 'd', 'f' };
            char[] arr2 = new char[] { 'a', 'f' };
            char[] arr3 = new char[] { 'f', 'z', 't', 'a', 'f' };

            Dictionary<char[], int> dictionary1 = new Dictionary<char[], int>();
            Dictionary<char[], int> dictionary2 = new Dictionary<char[], int>();

            dictionary1.Add(arr3, 11);
            dictionary2.Add(arr1, 3);
            dictionary2.Add(arr2, 5);
            // act

            TextAnalyseResult.MergeResults(dictionary1, dictionary2);

            // assert
            Assert.AreEqual(3, dictionary1[arr1]);
            Assert.AreEqual(5, dictionary1[arr2]);
            Assert.AreEqual(11, dictionary1[arr3]);
        }
    }
}
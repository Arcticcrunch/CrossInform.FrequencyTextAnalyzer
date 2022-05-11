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
    public class TextTripletAnalyserTests
    {
        [TestMethod()]
        [DataRow("as")]
        [DataRow("asdasdBSD")]
        [DataRow("foo bar barbar foofoofoo")]
        public void RandomText_AnalyseText_Test(string value)
        {
            // arrange

            TextTripletAnalyser analyser = new TextTripletAnalyser();
            MockTextProvider textProvider = new MockTextProvider();
            textProvider.SetText(value);

            // act
            analyser.SyncAnalyseText(textProvider, null);

            // assert
        }

        [TestMethod()]
        [DataRow(null)]
        [DataRow("")]
        public void NullOrEmptyCheck_AnalyseText_Test(string value)
        {
            // arrange

            TextTripletAnalyser analyser = new TextTripletAnalyser();
            MockTextProvider textProvider = new MockTextProvider();
            textProvider.SetText(value);

            // act

            // assert
            Assert.ThrowsException<Exception>(() => analyser.SyncAnalyseText(textProvider, null));
        }

        [TestMethod()]
        [DataRow("Text Lorem ipsum.txt")]
        public void AnalyseFromFileCheck_AnalyseText_Test(string value)
        {
            // arrange

            FileTextReader fileContent = new FileTextReader();
            fileContent.OpenFile(value);

            TextTripletAnalyser analyser = new TextTripletAnalyser();

            // act

            TextAnalyseResult result = (TextAnalyseResult)analyser.SyncAnalyseText(fileContent, null);

            // assert

            Assert.AreEqual(456, result.StatisticsResult.Count);
        }
        

        [TestMethod()]
        [DataRow("Text Lorem ipsum.txt", 1)]
        [DataRow("Text Lorem ipsum.txt", 10)]
        [DataRow("Text Lorem ipsum.txt", 24)]
        [DataRow("Text Lorem ipsum.txt", 50)]
        public void AnalyseFromFileCheckMultithreaded_AnalyseText_Test(string value, int value2)
        {
            // arrange

            FileTextReader fileContent = new FileTextReader();
            fileContent.OpenFile(value);

            TextTripletAnalyser analyser = new TextTripletAnalyser();
            TextAnalyseParams parameters = new TextAnalyseParams(value2);

            // act

            TextAnalyseResult result = (TextAnalyseResult)analyser.SyncAnalyseText(fileContent, parameters);

            // assert

            Assert.AreEqual(456, result.StatisticsResult.Count);
        }

    }
}
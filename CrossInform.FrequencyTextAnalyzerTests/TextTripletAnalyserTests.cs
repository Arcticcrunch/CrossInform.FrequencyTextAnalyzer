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
            analyser.SyncAnalyseText(textProvider);

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
            Assert.ThrowsException<Exception>(() => analyser.SyncAnalyseText(textProvider));
        }
    }
}
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
    public class TextUtilsTests
    {
        [TestMethod()]
        public void FindCharSequenceInTextTest()
        {
            // arrange

            string text = "asdf  ffd asdf asdq bb";
            string mostCommon = "asd";
            string secondCommon = "sdf";
            string thirdCommon = "ffd";

            string fifthCharsSequence = "bb";

            // act
            var dict = TextUtils.FindCharSequenceInText(text, 3, null);

            // assert
            Assert.AreEqual(4, dict.Keys.Count);
            Assert.AreEqual(3, dict[mostCommon]);
            Assert.AreEqual(2, dict[secondCommon]);
            Assert.AreEqual(1, dict[thirdCommon]);
            Assert.ThrowsException<KeyNotFoundException>(() => dict[fifthCharsSequence]);
        }
    }
}
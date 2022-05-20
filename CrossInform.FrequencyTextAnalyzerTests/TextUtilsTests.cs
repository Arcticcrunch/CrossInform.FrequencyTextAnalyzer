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

        [TestMethod()]
        public void QuickSortTest()
        {
            int[] a = new int[] { 3, 4, 0, -5 };

            Quicksort(a, 0, a.Length - 1);
            Assert.AreEqual(-5, a[0]);
            Assert.AreEqual(0, a[1]);
            Assert.AreEqual(3, a[2]);
            Assert.AreEqual(4, a[3]);
        }





        private void Quicksort(int[] x, int low, int hi)
        {
            if (low < hi)
            {
                // int referenceElement = Sort(x, low, hi);
                int referenceElement = 0;
                
                int pivot = x[(low + hi) / 2];
                int i = low;
                int j = hi;
                while (true)
                {
                    while (x[i] < pivot)
                    {
                        i++;
                    }
                    while (x[j] > pivot)
                    {
                        j--;
                    }
                    if (i >= j)
                    {
                        referenceElement = j;
                        break;
                    }
                    int temp = x[i];
                    x[i] = x[j];
                    x[j] = temp;
                    i++;
                    j--;
                }

                //Sort(x, low, hi);
                Quicksort(x, low, referenceElement);
                Quicksort(x, referenceElement + 1, hi);
            }
        }

        [Obsolete()]
        private int Sort(int[] x, int low, int hi)
        {
            int pivot = x[(low + hi) / 2];
            int i = low;
            int j = hi;
            while(true)
            {
                while(x[i] < pivot)
                {
                    i++;
                }
                while(x[j] > pivot)
                {
                    j--;
                }
                if (i >= j)
                {
                    return j;
                }
                int temp = x[i];
                x[i] = x[j];
                x[j] = temp;
                i++;
                j--;
            }
        }
        
    }
}
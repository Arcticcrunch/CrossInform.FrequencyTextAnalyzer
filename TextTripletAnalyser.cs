using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CrossInform.FrequencyTextAnalyzer.Interfaces;

namespace CrossInform.FrequencyTextAnalyzer
{
    public class TextTripletAnalyser : ITextAnalyser
    {
        private bool isAbortRequested = false;
        private bool isAnalysing = false;

        public ITextAnalyseResult AsyncAnalyseText(ITextProvider textProvider)
        {
            isAnalysing = true;
            Thread.Sleep(4000);
            TextAnalyseResult result = new TextAnalyseResult(100, textProvider.GetText(), textProvider);
            isAnalysing = false;
            return result;
        }

        public bool IsAnalysing
        {
            get
            {
                return isAnalysing;
            }
        }

        public void RequestAbort()
        {
            isAbortRequested = true;
        }
    }
}

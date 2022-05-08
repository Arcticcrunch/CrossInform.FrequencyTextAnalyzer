using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrossInform.FrequencyTextAnalyzer.Interfaces;

namespace CrossInform.FrequencyTextAnalyzer
{
    public class TextAnalyseResult : ITextAnalyseResult
    {
        private double executionDuration = 0;
        private string result;
        private ITextProvider textProvider;

        public TextAnalyseResult(double executionDuration, string result, ITextProvider textProvider)
        {
            this.executionDuration = executionDuration;
            this.result = result;
            this.textProvider = textProvider;
        }

        public double GetExecutionDuration()
        {
            return executionDuration;
        }

        public ITextProvider GetOriginText()
        {
            return this.textProvider;
        }

        public string GetResult()
        {
            return result;
        }
    }
}

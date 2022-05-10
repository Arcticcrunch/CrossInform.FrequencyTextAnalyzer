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
        private AnalyseResultState resultState = AnalyseResultState.ReadyToStart;
        private string result;
        private ITextProvider textProvider;

        public TextAnalyseResult(AnalyseResultState resultState, double executionDuration, string result, ITextProvider textProvider)
        {
            this.executionDuration = executionDuration;
            this.resultState = resultState;
            this.result = result;
            this.textProvider = textProvider;
        }

        public AnalyseResultState ResultState
        {
            get
            {
                return resultState;
            }
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

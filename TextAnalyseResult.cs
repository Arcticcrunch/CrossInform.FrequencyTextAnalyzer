using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrossInform.FrequencyTextAnalyzer.Interfaces;

namespace CrossInform.FrequencyTextAnalyzer
{
    public class TextAnalyseResult : ITextStatisticsAnalyseResult
    {
        private double executionDuration = 0;
        private AnalyseResultState resultState;
        private Dictionary<char[], int> statisticsResult = new Dictionary<char[], int>();
        private ITextProvider textProvider;

        public TextAnalyseResult(AnalyseResultState resultState, double executionDuration, Dictionary<char[], int> result, ITextProvider textProvider)
        {
            this.executionDuration = executionDuration;
            this.resultState = resultState;
            this.statisticsResult = result;
            this.textProvider = textProvider;
        }

        public AnalyseResultState ResultState
        {
            get
            {
                return resultState;
            }
        }

        public Dictionary<char[], int> StatisticsResult
        {
            get
            {
                return statisticsResult;
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
    }
}

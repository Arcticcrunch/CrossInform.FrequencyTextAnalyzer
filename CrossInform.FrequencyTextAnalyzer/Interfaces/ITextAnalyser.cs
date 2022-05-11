using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossInform.FrequencyTextAnalyzer.Interfaces
{
    public interface ITextAnalyser
    {
        ITextStatisticsAnalyseResult SyncAnalyseText(ITextProvider textProvider, TextAnalyseParams parameters);
        int ThreadsCount { get; set; }
        bool IsAnalysing { get; }
        bool IsAbortRequested { get; }
        void RequestAbort();
    }
}

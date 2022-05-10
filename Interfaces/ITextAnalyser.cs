using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossInform.FrequencyTextAnalyzer.Interfaces
{
    public interface ITextAnalyser
    {
        ITextAnalyseResult SyncAnalyseText(ITextProvider textProvider);
        int MinThreadsCount { get; set; }
        bool IsAnalysing { get; }
        void RequestAbort();
    }
}

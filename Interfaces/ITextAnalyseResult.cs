using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossInform.FrequencyTextAnalyzer.Interfaces
{
    public interface ITextAnalyseResult
    {
        string GetResult();
        AnalyseResultState ResultState { get; }
        double GetExecutionDuration();
        ITextProvider GetOriginText();
    }

    public enum AnalyseResultState
    {
        ReadyToStart, Complete, Aborted
    }
}

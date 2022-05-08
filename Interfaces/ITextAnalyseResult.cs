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
        double GetExecutionDuration();
        ITextProvider GetOriginText();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossInform.FrequencyTextAnalyzer.Interfaces
{
    /// <summary>
    /// Интерфейс для реализации хранения статистического анализа текста (какие были последовательности символы и их кол-во)
    /// </summary>
    public interface ITextStatisticsAnalyseResult
    {
        Dictionary<char[], int> StatisticsResult { get; }
        AnalyseResultState ResultState { get; }
        double GetExecutionDuration();
        ITextProvider GetOriginText();
    }

    public enum AnalyseResultState
    {
        ReadyToStart, Complete, Aborted
    }
}

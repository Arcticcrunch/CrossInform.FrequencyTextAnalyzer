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
        Dictionary<string, int> StatisticsResult { get; }
        void MergeResults(Dictionary<string, int> secondResult);

        AnalyseResultState ResultState { get; }
        long GetExecutionDuration();
        ITextProvider GetOriginText();
    }

    public enum AnalyseResultState
    {
        ReadyToStart, Complete, Aborted
    }
}

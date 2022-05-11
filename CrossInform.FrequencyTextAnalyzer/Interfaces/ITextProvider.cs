using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossInform.FrequencyTextAnalyzer.Interfaces
{
    /// <summary>
    /// Интерфейс источника текста для анализа
    /// </summary>
    public interface ITextProvider
    {
        string GetText();
    }
}

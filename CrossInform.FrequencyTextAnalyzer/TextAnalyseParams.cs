using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossInform.FrequencyTextAnalyzer
{
    /// <summary>
    /// Параметры анализа текста
    /// </summary>
    public class TextAnalyseParams
    {
        public const int MIN_THREADS_COUNT = 1;
        public const int MAX_THREADS_COUNT = 48;


        private int threadsCount = 1;

        public TextAnalyseParams()
        {
        }
        /// <summary>
        /// Параметры анализа текста
        /// </summary>
        /// <param name="threadsCount">Максимальное кол-во используемых потоков от MIN_THREADS_COUNT до MAX_THREADS_COUNT</param>
        public TextAnalyseParams(int threadsCount)
        {
            this.threadsCount = MathUtils.Clamp(MIN_THREADS_COUNT, MAX_THREADS_COUNT, threadsCount);
        }

        public int ThreadsCount
        {
            get
            {
                return threadsCount;
            }

            set
            {
                threadsCount = value;
            }
        }
    }
}

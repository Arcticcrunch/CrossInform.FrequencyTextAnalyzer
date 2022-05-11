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
        private long executionDuration = 0;
        private AnalyseResultState resultState;
        private Dictionary<char[], int> statisticsResult = new Dictionary<char[], int>();
        private ITextProvider textProvider;

        public TextAnalyseResult(AnalyseResultState resultState, long executionDuration, Dictionary<char[], int> result, ITextProvider textProvider)
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

        public long GetExecutionDuration()
        {
            return executionDuration;
        }

        public ITextProvider GetOriginText()
        {
            return this.textProvider;
        }


        // TODO: Вынести этот мтеов в отдельный Util класс т.к. в других реализациях ITextStatisticsAnalyseResult он может понадобиться 

        /// <summary>
        /// Добавляет элементы результирующей коллекции
        /// </summary>
        /// <param name="secondResult">Вторая последовательность (её элементы будут добавлен к первой)</param>
        /// <returns></returns>
        public void MergeResults( Dictionary<char[], int> secondResult)
        {
            // Использование статического варианта
            MergeResults(statisticsResult, secondResult);

            
        }

        /// <summary>
        /// Статический метод объеденения двух результатов коллекций
        /// </summary>
        /// <param name="firstResult">Первая последовательность (к ней будут добавлены элементы второй)</param>
        /// <param name="secondResult">Вторая последовательность (её элементы будут добавлен к первой)</param>
        public static void MergeResults(Dictionary<char[], int> firstResult, Dictionary<char[], int> secondResult)
        {
            if (firstResult == null || secondResult == null)
                throw new NullReferenceException();

            var keys = secondResult.Keys;
            foreach (var key in keys)
            {
                if (firstResult.ContainsKey(key))
                {
                    firstResult[key] = firstResult[key] + secondResult[key];
                }
                else
                {
                    firstResult.Add(key, secondResult[key]);
                }
            }
        }
    }
}

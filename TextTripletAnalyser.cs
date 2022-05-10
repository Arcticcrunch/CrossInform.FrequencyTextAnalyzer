using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using CrossInform.FrequencyTextAnalyzer.Interfaces;

namespace CrossInform.FrequencyTextAnalyzer
{
    public class TextTripletAnalyser : ITextAnalyser
    {
        public const int MIN_THREADS_COUNT = 1;

        private bool isAbortRequested = false;
        private bool isAnalysing = false;
        private int minThreadsCount = MIN_THREADS_COUNT;

        /// <summary>
        /// Синхронный вариант метода анализа текста в многопоточном режиме.
        /// </summary>
        /// <param name="textProvider"></param>
        /// <returns></returns>
        public ITextAnalyseResult SyncAnalyseText(ITextProvider textProvider)
        {
            isAnalysing = true;
            Stopwatch sw = new Stopwatch();
            sw.Start();

            string text = textProvider.GetText();
            // Этапы
            // 1) Определить кол-во потоков
            // 2) Разделить входной текст на соответствующее кол-во сегментов (с учётом пробелов)
            // 3) Запустить N-потоков которые будут анализировать свои сегменты и сохранять результаты в свои бакеты
            // 4) Подождать пока все потоки завершатся после чего соеденить результаты
            // 5) Вывести статистику по результатам

            AnalyseResultState resultState = AnalyseResultState.Complete;
            if (isAbortRequested)
            {
                resultState = AnalyseResultState.Aborted;
            }
            sw.Stop();
            TextAnalyseResult result = new TextAnalyseResult(resultState, sw.ElapsedMilliseconds, textProvider.GetText(), textProvider);
            isAnalysing = false;
            return result;
        }

        public bool IsAnalysing
        {
            get
            {
                return isAnalysing;
            }
        }

        public int MinThreadsCount
        {
            get
            {
                return minThreadsCount;
            }

            set
            {
                // TODO: решить должен ли здесь вобде быть экспешен, или достаточно задавать минимальное значение...
                if (value < 1)
                    throw new Exception("Минимальное кол-во потоков: " + MIN_THREADS_COUNT + "! Получено значение: " + value);
                this.minThreadsCount = value;
            }
        }

        public void RequestAbort()
        {
            isAbortRequested = true;
        }
    }
}

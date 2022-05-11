using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using CrossInform.FrequencyTextAnalyzer.Interfaces;

namespace CrossInform.FrequencyTextAnalyzer
{
    // TODO: Данный класс уже имеет функционал для нахождения n-последовательностей символов в тексте.
    // Достаточно лишь изменить имя класса и добавить ввод желаемого кол-ва символов

    /// <summary>
    /// Частотный анализатор текста находящий триплеты в тексте (3 буквы подряд).
    /// </summary>
    public class TextTripletAnalyser : ITextAnalyser
    {
        public const int CHARS_COUNT_TO_SEARCH = 3;
        
        private bool isAbortRequested = false;
        private bool isAnalysing = false;
        private int threadsCount = TextAnalyseParams.MAX_THREADS_COUNT;
        private int charsCountToSearch = CHARS_COUNT_TO_SEARCH;

        /// <summary>
        /// Синхронный вариант метода (останавливает родительский поток) анализа текста в многопоточном режиме.
        /// </summary>
        /// <param name="textProvider">Эклемпляр класса реализующий интерфейс ITextProvider</param>
        /// <param name="parameters">Экземпляр класса параметров для анализа. Может быть null</param>
        /// <returns></returns>
        public ITextStatisticsAnalyseResult SyncAnalyseText(ITextProvider textProvider, TextAnalyseParams parameters)
        {
            // Этапы
            // 1) Определить кол-во потоков
            // 2) Разделить входной текст на соответствующее кол-во сегментов (с учётом пробелов)
            // 3) Запустить N-потоков которые будут анализировать свои сегменты и сохранять результаты в свои бакеты
            // 4) Подождать пока все потоки завершатся после чего соеденить результаты
            // 5) Вывести статистику по результатам

            isAbortRequested = false;
            
            string text = textProvider.GetText();
            if (String.IsNullOrEmpty(text))
            {
                throw new Exception("Входная строка была пуста или null!");
            }

            // Копирование кол-ва потоков в отдельнюу переменную для защиты от изменения в процессе работы алгоритма
            
            int currentThreadsCount = threadsCount;
            if (parameters != null)
            {
                currentThreadsCount = MathUtils.Clamp(TextAnalyseParams.MIN_THREADS_COUNT, TextAnalyseParams.MAX_THREADS_COUNT, parameters.ThreadsCount);
                threadsCount = currentThreadsCount;
            }

            isAnalysing = true;
            Stopwatch sw = new Stopwatch();
            sw.Start();

            // Разделение текста на кол-во сегментов соответствующее кол-ву желаемых потоков
            string[] textSegments = SplitTextOnSegments(text, currentThreadsCount);
            int segmentsCount = textSegments.Length;

            // Создание массива тасков в количестве равном полученному кол-ву сегментов
            Task<Dictionary<string, int>>[] tasks = new Task<Dictionary<string, int>>[segmentsCount];
            for (int i = 0; i < segmentsCount; i++)
            {
                // Создал локальную переменную t т.к. textSegments выходит за границы контекста асинхронной задачи и ложит программу.
                string t = textSegments[i];
                tasks[i] = Task<Dictionary<string, int>>.Factory.StartNew(() => { return TextUtils.FindCharSequenceInText(t, charsCountToSearch, this); });
            }

            Dictionary<string, int>[] taskResults = new Dictionary<string, int>[segmentsCount];

            // Попытка считать результаты выполнения тасков. Если результат не готов - поток блокируется до его получения
            // Таким образом собираются результаты всех тасков (вместо использования .Join)
            for (int i = 0; i < segmentsCount; i++)
            {
                taskResults[i] = tasks[i].Result;
            }

            AnalyseResultState resultState = AnalyseResultState.Complete;
            Dictionary<string, int> resultDictionary = new Dictionary<string, int>();
            

            // Проверка был ли вызван аборт
            if (isAbortRequested)
            {
                resultState = AnalyseResultState.Aborted;
            }
            else
            {
                // Если аборта небыло - объеденение коллекций всех потоков в результирующую коллекцию
                foreach (var taskResult in taskResults)
                {
                    TextAnalyseResult.MergeResults(resultDictionary, taskResult);
                }
            }

            sw.Stop();

            TextAnalyseResult result = new TextAnalyseResult(resultState, sw.ElapsedMilliseconds, resultDictionary, textProvider);
            
            isAnalysing = false;
            return result;
        }


        // TODO: Вынести этот метод в отдельный Util класс т.к. в каждой реализации интерфейса ITextAnalyser будет необходим подобный класс
        // TODO: Сделать метод публичным и написать к нему тест

        /// <summary>
        /// Разделяет входной текст на сегменты по знаку пробела. Сегментов может быть меньше ожидаемого из-за меньшего
        /// кол-ва пробелов или их отсутствия.
        /// </summary>
        /// <param name="text">Водной текст</param>
        /// <param name="segmentsCount">Кол-во сегментов</param>
        /// <returns></returns>
        private string[] SplitTextOnSegments(string text, int segmentsCount)
        {
            if (segmentsCount <= 1 )
                return new string[] { text };
            else
            {
                List<string> segmentsList = new List<string>();
                StringBuilder buffer = new StringBuilder();
                int segmentLength = text.Length / segmentsCount;
                int currentSegmentLength = 0;
                char ch;

                // Разбиение текста на сегменты делается за 1 проход. Исходя из нужного кол-ва сегментов (потоков) подсчитывается длинна сегмента. 
                // Цикл копирует все символы в буфер пока нужная длинна не наберётся и не будет найден следующий пробел. Далее буфер копируется в список и очищается. 
                for (int i = 0; i < text.Length; i++)
                {
                    ch = text[i];
                    if (currentSegmentLength >= segmentLength)
                    {
                        if (ch == ' ')
                        {
                            segmentsList.Add(buffer.ToString());
                            buffer.Clear();
                            currentSegmentLength = 0;
                        }
                        else
                        {
                            buffer.Append(ch);
                            currentSegmentLength++;
                        }
                    }
                    else
                    {
                        buffer.Append(ch);
                        currentSegmentLength++;
                    }
                }

                if (buffer.Length != 0)
                    segmentsList.Add(buffer.ToString());

                return segmentsList.ToArray();
            }
        }


        public bool IsAnalysing
        {
            get
            {
                return isAnalysing;
            }
        }

        public int ThreadsCount
        {
            get
            {
                return threadsCount;
            }

            set
            {
                // TODO: решить должен ли здесь вобде быть экспешен, или достаточно задавать max значение...
                if (value < TextAnalyseParams.MIN_THREADS_COUNT || value > TextAnalyseParams.MAX_THREADS_COUNT)
                    throw new Exception("Заданное число поток вне допустимых границ: " + TextAnalyseParams.MAX_THREADS_COUNT + "-" + TextAnalyseParams.MAX_THREADS_COUNT + "! Получено значение: " + value);
                this.threadsCount = value;
            }
        }

        public bool IsAbortRequested
        {
            get
            {
                return isAbortRequested;
            }
        }

        public void RequestAbort()
        {
            isAbortRequested = true;
        }
    }

}

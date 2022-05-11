using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CrossInform.FrequencyTextAnalyzer.Interfaces;

namespace CrossInform.FrequencyTextAnalyzer
{
    class Program
    {
        #region Графика
        public const int CONSOLE_UPDATE_INTERVAL_MILLIS = 200;

        private static TimeSpan timePassed;
        private static DateTime lastUpdateTime;
        private static int dotsCount = 0;
        private static int maxDots = 3;
        #endregion

        private static bool isAnalysing = true;
        private static ITextAnalyser textAnalyser;
        private static FileTextReader textProvider;
        private static TextAnalyseResult result;

        private static Thread consoleOutputLoopThread;
        private static Thread analyseThread;


        static void Main(string[] args)
        {
            Console.Title = "Частотный анализатор текста";

            textAnalyser = new TextTripletAnalyser();

            //MockTextProvider textProvider = new MockTextProvider();
            //textProvider.SetText("asdf  ffd asdf asdq");

            textProvider = new FileTextReader();
            textProvider.OpenFile("Text Lorem ipsum.txt");


            consoleOutputLoopThread = new Thread(TextInConsoleAnalyseLoop);
            consoleOutputLoopThread.Start();

            analyseThread = new Thread(AnalyseTextLoop);
            analyseThread.Start();

            //analyseTask = new Task<ITextStatisticsAnalyseResult>(() => 
            //{
            //    isAnalysing = true;
            //    ITextStatisticsAnalyseResult r = textAnalyser.SyncAnalyseText(textProvider);
            //    isAnalysing = false;
            //    return r;
            //});
            //analyseTask.Start();
            


            while(isAnalysing)
            {
                var s = Console.ReadKey();
                if(s.Key == ConsoleKey.Escape)
                {
                    textAnalyser.RequestAbort();
                }
            }



            
            //Console.WriteLine("{0}Частотный анализ завершен.{0}{1}{0}{0}Время выполнения: {2} мс",
            //    Environment.NewLine, result.GetResult(), result.GetExecutionDuration());

            Console.ReadKey();
        }

        static private void TextInConsoleAnalyseLoop()
        {
            Console.Clear();
            string text = "Анализ текста";
            Console.WriteLine(text);
            timePassed = new TimeSpan(0);
            lastUpdateTime = DateTime.Now;

            while (isAnalysing)
            {
                //if (textAnalyser.IsAnalysing == false)
                //{
                //    isAnalysing = false;
                //    break;
                //}

                timePassed = timePassed.Add(DateTime.Now - lastUpdateTime);
                dotsCount++;
                if (dotsCount > maxDots)
                    dotsCount = 0;
                Console.SetCursorPosition(text.Length, 0);
                string dots = "";
                for (int i = 0; i < maxDots; i++)
                {
                    if (i < dotsCount)
                        dots += ".";
                    else dots += " ";
                }
                Console.Write(dots);

                Console.SetCursorPosition(0, 1);
                Console.WriteLine("Прошло времени: {0}", timePassed.ToString(@"hh\:mm\:ss"));
                Console.WriteLine("ESC - для отмены");

                lastUpdateTime = DateTime.Now;
                Thread.Sleep(CONSOLE_UPDATE_INTERVAL_MILLIS);
            }

            // Ожидание потока который анализирует данные. Это гарантирует то что когда этот поток (выводящий текст в консоль)
            // попытается получить результат анализа - он будет готов (исключит гонку потоков т.к. оба потока зависят от статической переменной)
            analyseThread.Join();

            Console.Clear();
            if (textAnalyser.IsAbortRequested)
            {
                Console.WriteLine("Анализ прерван. Прошло времени: " + timePassed.ToString(@"hh\:mm\:ss"));
            }
            else
            {
                Console.WriteLine("Анализ завершен.");
                Console.WriteLine(TextUtils.FormatTextAnalyseResult(result));
            }

        }

        static private void AnalyseTextLoop()
        {
            isAnalysing = true;
            result = (TextAnalyseResult)textAnalyser.SyncAnalyseText(textProvider);
            isAnalysing = false;
        }
    }
}

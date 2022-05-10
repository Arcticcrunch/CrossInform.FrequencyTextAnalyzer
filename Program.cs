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



        static void Main(string[] args)
        {
            textAnalyser = new TextTripletAnalyser();
            ITextProvider textProvider = new MockTextProvider();
            

            Thread th = new Thread(TextInConsoleAnalyseLoop);
            th.Start();

            ITextAnalyseResult result = textAnalyser.SyncAnalyseText(textProvider);

            while(isAnalysing)
            {
                var s = Console.ReadKey();
                if(s.Key == ConsoleKey.Escape)
                {
                    textAnalyser.RequestAbort();
                    //break;
                }
            }



            //Console.Title = "Частотный анализатор текста";
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
                if (textAnalyser.IsAnalysing == false)
                {
                    isAnalysing = false;
                    break;
                }

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

                lastUpdateTime = DateTime.Now;
                Thread.Sleep(CONSOLE_UPDATE_INTERVAL_MILLIS);
            }
            Console.Clear();
            Console.WriteLine("Завершено.");
        }
    }
}

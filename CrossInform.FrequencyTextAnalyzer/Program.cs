using System;
using System.IO;
using System.Threading;
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

        private static bool isExitRequested = false;
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

            while (isExitRequested == false)
            {
                string path;
                Console.Clear();
                Console.WriteLine("Введите путь к текстовому файлу для анализа или пустую строку для выхода:");

                bool isPathInputCanceled = false;
                while (true)
                {
                    try
                    {
                        path = Console.ReadLine();
                        if (path == "")
                        {
                            isPathInputCanceled = true;
                            break;
                        }
                        if (File.Exists(path))
                        {
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Данный файл не существует. Повторно введите путь к файлу или пустую строку для выхода:");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.Clear();
                        Console.WriteLine("При открытии файла возникла ошибка: " + e.Message + ". Повторно введите путь к файлу или пустую строку для выхода:");
                    }
                }
                if (isPathInputCanceled)
                    break;

                consoleOutputLoopThread = new Thread(TextInConsoleAnalyseLoop);
                consoleOutputLoopThread.Start();

                analyseThread = new Thread(AnalyseTextLoop);
                analyseThread.Start();
                



                while (isAnalysing)
                {
                    var s = Console.ReadKey();
                    if (s.Key == ConsoleKey.Escape)
                    {
                        textAnalyser.RequestAbort();
                    }
                }

                Console.ReadKey();
            }
        }


        /// <summary>
        /// Данный метод должен вызываться в собственном потоке. Отвечает за вывод текста в консоль пока выполняется анализ.
        /// </summary>
        private static void TextInConsoleAnalyseLoop()
        {
            Console.Clear();
            string text = "Анализ текста";
            Console.WriteLine(text);
            timePassed = new TimeSpan(0);
            lastUpdateTime = DateTime.Now;

            while (isAnalysing)
            {
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
                // Задержка потока обновления текста в консоли (нужно для анимации)
                Thread.Sleep(CONSOLE_UPDATE_INTERVAL_MILLIS);
            }

            // Ожидание потока который анализирует данные. Это гарантирует то что когда этот поток (выводящий текст в консоль)
            // попытается получить результат анализа - он будет готов (исключит гонку потоков т.к. оба потока зависят от статической переменной)
            analyseThread.Join();

            Console.Clear();
            if (textAnalyser.IsAbortRequested)
            {
                Console.WriteLine("Анализ прерван. Прошло времени: " + timePassed.ToString(@"hh\:mm\:ss"));
                Console.WriteLine("\nНажмите любую кнопку для продолжения...");
            }
            else
            {
                Console.WriteLine("Анализ текста на наличие триплетов (3 идущих подряд буквы слова) завершен.");
                Console.WriteLine(TextUtils.FormatTextAnalyseResult(result));
            }

        }

        /// <summary>
        /// Данный метод должен вызываться в собственном потоке. Начинает непосредственный анализ текста
        /// </summary>
        private static void AnalyseTextLoop()
        {
            isAnalysing = true;
            result = (TextAnalyseResult)textAnalyser.SyncAnalyseText(textProvider);
            isAnalysing = false;
        }

    }
}

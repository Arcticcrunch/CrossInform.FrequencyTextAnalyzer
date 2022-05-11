using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrossInform.FrequencyTextAnalyzer.Interfaces;

namespace CrossInform.FrequencyTextAnalyzer
{
    // Реализует интерфейс ITextProvider. Служит как источник текста из файла для частотного анализа
    public class FileTextReader : ITextProvider
    {
        private string text;

        /// <summary>
        /// Открыть и считать файл
        /// </summary>
        /// <param name="filePath">Путь к файлу (может быть как относительный так и абсолютный)</param>
        public void OpenFile(string filePath)
        {
            this.text = File.ReadAllText(filePath);
        }

        public string GetText()
        {
            return text;
        }
    }
}

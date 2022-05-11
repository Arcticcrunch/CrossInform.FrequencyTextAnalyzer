using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrossInform.FrequencyTextAnalyzer.Interfaces;

namespace CrossInform.FrequencyTextAnalyzer
{
    public class FileTextReader : ITextProvider
    {
        private string text;

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

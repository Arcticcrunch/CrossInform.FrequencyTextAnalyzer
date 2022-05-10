using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrossInform.FrequencyTextAnalyzer.Interfaces;

namespace CrossInform.FrequencyTextAnalyzer
{
    public class MockTextProvider : ITextProvider
    {
        private string text = "Test text, lalalalalalalla";

        public void SetText(string text)
        {
            this.text = text;
        }
        public string GetText()
        {
            return this.text;
        }
    }
}

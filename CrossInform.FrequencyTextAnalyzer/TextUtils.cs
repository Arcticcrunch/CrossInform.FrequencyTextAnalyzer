using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossInform.FrequencyTextAnalyzer
{
    public static class TextUtils
    {
        /// <summary>
        /// Синхронный однопоточный метод поиска n-последовательности символов в тексте
        /// </summary>
        /// <param name="text">Входной текст</param>
        /// <param name="charCount">Кол-во символов для поиска</param>
        /// <returns></returns>
        public static Dictionary<char[], int> FindCharSequenceInText(string text, int charCount)
        {
            Dictionary<char[], int> result = new Dictionary<char[], int>();

            char[] buffer = new char[charCount];
            int curentCharsCount = 0;
            int index = 0;

            //while (isAbortRequested == false && index < text.Length)
            //{
            //    char ch = text[index];
            //    if (char.IsLetter(ch))
            //    {
            //        buffer[curentCharsCount] = ch;
            //        curentCharsCount++;
            //    }
            //    else curentCharsCount = 0;
            //
            //    // Набралось нужное кол-во символов. Можно добавить его в словарь.
            //    //if (curentCharsCount == charCount)
            //    //{
            //    //    if (result.ContainsKey(buffer))
            //    //}
            //
            //    index++;
            //}

            return result;
        }
    }
}

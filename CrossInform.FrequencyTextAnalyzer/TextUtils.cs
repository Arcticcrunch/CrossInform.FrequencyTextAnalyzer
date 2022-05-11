using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrossInform.FrequencyTextAnalyzer.Interfaces;

namespace CrossInform.FrequencyTextAnalyzer
{
    public static class TextUtils
    {
        /// <summary>
        /// Синхронный однопоточный метод поиска n-последовательности символов в тексте
        /// </summary>
        /// <param name="text">Входной текст</param>
        /// <param name="charCount">Кол-во символов для поиска</param>
        /// <param name="analyser">Ссылка на объект ITextAnalyser (для проверки запрошена ли отмена). Использование null допускается</param>
        /// <returns></returns>
        public static Dictionary<string, int> FindCharSequenceInText(string text, int charCount, ITextAnalyser analyser)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            char[] buffer = new char[charCount];
            int curentCharsCount = 0;
            int index = 0;

            while (index < text.Length)
            {
                if (analyser != null)
                {
                    if (analyser.IsAbortRequested)
                        break;
                }

                char ch = text[index];
                if (char.IsLetter(ch))
                {
                    buffer[curentCharsCount] = char.ToLower(ch);
                    curentCharsCount++;
                }
                else curentCharsCount = 0;
            
                // Набралось нужное кол-во символов. Можно добавить его в словарь.
                if (curentCharsCount == charCount)
                {
                    // TODO: метод с инстанцированием новой строки каждый раз при проверке не самый эфективный... возможно стоит использовать хэшсет...
                    string bufferString = new string(buffer);
                    if (result.ContainsKey(bufferString))
                    {
                        result[bufferString] = result[bufferString] + 1;
                    }
                    else
                    {
                        result.Add(bufferString, 1);
                    }

                    // Сдвиг символов в буфере к концу массива (первый элемент выдавливается в конец)
                    char temp = buffer[curentCharsCount - 1];
                    int shiftPointer = 0;
                    while(shiftPointer < curentCharsCount - 1)
                    {
                        buffer[shiftPointer] = buffer[shiftPointer + 1];
                        buffer[shiftPointer + 1] = temp;
                        shiftPointer++;
                    }
                    curentCharsCount--;
                }
            
                index++;
            }

            return result;
        }
    }
}

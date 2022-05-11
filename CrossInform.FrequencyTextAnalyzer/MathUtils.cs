using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossInform.FrequencyTextAnalyzer
{
    /// <summary>
    /// Класс для вспомогательных математических функций и работы с числами
    /// </summary>
    public static class MathUtils
    {
        /// <summary>
        /// Ограничить число в диапазоне от min до max
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int Clamp(int min, int max, int value)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }
    }
}

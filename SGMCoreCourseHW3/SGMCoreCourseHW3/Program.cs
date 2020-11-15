using System;
using System.Text.RegularExpressions;

namespace SGMCoreCourseHW3
{
    internal class Program
    {
        public delegate void TextProcessor(string str);

        private static void Main(string[] args)
        {
            var alphaNumbericCollector = new AlphaNumbericCollector();
            var stringCollector = new StringCollector();
            AlphaNumericProcessor += alphaNumbericCollector.ProccessString;
            StringProcessor += stringCollector.AddString;
            var rgx = new Regex(@"\d+");
            string line;
            while (true)
            {
                line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    break;
                if (rgx.IsMatch(line))
                    AlphaNumericProcessor?.Invoke(line);
                else
                    StringProcessor?.Invoke(line);
            }

            AlphaNumericProcessor -= alphaNumbericCollector.ProccessString;
            StringProcessor -= stringCollector.AddString;

            alphaNumbericCollector.PrintAll();
            stringCollector.PrintAll();

            Console.ReadKey();
        }

        public static event TextProcessor AlphaNumericProcessor;
        public static event TextProcessor StringProcessor;
    }
}

/*
 *Event, Delegate, Action/Func/Predicate delegate наступне завдання.
 * Консольна програма має дозволяти користувачеві вводити безмежну кількість стрічок.
 * Стрічку, в якій є хоча б одна цифра, повинен опрацьовувати клас з назвою AlphaNumbericCollector, в іншому випадку клас StringCollector.
 * Згадані вище класи повинні зберігати стрічки в списку (накопичувати їх).
 * Взаємодію між вводом даних і згаданими класами слід реалізувати через події/делегати.
 */
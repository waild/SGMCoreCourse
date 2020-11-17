using System;
using System.Globalization;
using System.Linq;

namespace SGMCoreCourseHW4
{
    class Program
    {
        static void Main(string[] args)
        {
            var players = "Davis, Clyne, Fonte, Hooiveld, Shaw, Davis, Schneiderlin, Cork, Lallana, Rodriguez, Lambert";
            var formatedPlayers = players.Split(", ").Select((str, index) => $"{index + 1}. {str}");

            foreach (var fp in formatedPlayers)
            {
                Console.WriteLine(fp);
            }

            Console.WriteLine("-----------");

            var fullPlayersInfo =
                "Jason Puncheon, 26/06/1986; Jos Hooiveld, 22/04/1983; Kelvin Davis, 29/09/1976; Luke Shaw, 12/07/1995; Gaston Ramirez, 02/12/1990; Adam Lallana, 10/05/1988";
            CultureInfo provider = CultureInfo.InvariantCulture;
            var parsedInfo = fullPlayersInfo.Split("; ").Select(dt =>
            {
                var sp = dt.Split(", ");
                return
                    new
                    {
                        Name = sp[0],
                        Birthday = DateTime.ParseExact(sp[1], "dd/MM/yyyy", provider)
                    };
            }).OrderBy(x => x.Birthday);
            foreach (var fp in parsedInfo)
            {
                Console.WriteLine($"{fp.Name}: {fp.Birthday}");
            }

            Console.WriteLine("-----------");
            var songs = "4:12,2:43,3:51,4:29,3:24,3:14,4:46,3:25,4:52,3:27";
            var spanFormat = @"m\:ss";
            var parsedSongs = songs.Split(",").Select(x => TimeSpan.ParseExact(x, spanFormat, provider));
            var songsDuration = parsedSongs.Aggregate(TimeSpan.Zero,
                (span, timeSpan) => span + timeSpan);
            Console.WriteLine($"Songs duration: {songsDuration}");

            Console.ReadKey();
        }
    }
}


/*
2. Нижче є завдання, які слід вирішити, використовуючи лише Linq.
Для початку застосуйте функцію String.Split,
також вам можуть пригодитися наступні функції: String.Join, Enumerable.Range, Zip, Aggregate, SelectMany і клас TimeSpan.
а) На вхід є стрічка  Кожному гравцеві надайте номер, починаючи з 1,
    щоб вийшла стрічка подібна : "1. Davis, 2. Clyne, 3. Fonte" ...
б) Візьміть стрічку  і перетворіть її на IEnumerable гравців в порядку віку (і ще бажано вивести вік)
в) Візьміть стрічку, яка відображає довжину пісень в хвилинах і секундах і обрахуйте загальну довжину всіх пісень.
 */
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SGMCoreCourseHW6
{
    class Program
    {
        static object __lockObj = new object();
        static void Main(string[] args)
        {
            var links = new List<string>
            {
                "https://docs.microsoft.com/en-us/dotnet/core/introduction",
                "https://docs.microsoft.com/en-us/dotnet/standard/get-started",
                "https://docs.microsoft.com/en-us/dotnet/core/tutorials/with-visual-studio",
                "https://docs.microsoft.com/en-us/dotnet/standard/components",
                "https://docs.microsoft.com/en-us/dotnet/standard/class-libraries",
                "https://docs.microsoft.com/en-us/dotnet/standard/net-standard",
                "https://docs.microsoft.com/en-us/dotnet/core/releases-and-support",
                "https://docs.microsoft.com/en-us/dotnet/standard/glossary",
                "https://docs.microsoft.com/en-us/dotnet/core/dotnet-five",
                "https://docs.microsoft.com/en-us/dotnet/fundamentals/tools-and-productivity"
            };

            Dictionary<string, string> articlesByThreadParalel = new Dictionary<string, string>();
            Console.WriteLine("------Paralel Thread");
            foreach (var link in links)
            {
                
                Thread thread = new Thread(() =>
                {
                    Console.WriteLine($"Download started: {link}");
                    using HttpClient client = new HttpClient();
                    var response = client.GetAsync(new Uri(link)).Result;
                    var content = response.Content.ReadAsStringAsync().Result;

                    Console.WriteLine($"Download finished: {link}");
                    articlesByThreadParalel.Add(link, content);
                });
                thread.Start();
            }

            Thread.Sleep(5000);
            Console.WriteLine("------Paralel Task");
            Dictionary<string, string> articlesByTaskParalel = new Dictionary<string, string>();
            foreach (var link in links)
            {
                Task.Run(() =>
                {
                    Console.WriteLine($"Download started: {link}");
                    using HttpClient client = new HttpClient();
                    var response = client.GetAsync(new Uri(link)).Result;
                    var content = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine($"Download finished: {link}");
                    articlesByTaskParalel.Add(link, content);
                });
                
            }

            Thread.Sleep(5000);
            Console.WriteLine("------Consistently Thread");
            Dictionary<string, string> articlesByThread = new Dictionary<string, string>();
            foreach (var link in links)
            {
                Thread thread = new Thread(() =>
                {
                    lock (__lockObj)
                    {
                        Console.WriteLine($"Download started: {link}");
                        using HttpClient client = new HttpClient();
                        var response = client.GetAsync(new Uri(link)).Result;
                        var content = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine($"Download finished: {link}");
                        articlesByThread.Add(link, content);
                    }
                });
                thread.Start();
            }

            Thread.Sleep(5000);
            Console.WriteLine("------Consistently Task");
            Dictionary<string, string> articlesByTask = new Dictionary<string, string>();
            foreach (var link in links)
            {
                Task.Run(() =>
                {
                    Console.WriteLine($"Download started: {link}");
                    using HttpClient client = new HttpClient();
                    var response = client.GetAsync(new Uri(link)).Result;
                    var content = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine($"Download finished: {link}");
                    articlesByTask.Add(link, content);
                }).Wait();
            }

            Thread.Sleep(5000);
        }
    }
}
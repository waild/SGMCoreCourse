using System;
using System.Collections.Generic;

namespace SGMCoreCourseHW3
{
    public class StringCollector
    {
        private readonly List<string> collection = new List<string>();

        public void AddString(string str)
        {
            collection.Add(str);
        }

        public void PrintAll()
        {
            Console.WriteLine("Strings:");
            foreach (var str in collection) Console.WriteLine(str);
            Console.WriteLine("-------------");
        }
    }
}
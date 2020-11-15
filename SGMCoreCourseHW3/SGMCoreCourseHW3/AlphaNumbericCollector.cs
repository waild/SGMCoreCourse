using System;
using System.Collections.Generic;

namespace SGMCoreCourseHW3
{
    public class AlphaNumbericCollector
    {
        private readonly List<string> collection = new List<string>();

        public void ProccessString(string str)
        {
            collection.Add(str);
        }

        public void PrintAll()
        {
            Console.WriteLine("AlphaNumberic:");
            foreach (var str in collection) Console.WriteLine(str);
            Console.WriteLine("-------------");
        }
    }
}
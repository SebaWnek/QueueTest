using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueueTest
{
    public static class TestMethods
    {
        public static void Test1(string input)
        {
            Console.WriteLine("Test 1 invoked");
        }

        public static void Test2(string input)
        {
            Console.WriteLine($"Test2 invoked with {input}");
        }

        public static void WaitTest(string input)
        {
            int seconds;
            bool success = int.TryParse(input, out seconds);
            if (success) 
            { 
                Console.WriteLine($"Waiting for{seconds}");
                Thread.Sleep(seconds * 1000);
                Console.WriteLine("Waiting done");
            }
            else Console.WriteLine($"Incorrect arguments: {input}");
        }
    }
}

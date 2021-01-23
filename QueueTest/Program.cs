using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueTest
{
    class Program
    {
        static MethodsQueue queue = new MethodsQueue();
        static InputSwitch inSw = new InputSwitch();
        static bool shouldEnqueue = false;

        static void Main(string[] args)
        {
            inSw.RegisterMetod("TS1", TestMethods.Test1);
            inSw.RegisterMetod("TS2", TestMethods.Test2);
            inSw.RegisterMetod("WAI", TestMethods.WaitTest);
            inSw.RegisterMetod("STA", queue.StartInvoking);
            inSw.RegisterMetod("STO", queue.StopInvoking);
            inSw.RegisterMetod("ENU", queue.EnumerateQueue);
            inSw.RegisterMetod("ENQ", InvokeOrEnqueue);

            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    (Action<string>, string) method = inSw.ReturnMethod(input);
                    if (method.Item2.StartsWith("ASAP") || !shouldEnqueue) //Args "ASAP" mean method should be invoked ASAP, ignoring queue
                    {
                        method.Item1.Invoke(method.Item2);
                    }
                    else queue.Enqueue(method);
                }
                catch (Exception)
                {
                }
            }
        }

        public static void InvokeOrEnqueue(string args)
        {
            if (args == "1" || args == "ASAP1") shouldEnqueue = true;
            else shouldEnqueue = false;
            Console.WriteLine(shouldEnqueue);
        }
    }
}

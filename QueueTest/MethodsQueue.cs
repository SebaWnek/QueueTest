using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueueTest
{
    class MethodsQueue
    {
        private BlockingCollection<(Action<string>, string)> queue = new BlockingCollection<(Action<string>, string)>();
        private bool isWorking = false;
        CancellationTokenSource tokenSource = new CancellationTokenSource();

        public void Enqueue((Action<string>, string) method)
        {
            queue.Add(method);
        }

        public void EnumerateQueue(string input)
        {
            foreach((Action<string>, string) tuple in queue)
            {   
                Console.WriteLine(tuple.Item1.Method.Name);
            }
        }


        public void StartInvoking(string input)
        {
            if (isWorking) return;
            isWorking = true;
            Task worker = new Task(() =>
            {
                CancellationToken token = tokenSource.Token;
                while (true)
                {
                    if (token.IsCancellationRequested) break;
                    (Action<string>, string) method = queue.Take();
                    method.Item1.Invoke(method.Item2);
                }
            });
            worker.Start();
        }

        public void StopInvoking(string input)
        {
            tokenSource.Cancel();
            tokenSource.Dispose();
            tokenSource = new CancellationTokenSource();
            isWorking = false;
        }
    }
}

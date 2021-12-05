using System;
using System.Threading;

namespace MPP_Lab4
{
    public class Parallel
    {
        private static int runningCount;
        private static object sync = new object();
        public static void WaitAll(Action[] actions) 
        {
            runningCount = actions.Length;
            foreach (Action action in actions)
            {
                ThreadPool.QueueUserWorkItem(ExecuteAction, action);
            }
            lock (sync)
            {
                if (runningCount > 0)
                {
                    Monitor.Wait(sync);
                }
            } 
        }

        private static void ExecuteAction(object state)
        {
            var action = (Action)state;
            action();
            lock (sync)
            {
                runningCount--;
                if (runningCount == 0)
                {
                    Monitor.Pulse(sync);
                }
            }
        }
    }
}

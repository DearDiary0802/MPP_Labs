using System;
using System.Threading;

namespace MPP_Lab2
{
    class Mutex
    {
        private int lockedThreadId = 0;

        private static int currentThreadId {
            get { return Thread.CurrentThread.ManagedThreadId; }
        }
        public void Lock()
        {
            SpinWait spin = new SpinWait();
            while (Interlocked.CompareExchange(ref lockedThreadId, currentThreadId, 0) != 0)
            {
                spin.SpinOnce();
            }
        }

        public void Unlock() 
        {
            if (Interlocked.CompareExchange(ref lockedThreadId, 0, currentThreadId) != currentThreadId)
            {
                throw new Exception($"There is error with unlocking mutex by thread with id = {currentThreadId}");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading;

namespace MPP
{
    public delegate void TaskDelegate();
    class TaskQueue
    {
        private Thread[] threadPool;
        private int threadsQuantity;
        private Queue<TaskDelegate> tasksQueue = new Queue<TaskDelegate>();
        private object _synchronize = new object();
        private bool areTasksExecute = true;
        public TaskQueue(int threadsQuantity)
        {
            threadPool = new Thread[threadsQuantity];
            this.threadsQuantity = threadPool.Length;
            for (int i = 0; i < this.threadsQuantity; i++)
            {
                threadPool[i] = new Thread(TasksExecute) { IsBackground = true };
                threadPool[i].Start();
            }
        }

        public void EnqueueTask(TaskDelegate task)
        {
            lock (_synchronize)
            {
                tasksQueue.Enqueue(task);
            }
        }

        public void TasksExecute()
        {
            TaskDelegate taskDelegate = null;
            while (areTasksExecute)
            {
                if (tasksQueue.Count != 0)
                {
                    lock (_synchronize)
                    {
                        if (tasksQueue.Count != 0)
                        {
                            taskDelegate = tasksQueue.Dequeue();
                        }
                    }
                    if (taskDelegate != null)
                    {
                        taskDelegate.Invoke();
                    }
                    areTasksExecute = AreTasksInQueue();
                }
            }
        }
        public void Wait()
        {
            foreach (Thread thread in threadPool)
            {
                thread.Join();
            }
        }

        private bool AreTasksInQueue()
        {
            lock (_synchronize)
            {
                return tasksQueue.Count != 0;
            }
        }
    }
}

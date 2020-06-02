using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using ThreadPriority = System.Threading.ThreadPriority;

namespace VoxelValley.Engine.Threading
{
    public class ThreadManager : MonoBehaviour
    {
        //Singleton
        private static ThreadManager _instance;
        public static ThreadManager Instance { get { return _instance; } }

        int currentRunningThreads = 0;

        ConcurrentDictionary<Guid, Thread> threads = new ConcurrentDictionary<Guid, Thread>();
        Queue<Guid> queuedThreadsLowest = new Queue<Guid>();
        Queue<Guid> queuedThreadsBelowNormal = new Queue<Guid>();
        Queue<Guid> queuedThreadsNormal = new Queue<Guid>();
        Queue<Guid> queuedThreadsAboveNormal = new Queue<Guid>();
        Queue<Guid> queuedThreadsHighest = new Queue<Guid>();

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        void Update()
        {
            while (currentRunningThreads < ClientConstants.Threading.MaxThreads && (queuedThreadsLowest.Count > 0 || queuedThreadsBelowNormal.Count > 0 || queuedThreadsNormal.Count > 0 || queuedThreadsAboveNormal.Count > 0 || queuedThreadsHighest.Count > 0))
            {
                if (queuedThreadsHighest.Count > 0)
                    StartThread(queuedThreadsHighest.Dequeue(), ThreadPriority.Highest);
                if (queuedThreadsAboveNormal.Count > 0)
                    StartThread(queuedThreadsAboveNormal.Dequeue(), ThreadPriority.AboveNormal);
                else if (queuedThreadsNormal.Count > 0)
                    StartThread(queuedThreadsNormal.Dequeue(), ThreadPriority.Normal);
                else if (queuedThreadsBelowNormal.Count > 0)
                    StartThread(queuedThreadsBelowNormal.Dequeue(), ThreadPriority.BelowNormal);
                else if (queuedThreadsLowest.Count > 0)
                    StartThread(queuedThreadsLowest.Dequeue(), ThreadPriority.Lowest);
            }

            if (currentRunningThreads == ClientConstants.Threading.MaxThreads)
                UnityEngine.Debug.Log("Reached maximum concurrent Threads. Delaying until threads are finished.");
        }

        void StartThread(Guid id, ThreadPriority priority)
        {
            if (threads.TryGetValue(id, out Thread thread))
            {
                thread.Priority = priority;
                thread.Start();
                currentRunningThreads++;
            }
        }

        public void CreateThread(Action threadWorker, Action callbackMethod, string name, ThreadPriority priority)
        {
            ThreadStart starter = new ThreadStart(threadWorker);
            Guid id = Guid.NewGuid();

            //AddCallback
            starter += () =>
                {
                    callbackMethod();
                    threads.TryRemove(id, out Thread thread);
                    currentRunningThreads--;
                };

            threads.TryAdd(id, new Thread(starter)
            {
                Name = name,
                IsBackground = true
            });

            QueueThread(id, priority);
        }

        void QueueThread(Guid id, ThreadPriority priority)
        {
            switch (priority)
            {
                case ThreadPriority.Lowest:
                    queuedThreadsLowest.Enqueue(id);
                    break;
                case ThreadPriority.BelowNormal:
                    queuedThreadsBelowNormal.Enqueue(id);
                    break;
                case ThreadPriority.Normal:
                    queuedThreadsNormal.Enqueue(id);
                    break;
                case ThreadPriority.AboveNormal:
                    queuedThreadsAboveNormal.Enqueue(id);
                    break;
                case ThreadPriority.Highest:
                    queuedThreadsHighest.Enqueue(id);
                    break;
            }
        }
    }
}
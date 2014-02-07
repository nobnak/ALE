using System.Threading;

namespace ALE
{
    public class EventLoopWorker
    {
        public bool Idle { get; private set; }
        public bool Working {
            get { lock (_locker) { return _working; } }
            set { lock (_locker) { _working = value; } }
        }

        private readonly ManualResetEvent StopHandle = new ManualResetEvent(false);
        private object _locker = new object();
        private bool _working = false;

        public EventLoopWorker() {
            Idle = true;
        }

        public void Start()
        {
            if (!Working) {
                Working = true;
                ThreadPool.QueueUserWorkItem(Work);
            }
        }

        public void Stop()
        {
            Working = false;
        }

        public void Wait()
        {
            StopHandle.WaitOne();
        }

        private void Work(object stateInfo)
        {
            try {
                StopHandle.Reset();
                while (Working) {
                    Thread.Sleep(0);
                    var evt = EventLoop.NextEvent();
                    if (evt == null) {
                        Idle = true;
                        continue;
                    }
                    Idle = false;
                    evt();
                }
            } finally {
                StopHandle.Set();
            }
        }
    }
}
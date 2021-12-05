using System;

namespace MPP_Lab2
{
    class OSHandle : IDisposable
    {
        private IntPtr handle;
        private bool isDisposed = false;
        private Mutex mutex = new Mutex();
        public IntPtr Handle
        {
            get
            {
                if (!isDisposed)
                {
                    return handle;
                }
                else
                {
                    throw new Exception("This handle was disposed");
                }
            }
            set
            {
                handle = value;
            }
        }

        public OSHandle(IntPtr handle)
        {
            Handle = handle;
        }

        ~OSHandle()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                Dispose(true);
                GC.SuppressFinalize(this);
                isDisposed = true;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            mutex.Lock();

            if (handle != IntPtr.Zero)
            {
                handle = IntPtr.Zero;
            }
            isDisposed = true;

            mutex.Unlock();
        }
    }
}

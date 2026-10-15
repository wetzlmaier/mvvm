using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Mvvm
{
    public abstract class DisposableViewModelBase : ViewModelBase, IDisposableViewModel
    {
        public event EventHandler? Disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    foreach (var disposable in Disposables)
                    {
                        disposable.Dispose();
                    }
                }

                // dispose unmanaged ressources
                IsDisposed = true;

                if (Disposed != null)
                {
                    Disposed(this, EventArgs.Empty);
                }
            }
        }

        protected void CheckIfDisposed()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);
        }

        ~DisposableViewModelBase()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        protected ICollection<IDisposable> Disposables { get; } = new Collection<IDisposable>();

        [Browsable(false)]
        public bool IsDisposed { get; private set; }
    }
}

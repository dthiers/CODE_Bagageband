using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPINT_Wk3_Observer.Process
{
    public abstract class Observable<T> : IObservable<T>, IDisposable
    {
        #region class members
        private List<IObserver<T>> _observers;
        #endregion class members

        public Observable()
        {
            _observers = new List<IObserver<T>>();
        }

        #region unsubscriber
        public class Unsubscriber : IDisposable
        {
            private readonly Action _unsubscriber;
            public Unsubscriber(Action unsubscribe) { _unsubscriber = unsubscribe; }
            public void Dispose() { _unsubscriber.Invoke(); }
        }
        #endregion

        /// <summary>
        /// Register Observer:
        /// - Add to _observers
        /// - Return Unsubscriber so that Observer can unsubscribe. 
        ///     Or if Observer is disposed, Observable won't notify
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        public IDisposable Subscribe(IObserver<T> observer)
        {
            return new Unsubscriber(() => _observers.Remove(observer));
        }

        /// <summary>
        /// When Observable (this) has changed, for each observer in _observers call their OnNext method
        /// </summary>
        /// <param name="subject"></param>
        protected void Notify(T subject)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(subject);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

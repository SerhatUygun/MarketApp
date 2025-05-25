using System;
using System.Collections.Generic;

namespace MarketApp.VeritabaniErisimKatmani.Repositories.Base
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetAll();
        T GetItem(object key);
        void Add(T item);
        void Remove(T item);
        void Update(T item);
    }
}
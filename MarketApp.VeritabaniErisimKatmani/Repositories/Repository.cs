using MarketApp.VeritabaniErisimKatmani.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MarketApp.VeritabaniErisimKatmani.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext context;

        public Repository(DbContext context)
        {
            this.context = context;
        }

        public void Add(T item)
        {
            context.Set<T>().Add(item);
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public T GetItem(object key)
        {
            return context.Set<T>().Find(key);
        }

        public void Remove(T item)
        {
            if (context.Entry(item).State == EntityState.Detached)
                context.Set<T>().Attach(item);
            context.Entry(item).State = EntityState.Deleted;
        }

        public void Update(T item)
        {
            if (context.Entry(item).State == EntityState.Detached)
                context.Set<T>().Attach(item);
            context.Entry(item).State = EntityState.Modified;
        }

        public void Dispose()
        {
            context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
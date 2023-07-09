using System.Collections.Generic;
using Zadatak.Models;

namespace Zadatak.Dal
{
    public interface IRepository<T>
    {
        void Add(T item);
        void Delete(T item);
        IList<T> GetAll();
        T Get(int id);
        void Update(T item);
    }
}
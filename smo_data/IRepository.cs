using System;
using System.Collections;
using System.Collections.Generic;

namespace smo_data
{
    public interface IRepository<T>   where T : class 
    {
        T Add(T entity);

        T Update(long id,T entity);

        int Delete(long id);
        IEnumerable<T> Select();
    }
}

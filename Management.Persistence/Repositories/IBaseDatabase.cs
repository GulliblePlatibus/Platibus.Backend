using System;
using System.Collections.Generic;

namespace Management.Persistence.Repositories
{
    public interface IBaseDatabase<T>
    {
        long Insert(T value);
        long InsertMany(IEnumerable<T> valueList);
        bool Update(T value);
        T GetById(Guid id);
        bool DeleteByT(T value);
        bool DeleteMany(IEnumerable<T> valueList);
        


    }
}
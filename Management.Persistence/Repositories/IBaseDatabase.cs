using System;
using System.Collections.Generic;

namespace Management.Persistence.Repositories
{
    public interface IBaseDatabase<T>
    {
        /// <summary>
        ///
        /// Insert an object to a database
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Affected rows</returns>
        long Insert(T value);
        
        /// <summary>
        ///
        /// Insert a list of objects to a database
        /// </summary>
        /// <param name="valueList"></param>
        /// <returns>Affected rows</returns>
        long InsertMany(IEnumerable<T> valueList);
        
        
        /// <summary>
        ///
        /// Update a specfic object in the database based on the primary key
        /// </summary>
        /// <param name="value"></param>
        /// <returns>true if the object is updated</returns>
        bool Update(T value);
        
        
        /// <summary>
        /// Get an object from the DB based on the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The object that matches the id</returns>
        T GetById(Guid id);
        
        
        /// <summary>
        ///
        /// Delete an object from the db 
        /// </summary>
        /// <param name="value"></param>
        /// <returns>true if the object is deleted</returns>
        bool DeleteByT(T value);
        
        
        /// <summary>
        ///
        /// Delete a list of object in the db
        /// </summary>
        /// <param name="valueList"></param>
        /// <returns>true if all the objects were deleted</returns>
        bool DeleteMany(IEnumerable<T> valueList);
        


    }
}
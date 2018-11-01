using System.Collections.Generic;


namespace Management.Persistence.Helpers
{
    public static class PersistenceExtensions
    {
        public static T GetFirstElement<T>(this IEnumerable<T> list) where T : class
        {
            foreach (var entity in list)
            {
                return entity;
            }

            return null;
        } 
    }
}
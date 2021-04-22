using System;
using System.Collections.Generic;

public interface ICollectorCacheObjectData<T>
{
     IEnumerable<T> GetCollection();
}
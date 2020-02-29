using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoLinealStructures.Interfaces
{
    interface ITreeDataStructure<T>
    {
        void Insert(T value, Delegate comparer);
        int Find(T value, Delegate comparer, Delegate converter);
        void Delete(T value);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoLinealStructures.Interfaces
{
    interface ITreeDataStructure<T>
    {
        void Insert(T value);
        int Find(T value);
        void Delete(T value);       

    }
}

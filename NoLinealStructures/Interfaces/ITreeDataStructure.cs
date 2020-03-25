﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoLinealStructures.Structures;

namespace NoLinealStructures.Interfaces
{
    interface ITreeDataStructure<T>
    {
        void Insert(T value);
        int Find(T value);
        void Delete(T value);
        Node<T> InsertAVL(Node<T> node, T value);
    }
}

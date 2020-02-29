using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoLinealStructures.Structures
{
    public class Tree<T> : Interfaces.ITreeDataStructure<T>
    {
        static Node<T> Root { get; set; }
        static int Count;
        public void Delete(T value)
        {
            throw new NotImplementedException();
        }

        public int Find(T value, Delegate comparer)
        {
            return Find(Root, value, comparer);
        }

        private int Find(Node<T> nodeF, T value, Delegate comparer)
        {
            Node<T> node = new Node<T>(value);

            if (nodeF == null)
            {
                return 0;
            }
            else if ((int)comparer.DynamicInvoke(nodeF.Value, node.Value) == 0)
            {
                return 0;//Convertir tipo T a tipo int con delegados node.Value;
            }
            else if ((int)comparer.DynamicInvoke(nodeF.Value, node.Value) == 1)
            {
                return Find(nodeF.Left, value, comparer);
            }
            else
            {
                return Find(nodeF.Right, value, comparer);
            }
        }

        public void Insert(T value, Delegate del)
        {
            Node<T> node = new Node<T>(value);

            if (Root == null)
            {
                Root = node;
                Count++;
            }
            else
            {
                Insert(Root, node, del);
            }
        }
        private void Insert(Node<T> nodeF, Node<T> node, Delegate comparer)
        {
            if ((int)comparer.DynamicInvoke(nodeF.Value, node.Value) == 1)
            {
                if (nodeF.Left == null)
                {
                    nodeF.Left = node;
                    Count++;
                    return;
                }
                else
                {
                    Insert(nodeF.Left, node, comparer);
                }
            }
            else
            {
                if (nodeF.Right == null)
                {
                    nodeF.Right = node;
                    Count++;
                    return;
                }
                else
                {
                    Insert(nodeF.Right, node, comparer);
                }
            }
        }

    }
}

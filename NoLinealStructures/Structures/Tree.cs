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
        public static int Count;
        public Delegate Comparer;
        public Delegate Converter;

        public void Delete(T value)
        {
            throw new NotImplementedException();
        }

        public int Find(T value)
        {
            return Find(Root, value);
        }

        private int Find(Node<T> nodeF, T value)
        {
            Node<T> node = new Node<T>(value);

            if (nodeF == null)
            {
                return 0;
            }
            else if ((int)Comparer.DynamicInvoke(nodeF.Value, node.Value) == 0)
            {
                node.Value = nodeF.Value;
                return (int)Converter.DynamicInvoke(node.Value);                                     
            }
            else if ((int)Comparer.DynamicInvoke(nodeF.Value, node.Value) == 1)
            {
                return Find(nodeF.Left, value);
            }
            else
            {
                return Find(nodeF.Right, value);
            }
        }

        public void Insert(T value)
        {
            Node<T> node = new Node<T>(value);

            if (Root == null)
            {
                Root = node;
                Count++;
            }
            else
            {
                Insert(Root, node);
            }
        }
        private void Insert(Node<T> nodeF, Node<T> node)
        {
            if ((int)Comparer.DynamicInvoke(nodeF.Value, node.Value) == 1)
            {
                if (nodeF.Left == null)
                {
                    nodeF.Left = node;
                    Count++;
                    return;
                }
                else
                {
                    Insert(nodeF.Left, node);
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
                    Insert(nodeF.Right, node);
                }
            }
        }

       
    }
}

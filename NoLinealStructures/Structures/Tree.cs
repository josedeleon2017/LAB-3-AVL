using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace NoLinealStructures.Structures
{
    public class Tree<T> : Interfaces.ITreeDataStructure<T>
    {
        static Node<T> Root { get; set; }
        public static int Count;
        public Delegate Comparer;
        public Delegate Converter;
        public Delegate GetValue;
        private string preorder = "";
        private string postorder = "";
        private string inorder = "";

        public void Delete(T value)
        {
            throw new NotImplementedException();
        }

        private void Delete (Node<T> nodeF, T value)
        {
            Node<T> node = new Node<T>(value);

        }

        public int Find(T value)
        {
            return Find(Root, value);
        }
        public string Inorder()
        {
            inorder = "";
            try
            {
                if (Root.Value != null)
                {
                    Inorder(Root);
                }
                else
                {
                    return "Empty Tree";
                }
                return inorder;
            }
            catch
            {
                return "Empty Tree";
            }
        }

        private void Inorder(Node<T> node)
        {
            if (node.Left != null)
            {
                Inorder(node.Left);
            }
            inorder += (string)GetValue.DynamicInvoke(node.Value) + "\n";
            if (node.Right != null)
            {
                Inorder(node.Right);
            }
        }

        public string Postorder()
        {
            postorder = "";
            try
            {
                if (Root.Value != null)
                {
                    Postorder(Root);
                }
                else
                {
                    return "Empty Tree";
                }
                return postorder;
            }
            catch
            {
                return "Empty Tree";
            }
        }

        private void Postorder(Node<T> node)
        {
            if (node.Left != null)
            {
                Postorder(node.Left);
            }
            if (node.Right != null)
            {
                Postorder(node.Right);
            }
            postorder += (string)GetValue.DynamicInvoke(node.Value) + "\n";
        }

        public string Preorder()
        {
            preorder = "";
            try
            {
                if (Root.Value != null)
                {
                    Preorder(Root);
                }
                else
                {
                    return "Empty Tree";
                }
                return preorder;
            }
            catch
            {
                return "Empty Tree";
            }
        }

        private void Preorder(Node<T> node)
        {
            preorder += (string)GetValue.DynamicInvoke(node.Value) + "\n";
            if (node.Left != null)
            {
                Preorder(node.Left);
            }
            if (node.Right != null)
            {
                Preorder(node.Right);
            }
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

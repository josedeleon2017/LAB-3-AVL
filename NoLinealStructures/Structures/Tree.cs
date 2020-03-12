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

        //FIND
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

        //INSERT
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

        //DELETE
        public void Delete(T value)
        {
            Delete(Root, value);
        }

        private Node<T> Delete (Node<T> nodeF, T value)
        {
           if (nodeF == null)
            {
                return null;
            }
            if ((int)Comparer.DynamicInvoke(nodeF.Value, value) == 1)
            {
                Node<T> l;
                l = Delete(nodeF.Left, value);
                nodeF.Left = l;
            }
            else if ((int)Comparer.DynamicInvoke(nodeF.Value, value) == -1)
            {
                Node<T> r;
                r = Delete(nodeF.Right, value);
                nodeF.Right = r;
            }
            else
            {
                Node<T> q;
                q = nodeF;
                if (q.Left == null)
                {
                    nodeF = q.Right;
                }
                else if (q.Right == null)
                {
                    nodeF = q.Left;
                }
                else
                {
                    q = Replace(q);
                }
                q = null;
            }
            
            return nodeF;
        }

        private Node<T> Replace(Node<T> root)
        {
            Node<T> a, p;
            p = root;
            a = root.Left;
            while (a.Left != null)
            {
                p = a;
                a = a.Left;
            }
            root.Value = a.Value;
            if (p == root)
            {
                p.Left = a.Left;
            }
            else
            {
                p.Right = a.Left;
            }
            return a;
        }        

       //TRAVERSALS
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

        

       
    }
}

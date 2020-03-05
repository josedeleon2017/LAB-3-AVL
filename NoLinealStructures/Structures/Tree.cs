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
        public Delegate GetStock;
        private string preorder = "";
        private string postorder = "";
        private string inorder = "";

        public void NoStockCheck()
        {
            NoStockCheck(Root);
        }

        private void NoStockCheck(Node<T> node)
        {
            if (node.Left != null)
            {             
                NoStockCheck(node.Left);
            }
            
            if (node.Right != null)
            {
                NoStockCheck(node.Right);
            }
            if ((Int32)GetStock.DynamicInvoke(node.Value) == 0)
            {
                //Call Delete here
                Delete(node.Value);
            }

        }

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
                //nodeF.Left = Delete(nodeF.Left, value);
            }
            else if ((int)Comparer.DynamicInvoke(nodeF.Value, value) == -1)
            {
                Node<T> r;
                r = Delete(nodeF.Right, value);
                nodeF.Right = r;
                //nodeF.Right = Delete(nodeF.Right, value);
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
                /*
                if(nodeF.Left == null && nodeF.Right == null)
                {
                    nodeF = null;
                    nodeF = null;
                }
                else if(nodeF.Left == null)
                {
                    nodeF.Value = nodeF.Right.Value;
                    nodeF.Right = null;
                   // Node<T> temp = nodeF;
                   // nodeF = nodeF.Right;
                }
                else if (nodeF.Right == null)
                {
                    Node<T> temp = nodeF;
                    nodeF = nodeF.Left;
                }
                else
                {
                    Node<T> temp = null;
                    temp.Value = FindMin(nodeF.Right);
                    nodeF.Value = temp.Value;
                    nodeF.Right = Delete(nodeF.Right, temp.Value);
                }*/

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
            /*
            if (root == null)
            {  
                
            }
            if (root.Left != null)
            {
                return FindMin(root.Left);
            }
            return root.Value;
            */
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL_Tree
{
    public class AVL_Tree
    {
        public Node root;

        public AVL_Tree()
        {
            root = null;
        }

        public Boolean insertion(ref Node root, int key)
        {
            if (root == null)
            {
                root = new Node(key);
                return false;
            }
            else if (key < root.value)
            {
                Boolean rotated = insertion(ref root.left, key);
                if (!rotated) rotated = updateBalanceFactor(ref root, +1);
                return rotated;
            }
            else
            {
                Boolean rotated = insertion(ref root.right, key);
                if (!rotated) rotated = updateBalanceFactor(ref root, -1);
                return rotated;
            }
        }

        public void deletion(ref Node root, int key)
        {
            if (root != null)
            {
                if (key < root.value) deletion(ref root.left, key);
                else if (key > root.value) deletion(ref root.right, key);
                else
                { //key is found
                    if (root.left == null) root = root.right;
                    else if (root.right == null) root = root.left;
                    else
                    {
                        int predecessor = deletePredecessor(ref root);
                        root.value = predecessor;
                    }
                }
            }
        }

        private int deletePredecessor(ref Node root)
        {
            if (root.right != null) return deletePredecessor(ref root.right);
            else
            {
                int predecessor = root.value;
                root = root.left; //case 1 or 2
                return predecessor;
            }
        }

        private Boolean updateBalanceFactor(ref Node node, sbyte delta)
        {
            node.balanceFactor += delta;
            switch (node.balanceFactor)
            {
                case +2:
                    if (node.left.balanceFactor == -1)
                        doubleRightRotate(ref node);
                    else
                        singleRightRotate(ref node);
                    return true;

                case -2:
                    if (node.left.balanceFactor == +1)
                        doubleLeftRotate(ref node);
                    else
                        singleLeftRotate(ref node);
                    return true;
            }
            return false;
        }

        private sbyte max(sbyte a, sbyte b)
        {
            return (a > b) ? a : b;
        }

        private sbyte min(sbyte a, sbyte b)
        {
            return (a < b) ? a : b;
        }

        public void singleLeftRotate(ref Node p)
        {
            Node q = p.right;
            p.right = q.left;
            q.left = p;
            p.balanceFactor += (sbyte)(1 - min(q.balanceFactor, 0));
            q.balanceFactor += (sbyte)(1 + max(p.balanceFactor, 0));
            p = q;
        }

        public void singleRightRotate(ref Node q)
        {
            Node p = q.left;
            q.left = p.right;
            p.right = q;
            q.balanceFactor -= (sbyte)(1 + max(p.balanceFactor, 0));
            p.balanceFactor -= (sbyte)(1 - min(q.balanceFactor, 0));
            q = p;
        }

        public void doubleLeftRotate(ref Node x)
        {
            singleRightRotate(ref x.right);
            singleLeftRotate(ref x);
        }

        public void doubleRightRotate(ref Node x)
        {
            singleLeftRotate(ref x.left);
            singleRightRotate(ref x);
        }

        public void printInorder(Node node)
        {
            if (node == null)
                return;
            printInorder(node.left);
            Console.WriteLine(node.value + " ");
            printInorder(node.right);
        }
    }
}
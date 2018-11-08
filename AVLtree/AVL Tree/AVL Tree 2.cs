using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL_Tree
{
    public class AVL_Tree_2
    {
        public Node root;

        //with parent link
        public AVL_Tree_2()
        {
            root = null;
        }

        public void insert(ref Node root, int key)
        {
            Node newNode = new Node(key);
            if (root == null)
                root = newNode;
            else
            {
                Node current = root;
                while (true)
                {
                    if (key < current.value)
                    {
                        if (current.left == null)
                        {
                            current.left = newNode;
                            newNode.parent = current;
                            retraceAfterInsertion(ref root, newNode);
                            break;
                        }
                        current = current.left;
                    }
                    else
                    {
                        if (current.right == null)
                        {
                            current.right = newNode;
                            newNode.parent = current;
                            retraceAfterInsertion(ref root, newNode);
                            break;
                        }
                        current = current.right;
                    }
                }
            }
        }

        private void retraceAfterInsertion(ref Node root, Node node)
        {
            while (node.parent != null)
            {
                node = node.parent;
                if (updateBalanceFactor(ref root, ref node))
                    break;
            }
        }

        public void delete(ref Node root, int key)
        {
            Node current = find(root, key);
            if (current != null)
            {
                if (current.left == null || current.right == null)
                {
                    retraceAfterDeletion(ref root, current);
                    replaceNodeInParent(ref root, current, current.left == null ? current.right : current.left);
                }
                else
                {
                    Node predecessor = current.left;
                    while (predecessor.right != null)
                        predecessor = predecessor.right;
                    current.value = predecessor.value;
                    retraceAfterDeletion(ref root, predecessor);
                    replaceNodeInParent(ref root, predecessor, predecessor.left);
                }
            }
        }

        private Node find(Node root, int key)
        {
            if (root == null)
                return null;
            else if (root.value == key)
                return root;
            else if (root.value < key)
                return find(root.right, key);
            else if (root.value > key)
                return find(root.left, key);
            return null;
        }

        private void retraceAfterDeletion(ref Node root, Node node)
        {
            while (node.parent != null)
            {
                Node parent = node.parent;
                Node sibling;
                sbyte oldBalanceFactor = parent.balanceFactor;
                if (node == parent.left)
                {
                    parent.balanceFactor--;
                    sibling = parent.right;
                }
                else
                {
                    parent.balanceFactor++;
                    sibling = parent.left;
                }
                sbyte siblingBalanceFactor = (sbyte)(sibling == null ? 0 : sibling.balanceFactor);
                bool rotated = updateBalanceFactor(ref root, ref parent);
                if (rotated && siblingBalanceFactor == 0)
                {
                    break;
                }
                if (oldBalanceFactor == 0) break;
                node = parent;
            }
        }

        private void replaceNodeInParent(ref Node root, Node node, Node child)
        {
            if (node.parent == null)
                root = child;
            else if (node.parent.left == node)
                node.parent.left = child;
            else
                node.parent.right = child;

            if (child != null)
                child.parent = node.parent;
        }

        private Boolean updateBalanceFactor(ref Node root, ref Node node)
        {
            switch (node.balanceFactor)
            {
                case +2:
                    if (node.left.balanceFactor == -1)
                        node = doubleRightRotate(ref root, node);
                    else
                        node = singleRightRotate(ref root, node);
                    return true;

                case -2:
                    if (node.right.balanceFactor == +1)
                        node = doubleLeftRotate(ref root, node);
                    else
                        node = singleLeftRotate(ref root, node);
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

        public Node singleLeftRotate(ref Node root, Node p)
        {
            Node q = p.right;
            p.right = q.left;
            if (q.left != null) q.left.parent = p;
            q.left = p;
            replaceNodeInParent(ref root, p, q);
            p.parent = q;
            p.balanceFactor += (sbyte)(1 - min(q.balanceFactor, 0));
            q.balanceFactor += (sbyte)(1 + max(p.balanceFactor, 0));
            return q;
        }

        public Node singleRightRotate(ref Node root, Node q)
        {
            Node p = q.left;
            q.left = p.right;
            if (p.right != null) p.right.parent = q;
            p.right = q;
            replaceNodeInParent(ref root, q, p);
            q.parent = p;
            q.balanceFactor -= (sbyte)(1 + max(p.balanceFactor, 0));
            p.balanceFactor -= (sbyte)(1 - min(q.balanceFactor, 0));
            return p;
        }

        public Node doubleLeftRotate(ref Node root, Node x)
        {
            singleRightRotate(ref root, x.right);
            return singleLeftRotate(ref root, x);
        }

        public Node doubleRightRotate(ref Node root, Node x)
        {
            singleLeftRotate(ref root, x.left);
            return singleRightRotate(ref root, x);
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
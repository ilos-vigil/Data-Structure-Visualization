using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBlack_tree
{
    class node
    {
        public int key;
        public node left, right;
        public node parent;
        public byte color;
        public bool sentinel;

        public node(int key)
        {
            this.key = key;
            this.left = null;
            this.right = null;
            this.parent = null;
            this.color = 0;
            this.sentinel = false;
        }
        public node()
        {
            this.left = null;
            this.right = null;
            this.parent = null;
            this.color = 2;
            this.sentinel = true;
        }
    }
    class RBtree
    {
        public node root;
        public RBtree()
        {
            root = null;
        }
        public byte color(node n)
        {
            if (n == null)
            {
                return 1;
            }
            else
            {
                return n.color;
            }
        }
        public node singleLeftRotation(ref node root, node p)
        {
            node q = p.right;
            p.right = q.left;
            if (q.left != null)
            {
                q.left.parent = p;
            }
            q.left = p;
            replaceNodeInParent(ref root, p, q);
            p.parent = q;
            swapColor(p, q);
            return q;
        }
        public node singleRightRotation(ref node root, node q)
        {
            node p = q.left;
            q.left = p.right;
            if (p.right != null)
            {
                p.right.parent = q;
            }
            p.right = q;
            replaceNodeInParent(ref root, q, p);
            q.parent = p;
            swapColor(p, q);
            return q;
        }
        public void insert(ref node root, int key)
        {
            node newnode = new node(key);
            if (root == null)
            {
                root = newnode;
                root.color = 1;
            }
            else
            {
                node current = root;
                while (true)
                {
                    if (key < current.key)
                    {
                        if (current.left == null)
                        {
                            current.left = newnode;
                            newnode.parent = current;
                            retraceAfterInsertion(ref root, newnode);
                            break;
                        }
                        current = current.left;
                    }
                    else
                    {
                        if (current.right == null)
                        {
                            current.right = newnode;
                            newnode.parent = current;
                            retraceAfterInsertion(ref root, newnode);
                            break;
                        }
                        current = current.right;
                    }
                }
            }
        }
        public void retraceAfterInsertion(ref node root, node current)
        {
            while (true)
            {
                if (current == root)
                {
                    current.color = 1;
                    break;
                }
                if (current.parent.color == 0)
                {
                    node p = current.parent;
                    node g = p.parent;
                    node u = p == g.left ? g.right : g.left;
                    if (color(u) == 0)
                    {
                        p.color = 1;
                        u.color = 1;
                        g.color = 0;
                        current = g;
                    }
                    else
                    {
                        rotate(ref root, current, p, g);
                        break;
                    }
                }
                else break;
            }
        }
        public node rotate(ref node root, node r, node p, node g)
        {
            if (p == g.left && (r == null || r == p.left))
            {
                singleRightRotation(ref root, g);
                return r;
            }
            else if (p == g.left && r == p.right)
            {
                singleLeftRotation(ref root, g.left);
                singleRightRotation(ref root, g);
                return p;
            }
            else if (p == g.right && (r == p.right||r==null))
            {
                singleLeftRotation(ref root, g);
                return r;
            }
            else if (p == g.right && r == p.left)
            {
                singleRightRotation(ref root, g.right);
                singleLeftRotation(ref root, g);
                return p;
            }
            return null;

        }
        public void swapColor(node x, node y)
        {
            if (x.color != y.color)
            {
                byte temp = x.color;
                x.color = y.color;
                y.color = temp;
            }
        }
        public void replaceNodeInParent(ref node root, node node, node child)
        {
            if (node.parent == null)
            {
                root = child;
            }
            else if (node.parent.left == node)
            {
                node.parent.left = child;
            }
            else
            {
                node.parent.right = child;
            }
            if (child != null)
            {
                child.parent = node.parent;
            }
        }
        public node find(node root, int key) {
            node current = root;
            while (current != null && key != current.key) {
                if (key < current.key)
                {
                    current = current.left;
                }
                else
                {
                    current = current.right;
                }
            }
            return current;
        }
        public void delete(ref node root, int key)
        {
            node current = find(root, key);
            if (current != null)
            {
                if (current.left == null || current.right == null)
                {
                    replaceNodeInParentAndBalancing(ref root, current, current.left == null ? current.right : current.left);
                }
                else
                {
                    node predecessor = current.left;
                    while (predecessor.right != null)
                    {
                        predecessor = predecessor.right;
                    }
                    current.key = predecessor.key;
                    replaceNodeInParentAndBalancing(ref root, predecessor, predecessor.left);
                }
            }
        }
        public void replaceNodeInParentAndBalancing(ref node root, node node, node child)
        {
            if (node.color == 0 || color(child) == 0)
            {
                if (child != null)
                {
                    child.color = 1;
                }
            }
            else if (node.color == 1 && color(child) == 1){
                    if (child == null)
                    {
                        child = new node();
                    }
                    else
                    {
                        child.color = 2;
                    }
            }
                replaceNodeInParent(ref root, node, child);
            if (child != null)
            {
                retraceAfterDeletion(ref root, child);
            }
        }
        public void retraceAfterDeletion(ref node root, node u)
        {
            while (u.color == 2)
            {
                if (u == root)
                {
                    decreaseColor(ref root, u);
                    break;
                }
                node p = u.parent;
                node s;
                if (u == p.left)
                {
                    s = p.right;
                }
                else
                {
                    s = p.left;
                }
                if (color(s) == 0)
                {
                    rotate(ref root, null, s, p);
                }
                else
                {
                    node r = null;
                    if (s == p.left)
                    {
                        if (color(s.left) == 0) r = s.left;
                        else if (color(s.right) == 0) r = s.right;
                    }
                    else
                    {
                        if (color(s.right) == 0) r = s.right;
                        else if (color(s.left) == 0) r = s.left;
                    }
                    if (r != null)
                    {
                        r = rotate(ref root, r, s, p);
                        increaseColor(ref root, r);
                        decreaseColor(ref root, u);
                        break;
                    }
                    else
                    {
                        decreaseColor(ref root, u);
                        decreaseColor(ref root, s);
                        increaseColor(ref root, p);
                        u = p;
                    }
                }
            }
        }
        public void decreaseColor(ref node root, node node)
        {
            if (node.color == 2)
            {
                if (node.sentinel) {
                    if (node.parent == null) root = null;
                    else if (node.parent.left == node) node.parent.left = null;
                    else node.parent.right = null; }
                else node.color = 1;
            }
            else if (node.color == 1) node.color = 0;
        }
        public void increaseColor(ref node root, node node) {
            if (node.color == 0) node.color = 1;
            else if (node.color == 1) node.color = 2;
        }
        public void printorder(node node)
        {
            if (node == null)
            {
                return;
            }
            printorder(node.left);
            Console.WriteLine(node.key+" - "+node.color+"\n");
            printorder(node.right);
        }
    }
}

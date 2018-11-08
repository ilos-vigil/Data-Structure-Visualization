using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Btree
{
    class Btree
    {
        public Bnode bt;
        public int m;

        public Btree(int m)
        {
            bt = new Bnode(3);
            this.m = m;
        }

        public void insert(ref Bnode root, int key)
        {
            if (root == null) {
                root = new Bnode(m);
                root.insert(key);
            } else {
                Bnode current = root;
                while (true) {
                    if (current.children[0] == null) { //insert must be in leaf
                        int position = current.insert(key);
                        while (current.n > current.size) { //overflow
                                                           //split the node
                            Object[] result = split(ref root, current);
                            //try to insert middle key to parent
                            Bnode newNode = (Bnode)result[0];
                            key = (int)result[1]; //separator
                            current = current.parent;
                            position = current.insert(key);
                            current.children[position + 1] = newNode;
                        }
                        break;
                    }       
                    current = current.children[current.findPosition(key)];
                }
            }
        }

        public void delete(ref Bnode root, int key)
        {
            Node current = find(root, key);
            if (current != null) {
                if (current.multiNode.children[current.position] != null) {
                    //current is not leaf
                    //..
                    //find in-order predecessor
                    Bnode predecessor =
                    current.multiNode.children[current.position];
                    while (predecessor.children[predecessor.n] != null) {
                        predecessor = predecessor.children[predecessor.n];
                    }
                    //...
                    //replace with the predecessor
                    current.multiNode.keys[current.position] =
                    predecessor.keys[predecessor.n - 1];
                    //try to delete leaf
                    current = new Node(predecessor, predecessor.n - 1);
                }
                //delete leaf
                //...
                current.multiNode.delete(current.position);
                int minKeys = current.multiNode.size / 2;
                if (current.multiNode == root && current.multiNode.n == 0)
                    root = null;
                while (root != null && current.multiNode != root && current.multiNode.n < minKeys) //underflow
                    rebalanceAfterDeletion(ref root, ref current.multiNode, minKeys);
            }
        }

        public void inorder(ref Bnode root)
        {
            if (root != null) {
                for (int i = 0; i < root.n; i++) {
                    inorder(ref root.children[i]);
                    Console.WriteLine("Keys : "+root.keys[i] + " n : "+root.n);
                }
                inorder(ref root.children[root.n]);
            }
        }

        // other
        Object[] split(ref Bnode root, Bnode node)
        {
            int mid = node.n / 2;
            if (node.parent == null) {
                root = new Bnode(m);
                root.children[0] = node;
                node.parent = root;
            }
            Bnode newNode = new Bnode(m);
            newNode.parent = node.parent;
            int nk = 0;
            for (int k = mid + 1; k < node.n; k++, nk++) {
                newNode.insert(node.keys[k]);
                moveParent(newNode, nk, node.children[k]);
            }
            moveParent(newNode, nk, node.children[node.n]);
            node.n = mid;
            return new Object[] { newNode, node.keys[mid] };
        }

        void moveParent(Bnode parent, int position, Bnode child)
        {
            parent.children[position] = child;
            if (child != null)
                child.parent = parent;
        }

        Node find(Bnode root, int key)
        {
            Bnode current = root;
            while (current != null) {
                int position = current.findPosition(key);
                if (position < current.n && current.keys[position] == key)
                    return new Node(current, position); //found
                else
                    current = current.children[position];
            }
            return null; //not found
        }

        bool rebalanceAfterDeletion(ref Bnode root,ref Bnode current, int minKeys)
        {
            Bnode parent = current.parent;
            int i = parent.findChild(current);
            //case 1: look at left sibling
            if (i > 0 && parent.children[i - 1].n > minKeys) {
                rightRotate(parent, i - 1);
                return true;
            }
            //case 2: look at right sibling
            if (i > parent.n && parent.children[i + 1].n > minKeys) {
                leftRotate(parent, i);
                return true;
            }
            //...
            //merge
            if (i > 0)
                current = merge(parent, i - 1); //case 3: merge to left sibling
            else
                current = merge(parent, i); //case 3: merge to right sibling
            if (parent.n == 0 && parent == root) {
                root = current;
                current.parent = null;
            } else current = parent;
            return false;
        }


        void leftRotate(Bnode parent, int position)
        {
            Bnode left = parent.children[position];
            Bnode right = parent.children[position + 1];
            int newPosition = left.insert(parent.keys[position]);
            moveParent(left, newPosition + 1, right.children[0]);
            parent.keys[position] = right.keys[0];
            right.children[0] = right.children[1];
            right.delete(0);
        }

        void rightRotate(Bnode parent, int position)
        {
            Bnode left = parent.children[position];
            Bnode right = parent.children[position + 1];
            int newPosition = right.insert(parent.keys[position]);
            right.children[1] = right.children[0];
            moveParent(right, newPosition, left.children[left.n]);
            parent.keys[position] = left.keys[left.n - 1];
            left.n--;
        }

        Bnode merge(Bnode parent, int position)
        {
            Bnode left = parent.children[position];
            Bnode right = parent.children[position + 1];
            int newPosition = left.insert(parent.keys[position]);
            moveParent(left, newPosition + 1, right.children[0]);
            for (int i = 0; i < right.n; i++) {
                newPosition = left.insert(right.keys[i]);
                moveParent(left, newPosition + 1, right.children[i + 1]);
            }
            parent.delete(position);
            return left;
        }


    }
}

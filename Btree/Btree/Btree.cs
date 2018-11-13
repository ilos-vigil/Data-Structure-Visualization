using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Btree {
    class Btree {
        public Bnode root;
        public int m;

        public Btree(int m) {
            root = new Bnode(m);//,0);
            this.m = m;
        }

        public void insert(ref Bnode root, int key) {
            int currentDepth = 0;

            if (root == null) {
                root = new Bnode(m);//,0);
                root.insert(key);
            } else {
                Bnode current = root;
                while (true) {
                    if (current.children[0] == null) { //insert must be in leaf
                        int position = current.insert(key);
                        while (current.n > current.size) { //overflow
                                                           //split the node
                            Object[] result = split(ref root, current, currentDepth);
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

                    currentDepth++;
                }
            }
        }

        public void inorder(ref Bnode root) {
            if (root != null) {
                for (int i = 0; i < root.n; i++) {
                    inorder(ref root.children[i]);
                    Console.WriteLine("Keys : " + root.keys[i] + " n : " + root.n + " parent.keys[0] : " + (root.parent == null ? "-" : root.parent.keys[0].ToString()));
                }
                inorder(ref root.children[root.n]);
            }
        }

        public void delete(ref Bnode root, int key) {
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

        // other
        Object[] split(ref Bnode root, Bnode node, int currentDepth) {
            int mid = node.n / 2;
            if (node.parent == null) {
                root = new Bnode(m);//, currentDepth);
                root.children[0] = node;
                node.parent = root;
            }
            Bnode newNode = new Bnode(m);//, currentDepth);
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

        void moveParent(Bnode parent, int position, Bnode child) {
            parent.children[position] = child;
            if (child != null)
                child.parent = parent;
        }

        Node find(Bnode root, int key) {
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

        bool rebalanceAfterDeletion(ref Bnode root, ref Bnode current, int minKeys) {
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
            } else
                current = parent;
            return false;
        }


        void leftRotate(Bnode parent, int position) {
            Bnode left = parent.children[position];
            Bnode right = parent.children[position + 1];
            int newPosition = left.insert(parent.keys[position]);
            moveParent(left, newPosition + 1, right.children[0]);
            parent.keys[position] = right.keys[0];
            right.children[0] = right.children[1];
            right.delete(0);
        }

        void rightRotate(Bnode parent, int position) {
            Bnode left = parent.children[position];
            Bnode right = parent.children[position + 1];
            int newPosition = right.insert(parent.keys[position]);
            right.children[1] = right.children[0];
            moveParent(right, newPosition, left.children[left.n]);
            parent.keys[position] = left.keys[left.n - 1];
            left.n--;
        }

        Bnode merge(Bnode parent, int position) {
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

        /* Untuk visualisasi
         */


        // untuk visualisasi keys 
        public List<KeyPosition> keysPosition;
        public void getKeysPosition(ref Bnode root, int depth = 0, int childIndex = -1) {
            if (root != null) {
                int lastChildIndex = root.n;
                for (int i = 0; i < root.n; i++) {
                    getKeysPosition(ref root.children[i], depth + 1, i);
                    keysPosition.Add(new KeyPosition(depth, root.keys[i], childIndex));
                    Console.WriteLine("Keys : " + root.keys[i] + " n : " + root.n + " parent.keys[0] : " + (root.parent == null ? "-" : root.parent.keys[0].ToString()));
                }
                getKeysPosition(ref root.children[root.n], depth + 1, lastChildIndex);
            }
        }

        // untuk visualisasi node/line, unused
        public List<NodeContainer> nodesContainer;
        public void getNodesContainer(ref Bnode root, int depth = 0, int childIndex = -1) {
            if (root != null) {
                nodesContainer.Add(new NodeContainer(depth, childIndex, root.n));
                for (int i = 0; i < root.n; i++) {
                    getNodesContainer(ref root.children[i], depth + 1, i);
                }
            }
        }

        // prototype visualisasi
        int maxDepth;
        List<FakeBNode> fakeBNodes;
        public List<FakeBNode> getFakeBNodes() {
            fakeBNodes = new List<FakeBNode>();
            maxDepth = 0; getMaxDepth(ref root);
            int[] traverseIndex = new int[maxDepth+1];

            getFakeBNodes(ref root, ref traverseIndex);
            fakeBNodes.Sort(new CompareFakeBNode());

            return fakeBNodes;
        }

        public void getMaxDepth(ref Bnode root, int depth=0){
            if(root!=null){
                if(depth>maxDepth){
                    maxDepth = depth;
                }

                for (int i = 0; i < root.n; i++) {
                    getMaxDepth(ref root.children[i], depth + 1);
                }
            }
        }

        // BUG : insert keys & traverseIndex
        public void getFakeBNodes(ref Bnode root,ref int[] traverseIndex, int depth = 0) {
            if(root!=null){
                // get childCount & recursive
                int childCount = 0;
                for (int i = 0; i < root.n+1; i++) {
                    if(root.children[i]!=null){
                        childCount++;
                        traverseIndex[depth] = i;
                        getFakeBNodes(ref root.children[i], ref traverseIndex, depth + 1);
                    }
                }

                // add to fakeBNodes
                fakeBNodes.Add(new FakeBNode(depth, root.n, root.keys, traverseIndex, childCount));

                // debug
                Console.WriteLine("---Start---");
                Console.WriteLine("Depth : " + depth);
                Console.WriteLine("Bnode.n : " + root.n);
                Console.WriteLine("Child count : " + childCount);
                for (int i = 0; i < root.n; i++) {
                    Console.WriteLine("root.keys ke-" + i + "=" + root.keys[i]);
                }
                Console.WriteLine("----End----");
            }
        }
    }
}

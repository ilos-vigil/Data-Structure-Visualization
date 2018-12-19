using System;

namespace Btree {
    class Bnode {
        public int n, size;
        public int[] keys;
        public Bnode[] children;
        public Bnode parent;

        public Bnode(int m)//,int depth)
        { //m is order of multiway tree
            this.size = m - 1;
            this.n = 0;
            this.keys = new int[this.size + 1]; //may overflow 1 key
            this.children = new Bnode[this.size + 2];
            this.children[0] = null;
            this.parent = null;

            //this.depth = depth;
        }

        public int insert(int key) {
            //return -1 if node is full may overflow 1 key
            //otherwise return key position
            if (n == 0)
                return insertFirstKey(key);
            if (key > keys[n - 1])
                return insertLast(key);
            return insertOrder(key);
        }

        private int insertOrder(int key) {
            int position = findPosition(key);
            for (int i = n; i > position; i--) {
                children[i + 1] = children[i];
                keys[i] = keys[i - 1];
            }
            keys[position] = key;
            children[position + 1] = null;
            n++;
            return position;
        }

        private int insertFirstKey(int key) {
            keys[0] = key;
            children[1] = null;
            n = 1;
            return 0;
        }

        private int insertLast(int key) {
            keys[n++] = key;
            children[n] = null;
            return n - 1;
        }

        public int findPosition(int key) {
            int position = Array.BinarySearch(keys, 0, n, key);
            if (position < 0)
                position = ~position;
            return position;
        }

        public int delete(int position) {
            for (int i = position + 1; i < n; i++) { //shift left
                keys[i - 1] = keys[i];
                children[i] = children[i + 1];
            }
            n--;
            return n;
        }


        public int findChild(Bnode child) {
            for (int i = 0; i <= n; i++)
                if (children[i] == child)
                    return i;
            return -1; //not found
        }
    }
}

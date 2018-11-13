﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Btree {
    public class KeyPosition {
        public int depth, key, childIndex;

        public KeyPosition(int depth, int key, int childIndex) {
            this.depth = depth;
            this.key = key;
            this.childIndex = childIndex;
        }
    }

    public class LinePosition {
        public int x1, y1, x2, y2;

        public LinePosition(int x1, int y1, int x2, int y2) {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }
    }

    public class NodeContainer {
        public int x1, x2, depth, keysCount, childIndex;

        public NodeContainer(int x1, int x2, int depth, int keysCount) {
            this.x1 = x1;
            this.x2 = x2;
            this.depth = depth;
            this.keysCount = keysCount;
        }

        public NodeContainer(int depth, int childIndex, int keysCount) {
            this.depth = depth;
            this.childIndex = childIndex;
            this.keysCount = keysCount;
        }
    }

    public class CompareNodeContainer : IComparer<NodeContainer> {
        public int Compare(NodeContainer current, NodeContainer next) {
            int result = current.depth.CompareTo(next.depth);
            if (result == 0)
                result = current.x1.CompareTo(next.x1);

            return result;
        }
    }
    public class KeyContainer {
        public int x1, y1, x2, y2;

        public KeyContainer(int x1, int y1, int x2, int y2) {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }
    }

    public class CompareKeyContainer : IComparer<KeyContainer> {
        public int Compare(KeyContainer current, KeyContainer next) {
            int result = current.y1.CompareTo(next.y1);
            if (result == 0)
                result = current.x1.CompareTo(next.x1);

            return result;
        }
    }

    public class FakeBNode{
        public int depth;
        public int keysCount;
        public int[] keys;
        public int[] traverseIndex;
        public int childCount;

        public FakeBNode(int depth,int keysCount,int[] keys,int[] traverseIndex, int childCount){
            this.depth = depth;
            this.keysCount = keysCount;
            this.keys = keys;
            this.traverseIndex = traverseIndex;
            this.childCount = childCount;
        }
    }

    public class CompareFakeBNode : IComparer<FakeBNode> {
        public int Compare(FakeBNode x, FakeBNode y) {
            int result = x.depth.CompareTo(y.depth);
            if (result == 0)
                result = x.traverseIndex[x.depth - 1].CompareTo(y.traverseIndex[y.depth - 1]);

            return result;
        }
    }
}
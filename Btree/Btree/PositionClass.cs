using System.Collections.Generic;

namespace Btree {
    public class LinePosition { // temporily store line position
        public int x1, y1, x2, y2;

        public LinePosition(int x1, int y1, int x2, int y2) {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }
    }

    public class FakeBNode { // used to replace actual Btree nodes & to prevent modify actual Btree nodes
        public int depth;
        public int keysCount;
        public int[] keys;
        public int[] traverseIndex;
        public int childCount;

        public FakeBNode(int depth, int keysCount, int[] keys, int[] traverseIndex, int childCount) {
            this.depth = depth;
            this.keysCount = keysCount;
            this.keys = keys;
            this.traverseIndex = traverseIndex;
            this.childCount = childCount;
        }
    }

    public class CompareFakeBNode : IComparer<FakeBNode> { // compare based on depth/x position, to make visualization is correct
        public int Compare(FakeBNode x, FakeBNode y) {
            int result = x.depth.CompareTo(y.depth);
            if (result == 0) {
                for (int i = 0; i < x.depth; i++) {
                    result = x.traverseIndex[i].CompareTo(y.traverseIndex[i]);
                    if (result != 0) {
                        break;
                    }
                }
            }

            return result;
        }
    }
}

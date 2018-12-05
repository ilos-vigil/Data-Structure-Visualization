using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Btree {
    public class LinePosition {
        public int x1, y1, x2, y2;

        public LinePosition(int x1, int y1, int x2, int y2) {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
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
            if(result==0){
                for (int i = 0; i < x.depth; i++) {
                    result = x.traverseIndex[i].CompareTo(y.traverseIndex[i]);
                    if(result!=0){
                        break;
                    }
                }
            }

            return result;
        }
    }
}

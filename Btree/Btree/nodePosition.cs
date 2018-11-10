using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Btree {
    class nodePosition {
        public int depth;
        // public List<int> totalParentsChild = new List<int>();
        public List<int> childIndex = new List<int>();
        public int[] keys;

        public nodePosition(List<int> childIndex, int depth, int[] keys){
            this.depth = depth;
            this.childIndex = childIndex;
            this.keys = keys;
        }
    }
}

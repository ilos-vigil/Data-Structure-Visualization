using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Btree {
    class Node {
        public Bnode multiNode;
        public int position;
        public Node(Bnode multiNode, int position) {
            this.multiNode = multiNode;
            this.position = position;
        }
    }
}

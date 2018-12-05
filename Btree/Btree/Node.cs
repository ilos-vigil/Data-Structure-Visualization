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

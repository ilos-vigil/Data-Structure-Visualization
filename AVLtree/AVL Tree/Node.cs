using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL_Tree
{
    public class Node
    {
        public int value;
        public Node left, right;
        public sbyte balanceFactor;
        public Node parent;

        public Node(int val)
        {
            value = val;
            left = null;
            right = null;
            balanceFactor = 0;
            parent = null;
        }
    }
}
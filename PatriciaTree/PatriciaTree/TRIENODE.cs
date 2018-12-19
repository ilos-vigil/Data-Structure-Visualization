using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatriciaTree
{
    class TRIENODE
    {
        public char symbol;
        public string prefix;
        public TRIENODE equal;
        //child pointer
        public TRIENODE left, right;
        public TRIENODE parentTrie, parentLink;
        public string value;
        public int X, Y;

        public TRIENODE(string prefix, TRIENODE parentTrie, TRIENODE parentLink)
        {
            this.prefix = prefix;
            this.symbol = prefix[0];
            this.equal = null;
            this.left = null;
            this.right = null;
            this.parentTrie = parentTrie;
            this.parentLink = parentLink;
            this.value = null;
            X = 0;
            Y = 0;
        }
    }

}

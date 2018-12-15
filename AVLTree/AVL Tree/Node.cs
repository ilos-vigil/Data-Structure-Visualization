using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL_Tree
{
    public class Node
    {
        public int key, height;
        public Node left, right;
        public string idLingkaran;
        public string idGaris;

        public Node(int key,string idLingkaran)
        {
            this.key = key;
            this.idLingkaran = idLingkaran;
            height = 1;

        }
    }


}

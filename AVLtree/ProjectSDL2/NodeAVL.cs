using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSDL2
{
    public class NodeAVL
    {
        public int value;
        public NodeAVL left, right;
        public sbyte balanceFactor;
        public string idLingkaran;
        public string idGaris;

        public NodeAVL(int value,string idLingkaran)
        {
            this.value = value;
            this.left = null;
            this.right = null;
            this.balanceFactor = 0;
            this.idLingkaran = idLingkaran;
        }
    }
}

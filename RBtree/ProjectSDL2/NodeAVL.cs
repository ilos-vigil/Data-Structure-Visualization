using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSDL2
{
    public class NodeAVL
    {
        public int value;
        public NodeAVL left, right;
        public string idLingkaran;
        public NodeAVL parent;
        public int x, y;
        public Color color;
        public Boolean sentinel;
        public Boolean isRight;
        public int height;
        public Boolean passed = false;

        public NodeAVL(int value,string idLingkaran)
        {
            this.value = value;
            this.left = null;
            this.right = null;
            this.parent = null;
            this.idLingkaran = idLingkaran;
            color = Color.Red;
            this.sentinel = false;
            this.height = 1;
        }
        public NodeAVL(int value)
        {
            this.value = value;
            this.left = null;
            this.right = null;
            this.parent = null;
            color = Color.Red;
            this.sentinel = false;
            this.height = 1;
        }
        public NodeAVL()
        {
            this.left = null;
            this.right = null;
            this.parent = null;
            color = Color.Red;
            this.sentinel = true;
            this.height = 1;
        }
    }
}

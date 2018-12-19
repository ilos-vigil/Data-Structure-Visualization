using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace PatriciaTree
{
    public class Garis
    {
        public int x1, x2, y1, y2;
        public Pen p;
        public Garis(int x1, int y1, int x2, int y2,Pen p)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
            this.p = p;
        }

        public Garis(int x1, int y1, int x2, int y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }
    }
}

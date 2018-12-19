using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PatriciaTree
{
    public class PatriciaLingkaran
    {
        public int x;
        public int y;
        public Rectangle lingkaran;
        public string prefix;
        public string value;
        public Brush brush;
        public PatriciaLingkaran(int x, int y, string value, string prefix)
        {
            this.x = x;
            this.y = y;
            lingkaran = new Rectangle(this.x, this.y, 30, 30);
            this.value = value;
            this.prefix = prefix;
            if (this.value != null)
                brush = new SolidBrush(Color.Red);
            else
                brush = new SolidBrush(Color.Black);
        }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace ProjectSDL2
{
    public class Lingkaran
    {
        public int x;
        public int y;
        public string value;
        public Brush brush;
        public Lingkaran(int x,int y,string value,Color c)
        {
            this.x = x;
            this.y = y;
            this.value = value;
            brush = new SolidBrush(c);
        }
    }
}

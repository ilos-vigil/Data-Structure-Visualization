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
        public int balanceFactor;
        public string value;
        public Pen pen;
        public Lingkaran(int x,int y,string value,int balance)
        {
            this.x = x;
            this.y = y;
            this.value = value;
            this.balanceFactor = balance;
            pen = new Pen(Color.Black);
        }
    }
}

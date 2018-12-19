using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace PatriciaTree
{
    public class PatriciaContainer
    {
        public Rectangle charContainer;
        public Rectangle prefixContainer;
        public Rectangle value;
        public char symbol;
        public string prefix;
        public string val;
        public Rectangle prevPointer;
        public Rectangle equelsPointer;
        public Rectangle nextPointer;
        public int tinggiKotak = 20;
        public int panjangKotakPointer = 25;
        public int panjangKotakPrefVal = 100;
        public int panjangKotakChar = 20;
        public int totalWidth;
        public int startX;
        public int startY;
        public PatriciaContainer(int x,int y,string prefix,string val)
        {
            charContainer = new Rectangle(x, y, panjangKotakChar, tinggiKotak);
            prefixContainer = new Rectangle(x + panjangKotakChar, y , panjangKotakPrefVal, tinggiKotak);
            value = new Rectangle(x + charContainer.Width+prefixContainer.Width, y , panjangKotakPrefVal, tinggiKotak);
            prevPointer = new Rectangle(x + charContainer.Width + prefixContainer.Width+ value.Width, y, panjangKotakPointer, tinggiKotak);
            equelsPointer = new Rectangle(x + charContainer.Width + prefixContainer.Width+value.Width+prevPointer.Width, y, panjangKotakPointer, tinggiKotak);
            nextPointer = new Rectangle(x + charContainer.Width + prefixContainer.Width+value.Width+prevPointer.Width+equelsPointer.Width, y, panjangKotakPointer, tinggiKotak);
            this.val = val;
            this.prefix = prefix;
            this.symbol = prefix[0];
            totalWidth = panjangKotakPointer * 3 + panjangKotakPrefVal * 2 + panjangKotakChar;
            startX = x;
            startY = y;
        }
        public void geserKanan(int force)
        {
            charContainer.X += force;
            prefixContainer.X += force;
            value.X += force;
            prevPointer.X += force;
            equelsPointer.X += force;
            nextPointer.X += force;
        }

    }
}

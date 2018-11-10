using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Btree
{
    public partial class Form1 : Form
    {
        Btree bt;

        int width = 854, height = 480;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnOrdo_Click(object sender, EventArgs e) {
            int ordoSize = int.Parse(tbOrdo.Text);
            bt = new Btree(ordoSize);
            panel1.Refresh();
        }

        private void btnInsert_Click(object sender, EventArgs e) {
            int insertValue = int.Parse(tbInsert.Text);
            bt.insert(ref bt.root, insertValue);
            panel1.Refresh();
        }

        private void btnSearch_Click(object sender, EventArgs e) {

        }

        private void btnDelete_Click(object sender, EventArgs e) {
            int deleteValue = int.Parse(tbDelete.Text);
            bt.insert(ref bt.root, deleteValue);
            panel1.Refresh();
        }

        private void panel1_Paint(object sender, PaintEventArgs e) {
            if(bt!=null){
                /*
                bt.np = new List<nodePosition>();
                List<int> childIndex = new List<int>();
                bt.nodeInorder(ref bt.root, 0,ref childIndex);
                */

                bt.kp = new List<keyPosition>();
                bt.inorder(ref bt.root);

                int maxDepth = 0;
                for (int i = 0; i < bt.kp.Count; i++) {
                    if (bt.kp[i].depth > maxDepth)
                        maxDepth = bt.kp[i].depth;
                }

                int[] depthX = new int[maxDepth+1];
                int[] lastChildIndex = new int[maxDepth + 1];
                for (int i = 0; i < maxDepth; i++) {
                    depthX[i] = 0;
                    lastChildIndex[i] = -1;
                }

                for (int i = 0; i < bt.kp.Count; i++) {
                    int y = (bt.kp[i].depth * 50) + 10;                    
                    
                    int x = depthX[bt.kp[i].depth];
                    depthX[bt.kp[i].depth] += 30;
                    if (lastChildIndex[bt.kp[i].depth] != -1 && bt.kp[i].childIndex != lastChildIndex[bt.kp[i].depth]) {
                        depthX[bt.kp[i].depth] += 30;
                    }
                    lastChildIndex[bt.kp[i].depth] = bt.kp[i].childIndex;

                    Console.WriteLine("Key : " + bt.kp[i].key);
                    Console.WriteLine("Depth : " + bt.kp[i].depth);
                    Console.WriteLine("Y : " + y);
                    Console.WriteLine("X : " + x);
                    Console.WriteLine("Child index : " + bt.kp[i].childIndex);



                    // begin draw
                    Pen p = new Pen(Color.Black);
                    Brush b = new SolidBrush(Color.Black);
                    Font f = new Font("Courier New", 10, FontStyle.Regular);


                    e.Graphics.DrawRectangle(p, x, y, 30, 30);
                    e.Graphics.DrawString(bt.kp[i].key.ToString(), f, b, x, y);
                }

                bt.inorder(ref bt.root);
            }
        }
    }
}

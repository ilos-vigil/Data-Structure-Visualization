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
            setOrdo();
        }
        private void tbOrdo_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
                setOrdo();
        }
        private void setOrdo(){
            int ordoSize = int.Parse(tbOrdo.Text);
            bt = new Btree(ordoSize);
            tbOrdo.Text = "";
            panel1.Refresh();

            tbInsert.Enabled = true;
            btnInsert.Enabled = true;
            tbSearch.Enabled = true;
            btnSearch.Enabled = true;
            tbDelete.Enabled = true;
            btnDelete.Enabled = true;

            btnOrdo.Text = "Reset Tree";
        }

        private void btnInsert_Click(object sender, EventArgs e) {
            insertKey();
        }
        private void tbInsert_KeyUp(object sender, KeyEventArgs e) {
            if(e.KeyCode == Keys.Enter) insertKey();
        }
        private void insertKey() {
            int insertValue = int.Parse(tbInsert.Text);
            bt.insert(ref bt.root, insertValue);
            tbInsert.Text = "";
            panel1.Refresh();
        }

        private void btnSearch_Click(object sender, EventArgs e) {

        }
        private void tbSearch_KeyUp(object sender, KeyEventArgs e) {

        }

        private void btnDelete_Click(object sender, EventArgs e) {
            deleteKey();
        }
        private void tbDelete_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
                deleteKey();
        }
        private void deleteKey(){
            int deleteValue = int.Parse(tbDelete.Text);
            bt.delete(ref bt.root, deleteValue);
            tbDelete.Text = "";
            panel1.Refresh();
        }

        private void panel1_Paint(object sender, PaintEventArgs e) {
            if(bt!=null){
                bt.keysPosition = new List<keyPosition>();
                bt.getKeysPosition(ref bt.root);

                int maxDepth = 0;
                for (int i = 0; i < bt.keysPosition.Count; i++) { // get max depth
                    if (bt.keysPosition[i].depth > maxDepth)
                        maxDepth = bt.keysPosition[i].depth;
                }

                int[] lastXPosition = new int[maxDepth+1];
                int[] lastChildIndex = new int[maxDepth + 1];
                for (int i = 0; i < maxDepth; i++) {
                    lastXPosition[i] = 0;
                    lastChildIndex[i] = -2;
                }

                for (int i = 0; i < bt.keysPosition.Count; i++) {
                    int y = (bt.keysPosition[i].depth * 50) + 10;
                    int x = lastXPosition[bt.keysPosition[i].depth];
                    
                    lastChildIndex[bt.keysPosition[i].depth] = bt.keysPosition[i].childIndex;

                    // move last X position
                    lastXPosition[bt.keysPosition[i].depth] += 30;
                    if (i<bt.keysPosition.Count-1 && lastChildIndex[bt.keysPosition[i + 1].depth] !=-2 && bt.keysPosition[i+1].childIndex != lastChildIndex[bt.keysPosition[i+1].depth]) { // check if next key nodes on different node
                        lastXPosition[bt.keysPosition[i+1].depth] += 30;
                    }

                    // debug
                    Console.WriteLine("Key : " + bt.keysPosition[i].key);
                    Console.WriteLine("Depth : " + bt.keysPosition[i].depth);
                    Console.WriteLine("Y : " + y);
                    Console.WriteLine("X : " + x);
                    Console.WriteLine("Child index : " + bt.keysPosition[i].childIndex);

                    // begin draw
                    Pen p = new Pen(Color.Black);
                    Brush b = new SolidBrush(Color.Black);
                    Font f = new Font("Courier New", 10, FontStyle.Regular);

                    e.Graphics.DrawRectangle(p, x, y, 30, 30);
                    e.Graphics.DrawString(bt.keysPosition[i].key.ToString(), f, b, x, y);
                }
            }
        }
    }
}

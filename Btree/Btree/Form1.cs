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
                bt.np = new List<nodePosition>();
                List<int> childIndex = new List<int>();
                bt.nodeInorder(ref bt.root, 0,ref childIndex);

                for (int i = 0; i < bt.np.Count; i++) {
                    int y = (bt.np[i].depth * 50) + 10;
                    Console.WriteLine("Y : " + y);
                    int position = 0;
                    for (int j = 0; j < bt.np[i].childIndex.Count; j++) {
                        position += bt.np[i].childIndex[j] * (bt.np[i].childIndex.Count - j*bt.m);
                    }
                    int maxPosition = (int) Math.Pow((Double)bt.m,(Double) bt.np[i].childIndex.Count);
                    int x = bt.np[i].childIndex.Count == 0 ? width/2 : (width / (int)Math.Pow(2.0, (Double)bt.np[i].childIndex.Count));
                    x += width * position / maxPosition;

                    // begin draw
                    Pen p = new Pen(Color.Black);
                    Brush b = new SolidBrush(Color.Black);
                    Font f = new Font("Courier New", 10, FontStyle.Regular);

                    for (int k = 0; k < bt.m-1; k++) {
                        e.Graphics.DrawRectangle(p, x + k*30, y, 30, 30);

                        if(bt.np[i].keys[k]!=0){
                            e.Graphics.DrawString(bt.np[i].keys[k].ToString(), f, b, x + k * 30, y);
                        }
                    }
                }

                bt.inorder(ref bt.root);
            }
        }
    }
}

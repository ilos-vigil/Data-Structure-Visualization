using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectSDL2
{
    public partial class Form1 : Form
    {
        AVLTree avl;
        Brush temp;
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            avl = new AVLTree(this);
            temp = new SolidBrush(Color.White);
            pictureBox1.Width = this.Width;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            foreach (line x in avl.garis)
            {
                Pen p = new Pen(x.warna);
                e.Graphics.DrawLine(p, x.x1, x.y1, x.x2, x.y2);
            }
            foreach (var item in avl.lingkaran)
            {
                e.Graphics.FillEllipse(item.brush, item.x, item.y, 30, 30);
                e.Graphics.DrawString(item.value, new Font("Arial",14), temp,new Point(item.x,item.y+5));
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void insertBtn_Click(object sender, EventArgs e)
        {
            avl.insertion(ref avl.root, Convert.ToInt32(valueBox.Text.ToString()), this.Width/2, 50);
            valueBox.Text = "";
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            avl.search(ref avl.root, Convert.ToInt32(valueBox.Text.ToString()));
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            avl.delete(ref avl.root, Convert.ToInt32(valueBox.Text.ToString()));
        }
    }
}

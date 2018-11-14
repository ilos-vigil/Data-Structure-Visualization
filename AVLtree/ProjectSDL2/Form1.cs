using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
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
            temp = new SolidBrush(Color.Black);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var item in avl.lingkaran.Values)
            {
                e.Graphics.DrawEllipse(item.pen, item.x, item.y, 40, 40);
                e.Graphics.DrawString(item.value, new Font("Arial",16), temp,new Point(item.x+8,item.y+10));
                e.Graphics.DrawString(item.balanceFactor.ToString(), new Font("Arial", 16), temp, new Point(item.x + 38, item.y + 10));
            }   
            foreach(var item in avl.garis.Values)
            {
                e.Graphics.DrawLine(new Pen(Color.Black), item.x1, item.y1, item.x2, item.y2);
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
            avl.insertHelper(Convert.ToInt32(valueBox.Text));
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            avl.search(ref avl.root, Convert.ToInt32(valueBox.Text.ToString()));
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {

        }

        private void Inorder_Click(object sender, EventArgs e)
        {
            avl.inOrderHelper();
        }

        private void status_Validated(object sender, EventArgs e)
        {

        }

        private void status_TextChanged(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void status_Click(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            status.Text = "";
            timer1.Stop();
        }
    }
}

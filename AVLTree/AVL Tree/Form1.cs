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
namespace AVL_Tree
{
    public partial class Form1 : Form
    {
        
            AVLTree tree;
        public Form1()
        {
            InitializeComponent();
            this.Text = "AVL Visualization";
            tree = new AVLTree(this);
            ///* Constructing tree given in the above figure */
            //tree.root = tree.InsertHelper(tree.root, 9);
            //tree.root = tree.InsertHelper(tree.root, 5);
            //tree.root = tree.InsertHelper(tree.root, 10);
            //tree.root = tree.InsertHelper(tree.root, 0);
            //tree.root = tree.InsertHelper(tree.root, 6);
            //tree.root = tree.InsertHelper(tree.root, 11);
            //tree.root = tree.InsertHelper(tree.root, -1);
            //tree.root = tree.InsertHelper(tree.root, 1);
            tree.preOrder(tree.root);
            foreach (var item in tree.lingkaran)
            {
                Console.WriteLine("X : "+ item.Value.x + " Y: " + item.Value.y);

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
            foreach (var item in tree.garis.Values)
            {
                e.Graphics.DrawLine(new Pen(Color.Black), item.x1, item.y1, item.x2, item.y2);
            }
            foreach (var item in tree.lingkaran.Values)
            {
                e.Graphics.FillEllipse(item.brush, item.x, item.y, 50, 50);
                e.Graphics.DrawString(item.value, new Font("Arial", 16), new SolidBrush(Color.White), new Point(item.x + 8, item.y + 10));
                //    e.Graphics.DrawString(item.balanceFactor.ToString(), new Font("Arial", 16), new SolidBrush(Color.Black), new Point(item.x + 38, item.y + 10));
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        public void mbox(String text)
        {
            MessageBox.Show(text);
        }

        private void InsertBtn_Click(object sender, EventArgs e)
        {
            tree.root = tree.InsertHelper(tree.root, Convert.ToInt32(valueBox.Text));
            tree.inOrderHelper();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            tree.root = tree.deleteNode(tree.root, Convert.ToInt32(valueBox.Text));
            tree.inOrderHelper();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Status.Text = "";
            timer1.Stop();
        }

        private void Status_TextChanged(object sender, EventArgs e)
        {

            timer1.Start();
        }

        private void Find_Click(object sender, EventArgs e)
        {
            tree.find(Convert.ToInt32(valueBox.Text));
        }

        private void InOrder_Click(object sender, EventArgs e)
        {
            tree.inOrder(tree.root);
            MessageBox.Show("InOrder : " + tree.inOrderResult);
            tree.inOrderResult = "";
            tree.inOrderHelper();
        }
    }
}

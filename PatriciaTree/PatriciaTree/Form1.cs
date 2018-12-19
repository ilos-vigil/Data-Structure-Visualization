using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatriciaTree
{
    public partial class Form1 : Form
    {
        PatriciaTree tree;
        public Form1()
        {
            InitializeComponent();
            tree = new PatriciaTree(this);
            //tree.insert(ref tree.root, "tester", "tester");
            //tree.insert(ref tree.root, "slow", "slow");
            //tree.insert(ref tree.root, "water", "water");
            //tree.insert(ref tree.root, "slower", "slower");
            //tree.insert(ref tree.root, "test", "test");
            //tree.insert(ref tree.root, "team", "team");
            //belum bisa hapus merge
            //gara gara bandigin char
            //find salah
            //gambar pakai.next
            //dihapus blm di null(belum dipindah nodeinparent)
            //tree.delete(ref tree.root, "water");
            //tree.inOrder(ref tree.root);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void insertBtn_Click(object sender, EventArgs e)
        {
            tree.insert(ref tree.root, textBox1.Text, textBox1.Text);
            tree.inOrderDrawHelper2();
            tree.inOrderDrawHelper();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Brush b = new SolidBrush(Color.Black);

          

            if (checkBox1.Checked)
            {
                foreach (var con in tree.container)
                {
                    e.Graphics.DrawRectangle(new Pen(Color.Black), con.charContainer);
                    e.Graphics.DrawRectangle(new Pen(Color.Black), con.prefixContainer);
                    e.Graphics.DrawRectangle(new Pen(Color.Black), con.value);
                    e.Graphics.DrawRectangle(new Pen(Color.Black), con.prevPointer);
                    e.Graphics.DrawRectangle(new Pen(Color.Black), con.equelsPointer);
                    e.Graphics.DrawRectangle(new Pen(Color.Black), con.nextPointer);
                    e.Graphics.DrawString(con.val, new Font("Arial", 16), b, con.value);
                    e.Graphics.DrawString(con.symbol.ToString(), new Font("Arial", 16), b, con.charContainer);
                    e.Graphics.DrawString(con.prefix, new Font("Arial", 16), b, con.prefixContainer);

                }
                tree.garisContainer.ForEach(garis => e.Graphics.DrawLine(new Pen(Color.Black), garis.x1, garis.y1, garis.x2, garis.y2));


            }
            else
            {
                foreach (var lingkaran in tree.lingkaran)
                {
                    Console.WriteLine(tree.lingkaran.Count);
                    e.Graphics.FillEllipse(lingkaran.brush, lingkaran.lingkaran);
                    e.Graphics.DrawString(lingkaran.prefix, new Font("Arial", 16), new SolidBrush(Color.White), lingkaran.lingkaran);
                    if (lingkaran.value != null) e.Graphics.DrawString(lingkaran.value, new Font("Arial", 16), new SolidBrush(Color.Black), new Point(lingkaran.lingkaran.X, lingkaran.lingkaran.Y + 20));

                }
                tree.garis.ForEach(garis => e.Graphics.DrawLine(garis.p, garis.x1, garis.y1, garis.x2, garis.y2));

            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            tree.delete(ref tree.root, textBox1.Text);
            tree.inOrderDrawHelper2();
            tree.inOrderDrawHelper();
            tree.temp = null;
        }
    }
}

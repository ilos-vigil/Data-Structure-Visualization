using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AVL_Tree;
using Btree;
using ProjectSDL2; // red-black tree
using PatriciaTree;

namespace SDL {
    public partial class Form1 : Form {
        AVL_Tree.Form1 avl;
        ProjectSDL2.Form1 rb;
        Btree.Form1 bt;
        PatriciaTree.Form1 pt;

        public Form1() {
            InitializeComponent();

        }

        private void avlButton_Click(object sender, EventArgs e) {
            if(avl==null) {
                avl = new AVL_Tree.Form1();
                avl.FormClosed += avlClose;
                avl.Show();
            }
        }

        private void rbButton_Click(object sender, EventArgs e) {
            if(rb==null) {
                rb = new ProjectSDL2.Form1();
                rb.FormClosed += rbClose;
                rb.Show();
            }
        }

        private void btButton_Click(object sender, EventArgs e) {
            if(bt==null) {
                bt = new Btree.Form1();
                bt.FormClosed += btClose;
                bt.Show();
            }
        }

        private void patriciaButton_Click(object sender, EventArgs e) {
            if (pt == null) {
                pt = new PatriciaTree.Form1();
                pt.FormClosed += ptClose;
                pt.Show();
            }
        }

        private void avlClose(object sender, EventArgs e){
            avl = null;
        }

        private void rbClose(object sender, EventArgs e) {
            rb = null;
        }

        private void btClose(object sender, EventArgs e) {
            bt = null;
        }

        private void ptClose(object sender, EventArgs e)
        {
            pt = null;
        }

    }
}

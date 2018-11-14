using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
namespace ProjectSDL2
{
    public class AVLTree
    {
        public NodeAVL root;
        public Dictionary<string, Lingkaran> lingkaran = new Dictionary<string, Lingkaran>();
        public Dictionary<string, Garis> garis = new Dictionary<string, Garis>();


        int ctrGaris =1;
        int ctrLingkaran=1;
        Form1 parent;
        public AVLTree(Form1 parent)
        {
            root = null;
            this.parent = parent;
        }

        public void insertHelper(int value)
        {
            search(ref root, value);
            insertion(ref root, value, 484, 34);
            inOrderHelper();
        }
        public Boolean insertion(ref NodeAVL root, int key,int x,int y)
        {
            if (root == null)
            {
                lingkaran.Add("lingkaran" + ctrLingkaran, new Lingkaran(x,y,key.ToString(),0));
                ctrLingkaran++;
                parent.pictureBox1.Invalidate();
                root = new NodeAVL(key,"lingkaran"+ (ctrLingkaran-1));
                Console.WriteLine("lingkaran"+(ctrLingkaran-1));
                return false;
            }
            else if (key < root.value)
            {
                Boolean rotated = insertion(ref root.left, key,x-50,y+50);
                if (!rotated) rotated = updateBalanceFactor(ref root, +1);
                return rotated;
            }
            else
            {
                Boolean rotated = insertion(ref root.right, key, x + 50, y + 50);
                if (!rotated) rotated = updateBalanceFactor(ref root, -1);
                return rotated;
            }
        }
      
        public void search(ref NodeAVL root, int value)
        {
            NodeAVL temp = root;
            parent.insertBtn.Enabled = false;
            parent.searchBtn.Enabled = false;
            parent.deleteBtn.Enabled = false;
            if (this.root == null)
            {
                parent.status.Text = "Root is null";
                Application.DoEvents();
                Thread.Sleep(1000);
                parent.status.Text = "Insert "+ value + " at root";
                Application.DoEvents();
                Thread.Sleep(1000);
                parent.insertBtn.Enabled = true;
                parent.searchBtn.Enabled = true;
                parent.deleteBtn.Enabled = true;
                return;
            }
            while (temp != null)
            {
                lingkaran[temp.idLingkaran].pen = new Pen(Color.Yellow);
                parent.pictureBox1.Invalidate();
                Application.DoEvents();
                System.Threading.Thread.Sleep(1000);
                lingkaran[temp.idLingkaran].pen = new Pen(Color.Black);
                parent.pictureBox1.Invalidate();
                Application.DoEvents();
                if (temp.value == value)
                {
                    break;
                }
                if (value < temp.value)
                {
                    if (temp.left != null)
                        parent.status.Text = value + " is less than " + temp.value + " pointer go to left";
                    else
                        parent.status.Text = "Insert " + value + " left of " + temp.value + "child";
                    Application.DoEvents();
                    Thread.Sleep(1000);
                    temp = temp.left;
                }
                else
                {
                    if (temp.right != null)
                        parent.status.Text = value + " is greater than " + temp.value + " pointer go to right";
                    else
                        parent.status.Text = "Insert " + value + " right of " + temp.value + "child";
                    Application.DoEvents();
                    Thread.Sleep(1000);
                    temp = temp.right;
                }

            }

            parent.insertBtn.Enabled = true;
            parent.searchBtn.Enabled = true;
            parent.deleteBtn.Enabled = true;
        }
        private sbyte max(sbyte a, sbyte b)
        {
            return (a > b) ? a : b;
        }

        private sbyte min(sbyte a, sbyte b)
        {
            return (a < b) ? a : b;
        }
        public void singleLeftRotate(ref NodeAVL p)
        {
            parent.status.Text = "Left Rotation on Node "+p.value;
            Application.DoEvents();
            Thread.Sleep(1000);
            NodeAVL q = p.right;
            p.right = q.left;
            q.left = p;
           p.balanceFactor += (sbyte)(1 - min(q.balanceFactor, 0));
            q.balanceFactor += (sbyte)(1 + max(p.balanceFactor, 0));    
            p = q;
            inOrderHelper();
        }

        public void singleRightRotate(ref NodeAVL q)
        {
            parent.status.Text = "Right Rotation on Node "+ q.value;
            Application.DoEvents();
            Thread.Sleep(1000);
            NodeAVL p = q.left;
            q.left = p.right;
            p.right = q;
            q.balanceFactor -= (sbyte)(1 + max(p.balanceFactor, 0));
            p.balanceFactor -= (sbyte)(1 - min(q.balanceFactor, 0));
            q = p;
            inOrderHelper();
        }
        

        public void doubleLeftRotate(ref NodeAVL x)
        {
            parent.status.Text = "Double Left Rotation on "+ x.value;
            Application.DoEvents();
            Thread.Sleep(2000);
            singleRightRotate(ref x.right);
            System.Threading.Thread.Sleep(2000);
            singleLeftRotate(ref x);
            
        }

        public void doubleRightRotate(ref NodeAVL x)
        {
            parent.status.Text = "Double Right Rotation on " + x.value;
            Application.DoEvents();
            Thread.Sleep(2000);
            singleLeftRotate(ref x.left);
            System.Threading.Thread.Sleep(2000);
            singleRightRotate(ref x);
        }
        private Boolean updateBalanceFactor(ref NodeAVL node, sbyte delta)
        {
            node.balanceFactor += delta;
            switch (node.balanceFactor)
            {
                case +2:
                    if (node.left.balanceFactor == -1)
                    {
                        doubleRightRotate(ref node);
                    }
                    else
                    {
                        singleRightRotate(ref node);
                    }
                    return true;

                case -2:
                    if (node.right.balanceFactor == +1)
                    {
                        doubleLeftRotate(ref node);
                    }
                    else
                    {
                        singleLeftRotate(ref node);
                    }
                    return true;
            }
            return false;
        }

        public void inOrderHelper()
        {
            lingkaran.Clear();
            garis.Clear();
            ctrLingkaran = 1;
            ctrGaris = 1;
            inOrderClear(ref this.root,484,34);
        }
        public void inOrderClear(ref NodeAVL root,int x,int y)
        {
            if (root == null) return;
            inOrderClear(ref root.left, x - 50, y + 50);

            Console.WriteLine(root.value);
            lingkaran.Add("lingkaran" + ctrLingkaran, new Lingkaran(x, y,root.value.ToString(),root.balanceFactor));
            root.idLingkaran = "lingkaran" + ctrLingkaran;
            ctrLingkaran++;
            parent.pictureBox1.Invalidate();
            Application.DoEvents();
             if (root.left != null)
            {
                garis.Add("garis" + ctrGaris, new Garis(x, y+25, x - 25, y + 50));
                ctrGaris++;
                parent.pictureBox1.Invalidate();
            }

            if (root.right != null)
            {
                garis.Add("garis" + ctrGaris, new Garis(x+40, y+25, x + 75, y + 50));
                ctrGaris++;
                parent.pictureBox1.Invalidate();
            }


            inOrderClear(ref root.right, x + 50, y + 50);
        }
      
        




    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace ProjectSDL2
{
    public class AVLTree
    {
        public NodeAVL root;
        public List<line> garis = new List<line>();
        public List<Lingkaran> lingkaran = new List<Lingkaran>();
       

        int ctrGaris=1;
        int ctrLingkaran=1;
        Form1 parent;
        public AVLTree(Form1 parent)
        {
            root = null;
            this.parent = parent;
        }
        public void insertion(ref NodeAVL root, int key,int x,int y)
        {
            NodeAVL newnode = new NodeAVL(key);
            if (root == null)
            {
                root = new NodeAVL(key, "lingkaran" + (ctrLingkaran - 1));
                root.x = x;
                root.y = y;
                root.color = Color.Black;
                Console.WriteLine("lingkaran" + (ctrLingkaran - 1));
                garis.Clear();
                addgaris(ref root);
                parent.pictureBox1.Invalidate();
            }
            else
            {
                NodeAVL current = root;
                while (true)
                {
                    y += 50;
                    if (key < current.value)
                    {
                        x -= 50;
                        if (current.left == null)
                        {
                            current.left = newnode;
                            newnode.x = x;newnode.y = y;
                            newnode.parent = current;
                            retraceafterinsertion(ref root, newnode);
                            break;
                        }
                        current = current.left;
                    }
                    else
                    {
                        x += 50;
                        if (current.right == null)
                        {
                            current.right = newnode;
                            newnode.x = x; newnode.y = y;
                            newnode.parent = current;
                            retraceafterinsertion(ref root, newnode);
                            break;
                        }
                        current = current.right;
                    }
                }
            }
            lingkaran.Clear();
            updatelingkaran(ref root);
            parent.pictureBox1.Invalidate();
        }
        private sbyte max(sbyte a, sbyte b)
        {
            return (a > b) ? a : b;
        }

        private sbyte min(sbyte a, sbyte b)
        {
            return (a < b) ? a : b;
        }
        public void swapcolor(NodeAVL p, NodeAVL q)
        {
            if (p.color != q.color)
            {
                Color temp = p.color;
                p.color = q.color;
                q.color = temp;
            }
        }
        public NodeAVL singleLeftRotate(ref NodeAVL root,NodeAVL p)
        {
            NodeAVL q = p.right;
            p.right = q.left;
            if (q.left != null) q.left.parent = p;
            q.left = p;
            replacenodeinparent(ref root, p, q);
            p.parent = q;
            swapcolor(p, q);
            return q;
        }
        public NodeAVL singleRightRotate(ref NodeAVL root, NodeAVL q)
        {
            NodeAVL p = q.left;
            q.left = p.right;
            if (p.right != null) p.right.parent = q;
            p.right = q;
            replacenodeinparent(ref root, q, p);
            q.parent = p;
            swapcolor(p, q);
            return p;
        }
        public void retraceafterinsertion(ref NodeAVL root,NodeAVL current)
        {
            garis.Clear();
            addgaris(ref root);
            while (true)
            {
                if (current == root)
                {
                    current.color = Color.Black;
                    break;
                }
                if (current.parent.color == Color.Red)
                {
                    NodeAVL p = current.parent;
                    NodeAVL g = p.parent;
                    NodeAVL u = p == g.left ? g.right : g.left;
                    if (u.color == Color.Red)
                    {
                        p.color = Color.Black;
                        u.color = Color.Black;
                        g.color = Color.Red;
                        current = g;
                    }
                    else
                    {
                        rotate(ref root, current, p, g);
                        break;
                    }
                }
                else break;
            }
            parent.pictureBox1.Invalidate();
        }
        public void rotate(ref NodeAVL root,NodeAVL r,NodeAVL p,NodeAVL g)
        {
            if(p==g.left && r == p.left)
            {
                singleRightRotate(ref root, g);
            }else if (p == g.left && r == p.right)
            {
                doubleRightRotate(ref root, g);
            }else if (p == g.right && r == p.right)
            {
                singleLeftRotate(ref root, g);
            }else if(p==g.right && r == p.left)
            {
                doubleLeftRotate(ref root, g);
            }
        }
        public void doubleLeftRotate(ref NodeAVL root, NodeAVL q)
        {
            singleRightRotate(ref root,q.right);
            singleLeftRotate(ref root, q);
        }
        public void doubleRightRotate(ref NodeAVL root, NodeAVL q)
        {
            singleLeftRotate(ref root,q.left);
            singleRightRotate(ref root,q);
        }
        public void updatelingkaran(ref NodeAVL root)
        {
            if (root != null)
            {
                updatelingkaran(ref root.left);
                lingkaran.Add(new Lingkaran(root.x, root.y, root.value.ToString(), root.color));
                updatelingkaran(ref root.right);
            }
            else
            {
                return;
            }
        }
        public void addgaris(ref NodeAVL root)
        {
            if (root != null)
            {
                if (root.left != null) garis.Add(new line(root.x + 15, root.y + 15, root.left.x + 15, root.left.y + 15));
                addgaris(ref root.left);
                if (root.right != null) garis.Add(new line(root.x + 15, root.y + 15, root.right.x + 15, root.right.y + 15));
                addgaris(ref root.right);
            }
            else
            {
                return;
            }
        }
        public Color nodecolor(NodeAVL node)
        {
            return node.color == Color.Black ? Color.Black : node.color;
        }
        
        public void replacenodeinparent(ref NodeAVL root,NodeAVL node,NodeAVL child)
        {
            if (node.parent == null)
            {
                root = child;
            }
            else if(node.parent.left == node)
            {
                node.parent.left = child;
            }
            else
            {
                node.parent.right = child;
            }
            if (child != null)
            {
                child.parent = node.parent;
            }
        }


    }
}

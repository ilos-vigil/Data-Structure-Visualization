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
                root = newnode;
                root.x = x;
                root.y = y;
                root.color = Color.Black;
                parent.pictureBox1.Invalidate();
            }
            else
            {
                NodeAVL current = root;
                while (true)
                {
                    if (key < current.value)
                    {
                        if (current.left == null)
                        {
                            current.left = newnode;
                            newnode.parent = current;
                            retraceafterinsertion(ref root, newnode);
                            break;
                        }
                        current = current.left;
                    }
                    else
                    {
                        if (current.right == null)
                        {
                            current.right = newnode;
                            newnode.parent = current;
                            retraceafterinsertion(ref root, newnode);
                            break;
                        }
                        current = current.right;
                    }
                }
            }
            resetheight(ref root);
            lingkaran.Clear();
            updatelingkaran(ref root);
            garis.Clear();
            addgaris(ref root);
            parent.pictureBox1.Invalidate();
        }
        private int max(int a, int b)
        {
            return (a > b) ? a : b;
        }

        private int min(int a, int b)
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
            if (p.right != null)
            {
                NodeAVL q = p.right;
                p.right = q.left;
                if (q.left != null)
                {
                    q.left.parent = p;
                }
                q.left = p;
                replacenodeinparent(ref root, p, q);
                p.parent = q;
                swapcolor(p, q);
                return q;
            }
            else
            {
                return null;
            }
        }
        public NodeAVL singleRightRotate(ref NodeAVL root, NodeAVL q)
        {
            if (q.left != null)
            {
                NodeAVL p = q.left;
                q.left = p.right;
                if (p.right != null)
                {
                    p.right.parent = q;
                }
                p.right = q;
                replacenodeinparent(ref root, q, p);
                q.parent = p;
                swapcolor(p, q);
                return q;
            }
            else
            {
                return null;
            }
        }
        public Color color(NodeAVL n)
        {
            if (n == null)
            {
                return Color.Black;
            }
            else
            {
                return n.color;
            }
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
                    if (color(u)==Color.Red)
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
            lingkaran.Clear();
            updatelingkaran(ref root);
            parent.pictureBox1.Invalidate();
        }
        public NodeAVL rotate(ref NodeAVL root,NodeAVL r,NodeAVL p,NodeAVL g)
        {
            if (p == g.left && (r == null || r == p.left))
            {
                singleRightRotate(ref root, g);
                return r;
            }
            else if (p == g.left && r == p.right)
            {
                singleLeftRotate(ref root, g.left);
                singleRightRotate(ref root, g);
                return p;
            }
            else if (p == g.right && (r == p.right||r==null))
            {
                singleLeftRotate(ref root, g);
                return r;
            }
            else if (p == g.right && r == p.left)
            {
                singleRightRotate(ref root, g.right);
                singleLeftRotate(ref root, g);
                return p;
            }
            return null;

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
        public void resetheight(ref NodeAVL root)
        {
            if (root != null)
            {
                resetheight(ref root.left);
                root.height = 1;
                resetheight(ref root.right);
            }
            else
            {
                return;
            }
        }
        public void addlingkaran(ref NodeAVL root)
        {
            if (root.parent != null)
            {
                if (!root.isRight)
                {
                    root.x = root.parent.x - 300 / root.height;
                    root.y = root.parent.y + 50;
                }
                else
                {
                    root.x = root.parent.x + 300 / root.height;
                    root.y = root.parent.y + 50;
                }
            }
            if (this.root == root)
            {
                this.root.x = root.x;
                this.root.y = root.y;
            }
            if (root.passed)
            {
                lingkaran.Add(new Lingkaran(root.x, root.y, root.value.ToString(), Color.Yellow));
            }
            else
            {
                lingkaran.Add(new Lingkaran(root.x, root.y, root.value.ToString(), root.color));
            }
        }
        public void updatelingkaran(ref NodeAVL root)
        {
            if (root != null)
            {
                Console.WriteLine(root.value+"_"+root.height);
                if (root.left != null)
                {
                    root.left.parent = root;
                    root.left.isRight = false;
                    root.left.height = (root.height + 1);
                }
                updatelingkaran(ref root.left);
                addlingkaran(ref root);
                if (root.right != null)
                {
                    root.right.parent = root;
                    root.right.isRight = true;
                    root.right.height = (root.height + 1);
                }
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
                if (root.left != null) {
                    if (root.left.passed && root.passed)
                    {
                        garis.Add(new line(root.x + 15, root.y + 15, root.left.x + 15, root.left.y + 15,Color.Yellow));
                    }
                    else
                    {
                        garis.Add(new line(root.x + 15, root.y + 15, root.left.x + 15, root.left.y + 15,Color.Black));
                    }
                }
                addgaris(ref root.left);
                if (root.right != null)
                {
                    if (root.right.passed && root.passed)
                    {
                        garis.Add(new line(root.x + 15, root.y + 15, root.right.x + 15, root.right.y + 15,Color.Yellow));
                    }
                    else
                    {
                        garis.Add(new line(root.x + 15, root.y + 15, root.right.x + 15, root.right.y + 15,Color.Black));
                    }
                }
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
        public NodeAVL find(NodeAVL root, int value)
        {
            NodeAVL current = root;
            while (current != null && value != current.value)
            {
                if (value < current.value)
                {
                    current = current.left;
                }
                else
                {
                    current = current.right;
                }
            }
            return current;
        }
        public void delete(ref NodeAVL root, int value)
        {
            NodeAVL current = find(root, value);
            if (current != null)
            {
                if (current.left == null || current.right == null)
                {
                    replaceNodeInParentAndBalancing(ref root, current, current.left == null ? current.right : current.left);
                }
                else
                {
                    NodeAVL predecessor = current.left;
                    while (predecessor.right != null)
                    {
                        predecessor = predecessor.right;
                    }
                    current.value = predecessor.value;
                    replaceNodeInParentAndBalancing(ref root, predecessor, predecessor.left);
                }
                garis.Clear();
                addgaris(ref root);
                lingkaran.Clear();
                updatelingkaran(ref root);
                parent.pictureBox1.Invalidate();
            }
        }
        public void search(ref NodeAVL root, int value)
        {
            searching(ref root, value);
            lingkaran.Clear();
            updatelingkaran(ref root);
            garis.Clear();
            addgaris(ref root);
            parent.pictureBox1.Invalidate();
            resetsearch(ref root,value);
        }
        public NodeAVL searching(ref NodeAVL root,int value)
        {
            NodeAVL current = root;
            while (current != null && value != current.value)
            {
                if (value < current.value)
                {
                    current.passed = true;
                    current = current.left;
                }
                else
                {
                    current.passed = true;
                    current = current.right;
                }
            }
            current.passed = true;
            return current;
        }
        public void resetsearch(ref NodeAVL root,int value)
        {
            NodeAVL current = root;
            while (current != null && value != current.value)
            {
                if (value < current.value)
                {
                    current.passed = false;
                    current = current.left;
                }
                else
                {
                    current.passed = false;
                    current = current.right;
                }
            }
            current.passed = false;
        }
        public void replaceNodeInParentAndBalancing(ref NodeAVL root, NodeAVL node, NodeAVL child)
        {
            if (node.color == Color.Red || color(child) == Color.Red)
            {
                if (child != null)
                {
                    child.color = Color.Black;
                }
            }
            else if (node.color == Color.Black && color(child) == Color.Black)
            {
                if (child == null)
                {
                    child = new NodeAVL();
                }
                else
                {
                    child.color = Color.Gray;
                }
            }
            replacenodeinparent(ref root, node, child);
            if (child != null)
            {
                retraceafterdeletion(ref root, child);
            }
        }
        public void decreaseColor(ref NodeAVL root, NodeAVL node)
        {
            if (node.color == Color.Gray)
            {
                if (node.sentinel)
                {
                    if (node.parent == null) root = null;
                    else if (node.parent.left == node) node.parent.left = null;
                    else node.parent.right = null;
                }
                else node.color = Color.Black;
            }
            else if (node.color == Color.Black) node.color = Color.Red;
        }
        public void increaseColor(ref NodeAVL root, NodeAVL node)
        {
            if (node.color == Color.Red) node.color = Color.Black;
            else if (node.color == Color.Black) node.color = Color.Gray;
        }
        public void retraceafterdeletion(ref NodeAVL root, NodeAVL u)
        {
            while (u.color == Color.Gray)
            {
                if (u == root)
                {
                    decreaseColor(ref root, u);
                    break;
                }
                NodeAVL p = u.parent;
                NodeAVL s;
                if (u == p.left)
                {
                    s = p.right;
                }
                else
                {
                    s = p.left;
                }
                if (color(s) == Color.Red)
                {
                    rotate(ref root, null, s, p);
                }
                else
                {
                    NodeAVL r = null;
                    if (s == p.left)
                    {
                        if (color(s.left) == Color.Red) r = s.left;
                        else if (color(s.right) == Color.Red) r = s.right;
                    }
                    else
                    {
                        if (color(s.right) == Color.Red) r = s.right;
                        else if (color(s.left) == Color.Red) r = s.left;
                    }
                    if (r != null)
                    {
                        r = rotate(ref root, r, s, p);
                        increaseColor(ref root, r);
                        decreaseColor(ref root, u);
                        break;
                    }
                    else
                    {
                        decreaseColor(ref root, u);
                        decreaseColor(ref root, s);
                        increaseColor(ref root, p);
                        u = p;
                    }
                }
            }
        }
    }
}

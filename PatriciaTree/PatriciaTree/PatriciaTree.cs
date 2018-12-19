using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace PatriciaTree
{
    class PatriciaTree
    {
        public List<PatriciaContainer> container = new List<PatriciaContainer>();
        public List<Garis> garis = new List<Garis>();
        public List<PatriciaLingkaran> lingkaran = new List<PatriciaLingkaran>();
        public List<Garis> garisContainer = new List<Garis>();
        public TRIENODE root;
        public TRIENODE temp;
        public Form1 form;
        public PatriciaTree(Form1 form)
        {
            root = null;
            this.form = form;
        }

        public void inOrderDrawHelper()
        {
            container.Clear();
            garisContainer.Clear();
            inOrderDraw(ref root, 1,1,50,100);
            form.pictureBox1.Invalidate();
        }
        public void inOrderDrawHelper2()
        {
            lingkaran.Clear();
            garis.Clear();
            inOrderDraw2(ref root, 1,1,50);
            form.pictureBox1.Invalidate();
        }

        public void inOrderDraw2(ref TRIENODE root,int kali,int bagi,int tinggi)
        {

            if (root != null)
            {
                inOrderDraw2(ref root.left, (kali * 3) - 1, bagi * 3, tinggi + 100);
                inOrderDraw2(ref root.right, (kali * 3) + 1, bagi * 3, tinggi + 100);
                inOrderDraw2(ref root.equal, (kali * 3), bagi * 3, tinggi + 100);

                 lingkaran.Add(new PatriciaLingkaran((1500 / bagi * kali + 1), tinggi, root.value, root.prefix) );

                if (root.equal != null)
                {
                    garis.Add(new Garis((1500 / bagi * kali + 1) + 15, tinggi + 30, (1500 / (bagi * 3)) * ((kali * 3)) + 15, tinggi + 100, new Pen(Color.Green)));

                }
                if (root.left != null)
                {
                    garis.Add(new Garis((1500 / bagi * kali + 1) + 15, tinggi + 30, (1500 / (bagi * 3)) * ((kali * 3) - 1) + 15, tinggi + 100, new Pen(Color.Red)) );


                }
                if (root.right != null)
                {
                    garis.Add(new Garis((1500 / bagi * kali + 1) + 15, tinggi + 30, (1500 / (bagi * 3)) * ((kali * 3) + 1) + 15, tinggi + 100, new Pen(Color.Blue)));

                }
            }
          
        }

        public void inOrderDraw(ref TRIENODE root, int kali,int bagi,int tinggi,int size)
        {
            // tabrakan yang ke bawah linenya belum di coding
            if (root!=null)
            {
                inOrderDraw(ref root.left, (kali * 2) - 1, bagi * 2, tinggi + 100,size-100);
                inOrderDraw(ref root.right, (kali * 2) + 1, bagi * 2, tinggi + 100, size + 200);
                inOrderDraw(ref root.equal, (kali * 2), bagi * 2, tinggi + 100, size + 100);

                int tempX = (1500 / bagi * kali + 1) + size;
                int tempY = tinggi;
                Rectangle temp = new Rectangle(tempX, tempY, 400, 40);
                bool tabrakan = false;
             /*   for (int i = 0; i < container.Count; i++)
                {
                    Rectangle rect = new Rectangle(container[i].startX, container[i].startY, 400, 40);
                    if (temp.IntersectsWith(rect))
                    {
                        temp.Y += 100;
                        container.Add(new PatriciaContainer((484 / bagi * kali + 1) + size, tinggi+100, root.prefix, root.value));
                        root.X = tempX;
                        root.Y = tempY+100;

                        tabrakan = true;
                        i = 0;
                    }

                }
                */
                if (!tabrakan)
                {
                    PatriciaContainer temp3 = new PatriciaContainer((1500 / bagi * kali + 1) + size, tinggi, root.prefix, root.value);
                    container.Add(temp3);

                    root.X = (1500 / bagi * kali + 1) + size;
                    root.Y = tinggi;
                  
                }

                drawGaris(root, tinggi);
            }
        }

        public void drawGaris(TRIENODE root,int tinggi)
        {
            if (root.equal != null)
            {
                garisContainer.Add(new Garis(root.X+220+25+12, tinggi+20, root.equal.X+295/2, root.equal.Y));

            }
            if (root.left != null)
            {
                garisContainer.Add(new Garis(root.X + 220+12 , tinggi + 20, root.left.X+295/2, root.left.Y));

            }
            if (root.right != null)
            {
                garisContainer.Add(new Garis(root.X + 220 + 25+25+12, tinggi + 20, root.right.X+295/2, root.right.Y));

            }
        }
        public void inOrder(ref TRIENODE root)
        {
            if (root!=null)
            {
                inOrder(ref root.left);
                Console.WriteLine(root.value);
                inOrder(ref root.equal);
                inOrder(ref root.right);

            }
        }

        public TRIENODE inOrderFind(ref TRIENODE root, string key)
        {
            if (root != null)
            {
                inOrderFind(ref root.left,key);
                if (key == root.value) temp=root;
                inOrderFind(ref root.equal,key);
                inOrderFind(ref root.right,key);

            }
            return null;
        }
        public void insert(ref TRIENODE root, string key, string value)
        {
            TRIENODE current = root;
            TRIENODE last = null;
            bool equal = false;
            while (!equal)
            {
                if (current == null)
                {
                    if (last == null)
                        current = root = new TRIENODE(key, null, null);
                    else current = last.equal = new TRIENODE(key, last, null);
                    equal = true;
                }
                else
                {
                    char symbol = key[0];
                    equal = false;
                    while (symbol != current.symbol)
                    {
                        if (symbol < current.symbol)
                        {
                            if (current.left == null)
                            {
                                current.left = new TRIENODE(key, last, current);
                                equal = true;
                            }
                            last = current;
                            current = current.left;
                        }
                        else
                        {
                            if (current.right == null)
                            {
                                current.right = new TRIENODE(key, last, current);
                                equal = true;
                            }
                            last = current;
                            current = current.right;

                        }
                    }

                }
                if (!equal)
                {
                    int i;
                    //cek kata sama atau tidak
                    for (i = 0; i < key.Length && i < current.prefix.Length; i++)
                    {
                        //cek huruf sama atau tidak
                        if (key[i] != current.prefix[i])
                            break;
                    }
                    //pecah node
                    if (i < current.prefix.Length)
                    {
                        TRIENODE newnode = new TRIENODE(current.prefix.Substring(i), null, null);
                        newnode.equal = current.equal;
                        newnode.value = current.value;
                        current.value = null; //aneh
                        current.equal = newnode;
                        current.prefix = current.prefix.Substring(0, i);

                    }
                    if (i == key.Length)
                        equal = true;
                    else
                        key = key.Substring(i);
                }

                last = current;
                current = current.equal;
            }
            last.value = value;
        }

        public TRIENODE find(TRIENODE root,string key)
        {
            inOrderFind(ref root, key);
            TRIENODE current = temp;
            while(true)
            {
                if (current == null) break;
                if (key[0]==current.symbol)
                {
                    if (key == current.value)
                    {
                        return current;
                    }
                     current = current.equal;
                   
                }
                else if (key[0]<current.symbol)
                {
                    current = current.left;
                }
                else
                {
                    current = current.right;
                }

            }
            return null;
        }
      public  void delete(ref TRIENODE root, string key)
        {
            inOrderFind(ref root, key);
            TRIENODE current = temp;
            if (current != null)
            {
                current.value = null;
                //unmark leaf
                while (current != null)
                {
                    bool modified=false;
                    if (current.equal != null)
                        //merge with its child if the current only has one child 
                        modified = tryMerge(current.equal);
                    if (current.equal == null && current.value == null)
                    {
                        //has no children and no value attached -> delete the current ...
                        //binary search tree’s node deletion 
                        
                        /*
                        if (current.left ==null && current.right==null && current.equal==null)
                        {
                            current = null;
                            kenapa ga jalan? 
                            karena currentnya yang di null bukan parent nextnya
                            return;
                        }
                        */
                        if (current.left == null || current.right == null)
                        {
                            //disini
                            //harusnya current tadi ref root
                            //kalau pakai current delete node paling bawah benar tapi ga bisa delete yang atasnya hitam (yang hitam)
                            //yang if masih salah
                            //harusnya ganti dengan childnnya
                            try
                            {
                                if (current.parentTrie == null)
                                    replaceNodeInParent(ref current, current.parentLink.equal, current.left == null ? current.right : current.left);
                                else replaceNodeInParent(ref current.parentTrie.equal, current, current.left == null ? current.right : current.left);

                            }
                            catch (Exception)
                            {

                            }
                           
                            
                            current = current.parentTrie;

                            modified = true;
                        }
                        else
                        {
                            TRIENODE predecessor = current.left;
                            while (predecessor.right != null)
                                predecessor = predecessor.right;
                            current.prefix = predecessor.prefix;
                            current.symbol = predecessor.symbol;
                            current.value = predecessor.value;
                            current.equal = predecessor.equal;
                            replaceNodeInParent(ref root, predecessor, predecessor.left);

                        }

                    }

                    if (!modified)
                        current = current.parentTrie;
                    if (modified) break;

                }
            }


            void replaceNodeInParent(ref TRIENODE rootz, TRIENODE node, TRIENODE child)
            {
                //masih error kalau atasnya hitam(ga ada value)
                try
                {
                    if (node.parentLink == null)
                        rootz = child;
                    else if (node.parentLink.left == node)
                        node.parentLink.left = child;
                    else
                        node.parentLink.right = child;
                    if (child != null) child.parentLink = node.parentLink;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

              }

            bool tryMerge(TRIENODE node)
            { //merge with parent if its parent only has one child 
                if (node.parentTrie != null)
                {
                    TRIENODE parent = node.parentTrie;
                    TRIENODE firstChild = parent.equal;
                    if (firstChild.left != null || firstChild.right != null)
                        return false;
                    //has more than one child, then exit, no merging 
                    parent.prefix += node.prefix;
                    parent.value = node.value;
                    parent.equal = node.equal;
                    if (node.equal != null) node.equal.parentTrie = parent;
                    return true;
                }
                return false;
            }


        }

    }
}
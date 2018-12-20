using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace AVL_Tree
{
    public class AVLTree
    {
        public Node root;
        public string inOrderResult;

        // A utility function to get height of the tree  
        public Dictionary<string, Lingkaran> lingkaran = new Dictionary<string, Lingkaran>();
        public Dictionary<string, Garis> garis = new Dictionary<string, Garis>();

        Form1 form;
        int ctrGaris = 1;
        int ctrLingkaran = 1;

        public AVLTree(Form1 form)
        {
            this.form = form;
            root = null;
        }

        
        int height(Node N)
        {
            if (N == null)
                return 0;
            return N.height;
        }

        // A utility function to get maximum of two integers  
        int max(int a, int b)
        {
            return (a > b) ? a : b;
        }

        // A utility function to right rotate subtree rooted with y  
        // See the diagram given above.  
        Node rightRotate(Node y)
        {
            Node x = y.left;
            Node T2 = x.right;

            // Perform rotation  
            x.right = y;
            y.left = T2;

            // Update heights  
            y.height = max(height(y.left), height(y.right)) + 1;
            x.height = max(height(x.left), height(x.right)) + 1;

            // Return new root  
            return x;
        }

        // A utility function to left rotate subtree rooted with x  
        // See the diagram given above.  
        Node leftRotate(Node x)
        {
            //y= new root 
            //T2= anak kiri root baru
            Node y = x.right;
            //50
            Node T2 = y.left;
            //48
            // Perform rotation  

            //kiri 50 44 sama 17
            y.left = x;
            //kanan 44 48
            x.right = T2;

            // Update heights  
            x.height = max(height(x.left), height(x.right)) + 1;
            y.height = max(height(y.left), height(y.right)) + 1;

            // Return new root  
            return y;
        }

        // Get Balance factor of node N  
        public void delay()
        {
            form.pictureBox1.Invalidate();
            Application.DoEvents();
            Thread.Sleep(300);

        }
        int getBalance(Node N)
        {
            if (N == null)
                return 0;
            return height(N.left) - height(N.right);
        }
        public Node InsertHelper(Node node,int key)
        {
            return insert(node, key,1,1, 34);
        }
        public Node insert(Node node, int key,int kali,int bagi,int tinggi)
        {
            if (node == null)
            {
                form.Status.Text = "Current is null";
                delay();
                form.Status.Text = "Inserting node";
                delay();

                lingkaran.Add("lingkaran" + ctrLingkaran, new Lingkaran(484 / bagi * kali + 1, tinggi,key.ToString()) );
                ctrLingkaran++;
                form.pictureBox1.Invalidate();
                return (new Node(key,"lingkaran"+(ctrLingkaran-1)));

            }

            if (key < node.key)
            {
                form.Status.Text = key + " is lesser than " + node.key;
                lingkaran[node.idLingkaran].brush = new SolidBrush(Color.Yellow);
                delay();
                form.Status.Text =" Current go to left" ;
                lingkaran[node.idLingkaran].brush = new SolidBrush(Color.Black);
                delay();

                node.left = insert(node.left, key, (kali * 2) - 1, bagi * 2, tinggi + 100);
            }
            else if (key > node.key)
            {
                form.Status.Text = key + " is greater than " + node.key;
                lingkaran[node.idLingkaran].brush = new SolidBrush(Color.Yellow);
                delay();
                form.Status.Text = " Current go to right";
                lingkaran[node.idLingkaran].brush = new SolidBrush(Color.Black);
                delay();
                node.right = insert(node.right, key, (kali * 2) + 1, bagi * 2, tinggi + 100);
            }
            else
                return node;

            /* 2. Update height of this ancestor node */
            node.height = 1 + max(height(node.left),
                                height(node.right));

            /* 3. Get the balance factor of this ancestor  
            node to check whether this node became  
            Wunbalanced */
            int balance = getBalance(node);

            // If this node becomes unbalanced, then  
            // there are 4 cases Left Left Case  
            if (balance > 1 && key < node.left.key)
            {
                form.Status.Text = "Right Rotate on " + node.key;
                delay();
                return rightRotate(node);
            }

            // Right Right Case  
            if (balance < -1 && key > node.right.key)
            {
                form.Status.Text = "Right Rotate on " + node.key;
                delay();
                return leftRotate(node);
            }

            // Left Right Case  
            if (balance > 1 && key > node.left.key)
            {
                form.Status.Text = "Double right Rotate on " + node.key;
                delay();
                node.left = leftRotate(node.left);
                return rightRotate(node);
            }

            // Right Left Case  
            if (balance < -1 && key < node.right.key)
            {
                form.Status.Text = "Double left Rotate on " + node.key;
                delay();
                node.right = rightRotate(node.right);
                return leftRotate(node);
            }

            /* return the (unchanged) node pointer */
            return node;
        }

        /* Given a non-empty binary search tree, return the  
        node with minimum key value found in that tree.  
        Note that the entire tree does not need to be  
        searched. */
        Node minValueNode(Node node)
        {
            Node current = node;

            /* loop down to find the leftmost leaf */
            while (current.left != null)
                current = current.left;

            return current;
        }

        public Node deleteNode(Node root, int key)
        {
            // STEP 1: PERFORM STANDARD BST DELETE  
            if (root == null)
            {
                form.Status.Text ="Deleted key not found in tree";
                delay();
                return root;
            }

            // If the key to be deleted is smaller than  
            // the root's key, then it lies in left subtree  
            if (key < root.key)
            {
                form.Status.Text = key + " is lesser than " + root.key;
                lingkaran[root.idLingkaran].brush = new SolidBrush(Color.Yellow);
                delay();
                form.Status.Text = " Current go to left";
                lingkaran[root.idLingkaran].brush = new SolidBrush(Color.Black);
                delay();

                root.left = deleteNode(root.left, key);
            }

            // If the key to be deleted is greater than the  
            // root's key, then it lies in right subtree  
            else if (key > root.key)
            {
                form.Status.Text = key + " is greater than " + root.key;
                lingkaran[root.idLingkaran].brush = new SolidBrush(Color.Yellow);
                delay();
                form.Status.Text = " Current go to right";
                lingkaran[root.idLingkaran].brush = new SolidBrush(Color.Black);
                delay();

                root.right = deleteNode(root.right, key);
            }

            // if key is same as root's key, then this is the node  
            // to be deleted  
            else
            {
                form.Status.Text ="Node is found";
                lingkaran[root.idLingkaran].brush = new SolidBrush(Color.Yellow);
                delay();

                // node with only one child or no child  
                if ((root.left == null) || (root.right == null))
                {
                    Node temp = null;
                    if (temp == root.left)
                        temp = root.right;
                    else
                        temp = root.left;

                    // No child case  
                    if (temp == null)
                    {
                        temp = root;
                        root = null;
                    }
                    else // One child case  
                        root = temp; // Copy the contents of  
                                     // the non-empty child  
                }
                else
                {

                    // node with two children: Get the inorder  
                    // successor (smallest in the right subtree)  
                    Node temp = minValueNode(root.right);

                    // Copy the inorder successor's data to this node  
                    root.key = temp.key;

                    // Delete the inorder successor  
                    root.right = deleteNode(root.right, temp.key);
                }

            }

            // If the tree had only one node then return  
            if (root == null)
                return root;

            // STEP 2: UPDATE HEIGHT OF THE CURRENT NODE  
            root.height = max(height(root.left), height(root.right)) + 1;

            // STEP 3: GET THE BALANCE FACTOR OF THIS NODE (to check whether  
            // this node became unbalanced)  
            int balance = getBalance(root);

            // If this node becomes unbalanced, then there are 4 cases  
            // Left Left Case  
            if (balance > 1 && getBalance(root.left) >= 0)
            {
                form.Status.Text = "Right Rotate on " + root.key;
                delay();
                return rightRotate(root);
            }
            // Left Right Case  
            if (balance > 1 && getBalance(root.left) < 0)
            {

                form.Status.Text = "Double Right Rotate on " + root.key;
                delay();

                root.left = leftRotate(root.left);
                return rightRotate(root);
            }

            // Right Right Case  
            if (balance < -1 && getBalance(root.right) <= 0)
            {
                form.Status.Text = "Left Rotate on " + root.key;
                delay();
                return leftRotate(root);
            }

            // Right Left Case  
            if (balance < -1 && getBalance(root.right) > 0)
            {
                form.Status.Text = "Double Rotate on " + root.key;
                delay();

                root.right = rightRotate(root.right);
                return leftRotate(root);
            }

            return root;
        }

        // A utility function to print preorder traversal of  
        // the tree. The function also prints height of every  
        // node  

        public void preOrder(Node node)
        {
            if (node != null)
            {
                Console.WriteLine(node.key + " ");
                preOrder(node.left);
                preOrder(node.right);
            }
        }
        public void find(int value)
        {
            if (root == null) return;
            Node current = root;
            while (current!=null)
            {
                if (value < current.key)
                {
                    form.Status.Text = value + " is lesser than " + current.key;
                    lingkaran[current.idLingkaran].brush = new SolidBrush(Color.Yellow);
                    delay();
                    form.Status.Text = " Current go to left";
                    lingkaran[current.idLingkaran].brush = new SolidBrush(Color.Black);
                    delay();
                    current = current.left;
                }
                else if (value > current.key)
                {
                    form.Status.Text = value + " is greater than " + current.key;
                    lingkaran[current.idLingkaran].brush = new SolidBrush(Color.Yellow);
                    delay();
                    form.Status.Text = " Current go to right";
                    lingkaran[current.idLingkaran].brush = new SolidBrush(Color.Black);
                    current = current.right;
                    delay();
                }
                else
                {
                    form.Status.Text = value + " exist in tree" ;
                    lingkaran[current.idLingkaran].brush = new SolidBrush(Color.Yellow);
                    delay();
                    lingkaran[current.idLingkaran].brush = new SolidBrush(Color.Black);
                    form.mbox(value + " has been found in tree");
                    delay();
                    return;
                }

            }
            form.Status.Text = value + "Value not found";
            form.mbox(value + " doesn't exist in tree");
            delay();

        }
        public void inOrder(Node root)
        {
            if (root == null) return;
            inOrder(root.left);
            lingkaran[root.idLingkaran].brush = new SolidBrush(Color.Yellow);
            delay();
            inOrderResult += root.key + " ";
            inOrder(root.right);

        }

        public void inOrderHelper()
        {
            lingkaran.Clear();
            garis.Clear();
            ctrLingkaran = 1;
            ctrGaris = 1;
            inOrderClear(ref this.root, 1, 1,50);
        }
        public void inOrderClear(ref Node root, int kali, int bagi,int tinggi)
        {
            if (root == null) return;
            inOrderClear(ref root.left, (kali*2)-1,bagi*2 , tinggi + 100);
            
            lingkaran.Add("lingkaran" + ctrLingkaran, new Lingkaran(484 / bagi * kali + 1, tinggi, root.key.ToString()));
            root.idLingkaran = "lingkaran" + ctrLingkaran;
            ctrLingkaran++;
            form.pictureBox1.Invalidate();
            Application.DoEvents();
            if (root.left != null)
            {
                garis.Add("garis" + ctrGaris, new Garis((484 / bagi * kali + 1)+25, tinggi+30, (484 / (bagi * 2)) * ((kali * 2) - 1)+25, tinggi + 100));
                ctrGaris++;
                form.pictureBox1.Invalidate();
            }

            if (root.right != null)
            {
                garis.Add("garis" + ctrGaris, new Garis((484 / bagi * kali + 1)+25, tinggi+30, (484 / (bagi * 2)) * ((kali * 2) + 1)+25, tinggi + 100));
                ctrGaris++;
                form.pictureBox1.Invalidate();
            }
            inOrderClear(ref root.right, (kali * 2) + 1, bagi * 2, tinggi + 100);
        }



    }

   }

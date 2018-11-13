using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Btree {
    public partial class Form1 : Form {
        Btree bt;

        int width = 854, height = 480;

        public Form1() {
            InitializeComponent();
        }

        private void BtnOrdo_Click(object sender, EventArgs e) {
            SetOrdo();
        }
        private void TbOrdo_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
                SetOrdo();
        }
        private void SetOrdo() {
            int ordoSize = int.Parse(tbOrdo.Text);
            bt = new Btree(ordoSize);
            tbOrdo.Text = "";
            panel1.Refresh();

            tbInsert.Enabled = true;
            btnInsert.Enabled = true;
            tbSearch.Enabled = true;
            btnSearch.Enabled = true;
            tbDelete.Enabled = true;
            btnDelete.Enabled = true;

            btnOrdo.Text = "Reset Tree";
        }

        private void BtnInsert_Click(object sender, EventArgs e) {
            InsertKey();
        }
        private void TbInsert_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
                InsertKey();
        }
        private void InsertKey() {
            int insertValue = int.Parse(tbInsert.Text);
            bt.insert(ref bt.root, insertValue);
            Console.WriteLine("Insert " + tbInsert.Text);
            tbInsert.Text = "";
            panel1.Refresh();
        }

        private void BtnSearch_Click(object sender, EventArgs e) {
            SearchKey();
        }
        private void TbSearch_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
                SearchKey();
        }
        private void SearchKey() {
            //int searchValue = int.Parse(tbSearch.Text);
            // search method
            //tbDelete.Text = "";
            panel1.Refresh();
        }

        private void BtnDelete_Click(object sender, EventArgs e) {
            DeleteKey();
        }
        private void TbDelete_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
                DeleteKey();
        }
        private void DeleteKey() {
            int deleteValue = int.Parse(tbDelete.Text);
            bt.delete(ref bt.root, deleteValue);
            tbDelete.Text = "";
            panel1.Refresh();
        }

        private void Panel1_Paint(object sender, PaintEventArgs e) {            
            if (bt != null) {
                // prototype
                Pen p = new Pen(Color.Black);
                Brush b = new SolidBrush(Color.Black);
                Font f = new Font("Courier New", 10, FontStyle.Regular);

                const int RECTANGLE_SIZE = 30, RECTANGLE_DISTANCE=50;

                List<FakeBNode> fakeBNodes = bt.getFakeBNodes();

                int lastDepth=0,totalKeysCount=0,lastNodeIndex = -1;
                for (int i = 0; i < fakeBNodes.Count; i++) {
                    if(fakeBNodes[i].depth!=lastDepth){
                        lastDepth = fakeBNodes[i].depth;
                        lastNodeIndex = 0;
                        totalKeysCount = fakeBNodes[i].keysCount;
                    } else{
                        lastNodeIndex++;
                        totalKeysCount += fakeBNodes[i].keysCount;
                    }

                    for (int j = 0; j < fakeBNodes[i].keysCount; j++) {
                        int x = (totalKeysCount - fakeBNodes[i].keysCount + j) * RECTANGLE_SIZE + lastNodeIndex * RECTANGLE_SIZE;
                        int y = lastDepth * RECTANGLE_DISTANCE;
                        e.Graphics.DrawRectangle(p,x,y,RECTANGLE_SIZE,RECTANGLE_SIZE);
                        e.Graphics.DrawString(fakeBNodes[i].keys[j].ToString(), f, b, x, y);
                    }
                }

                Console.WriteLine("Inorder!");
                bt.inorder(ref bt.root);

                /* old
                List<KeyContainer> keysContainer = new List<KeyContainer>();
                List<NodeContainer> nodeContainer = new List<NodeContainer>();
                List<LinePosition> lines = new List<LinePosition>();

                bt.keysPosition = new List<KeyPosition>();
                bt.getKeysPosition(ref bt.root);

                int maxDepth = 0;
                for (int i = 0; i < bt.keysPosition.Count; i++) { // get max depth
                    if (bt.keysPosition[i].depth > maxDepth)
                        maxDepth = bt.keysPosition[i].depth;
                }

                int[] lastXPosition = new int[maxDepth + 1];
                int[] lastChildIndex = new int[maxDepth + 1];
                for (int i = 0; i < maxDepth; i++) {
                    lastXPosition[i] = 0;
                    lastChildIndex[i] = -2;
                }

                // set rectangle keys
                for (int i = 0; i < bt.keysPosition.Count; i++) {
                    int y = (bt.keysPosition[i].depth * 50) + 10;
                    int x = lastXPosition[bt.keysPosition[i].depth];

                    // basic depth code
                    //int startingX = width / (int) Math.Pow(2, (double) bt.keysPosition[i].depth+1);
                    //x += startingX;

                    lastChildIndex[bt.keysPosition[i].depth] = bt.keysPosition[i].childIndex;

                    // move last X position
                    lastXPosition[bt.keysPosition[i].depth] += 30;
                    if (i < bt.keysPosition.Count - 1 && lastChildIndex[bt.keysPosition[i + 1].depth] != -2 && bt.keysPosition[i + 1].childIndex != lastChildIndex[bt.keysPosition[i + 1].depth]) { // check if next key nodes on different node
                        lastXPosition[bt.keysPosition[i + 1].depth] += 30;
                    }

                    // debug
                    Console.WriteLine("Key : " + bt.keysPosition[i].key);
                    Console.WriteLine("Depth : " + bt.keysPosition[i].depth);
                    Console.WriteLine("Y : " + y);
                    Console.WriteLine("X : " + x);
                    Console.WriteLine("Child index : " + bt.keysPosition[i].childIndex);

                    keysContainer.Add(new KeyContainer(x, y, x + 30, y + 30));
                }

                // draw
                Pen p = new Pen(Color.Black);
                Brush b = new SolidBrush(Color.Black);
                Font f = new Font("Courier New", 10, FontStyle.Regular);

                // draw keys & keys container
                for (int i = 0; i < bt.keysPosition.Count; i++) {
                    Rectangle keyContainer = new Rectangle(keysContainer[i].x1, keysContainer[i].y1, 30, 30);
                    e.Graphics.DrawRectangle(p, keyContainer);
                    e.Graphics.DrawString(bt.keysPosition[i].key.ToString(), f, b, keysContainer[i].x1, keysContainer[i].y1);
                }

                // prepare line
                //bt.nodesContainer = new List<NodeContainer>();
                //bt.getNodesContainer(ref bt.root);
                keysContainer.Sort(new CompareKeyContainer()); // sort Y,X
                int startX = -1, endX = -1, currentDepth = -1, keysCount = 1;
                for (int i = 0; i < keysContainer.Count; i++) {
                    Rectangle currentKeysContainer = new Rectangle(keysContainer[i].x1, keysContainer[i].y1, 30, 30);
                    currentKeysContainer.Width += 1;
                    Rectangle nextKeysContainer = new Rectangle(0, 0, 0, 0);
                    if (i < keysContainer.Count - 1) {
                        nextKeysContainer = new Rectangle(keysContainer[i + 1].x1, keysContainer[i + 1].y1, 30, 30);
                    }

                    if (i < keysContainer.Count - 1 && currentKeysContainer.IntersectsWith(nextKeysContainer)) {
                        if (startX == -1)
                            startX = keysContainer[i].x1;
                        currentDepth = (keysContainer[i].y1 - 10) / 50;
                        endX = keysContainer[i + 1].x2;
                        keysCount++;
                        Console.WriteLine("Intersect");
                    } else if (i < keysContainer.Count - 1 && !currentKeysContainer.IntersectsWith(nextKeysContainer) && startX != -1) {
                        nodeContainer.Add(new NodeContainer(startX, endX, currentDepth, keysCount));
                        startX = -1;
                        endX = -1;
                        currentDepth = -1;
                        keysCount = 1;
                    } else if (i < keysContainer.Count - 1 && !currentKeysContainer.IntersectsWith(nextKeysContainer) && startX == -1) {
                        nodeContainer.Add(new NodeContainer(keysContainer[i].x1, keysContainer[i].x2, (keysContainer[i].y1 - 10) / 50, 1));
                        startX = -1;
                        endX = -1;
                        currentDepth = -1;
                        keysCount = 1;
                    } else if (i == keysContainer.Count - 1 && startX == -1) {
                        nodeContainer.Add(new NodeContainer(keysContainer[i].x1, keysContainer[i].x2, (keysContainer[i].y1 - 10) / 50, 1));
                        startX = -1;
                        endX = -1;
                        currentDepth = -1;
                        keysCount = 1;
                    } else {
                        nodeContainer.Add(new NodeContainer(startX, endX, currentDepth, keysCount));
                        startX = -1;
                        endX = -1;
                        currentDepth = -1;
                        keysCount = 1;
                    }
                }

                // get keys count / depth
                int[] keysCountAtDepth = new int[maxDepth + 1];
                for (int i = 0; i < nodeContainer.Count; i++) {
                    keysCountAtDepth[nodeContainer[i].depth] += nodeContainer[i].keysCount;
                }

                // set line, buggy
                nodeContainer.Sort(new CompareNodeContainer());
                int lastDepth = 0, nodeIndexAtDepth = -1, totalKeysAtDepth = 0;
                for (int i = 0; i < nodeContainer.Count; i++) {
                    if (nodeContainer[i].depth < maxDepth) {
                        if (lastDepth == nodeContainer[i].depth) {
                            nodeIndexAtDepth++;
                            totalKeysAtDepth += nodeContainer[i].keysCount;
                        } else {
                            totalKeysAtDepth = 0;
                            nodeIndexAtDepth = 0;
                            lastDepth = nodeContainer[i].depth;
                        }

                        for (int j = 0; j <= nodeContainer[i].keysCount; j++) {
                            // int x1 = nodeContainer[i].x1 + j * 30 + nodeIndexAtDepth * 30;
                            int x1 = (totalKeysAtDepth - nodeContainer[i].keysCount + j) * 30 + nodeIndexAtDepth * 30;
                            int y1 = nodeContainer[i].depth * 50 + 10 + 30;
                            int x2 = 0;
                            int y2 = y1 + 20;


                            int ctr = 0;
                            for (int k = i + 1; k < nodeContainer.Count; k++) {
                                if (nodeContainer[i].depth + 1 == nodeContainer[k].depth) {
                                    if (ctr == totalKeysAtDepth - nodeContainer[i].keysCount + j) {
                                        x2 = nodeContainer[k].x1;
                                    }
                                    ctr++;
                                }
                            }
                            lines.Add(new LinePosition(x1, y1, x2, y2));
                        }
                    }
                }

                // draw line
                for (int i = 0; i < lines.Count; i++) {
                    e.Graphics.DrawLine(p, lines[i].x1, lines[i].y1, lines[i].x2, lines[i].y2);
                }

                if (lines.Count > 0) {
                    Console.WriteLine("DONE DRAW!");
                }
                */
            }
        }
    }
}

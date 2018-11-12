using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Btree
{
    public partial class Form1 : Form
    {
        Btree bt;

        int width = 854, height = 480;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnOrdo_Click(object sender, EventArgs e) {
            setOrdo();
        }
        private void tbOrdo_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
                setOrdo();
        }
        private void setOrdo(){
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

        private void btnInsert_Click(object sender, EventArgs e) {
            insertKey();
        }
        private void tbInsert_KeyUp(object sender, KeyEventArgs e) {
            if(e.KeyCode == Keys.Enter) insertKey();
        }
        private void insertKey() {
            int insertValue = int.Parse(tbInsert.Text);
            bt.insert(ref bt.root, insertValue);
            tbInsert.Text = "";
            panel1.Refresh();
        }

        private void btnSearch_Click(object sender, EventArgs e) {

        }
        private void tbSearch_KeyUp(object sender, KeyEventArgs e) {

        }

        private void btnDelete_Click(object sender, EventArgs e) {
            deleteKey();
        }
        private void tbDelete_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
                deleteKey();
        }
        private void deleteKey(){
            int deleteValue = int.Parse(tbDelete.Text);
            bt.delete(ref bt.root, deleteValue);
            tbDelete.Text = "";
            panel1.Refresh();
        }

        private void panel1_Paint(object sender, PaintEventArgs e) {
            if(bt!=null){
                List<KeyContainer> keysContainer = new List<KeyContainer>();
                List<NodeContainer> nodeContainer = new List<NodeContainer>();
                // List<String> keys = new List<String>();
                List<linePosition> lines = new List<linePosition>();

                bt.keysPosition = new List<KeyPosition>();
                bt.getKeysPosition(ref bt.root);

                int maxDepth = 0;
                for (int i = 0; i < bt.keysPosition.Count; i++) { // get max depth
                    if (bt.keysPosition[i].depth > maxDepth)
                        maxDepth = bt.keysPosition[i].depth;
                }

                int[] lastXPosition = new int[maxDepth+1];
                int[] lastChildIndex = new int[maxDepth + 1];
                for (int i = 0; i < maxDepth; i++) {
                    lastXPosition[i] = 0;
                    lastChildIndex[i] = -2;
                }

                // set rectangle keys
                for (int i = 0; i < bt.keysPosition.Count; i++) {
                    int y = (bt.keysPosition[i].depth * 50) + 10;
                    int x = lastXPosition[bt.keysPosition[i].depth];
                    
                    lastChildIndex[bt.keysPosition[i].depth] = bt.keysPosition[i].childIndex;

                    // move last X position
                    lastXPosition[bt.keysPosition[i].depth] += 30;
                    if (i<bt.keysPosition.Count-1 && lastChildIndex[bt.keysPosition[i + 1].depth] !=-2 && bt.keysPosition[i+1].childIndex != lastChildIndex[bt.keysPosition[i+1].depth]) { // check if next key nodes on different node
                        lastXPosition[bt.keysPosition[i+1].depth] += 30;
                    }

                    // debug
                    Console.WriteLine("Key : " + bt.keysPosition[i].key);
                    Console.WriteLine("Depth : " + bt.keysPosition[i].depth);
                    Console.WriteLine("Y : " + y);
                    Console.WriteLine("X : " + x);
                    Console.WriteLine("Child index : " + bt.keysPosition[i].childIndex);

                    keysContainer.Add(new KeyContainer(x, y, x+30, y+30));
                }

                // draw
                Pen p = new Pen(Color.Black);
                Brush b = new SolidBrush(Color.Black);
                Font f = new Font("Courier New", 10, FontStyle.Regular);

                // draw keys & keys container
                for (int i = 0; i < bt.keysPosition.Count; i++) {
                    Rectangle keyContainer = new Rectangle(keysContainer[i].x1, keysContainer[i].y1, 30,30);
                    e.Graphics.DrawRectangle(p, keyContainer);
                    e.Graphics.DrawString(bt.keysPosition[i].key.ToString(), f, b, keysContainer[i].x1, keysContainer[i].y1);
                }

                // prepare line
                keysContainer.Sort(new CompareKeyContainer()); // sort Y,X
                bool changeNode = false;
                int startX=-1,endX=-1,currentDepth=-1,keysCount=1;
                for (int i = 0; i < keysContainer.Count; i++) {
                    Rectangle currentKeysContainer = new Rectangle(keysContainer[i].x1, keysContainer[i].y1, 30,30);
                    Rectangle nextKeysContainer = new Rectangle(0,0,0,0);
                    if (i < keysContainer.Count - 1) {
                        nextKeysContainer = new Rectangle(keysContainer[i + 1].x1, keysContainer[i + 1].y1, 30, 30);
                    }
                    currentKeysContainer.Width += 1;
                    if (i < keysContainer.Count-1 && currentKeysContainer.IntersectsWith(nextKeysContainer)) {
                        if (startX == -1) startX = keysContainer[i].x1;
                        currentDepth = (keysContainer[i].y1 - 10) / 50;
                        Console.WriteLine("Intersect");
                        endX = keysContainer[i + 1].x1 + 30;
                        keysCount++;
                    } else {
                        if(i==keysContainer.Count-1 || startX == -1) {
                            nodeContainer.Add(new NodeContainer(keysContainer[i].x1,keysContainer[i].x2,(keysContainer[i].y1-10)/50,1));
                        } else {
                            nodeContainer.Add(new NodeContainer(startX, endX, currentDepth, keysCount));
                        }
                    }
                }

                // get keys count / depth
                int[] keysCountAtDepth = new int[maxDepth + 1];
                for (int i = 0; i < nodeContainer.Count; i++) {
                    keysCountAtDepth[nodeContainer[i].depth] += nodeContainer[i].keysCount;
                }

                // set line
                nodeContainer.Sort(new CompareNodeContainer());
                int lastDepth=0,nodeIndexAtDepth = -1,totalKeysAtDepth=0;
                for (int i = 0; i < nodeContainer.Count; i++) {
                    if (nodeContainer[i].depth < maxDepth) {
                        if (lastDepth == nodeContainer[i].depth) {
                            nodeIndexAtDepth++;
                            totalKeysAtDepth += nodeContainer[i].keysCount;
                        } else {
                            totalKeysAtDepth = 0;
                            nodeIndexAtDepth = -1;
                            lastDepth = nodeContainer[i].depth;
                        }
                        
                        for (int j = 0; j <= nodeContainer[i].keysCount; j++) {
                            int x1 = nodeContainer[i].x1 + j * 30 + nodeIndexAtDepth * 30;
                            int y1 = nodeContainer[i].depth * 50 + 10 + 30;
                            int x2 = 0;
                            int y2 = y1 + 20;


                            int ctr = 0;
                            for (int k = i + 1; k < nodeContainer.Count; k++) {
                                if (nodeContainer[i].depth + 1 == nodeContainer[k].depth) {
                                    if (ctr == totalKeysAtDepth-nodeContainer[i].keysCount+j) {
                                        x2 = nodeContainer[k].x1;
                                    }
                                    ctr++;
                                }
                            }
                            lines.Add(new linePosition(x1, y1, x2, y2));
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
            }
        }
    }
}

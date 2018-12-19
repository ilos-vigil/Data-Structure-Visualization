using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Btree {
    public partial class Form1 : Form {
        Btree bt;
        int ordoSize, insertValue, deleteValue, searchValue;
        const int RECTANGLE_SIZE = 30, RECTANGLE_Y_DISTANCE = 20;// RECTANGLE_SIZE also used as RECTANGLE_X_DISTANCE

        // to help on search process
        bool onSearch;
        List<int> searchTraverseIndex;
        int[] oldEndPosition;
        int[] newStartPosition;

        public Form1() {
            InitializeComponent();
            onSearch = false;
        }
        private void Form1_Resize(object sender, EventArgs e) {
            panel1.Width = this.Width - 10;
            panel1.Height = this.Height - 50;
        }
        // SET
        private void BtnOrdo_Click(object sender, EventArgs e) {
            SetOrdo();
        }
        private void TbOrdo_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
                SetOrdo();
        }
        private void SetOrdo() {
            ordoSize = int.Parse(tbOrdo.Text);
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
        // INSERT
        private void BtnInsert_Click(object sender, EventArgs e) {
            InsertKey();
        }
        private void TbInsert_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
                InsertKey();
        }
        private void InsertKey() {
            insertValue = int.Parse(tbInsert.Text);
            bt.insert(ref bt.root, insertValue);
            Console.WriteLine("Insert " + tbInsert.Text);
            tbInsert.Text = "";
            panel1.Refresh();
        }
        // SEARCH
        private void BtnSearch_Click(object sender, EventArgs e) {
            SearchKey();
        }
        private void TbSearch_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
                SearchKey();
        }
        private void SearchKey() {
            searchValue = int.Parse(tbSearch.Text);
            Console.WriteLine("search value :" + searchValue);
            searchTraverseIndex = bt.getFindTraverseIndex(bt.root, searchValue); // bug return array != array on Form1
            if (searchTraverseIndex!=null) {
                onSearch = true;
                panel1.Refresh();
                onSearch = false;
            }else{
                panel1.Refresh();
            }
            tbSearch.Text = "";
        }
        // DELETE
        private void BtnDelete_Click(object sender, EventArgs e) {
            DeleteKey();
        }
        private void TbDelete_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
                DeleteKey();
        }
        private void DeleteKey() {
            deleteValue = int.Parse(tbDelete.Text);
            bt.delete(ref bt.root, deleteValue);
            tbDelete.Text = "";
            panel1.Refresh();
        }
        // DRAW
        private void Panel1_Paint(object sender, PaintEventArgs e) {
            if (bt != null) {
                // non-search property
                List<Rectangle> nodeRectangle = new List<Rectangle>();
                List<int> nodeKey = new List<int>();
                List<LinePosition> nodeLines = new List<LinePosition>();
                // search property
                List<Rectangle> nodeSearchRectangle = new List<Rectangle>();
                List<int> nodeSearchKey = new List<int>();
                List<LinePosition> nodeSearchLines = new List<LinePosition>();
                // draw tools for non-search object
                Pen p = new Pen(Color.Black);
                Brush b = new SolidBrush(Color.Black);
                Font f = new Font("Courier New", 10, FontStyle.Regular);
                // draw tools for non-search object
                Pen ps = new Pen(Color.Red);
                Brush bs = new SolidBrush(Color.Red);
                Font fs = new Font("Courier New", 10, FontStyle.Bold);
                // NODE position
                List<FakeBNode> fakeBNodes = bt.getFakeBNodes();
                // used to know last depth on setting key, node, line position
                int lastDepth = 0;

                // reset position when not on search
                if (!onSearch){
                    oldEndPosition = new int[bt.maxDepth + 1];
                    newStartPosition = new int[bt.maxDepth + 1];
                }

                // step 1 and step 2
                int[] keysCount = new int[bt.maxDepth + 1], nodeCount = new int[bt.maxDepth + 1], childCount = new int[bt.maxDepth + 1];
                for (int i = 0; i < fakeBNodes.Count; i++) {
                    // step 1. determine key position
                    if (fakeBNodes[i].depth != lastDepth) {
                        lastDepth = fakeBNodes[i].depth;
                        nodeCount[lastDepth] = 0;
                        childCount[lastDepth] = fakeBNodes[i].childCount;
                        keysCount[lastDepth] = fakeBNodes[i].keysCount;
                    } else {
                        if (i != 0)
                            nodeCount[lastDepth]++;
                        childCount[lastDepth] += fakeBNodes[i].childCount;
                        keysCount[lastDepth] += fakeBNodes[i].keysCount;
                    }
                    
                    // check if key on search path
                    bool isSearchPath = true;
                    if (onSearch && fakeBNodes[i].traverseIndex.Count() <= searchTraverseIndex.Count()) {
                        for (int m = 0; m < fakeBNodes[i].traverseIndex.Count(); m++) {
                            if (fakeBNodes[i].traverseIndex[m] != searchTraverseIndex[m] && fakeBNodes[i].depth!=0) {
                                isSearchPath = false;
                                break;
                            }
                        }
                    }else{
                        isSearchPath = false;
                    }

                    for (int a = 0; a < fakeBNodes[i].keysCount; a++) {
                        // step 2. set key and it's rectangle position
                        int x = (keysCount[lastDepth] - fakeBNodes[i].keysCount + a) * RECTANGLE_SIZE + nodeCount[lastDepth] * RECTANGLE_SIZE;
                        int y = lastDepth * (RECTANGLE_SIZE + RECTANGLE_Y_DISTANCE);
                        if (onSearch && isSearchPath && fakeBNodes[i].depth == 0 && (a == searchTraverseIndex[0] || a + 1 == searchTraverseIndex[0])) { // root
                            nodeSearchRectangle.Add(new Rectangle(x, y, RECTANGLE_SIZE, RECTANGLE_SIZE));
                            nodeSearchKey.Add(fakeBNodes[i].keys[a]);
                        } else if (onSearch && isSearchPath && fakeBNodes[i].depth < searchTraverseIndex.Count && (a == searchTraverseIndex[fakeBNodes[i].depth] || a + 1 == searchTraverseIndex[fakeBNodes[i].depth])) { // traverse path
                            nodeSearchRectangle.Add(new Rectangle(x, y, RECTANGLE_SIZE, RECTANGLE_SIZE));
                            nodeSearchKey.Add(fakeBNodes[i].keys[a]);
                        } else if (onSearch && isSearchPath && fakeBNodes[i].keys[a]==searchValue) { // selected key
                            nodeSearchRectangle.Add(new Rectangle(x, y, RECTANGLE_SIZE, RECTANGLE_SIZE));
                            nodeSearchKey.Add(fakeBNodes[i].keys[a]);
                        } else {
                            nodeRectangle.Add(new Rectangle(x, y, RECTANGLE_SIZE, RECTANGLE_SIZE));
                            nodeKey.Add(fakeBNodes[i].keys[a]);
                        }
                    }
                }

                // step 3. get line position based on key/node position
                int currentNodeIndex = -1, currentChildIndex = 0, currentChildCount = 0;
                int childNodeIndex = 0, childKeyCount = 0;
                lastDepth = 0;
                for (int i = 0; i < fakeBNodes.Count; i++) {
                    if (fakeBNodes[i].depth != lastDepth) {
                        currentNodeIndex = 0;
                        currentChildIndex = 0;
                        currentChildCount = fakeBNodes[i].childCount;
                        lastDepth = fakeBNodes[i].depth;
                    } else {
                        currentNodeIndex++;
                        currentChildCount += fakeBNodes[i].childCount;
                    }

                    // search visualization
                    bool currentIsSearchPath = true;
                    if (onSearch && fakeBNodes[i].traverseIndex.Count() <= searchTraverseIndex.Count()) {
                        for (int m = 0; m < fakeBNodes[i].traverseIndex.Count(); m++) {
                            if (fakeBNodes[i].traverseIndex[m] != searchTraverseIndex[m] && fakeBNodes[i].depth != 0) {
                                currentIsSearchPath = false;
                                break;
                            }
                        }
                    } else {
                        currentIsSearchPath = false;
                    }

                    if (fakeBNodes[i].depth < bt.maxDepth) {
                        for (int x = 0; x < fakeBNodes[i].childCount; x++) {
                            childNodeIndex = 0;
                            childKeyCount = 0;
                            for (int j = i + 1; j < fakeBNodes.Count; j++) {
                                if (fakeBNodes[i].depth + 1 == fakeBNodes[j].depth) {
                                    childKeyCount += fakeBNodes[j].keysCount;
                                    if (childNodeIndex == currentChildIndex) {
                                        int x1 = (currentChildCount - fakeBNodes[i].childCount + x) * RECTANGLE_SIZE;
                                        int y1 = lastDepth * (RECTANGLE_SIZE + RECTANGLE_Y_DISTANCE) + RECTANGLE_SIZE;
                                        int x2 = childNodeIndex * RECTANGLE_SIZE + (childKeyCount - fakeBNodes[j].keysCount) * RECTANGLE_SIZE;
                                        int y2 = y1 + RECTANGLE_Y_DISTANCE;

                                        // search visualization
                                        bool nextIsSearchPath = true;
                                        if (onSearch && currentIsSearchPath && fakeBNodes[j].traverseIndex.Count() <= searchTraverseIndex.Count()) {
                                            for (int m = 0; m < fakeBNodes[j].traverseIndex.Count(); m++) {
                                                if (fakeBNodes[j].traverseIndex[m] != searchTraverseIndex[m] && fakeBNodes[j].depth != 0) {
                                                    nextIsSearchPath = false;
                                                    break;
                                                }
                                            }
                                        } else {
                                            nextIsSearchPath = false;
                                        }

                                        if(onSearch && currentIsSearchPath && nextIsSearchPath){
                                            nodeSearchLines.Add(new LinePosition(x1, y1, x2, y2));
                                        }else {
                                            nodeLines.Add(new LinePosition(x1, y1, x2, y2));
                                        }
                                    }
                                    childNodeIndex++;
                                }
                            }
                            currentChildIndex++;
                        }
                    }
                }

                // step 4. move everything to center
                // 4a. get most right coordinate of each node depth
                int currentDepth = 0, lastY = 0, lastX = 0;
                if (!onSearch){
                    for (int i = 0; i < nodeRectangle.Count(); i++) {
                        // change depth
                        if (i == 0) {
                            lastY = nodeRectangle[i].Top;
                            currentDepth = nodeRectangle[i].Top / (RECTANGLE_SIZE + RECTANGLE_Y_DISTANCE);
                        } else if (currentDepth != nodeRectangle[i].Top / (RECTANGLE_SIZE + RECTANGLE_Y_DISTANCE)) {
                            // add to startX,endX
                            oldEndPosition[currentDepth] = lastX;
                            // reset
                            currentDepth = nodeRectangle[i].Top / (RECTANGLE_SIZE + RECTANGLE_Y_DISTANCE);
                        } else if (i == nodeRectangle.Count() - 1) {
                            oldEndPosition[currentDepth] = nodeRectangle[i].Right;
                        }

                        // get
                        lastX = nodeRectangle[i].Right;
                    }
                }

                // 4b. set added X
                for (int i = 0; i < bt.maxDepth + 1; i++) {
                    newStartPosition[i] = (panel1.Width - oldEndPosition[i]) / 2;
                }

                // 4c. move key & rectangle to center
                currentDepth = 0;
                lastY = 0;
                for (int i = 0; i < nodeRectangle.Count(); i++) {
                    currentDepth = nodeRectangle[i].Top / (RECTANGLE_SIZE + RECTANGLE_Y_DISTANCE);
                    nodeRectangle[i] = new Rectangle(nodeRectangle[i].X + newStartPosition[currentDepth], nodeRectangle[i].Y, nodeRectangle[i].Width, nodeRectangle[i].Height); // = endX[currentDepth];
                }

                // 4d. move search rectangle + key to center
                currentDepth = 0;
                lastY = 0;
                for (int i = 0; i < nodeSearchRectangle.Count(); i++) {
                    if (i == 0) {
                        lastY = nodeSearchRectangle[i].Top;
                    } else if (nodeSearchRectangle[i].Top != lastY) {
                        currentDepth++;
                        lastY = nodeSearchRectangle[i].Top;
                    }
                    nodeSearchRectangle[i] = new Rectangle(nodeSearchRectangle[i].X + newStartPosition[currentDepth], nodeSearchRectangle[i].Y, nodeSearchRectangle[i].Width, nodeSearchRectangle[i].Height); // = endX[currentDepth];
                }

                // 4e. move line to center
                for (int i = 0; i < nodeLines.Count; i++) {
                    currentDepth = (nodeLines[i].y1 - RECTANGLE_SIZE) / (RECTANGLE_SIZE + RECTANGLE_Y_DISTANCE);
                    nodeLines[i] = new LinePosition(nodeLines[i].x1 + newStartPosition[currentDepth], nodeLines[i].y1, nodeLines[i].x2 + newStartPosition[currentDepth + 1], nodeLines[i].y2);
                }

                // 4f. move search line to center
                currentDepth = 0;
                lastY = 0;
                for (int i = 0; i < nodeSearchLines.Count; i++) {
                    if (i == 0) {
                        lastY = nodeSearchLines[i].y1;
                    } else if (nodeSearchLines[i].y1 != lastY) {
                        currentDepth++;
                        lastY = nodeSearchLines[i].y1;
                    }

                    nodeSearchLines[i] = new LinePosition(nodeSearchLines[i].x1 + newStartPosition[currentDepth], nodeSearchLines[i].y1, nodeSearchLines[i].x2 + newStartPosition[currentDepth + 1], nodeSearchLines[i].y2);
                }

                // step 5. draw rectangle + key
                for (int i = 0; i < nodeRectangle.Count; i++) {
                    e.Graphics.DrawRectangle(p, nodeRectangle[i]);
                    e.Graphics.DrawString(nodeKey[i].ToString(), f, b, nodeRectangle[i]);
                }
                for (int i = 0; i < nodeSearchRectangle.Count; i++) {
                    e.Graphics.DrawRectangle(ps, nodeSearchRectangle[i]);
                    e.Graphics.DrawString(nodeSearchKey[i].ToString(), f, bs, nodeSearchRectangle[i]);
                }

                // step 6. draw line
                for (int m = 0; m < nodeLines.Count; m++) {
                    e.Graphics.DrawLine(p, nodeLines[m].x1, nodeLines[m].y1, nodeLines[m].x2, nodeLines[m].y2);
                }
                for (int m = 0; m < nodeSearchLines.Count(); m++) {
                    e.Graphics.DrawLine(ps, nodeSearchLines[m].x1, nodeSearchLines[m].y1, nodeSearchLines[m].x2, nodeSearchLines[m].y2);
                }
            }
        }
    }
}

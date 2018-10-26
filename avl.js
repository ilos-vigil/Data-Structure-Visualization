/**
 * @compiler Bridge.NET 17.4.0
 */
Bridge.assembly("Demo", function ($asm, globals) {
    "use strict";

    Bridge.define("AVL_Tree.AVL_Tree_2", {
        fields: {
            root: null
        },
        ctors: {
            ctor: function () {
                this.$initialize();
                this.root = null;
            }
        },
        methods: {
            insert: function (root, key, kali, bagi, tinggi) {
                var newNode = new AVL_Tree.Node(key);
                if (root.v == null) {
                    root.v = newNode;
                    ctr++;
                    createCircle(34, 484, ctr);
                    drawString(484, 34, key);
                } else {
                    var current = root.v;
                    while (true) {
                        if (key < current.value) {
                            if (current.left == null) {
                                current.left = newNode;
                                newNode.parent = current;
                                ctr++;
                                createLine(484 / bagi * kali + 1, tinggi, (484 / (bagi * 2)) * ((kali * 2) - 1), tinggi + 50, ctrLines);
                                drawString((484 / (bagi * 2)) * ((kali * 2) - 1), tinggi + 50, key + "");
                                createCircle(tinggi + 50, (484 / (bagi * 2)) * ((kali * 2) - 1), ctr);
                                this.retraceAfterInsertion(root, newNode);
                                break;
                            }else{
                                kali = (kali * 2) - 1;
                                bagi = bagi * 2;
                                tinggi = tinggi + 50;
                            }
                            current = current.left;
                        } else {
                            if (current.right == null) {
                                current.right = newNode;
                                newNode.parent = current;
                                ctr++;
                                createLine(484 / bagi * kali + 1, tinggi, (484 / (bagi * 2)) * ((kali * 2) + 1), tinggi + 50, ctrLines);
                                drawString((484 / (bagi * 2)) * ((kali * 2) + 1), tinggi + 50, key + "");
                                createCircle(tinggi + 50, (484 / (bagi * 2)) * ((kali * 2) + 1), ctr);
                                this.retraceAfterInsertion(root, newNode);
                                break;
                            }else{
                                kali = (kali * 2) + 1;
                                bagi = bagi * 2;
                                tinggi = tinggi + 50;
                                ;
                            }
                            current = current.right;
                        }
                    }
                }
            },
            retraceAfterInsertion: function (root, node) {
                node = { v: node };
                while (node.v.parent != null) {
                    node.v = node.v.parent;
                    if (this.updateBalanceFactor(root, node)) {
                        break;
                    }
                }
            },
            delete: function (root, key) {
                var current = this.find(root.v, key);
                if (current != null) {
                    if (current.left == null || current.right == null) {
                        this.retraceAfterDeletion(root, current);
                        this.replaceNodeInParent(root, current, current.left == null ? current.right : current.left);
                    } else {
                        var predecessor = current.left;
                        while (predecessor.right != null) {
                            predecessor = predecessor.right;
                        }
                        current.value = predecessor.value;
                        this.retraceAfterDeletion(root, predecessor);
                        this.replaceNodeInParent(root, predecessor, predecessor.left);
                    }
                }
            },
            find: function (root, key) {
                if (root == null) {
                    return null;
                } else {
                    if (root.value === key) {
                        return root;
                    } else {
                        if (root.value < key) {
                            return this.find(root.right, key);
                        } else {
                            if (root.value > key) {
                                return this.find(root.left, key);
                            }
                        }
                    }
                }
                return null;
            },
            retraceAfterDeletion: function (root, node) {
                while (node.parent != null) {
                    var parent = { v: node.parent };
                    var sibling;
                    var oldBalanceFactor = parent.v.balanceFactor;
                    if (Bridge.referenceEquals(node, parent.v.left)) {
                        parent.v.balanceFactor = Bridge.Int.sxb((parent.v.balanceFactor - 1) & 255);
                        sibling = parent.v.right;
                    } else {
                        parent.v.balanceFactor = Bridge.Int.sxb((parent.v.balanceFactor + 1) & 255);
                        sibling = parent.v.left;
                    }
                    var siblingBalanceFactor = Bridge.Int.sxb((sibling == null ? 0 : sibling.balanceFactor) & 255);
                    var rotated = this.updateBalanceFactor(root, parent);
                    if (rotated && siblingBalanceFactor === 0) {
                        break;
                    }
                    if (oldBalanceFactor === 0) {
                        break;
                    }
                    node = parent.v;
                }
            },
            replaceNodeInParent: function (root, node, child) {
                if (node.parent == null) {
                    root.v = child;
                } else {
                    if (Bridge.referenceEquals(node.parent.left, node)) {
                        node.parent.left = child;
                    } else {
                        node.parent.right = child;
                    }
                }

                if (child != null) {
                    child.parent = node.parent;
                }
            },
            updateBalanceFactor: function (root, node) {
                switch (node.v.balanceFactor) {
                    case 2:
                        if (node.v.left.balanceFactor === -1) {
                            node.v = this.doubleRightRotate(root, node.v);
                        } else {
                            node.v = this.singleRightRotate(root, node.v);
                        }
                        return true;
                    case -2:
                        if (node.v.right.balanceFactor === 1) {
                            node.v = this.doubleLeftRotate(root, node.v);
                        } else {
                            node.v = this.singleLeftRotate(root, node.v);
                        }
                        return true;
                }
                return false;
            },
            max: function (a, b) {
                return (a > b) ? a : b;
            },
            min: function (a, b) {
                return (a < b) ? a : b;
            },
            singleLeftRotate: function (root, p) {
                var q = p.right;
                p.right = q.left;
                if (q.left != null) {
                    q.left.parent = p;
                }
                q.left = p;
                this.replaceNodeInParent(root, p, q);
                p.parent = q;
                p.balanceFactor = Bridge.Int.sxb((p.balanceFactor + (Bridge.Int.sxb((((1 - this.min(q.balanceFactor, 0)) | 0)) & 255))) & 255);
                q.balanceFactor = Bridge.Int.sxb((q.balanceFactor + (Bridge.Int.sxb((((1 + this.max(p.balanceFactor, 0)) | 0)) & 255))) & 255);
                return q;
            },
            singleRightRotate: function (root, q) {
                var p = q.left;
                q.left = p.right;
                if (p.right != null) {
                    p.right.parent = q;
                }
                p.right = q;
                this.replaceNodeInParent(root, q, p);
                q.parent = p;
                q.balanceFactor = Bridge.Int.sxb((q.balanceFactor - (Bridge.Int.sxb((((1 + this.max(p.balanceFactor, 0)) | 0)) & 255))) & 255);
                p.balanceFactor = Bridge.Int.sxb((p.balanceFactor - (Bridge.Int.sxb((((1 - this.min(q.balanceFactor, 0)) | 0)) & 255))) & 255);
                return p;
            },
            doubleLeftRotate: function (root, x) {
                this.singleRightRotate(root, x.right);
                return this.singleLeftRotate(root, x);
            },
            doubleRightRotate: function (root, x) {
                this.singleLeftRotate(root, x.left);
                return this.singleRightRotate(root, x);
            },
            printInorder: function (node) {
                if (node == null) {
                    return;
                }
                this.printInorder(node.left);
                System.Console.WriteLine(node.value + " ");
                this.printInorder(node.right);
            }
        }
    });

    Bridge.define("AVL_Tree.Node", {
        fields: {
            value: 0,
            left: null,
            right: null,
            balanceFactor: 0,
            parent: null
        },
        ctors: {
            ctor: function (val) {
                this.$initialize();
                this.value = val;
                this.left = null;
                this.right = null;
                this.balanceFactor = 0;
                this.parent = null;
            }
        }
    });

    /*
    Bridge.define("AVL_Tree.Program", {
        main: function Main(args) {
            var trace2 = new AVL_Tree.AVL_Tree_2();
            trace2.insert(Bridge.ref(trace2, "root"), 44);
            trace2.insert(Bridge.ref(trace2, "root"), 17);
            trace2.insert(Bridge.ref(trace2, "root"), 50);
            trace2.insert(Bridge.ref(trace2, "root"), 48);
            trace2.insert(Bridge.ref(trace2, "root"), 78);
            trace2.insert(Bridge.ref(trace2, "root"), 62);
            trace2.insert(Bridge.ref(trace2, "root"), 88);
            trace2.printInorder(trace2.root);

            var trace = new AVL_Tree.AVL_Tree_2();
            System.Console.WriteLine("Link Parent sebelum Delete");
            trace.insert(Bridge.ref(trace, "root"), 44);
            trace.insert(Bridge.ref(trace, "root"), 17);
            trace.insert(Bridge.ref(trace, "root"), 32);
            trace.insert(Bridge.ref(trace, "root"), 78);
            trace.insert(Bridge.ref(trace, "root"), 50);
            trace.insert(Bridge.ref(trace, "root"), 88);
            trace.insert(Bridge.ref(trace, "root"), 48);
            trace.insert(Bridge.ref(trace, "root"), 62);
            trace.printInorder(trace.root);
            trace.delete(Bridge.ref(trace, "root"), 32);
            System.Console.WriteLine("========");
            System.Console.WriteLine("Link Parent Setelah Delete");
            trace.printInorder(trace.root);
        }
    });
    */
});

var svgNS = "http://www.w3.org/2000/svg";
var ctr = 0;
var ctrLines = 0;
function createCircle(y, x, ctr) {
    var myCircle = document.createElementNS(svgNS, "circle"); //to create a circle. for rectangle use "rectangle"
    myCircle.setAttributeNS(null, "id", "node" + ctr);
    myCircle.setAttributeNS(null, "cx", x);
    myCircle.setAttributeNS(null, "cy", y);
    myCircle.setAttributeNS(null, "r", 18);
    myCircle.setAttributeNS(null, "fill", "black");
    myCircle.setAttributeNS(null, "stroke", "none");
    document.getElementById("nodes").appendChild(myCircle);
}
function createLine(x1, y1, x2, y2, ctrLines) {
    var myLine = document.createElementNS(svgNS, "line");
    myLine.setAttributeNS(null, 'id', "line" + ctrLines);
    myLine.setAttribute('x1', x1);
    myLine.setAttribute('x2', x2);
    myLine.setAttribute('y1', y1);
    myLine.setAttribute('y2', y2);
    myLine.setAttribute('stroke', 'rgb(0,0,0)');
    myLine.setAttribute('stroke-width', 2);
    document.getElementById("lines").appendChild(myLine);
}
function drawString(x, y, val) {
    var myString = document.createElementNS(svgNS, "text");
    myString.setAttributeNS(null, 'id', 'value' + ctr);
    myString.setAttribute('x', x);
    myString.setAttribute('y', y);
    myString.setAttribute('fill', 'white');
    myString.textContent = val;
    myString.innerHTML = val;
    document.getElementById("text").appendChild(myString);
}

var avltree = new AVL_Tree.AVL_Tree_2();
function insertNode() {
    avltree.insert(Bridge.ref(avltree, "root"), parseInt(document.getElementById("insertVal").value), 1, 1, 34);
}
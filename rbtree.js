/**
 * @compiler Bridge.NET 17.4.0
 */
Bridge.assembly("Demo", function ($asm, globals) {
    "use strict";

    Bridge.define("RedBlack_tree.node", {
        fields: {
            key: 0,
            left: null,
            right: null,
            parent: null,
            color: 0,
            sentinel: false
        },
        ctors: {
            $ctor1: function (key) {
                this.$initialize();
                this.key = key;
                this.left = null;
                this.right = null;
                this.parent = null;
                this.color = 0;
                this.sentinel = false;
            },
            ctor: function () {
                this.$initialize();
                this.left = null;
                this.right = null;
                this.parent = null;
                this.color = 2;
                this.sentinel = true;
            }
        }
    });

    Bridge.define("RedBlack_tree.Program", {
        main: function Main(args) {
            var tree = new RedBlack_tree.RBtree();
            tree.insert(Bridge.ref(tree, "root"), 10);
            tree.insert(Bridge.ref(tree, "root"), 5);
            tree.insert(Bridge.ref(tree, "root"), 18);
            tree.delete(Bridge.ref(tree, "root"), 5);
            tree.printorder(tree.root);
        }
    });

    Bridge.define("RedBlack_tree.RBtree", {
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
            color: function (n) {
                if (n == null) {
                    return 1;
                } else {
                    return n.color;
                }
            },
            singleLeftRotation: function (root, p) {
                var q = p.right;
                p.right = q.left;
                if (q.left != null) {
                    q.left.parent = p;
                }
                q.left = p;
                this.replaceNodeInParent(root, p, q);
                p.parent = q;
                this.swapColor(p, q);
                return q;
            },
            singleRightRotation: function (root, q) {
                var p = q.left;
                q.left = p.right;
                if (p.right != null) {
                    p.right.parent = q;
                }
                p.right = q;
                this.replaceNodeInParent(root, q, p);
                q.parent = p;
                this.swapColor(p, q);
                return q;
            },
            insert: function (root, key) {
                var newnode = new RedBlack_tree.node.$ctor1(key);
                if (root.v == null) {
                    root.v = newnode;
                    root.v.color = 1;
                } else {
                    var current = root.v;
                    while (true) {
                        if (key < current.key) {
                            if (current.left == null) {
                                current.left = newnode;
                                newnode.parent = current;
                                this.retraceAfterInsertion(root, newnode);
                                break;
                            }
                            current = current.left;
                        } else {
                            if (current.right == null) {
                                current.right = newnode;
                                newnode.parent = current;
                                this.retraceAfterInsertion(root, newnode);
                                break;
                            }
                            current = current.right;
                        }
                    }
                }
            },
            retraceAfterInsertion: function (root, current) {
                while (true) {
                    if (Bridge.referenceEquals(current, root.v)) {
                        current.color = 1;
                        break;
                    }
                    if (current.parent.color === 0) {
                        var p = current.parent;
                        var g = p.parent;
                        var u = Bridge.referenceEquals(p, g.left) ? g.right : g.left;
                        if (this.color(u) === 0) {
                            p.color = 1;
                            u.color = 1;
                            g.color = 0;
                            current = g;
                        } else {
                            this.rotate(root, current, p, g);
                            break;
                        }
                    } else {
                        break;
                    }
                }
            },
            rotate: function (root, r, p, g) {
                if (Bridge.referenceEquals(p, g.left) && (r == null || Bridge.referenceEquals(r, p.left))) {
                    this.singleRightRotation(root, g);
                    return r;
                } else if (Bridge.referenceEquals(p, g.left) && Bridge.referenceEquals(r, p.right)) {
                    this.singleLeftRotation(root, g.left);
                    this.singleRightRotation(root, g);
                    return p;
                } else if (Bridge.referenceEquals(p, g.right) && (Bridge.referenceEquals(r, p.right) || r == null)) {
                    this.singleLeftRotation(root, g);
                    return r;
                } else if (Bridge.referenceEquals(p, g.right) && Bridge.referenceEquals(r, p.left)) {
                    this.singleRightRotation(root, g.right);
                    this.singleLeftRotation(root, g);
                    return p;
                }
                return null;

            },
            swapColor: function (x, y) {
                if (x.color !== y.color) {
                    var temp = x.color;
                    x.color = y.color;
                    y.color = temp;
                }
            },
            replaceNodeInParent: function (root, node, child) {
                if (node.parent == null) {
                    root.v = child;
                } else if (Bridge.referenceEquals(node.parent.left, node)) {
                    node.parent.left = child;
                } else {
                    node.parent.right = child;
                }
                if (child != null) {
                    child.parent = node.parent;
                }
            },
            find: function (root, key) {
                var current = root;
                while (current != null && key !== current.key) {
                    if (key < current.key) {
                        current = current.left;
                    } else {
                        current = current.right;
                    }
                }
                return current;
            },
            delete: function (root, key) {
                var current = this.find(root.v, key);
                if (current != null) {
                    if (current.left == null || current.right == null) {
                        this.replaceNodeInParentAndBalancing(root, current, current.left == null ? current.right : current.left);
                    } else {
                        var predecessor = current.left;
                        while (predecessor.right != null) {
                            predecessor = predecessor.right;
                        }
                        current.key = predecessor.key;
                        this.replaceNodeInParentAndBalancing(root, predecessor, predecessor.left);
                    }
                }
            },
            replaceNodeInParentAndBalancing: function (root, node, child) {
                if (node.color === 0 || this.color(child) === 0) {
                    if (child != null) {
                        child.color = 1;
                    }
                } else if (node.color === 1 && this.color(child) === 1) {
                    if (child == null) {
                        child = new RedBlack_tree.node.ctor();
                    } else {
                        child.color = 2;
                    }
                }
                this.replaceNodeInParent(root, node, child);
                if (child != null) {
                    this.retraceAfterDeletion(root, child);
                }
            },
            retraceAfterDeletion: function (root, u) {
                while (u.color === 2) {
                    if (Bridge.referenceEquals(u, root.v)) {
                        this.decreaseColor(root, u);
                        break;
                    }
                    var p = u.parent;
                    var s;
                    if (Bridge.referenceEquals(u, p.left)) {
                        s = p.right;
                    } else {
                        s = p.left;
                    }
                    if (this.color(s) === 0) {
                        this.rotate(root, null, s, p);
                    } else {
                        var r = null;
                        if (Bridge.referenceEquals(s, p.left)) {
                            if (this.color(s.left) === 0) {
                                r = s.left;
                            } else {
                                if (this.color(s.right) === 0) {
                                    r = s.right;
                                }
                            }
                        } else {
                            if (this.color(s.right) === 0) {
                                r = s.right;
                            } else {
                                if (this.color(s.left) === 0) {
                                    r = s.left;
                                }
                            }
                        }
                        if (r != null) {
                            r = this.rotate(root, r, s, p);
                            this.increaseColor(root, r);
                            this.decreaseColor(root, u);
                            break;
                        } else {
                            this.decreaseColor(root, u);
                            this.decreaseColor(root, s);
                            this.increaseColor(root, p);
                            u = p;
                        }
                    }
                }
            },
            decreaseColor: function (root, node) {
                if (node.color === 2) {
                    if (node.sentinel) {
                        if (node.parent == null) {
                            root.v = null;
                        } else {
                            if (Bridge.referenceEquals(node.parent.left, node)) {
                                node.parent.left = null;
                            } else {
                                node.parent.right = null;
                            }
                        }
                    } else {
                        node.color = 1;
                    }
                } else if (node.color === 1) {
                    node.color = 0;
                }
            },
            increaseColor: function (root, node) {
                if (node.color === 0) {
                    node.color = 1;
                } else {
                    if (node.color === 1) {
                        node.color = 2;
                    }
                }
            },
            printorder: function (node) {
                if (node == null) {
                    return;
                }
                this.printorder(node.left);
                System.Console.WriteLine(node.key + " - " + node.color + "\n");
                this.printorder(node.right);
            }
        }
    });
});
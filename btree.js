/**
 * @compiler Bridge.NET 17.4.0
 */
Bridge.assembly("Demo", function ($asm, globals) {
    "use strict";

    Bridge.define("Btree.Bnode", {
        fields: {
            n: 0,
            size: 0,
            keys: null,
            children: null,
            parent: null
        },
        ctors: {
            ctor: function (m) {
                this.$initialize();
                this.size = (m - 1) | 0;
                this.n = 0;
                this.keys = System.Array.init(((this.size + 1) | 0), 0, System.Int32);
                this.children = System.Array.init(((this.size + 2) | 0), null, Btree.Bnode);
                this.children[System.Array.index(0, this.children)] = null;
                this.parent = null;
            }
        },
        methods: {
            insert: function (key) {
                if (this.n === 0) {
                    return this.insertFirstKey(key);
                }
                if (key > this.keys[System.Array.index(((this.n - 1) | 0), this.keys)]) {
                    return this.insertLast(key);
                }
                return this.insertOrder(key);
            },
            insertOrder: function (key) {
                var position = this.findPosition(key);
                for (var i = this.n; i > position; i = (i - 1) | 0) {
                    this.children[System.Array.index(((i + 1) | 0), this.children)] = this.children[System.Array.index(i, this.children)];
                    this.keys[System.Array.index(i, this.keys)] = this.keys[System.Array.index(((i - 1) | 0), this.keys)];
                }
                this.keys[System.Array.index(position, this.keys)] = key;
                this.children[System.Array.index(((position + 1) | 0), this.children)] = null;
                this.n = (this.n + 1) | 0;
                return position;
            },
            insertFirstKey: function (key) {
                this.keys[System.Array.index(0, this.keys)] = key;
                this.children[System.Array.index(1, this.children)] = null;
                this.n = 1;
                return 0;
            },
            insertLast: function (key) {
                this.keys[System.Array.index(Bridge.identity(this.n, (this.n = (this.n + 1) | 0)), this.keys)] = key;
                this.children[System.Array.index(this.n, this.children)] = null;
                return ((this.n - 1) | 0);
            },
            findPosition: function (key) {
                var position = System.Array.binarySearch(this.keys, 0, this.n, key);
                if (position < 0) {
                    position = ~position;
                }
                return position;
            },
            delete: function (position) {
                for (var i = (position + 1) | 0; i < this.n; i = (i + 1) | 0) {
                    this.keys[System.Array.index(((i - 1) | 0), this.keys)] = this.keys[System.Array.index(i, this.keys)];
                    this.children[System.Array.index(i, this.children)] = this.children[System.Array.index(((i + 1) | 0), this.children)];
                }
                this.n = (this.n - 1) | 0;
                return this.n;
            },
            findChild: function (child) {
                for (var i = 0; i <= this.n; i = (i + 1) | 0) {
                    if (Bridge.referenceEquals(this.children[System.Array.index(i, this.children)], child)) {
                        return i;
                    }
                }
                return -1;
            }
        }
    });

    Bridge.define("Btree.Btree", {
        fields: {
            bt: null,
            m: 0
        },
        ctors: {
            ctor: function (m) {
                this.$initialize();
                this.bt = new Btree.Bnode(3);
                this.m = m;
            }
        },
        methods: {
            insert: function (root, key) {
                if (root.v == null) {
                    root.v = new Btree.Bnode(this.m);
                    root.v.insert(key);
                } else {
                    var current = root.v;
                    while (true) {
                        if (current.children[System.Array.index(0, current.children)] == null) {
                            var position = current.insert(key);
                            while (current.n > current.size) {
                                var result = this.split(root, current);
                                var newNode = Bridge.cast(result[System.Array.index(0, result)], Btree.Bnode);
                                key = Bridge.cast(result[System.Array.index(1, result)], System.Int32);
                                current = current.parent;
                                position = current.insert(key);
                                current.children[System.Array.index(((position + 1) | 0), current.children)] = newNode;
                            }
                            break;
                        }
                        current = current.children[System.Array.index(current.findPosition(key), current.children)];
                    }
                }
            },
            delete: function (root, key) {
                var $t, $t1, $t2;
                var current = this.find(root.v, key);
                if (current != null) {
                    if (($t = current.multiNode.children)[System.Array.index(current.position, $t)] != null) {
                        var predecessor = ($t1 = current.multiNode.children)[System.Array.index(current.position, $t1)];
                        while (predecessor.children[System.Array.index(predecessor.n, predecessor.children)] != null) {
                            predecessor = predecessor.children[System.Array.index(predecessor.n, predecessor.children)];
                        }
                        ($t2 = current.multiNode.keys)[System.Array.index(current.position, $t2)] = predecessor.keys[System.Array.index(((predecessor.n - 1) | 0), predecessor.keys)];
                        current = new Btree.Node(predecessor, ((predecessor.n - 1) | 0));
                    }
                    current.multiNode.delete(current.position);
                    var minKeys = (Bridge.Int.div(current.multiNode.size, 2)) | 0;
                    if (Bridge.referenceEquals(current.multiNode, root.v) && current.multiNode.n === 0) {
                        root.v = null;
                    }
                    while (root.v != null && !Bridge.referenceEquals(current.multiNode, root.v) && current.multiNode.n < minKeys) {
                        this.rebalanceAfterDeletion(root, Bridge.ref(current, "multiNode"), minKeys);
                    }
                }
            },
            inorder: function (root) {
                if (root.v != null) {
                    for (var i = 0; i < root.v.n; i = (i + 1) | 0) {
                        this.inorder(Bridge.ref(root.v.children, i));
                        System.Console.WriteLine("Keys : " + root.v.keys[System.Array.index(i, root.v.keys)] + " n : " + root.v.n + " parent value : " + (root.v.parent==null ? "null" : root.v.parent.keys[0]));
                    }
                    this.inorder(Bridge.ref(root.v.children, root.v.n));
                }
            },
            split: function (root, node) {
                var mid = (Bridge.Int.div(node.n, 2)) | 0;
                if (node.parent == null) {
                    root.v = new Btree.Bnode(this.m);
                    root.v.children[System.Array.index(0, root.v.children)] = node;
                    node.parent = root.v;
                }
                var newNode = new Btree.Bnode(this.m);
                newNode.parent = node.parent;
                var nk = 0;
                for (var k = (mid + 1) | 0; k < node.n; k = (k + 1) | 0, nk = (nk + 1) | 0) {
                    newNode.insert(node.keys[System.Array.index(k, node.keys)]);
                    this.moveParent(newNode, nk, node.children[System.Array.index(k, node.children)]);
                }
                this.moveParent(newNode, nk, node.children[System.Array.index(node.n, node.children)]);
                node.n = mid;
                return System.Array.init([newNode, node.keys[System.Array.index(mid, node.keys)]], System.Object);
            },
            moveParent: function (parent, position, child) {
                parent.children[System.Array.index(position, parent.children)] = child;
                if (child != null) {
                    child.parent = parent;
                }
            },
            find: function (root, key) {
                var current = root;
                while (current != null) {
                    var position = current.findPosition(key);
                    if (position < current.n && current.keys[System.Array.index(position, current.keys)] === key) {
                        return new Btree.Node(current, position);
                    } else {
                        current = current.children[System.Array.index(position, current.children)];
                    }
                }
                return null;
            },
            rebalanceAfterDeletion: function (root, current, minKeys) {
                var parent = current.v.parent;
                var i = parent.findChild(current.v);
                if (i > 0 && parent.children[System.Array.index(((i - 1) | 0), parent.children)].n > minKeys) {
                    this.rightRotate(parent, ((i - 1) | 0));
                    return true;
                }
                if (i > parent.n && parent.children[System.Array.index(((i + 1) | 0), parent.children)].n > minKeys) {
                    this.leftRotate(parent, i);
                    return true;
                }
                if (i > 0) {
                    current.v = this.merge(parent, ((i - 1) | 0));
                } else {
                    current.v = this.merge(parent, i);
                }
                if (parent.n === 0 && Bridge.referenceEquals(parent, root.v)) {
                    root.v = current.v;
                    current.v.parent = null;
                } else {
                    current.v = parent;
                }
                return false;
            },
            leftRotate: function (parent, position) {
                var left = parent.children[System.Array.index(position, parent.children)];
                var right = parent.children[System.Array.index(((position + 1) | 0), parent.children)];
                var newPosition = left.insert(parent.keys[System.Array.index(position, parent.keys)]);
                this.moveParent(left, ((newPosition + 1) | 0), right.children[System.Array.index(0, right.children)]);
                parent.keys[System.Array.index(position, parent.keys)] = right.keys[System.Array.index(0, right.keys)];
                right.children[System.Array.index(0, right.children)] = right.children[System.Array.index(1, right.children)];
                right.delete(0);
            },
            rightRotate: function (parent, position) {
                var left = parent.children[System.Array.index(position, parent.children)];
                var right = parent.children[System.Array.index(((position + 1) | 0), parent.children)];
                var newPosition = right.insert(parent.keys[System.Array.index(position, parent.keys)]);
                right.children[System.Array.index(1, right.children)] = right.children[System.Array.index(0, right.children)];
                this.moveParent(right, newPosition, left.children[System.Array.index(left.n, left.children)]);
                parent.keys[System.Array.index(position, parent.keys)] = left.keys[System.Array.index(((left.n - 1) | 0), left.keys)];
                left.n = (left.n - 1) | 0;
            },
            merge: function (parent, position) {
                var left = parent.children[System.Array.index(position, parent.children)];
                var right = parent.children[System.Array.index(((position + 1) | 0), parent.children)];
                var newPosition = left.insert(parent.keys[System.Array.index(position, parent.keys)]);
                this.moveParent(left, ((newPosition + 1) | 0), right.children[System.Array.index(0, right.children)]);
                for (var i = 0; i < right.n; i = (i + 1) | 0) {
                    newPosition = left.insert(right.keys[System.Array.index(i, right.keys)]);
                    this.moveParent(left, ((newPosition + 1) | 0), right.children[System.Array.index(((i + 1) | 0), right.children)]);
                }
                parent.delete(position);
                return left;
            }
        }
    });

    Bridge.define("Btree.Node", {
        fields: {
            multiNode: null,
            position: 0
        },
        ctors: {
            ctor: function (multiNode, position) {
                this.$initialize();
                this.multiNode = multiNode;
                this.position = position;
            }
        }
    });

    Bridge.define("Btree.Program", {
        main: function Main(args) {
            var bt = new Btree.Btree(3);
            bt.insert(Bridge.ref(bt, "bt"), 20);
            bt.insert(Bridge.ref(bt, "bt"), 10);
            bt.insert(Bridge.ref(bt, "bt"), 5);
            /*
            bt.insert(Bridge.ref(bt, "bt"), 7);
            bt.insert(Bridge.ref(bt, "bt"), 9);
            bt.insert(Bridge.ref(bt, "bt"), 6);
            */
            System.Console.WriteLine("Print!");
            bt.inorder(Bridge.ref(bt, "bt"));
            /*
            bt.delete(Bridge.ref(bt, "bt"), 7);
            System.Console.WriteLine("After delete 7!");
            bt.inorder(Bridge.ref(bt, "bt"));
            */
        }
    });
});

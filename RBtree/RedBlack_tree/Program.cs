using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBlack_tree
{
    class Program
    {
        static void Main(string[] args)
        {
            RBtree tree = new RBtree();
            tree.insert(ref tree.root, 10);
            tree.insert(ref tree.root, 5);
            tree.insert(ref tree.root, 18);
            tree.delete(ref tree.root, 5);
            tree.printorder(tree.root);
            Console.ReadKey();
        }
    }
}



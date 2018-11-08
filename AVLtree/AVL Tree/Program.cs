using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL_Tree
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Balance factor
            AVL_Tree trace2 = new AVL_Tree();
            trace2.insertion(ref trace2.root, 44);
            trace2.insertion(ref trace2.root, 17);
            trace2.insertion(ref trace2.root, 50);
            trace2.insertion(ref trace2.root, 48);
            trace2.insertion(ref trace2.root, 78);
            trace2.insertion(ref trace2.root, 62);
            trace2.insertion(ref trace2.root, 88);
            trace2.printInorder(trace2.root);

            AVL_Tree_2 trace = new AVL_Tree_2();
            //example 1 slide
            Console.WriteLine("Link Parent sebelum Delete");
            trace.insert(ref trace.root, 44);
            trace.insert(ref trace.root, 17);
            trace.insert(ref trace.root, 32);
            trace.insert(ref trace.root, 78);
            trace.insert(ref trace.root, 50);
            trace.insert(ref trace.root, 88);
            trace.insert(ref trace.root, 48);
            trace.insert(ref trace.root, 62);
            trace.printInorder(trace.root);
            trace.delete(ref trace.root, 32);
            Console.WriteLine("========");
            Console.WriteLine("Link Parent Setelah Delete");
            trace.printInorder(trace.root);
            Console.ReadKey();
        }
    }
}
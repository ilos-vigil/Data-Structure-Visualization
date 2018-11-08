using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;



namespace Btree
{

    class Program
    {
        [STAThread]
        static void Main()
        {

            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            /*
            Btree bt = new Btree(3);
            bt.insert(ref bt.bt,20);
            bt.insert(ref bt.bt,10);
            bt.insert(ref bt.bt, 5);
            bt.insert(ref bt.bt, 7);
            bt.insert(ref bt.bt, 9);
            bt.insert(ref bt.bt, 6);
            Console.WriteLine("Before delete!");
            bt.inorder(ref bt.bt);

            bt.delete(ref bt.bt, 7);
            Console.WriteLine("After delete 7!");
            bt.inorder(ref bt.bt);

            

            Console.ReadLine();
            */
        }
    }
}

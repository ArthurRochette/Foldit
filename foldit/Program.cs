using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace foldit
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if(args.Length > 0)
            {
                Application.Run(new Form1(int.Parse(args[0])));
            }
            else
            {
                Application.Run(new FolditConfig()); 

            }
        }
    }
}

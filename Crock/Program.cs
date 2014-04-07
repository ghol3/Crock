using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Crock._Forms;
using Crock._Settings;
using Crock._Languages;

namespace Crock
{
    class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
            Setting.SaveAll();
            Application.Exit();
        }
    }
}

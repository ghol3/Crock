using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

using Crock._Settings;
using Crock._Data;
using Crock._Languages;

namespace Crock._Forms
{
    public partial class Update : Form
    {
        private Texts text;
        private Database database;

        public Update()
        {
            InitializeComponent();
            this.text = new Texts();
            this.database = new Database();
        }

        /// <summary>
        /// Override OnLoad method from basic class Form.
        /// This method start when we open MainForm.
        /// </summary>
        /// <param name="e">Event class with parameters about event.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            #region Texts
            this.label1.Text = text["New version available!"];
            this.label2.Text = text["Version"] + ": " + database.Version;

            this.Text = text["Update"];
            this.button1.Text = text["Download"];
            #endregion
        }

        /// <summary>
        /// Open browser with url crock.com
        /// </summary>
        /// <param name="sender">object Button</param>
        /// <param name="e">Event class with parameters about event.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("http://crock.com");
        }
    }
}

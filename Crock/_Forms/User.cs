using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Crock._Settings;
using Crock._Languages;

namespace Crock._Forms
{
    public partial class User : Form
    {
        private Texts text;

        public User()
        {
            InitializeComponent();
            this.text = new Texts();
        }

        /// <summary>
        /// Override OnLoad method from basic class Form.
        /// This method start when we open MainForm.
        /// </summary>
        /// <param name="e">Event class with parameters about event.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.label1.Text = Setting.User.ToShortName();
            this.button1.Text = text["Edit"];
            this.Text = text["User"];
        }

        /// <summary>
        /// Open Settings form
        /// </summary>
        /// <param name="sender">object Button</param>
        /// <param name="e">Event class with parameters about event.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            Settings settingsForm = new Settings();
            settingsForm.Show();
        }
    }
}

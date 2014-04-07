using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Crock._Languages;
using Crock._Settings;

namespace Crock._Forms
{
    public partial class Settings : Form
    {
        private Texts text;

        public Settings()
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
            #region Texts
            this.Text = text["Settings"];
            this.label1.Text = text["Name"];
            this.label2.Text = text["Last name"];
            this.groupBox1.Text = text["User"];
            this.button1.Text = text["Save"];

            this.label3.Text = text["Title"];
            this.label4.Text = text["Text"];
            this.label5.Text = text["Default Template"];

            this.groupBox2.Text = text["End of presentation"];

            this.textBox1.Text = Setting.User.Name;
            this.textBox2.Text = Setting.User.LastName;
            this.textBox3.Text = Setting.App.Template;
            this.endTitleTextBox.Text = Setting.App.EndSlideTitle;
            this.endTextBox.Text = Setting.App.EndSlideText;

            this.button1.Text = text["Save"];
            #endregion
        }

        /// <summary>
        /// Method for saving settings.
        /// </summary>
        /// <param name="sender">object Button</param>
        /// <param name="e">Event class with parameters about event.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            Setting.User.Name = this.textBox1.Text;
            Setting.User.LastName = this.textBox2.Text;

            Setting.App.EndSlideText = this.endTextBox.Text;
            Setting.App.EndSlideTitle = this.endTitleTextBox.Text;

            Setting.SaveAll();
            this.Close();
        }

        /// <summary>
        /// Open file dialog and load path to presentation template
        /// </summary>
        /// <param name="sender">Object button</param>
        /// <param name="e">Event class with parameters about event.</param>
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            Setting.App.Template = ofd.FileName;
            this.textBox3.Text = ofd.FileName;
        }
    }
}

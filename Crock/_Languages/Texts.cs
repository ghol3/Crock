using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

using Crock._Settings;

namespace Crock._Languages
{
    class Texts
    {
        private string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private string language = Setting.App.Language;
        public string Language
        {
            get
            {
                return this.language;
            }
            set
            {
                Setting.App.Language = value;
                this.language = value;
                this.Load();
            }
        }

        private Dictionary<string, string> texts;

        public Texts()
        {
            this.texts = new Dictionary<string, string>();
            this.Load();
        }

        /// <summary>
        /// Load text from file to dictionary texts.
        /// </summary>
        public void Load()
        {
            try
            {
                string p = Path.Combine(this.path, "Crock", "Languages", this.language + ".txt");
                using (StreamReader sr = new StreamReader(p))
                {
                    this.texts.Clear();
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        string[] ss = s.Split('-');
                        this.texts.Add(ss[0], ss[1]);
                    }
                    sr.Close();
                }
            }
            catch
            {
                //MessageBox.Show("cannot loading language");
            }
        }

        /// <summary>
        /// Index for acces to dictionary texts with data about translate language.
        /// </summary>
        /// <param name="text">text for translating</param>
        /// <returns>translate text</returns>
        public string this[string text]
        {
            get
            {
                if (this.texts.ContainsKey(text))
                    return this.texts[text];
                else
                    return text;
            }
        }
    }
}

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
using System.IO;

using Crock._Parser;
using Crock._Settings;
using Crock._Data;
using Crock._Languages;

namespace Crock._Forms
{
    public partial class MainForm : Form
    {
        #region Class instances
        private string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private Database database;      
        private Texts text;
        private Wikipedia wikipedia;
        #endregion

        /// <summary>
        /// MainForm Constructor open new MainForm when aplication begin.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            this.database = new Database();
            this.text = new Texts();
            this.wikipedia = new Wikipedia();
            this.CheckFolders();
        }

        public void CheckFolders()
        {
            string crockFolder = Path.Combine(this.path, "Crock");
            string crockLngFolder = Path.Combine(this.path, "Crock", "Languages");
            if (!Directory.Exists(crockFolder) || !Directory.Exists(crockLngFolder))
            {
                Directory.CreateDirectory(crockLngFolder);
                Directory.CreateDirectory(crockLngFolder);
            }
        }

        /// <summary>
        /// Override OnLoad method from basic class Form.
        /// This method start when we open MainForm.
        /// </summary>
        /// <param name="e">Event class with parameters about event.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);     // Call base OnLoad method in class Form    
            LoadLanguages();    // Create item "Language" in Setting menu and add submenu items from file /AppData/Roaming/Languages
            CheckUpdate();      // Check aplication updates from database
            //
            // Change all text on this form to active language
            #region Texts
            //
            // All Button components
            urlCreateButton.Text =
            fileCreateButton.Text = text["Create"];
            searchThemeButton.Text = text["Search"];
            //
            // All label components
            fileLabel.Text = text["File"] + ":";
            themeLabel.Text = text["Theme"] + ":";
            //
            // All items in menu
            officialWikipediaorgToolStripMenuItem.Text = text["Official wikipedia.org"];
            exitToolStripMenuItem.Text = text["Exit"];
            userToolStripMenuItem.Text = text["User"];
            profileToolStripMenuItem.Text = text["Profile"];
            editToolStripMenuItem.Text = text["Edit"];
            removeToolStripMenuItem.Text = text["Remove"];
            settingsToolStripMenuItem.Text = text["Settings"];
            allToolStripMenuItem.Text = text["All"];
            languageToolStripMenuItem.Text = text["Language"];
            removeSettingsToolStripMenuItem.Text = text["Remove Settings"];
            helpToolStripMenuItem.Text = text["Help"];
            viewHelpToolStripMenuItem.Text = text["Help"];
            ourOfficialWebPageToolStripMenuItem.Text = text["Our Web Page"];
            checkUpdatesToolStripMenuItem.Text = text["Check Updates"];
            #endregion
        }

        /// <summary>
        ///  Method for new version with graphic slider.
        /// </summary>
        public void LoadSlider()
        {
            //slider1.Items.Clear();
            //slider1.Items.AddRange(this.database.GetSliderItems());
            //this.slider.Items = Database.GetSliderItems();
        }

        /// <summary>
        /// Create item "Language" in Setting menu and add submenu items from file /AppData/Roaming/Languages
        /// </summary>
        public void LoadLanguages()
        {
            List<ToolStripItem> languages = new List<ToolStripItem>();                                              // Create new List with ToolStripItems
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);                     // Get path to user folder (C:\Users\user_name\AppData\Roaming) in W7
            string[] names = System.IO.Directory.GetFiles(System.IO.Path.Combine(path, "Crock", "Languages"));      // Combine paths to get path C:\Users\ghol3\AppData\Roaming\Crock\Languages and get all file names
            foreach (string lng in names)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();       // Create new menu item
                item.Text = ParsePath(lng);                             // Add atribute Text for item
                item.Click += new EventHandler(language_Click);         // Add Event Click for item 
                languages.Add(item);                                    // Add item to list languages
            }
            this.languageToolStripMenuItem.DropDownItems.AddRange(languages.ToArray());                             // Add array of items to Menu  
        }

        /// <summary>
        /// Method for parse path.
        /// From "C:\Users\ghol3\AppData\Roaming\Crock\Languages\cz.txt" return "cz"
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>name of file without path</returns>
        private string ParsePath(string path)
        {
            string[] parsePath = path.Split('\\');
            string fileNametxt = parsePath[parsePath.Length - 1];
            string[] name = fileNametxt.Split('.');
            return name[0];
        }

        /// <summary>
        /// Click method for change language (if user click on item in menu Languages)
        /// </summary>
        /// <param name="o">Object ToolStripMenuItem</param>
        /// <param name="e">Event class with parameters about event.</param>
        private void language_Click(object o, EventArgs e)
        {
            ToolStripMenuItem lng = (ToolStripMenuItem)o;           // Retyping object to ToolStripMenuItem
            text.Language = lng.Text;                               // Change active language in Class Texts
            Application.Restart();                                  // Restart aplication
        }

        /// <summary>
        /// Method Check version of aplication (in Setting) and avaliable version in database
        /// </summary>
        public void CheckUpdate()
        {
            if (Setting.App.CheckUpdates && Setting.App.Version != this.database.Version)
            {
                Update u = new Update();
                u.ShowDialog();
            }
        }

        /// <summary>
        /// Method for click on menu item (Crock->Exit)
        /// </summary>
        /// <param name="sender">Object ToolStripMenuItem</param>
        /// <param name="e">Event class with parameters about event.</param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Method for click on menu item (Crock->Official wikipedia page)
        /// </summary>
        /// <param name="sender">Object ToolStripMenuItem</param>
        /// <param name="e">Event class with parameters about event.</param>
        private void officialWikipediaorgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://wikipedia.org");
        }

        /// <summary>
        /// Method for click on menu item (User->Profile)
        /// </summary>
        /// <param name="sender">Object ToolStripMenuItem</param>
        /// <param name="e">Event class with parameters about event.</param>
        private void profileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User userForm = new User();
            userForm.Show();
        }

        /// <summary>
        /// Method for click on menu item (User->Remove)
        /// </summary>
        /// <param name="sender">Object ToolStripMenuItem</param>
        /// <param name="e">Event class with parameters about event.</param>
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setting.User.Reset();
        }

        /// <summary>
        /// Method for click on menu item (Help->Check updates)
        /// </summary>
        /// <param name="sender">Object ToolStripMenuItem</param>
        /// <param name="e">Event class with parameters about event.</param>
        private void checkUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckUpdate();
        }

        /// <summary>
        /// Method for click on menu item (Help->Our official web page)
        /// </summary>
        /// <param name="sender">Object ToolStripMenuItem</param>
        /// <param name="e">Event class with parameters about event.</param>
        private void ourOfficialWebPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://crock.com");
        }

        /// <summary>
        /// Method for click on menu item (Help->View help)
        /// </summary>
        /// <param name="sender">Object ToolStripMenuItem</param>
        /// <param name="e">Event class with parameters about event.</param>
        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://crock.com/help");
        }

        /// <summary>
        /// Method for click on menu item (Settings->all)
        /// </summary>
        /// <param name="sender">Object ToolStripMenuItem</param>
        /// <param name="e">Event class with parameters about event.</param>
        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings settingsForm = new Settings();
            settingsForm.Show();
        }

        /// <summary>
        /// Method for click on menu item (Settings->Remove Settings)
        /// </summary>
        /// <param name="sender">Object ToolStripMenuItem</param>
        /// <param name="e">Event class with parameters about event.</param>
        private void removeSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setting.App.Reset();
            Setting.User.Reset();
        }

        /// <summary>
        /// Method for create presentation from url adres when click on button
        /// </summary>
        /// <param name="sender">Object Button</param>
        /// <param name="e">Event class with parameters about event.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            //dynamic d = Activator.CreateInstance(Type.GetType("Crock._Parser.Wikipedia"));        // demonstration for call classes and method when we get more parsers
            this.wikipedia.LoadFromUrl(this.urlTextBox.Text);
            this.wikipedia.Parse();
            CreatePresentation();
        }

        /// <summary>
        /// Method for Button search theme on wikipedia.
        /// This method open default browser with url what we create from language and theme.
        /// </summary>
        /// <param name="sender">Object Button</param>
        /// <param name="e">Event class with parameters about event.</param>
        private void button2_Click(object sender, EventArgs e)
        {
            string url = "http://" + Setting.App.Language + ".wikipedia.org/wiki/" + this.themeTextBox.Text;
            Process.Start(url);
            this.urlTextBox.Text = url;
        }

        #region Clear Buttons
        //
        // Buttons for clear textboxs.
        private void button3_Click(object sender, EventArgs e)
        {
            this.urlTextBox.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.themeTextBox.Clear();
        }

        private void fileXButton_Click(object sender, EventArgs e)
        {
            this.fileTextBox.Clear();
        }
        #endregion

        /// <summary>
        /// Button for load path to file with html code
        /// </summary>
        /// <param name="sender">Object Button</param>
        /// <param name="e">Event class with parameters about event.</param>
        private void ofdButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.ShowDialog();
            this.fileTextBox.Text = openFile.FileName;
        }

        /// <summary>
        /// Method for create presentation from file in computer when clicked on button
        /// </summary>
        /// <param name="sender">Object Button</param>
        /// <param name="e">Event class with parameters about event.</param>
        private void fileCreateButton_Click(object sender, EventArgs e)
        {
            this.wikipedia.LoadFromFile(this.fileTextBox.Text);
            this.wikipedia.Parse();
            CreatePresentation();
        }

        /// <summary>
        /// Create new instance of PowerPoint class and send data to this class.
        /// </summary>
        private void CreatePresentation()
        {
            PowerPoint ppt = new PowerPoint()
            {
                pptAuthor = Setting.User.ToShortName(),
                pptDate = DateTime.Now.ToShortDateString(),
                pptFileName = "Presentation" + wikipedia.ArticleName,
                pptTitle = wikipedia.ArticleName
            };
            if (!ppt.RunPowerPoint())
                return;
            ppt.Create(wikipedia.GetList());
        }
    }
}

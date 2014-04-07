using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ppt = Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Core;
using Crock._Parser;
using Crock._Settings;

namespace Crock
{
    class PowerPoint
    {
        public string pptTitle { get; set; }
        public string pptAuthor { get; set; }
        public string pptDate { get; set; }
        public string pptFileName { get; set; }
        private bool LoadTemplate;

        private MsoTriState msoTrue;
        private MsoTriState msoFalse;

        private ppt.Application pptApp;
        private ppt.Presentations pptPress;
        private ppt.Presentation pptPres;

        private string template = Setting.App.Template;

        private ppt.Slides pptSlides;
        private ppt.Slide pptSlide;

        private ppt.TextRange pptTextRange;

        public PowerPoint()
        {
            this.msoTrue = MsoTriState.msoTrue;
            this.msoFalse = MsoTriState.msoFalse;
            this.LoadTemplate = true;
        }

        /// <summary>
        /// If user have instaled microsoft power point we open actualy version.
        /// </summary>
        /// <returns>bool if ms power point was opened.</returns>
        public bool RunPowerPoint()
        {
            try
            {
                this.pptApp = new ppt.Application();
                this.pptApp.Visible = msoTrue;
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("you must instal Microsoft Power Point" + 
                                Environment.NewLine + ex.Message);
                return false;
            }

        }

        /// <summary>
        /// Create new presentation.
        /// 1. option open template.
        /// 2. option add blank presentation.
        /// </summary>
        /// <param name="item"></param>
        public void Create(List<ppItem> item) 
        {
            this.pptPress = this.pptApp.Presentations;
            if (this.template != null || this.template != "")
            {
                try
                {
                    this.pptPres = this.pptPress.Open(this.template, this.msoFalse, this.msoFalse, this.msoTrue);
                }
                catch
                {
                    this.LoadTemplate = false;
                }
            }
            if (!this.LoadTemplate)
            {
                this.pptPres = this.pptPress.Add(this.msoTrue);
            }


            
            this.pptSlides = this.pptPres.Slides;
            CreateTitle();
            CreateContent(item);
            CreateEnd();
        }

        /// <summary>
        /// Create First slide with presentation theme like name and 
        /// combine this with user name and date.
        /// </summary>
        private void CreateTitle()
        {
            try{
                this.pptSlide = this.pptSlides.Add(1, ppt.PpSlideLayout.ppLayoutTitle);}
            catch{}
            this.pptTextRange = this.pptSlide.Shapes[1].TextFrame.TextRange;
            this.pptTextRange.Text = this.pptTitle;
            this.pptTextRange.Font.Name = "Comic Sans MS";
            this.pptTextRange.Font.Size = 48;

            this.pptTextRange = this.pptSlide.Shapes[2].TextFrame.TextRange;
            this.pptTextRange.Text = this.pptAuthor + Environment.NewLine + this.pptDate;
            this.pptTextRange.Font.Name = "Comic Sans MS";
            this.pptTextRange.Font.Size = 30;
            return;
        }

        /// <summary>
        /// Cycle for adding slides with maximum characters (300) on slide
        /// </summary>
        /// <param name="items"></param>
        private void CreateContent(List<ppItem> items)
        {
            string subTitle = "";
            string text = "";

            foreach(ppItem item in items)
            {
                if (item.Type == ppItemType.Title)
                {
                    if (text != "" || text != null)
                        AddSlide(subTitle, text);
                    subTitle = item.Text;
                    text = "";
                    continue;
                }
                if (text.Length + item.Text.Length > 300)
                {
                    AddSlide(subTitle, text);
                    text = "";
                }
                text += item.Text + Environment.NewLine;
                
            }
        }

        /// <summary>
        /// Add new body slide to presentation.
        /// </summary>
        /// <param name="title">Subtitle on new slide.</param>
        /// <param name="text">text in body on slide.</param>
        private void AddSlide(string title, string text)
        {
            if (title == "" || text == "")
                return;
            this.pptSlide = this.pptSlides.Add(pptSlides.Count + 1, ppt.PpSlideLayout.ppLayoutText);
            this.pptTextRange = this.pptSlide.Shapes[1].TextFrame.TextRange;
            this.pptTextRange.Text = title;
            this.pptTextRange.Font.Name = "Comic Sans MS";
            this.pptTextRange.Font.Size = 48;

            this.pptTextRange = this.pptSlide.Shapes[2].TextFrame.TextRange;
            this.pptTextRange.Text = text;
            this.pptTextRange.Font.Name = "Comic Sans MS";
            this.pptTextRange.Font.Size = 32;
        }

        /// <summary>
        /// Add last slide if we have some data in setting
        /// </summary>
        private void CreateEnd()
        {
            if (Setting.App.EndSlideTitle == "" &&
               Setting.App.EndSlideText == "")
                return;
            this.pptSlide = this.pptSlides.Add(pptSlides.Count + 1, ppt.PpSlideLayout.ppLayoutText);
            this.pptTextRange = this.pptSlide.Shapes[1].TextFrame.TextRange;
            this.pptTextRange.Text = Setting.App.EndSlideTitle;
            this.pptTextRange.Font.Name = "Comic Sans MS";
            this.pptTextRange.Font.Size = 48;

            this.pptTextRange = this.pptSlide.Shapes[2].TextFrame.TextRange;
            this.pptTextRange.Text = Setting.App.EndSlideText;
            this.pptTextRange.Font.Name = "Comic Sans MS";
            this.pptTextRange.Font.Size = 32;
        }
    }
}

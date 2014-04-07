using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using Forms = System.Windows.Forms;
using HtmlAgilityPack;
using System.Net;
using System.IO;

namespace Crock._Parser
{
    class Wikipedia : Parser
    {
        /// <summary>
        /// Constructor what call base constructor in Parser
        /// </summary>
        public Wikipedia()
            : base()
        {
        }

        /// <summary>
        /// Parse the code with some rules.
        /// </summary>
        public void Parse()
        {
            if (this.htmlCode == null)
                return;
            document.LoadHtml(this.htmlCode);
            this.ArticleName = this.GetOneTag("h1").InnerText;
            this.RemoveTags("table");
            this.LoadHtmlIn("div[@id='mw-content-text']");
            string[] tagsArray = Enum.GetNames(typeof(Tags));
            foreach (string tag in tagsArray)
            {
                foreach (HtmlNode node in this.GetItems(tag))
                {
                    this.Items.Add(new ppItem()
                    {
                        Index = node.Line,
                        Text = tag == "h2" ? EditH2Tag(node.InnerText)
                                           : node.InnerText,
                        Type = tag == "h2" ? ppItemType.Title  
                                           : ppItemType.Text
                    });
                    
                }
            } 
        }
        
        /// <summary>
        /// Remove useless text form subtitle.
        /// </summary>
        /// <param name="text">title</param>
        /// <returns>parse title</returns>
        private string EditH2Tag(string text)
        {
            //text will be in format "some text [edit|add]" and we need get just only some text (y)
            Regex regex = new Regex(string.Format("\\[.*?\\]"));
            return regex.Replace(text, string.Empty);
        }

        /// <summary>
        /// override method Parser.GetList() what return only list without sort
        /// </summary>
        /// <returns></returns>
        public override List<ppItem> GetList()
        {
            return this.Items.OrderBy(ppItem => ppItem.Index).ToList();
        }

        /// <summary>
        /// delete later this method is in Parser.RemoveTags(string tagName)
        /// </summary>
        private void RemoveTables()
        {
            try
            {
                foreach (HtmlNode node in document.DocumentNode.SelectNodes("//table"))
                    node.Remove();
            }
            catch { }
        }
    }

    /// <summary>
    /// tags for parsing every parser can have different tags to parse
    /// </summary>
    public enum Tags
    {
        h2,
        p,
        li
    }
}

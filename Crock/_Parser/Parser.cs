using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Forms = System.Windows.Forms;
using HtmlAgilityPack;
using System.Net;
using System.IO;

namespace Crock._Parser
{
    class Parser
    {
        protected string url;
        protected string htmlCode;

        public string ArticleName { get; set; }

        protected List<ppItem> Items;
        protected HtmlDocument document;

        public Parser()
        {
            this.Items = new List<ppItem>();
            this.document = new HtmlDocument();
        }

        /// <summary>
        /// Download source code and load him to document
        /// </summary>
        /// <param name="url"></param>
        public void LoadFromUrl(string url)
        {
            this.url = url;
            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            try
            {
                this.htmlCode = webClient.DownloadString(this.url);
            }
            catch(Exception e)
            {
                Forms.MessageBox.Show("this is not a valid url or you dont have connection to internet..." + e.Message);
            }
        }

        /// <summary>
        /// Load document form file.
        /// </summary>
        /// <param name="path">file path in computer</param>
        public void LoadFromFile(string path)
        {
            string s = string.Empty;
            this.htmlCode = string.Empty;

            using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
            {
                while ((s = sr.ReadLine()) != null)
                    this.htmlCode += s + Environment.NewLine;
                sr.Close();
            }
        }

        /// <summary>
        /// Delete tags from document.
        /// </summary>
        /// <param name="tagName">tag name("p")</param>
        protected void RemoveTags(string tagName)
        {
            try
            {
                foreach (HtmlNode node in document.DocumentNode.SelectNodes("//" + tagName))
                    node.Remove();
            }
            catch { }
        }

        /// <summary>
        /// like "//div[@id='mw-content-text']" load inner html in this div
        /// </summary>
        /// <param name="tag"></param>
        protected void LoadHtmlIn(string tagName)
        {
            this.document.LoadHtml(this.document.DocumentNode.SelectSingleNode("//" + tagName).InnerHtml);
        }

        /// <summary>
        /// Select one tag from document.
        /// </summary>
        /// <param name="tagName">tag name("li")</param>
        /// <returns>htmlnode - parse tag</returns>
        protected HtmlNode GetOneTag(string tagName)
        {
            return this.document.DocumentNode.SelectSingleNode("//" + tagName);
        }

        /// <summary>
        /// Get colection of tags.
        /// </summary>
        /// <param name="tagName">tag name("li")</param>
        /// <returns>colection of structurs htmlnode</returns>
        protected HtmlNodeCollection GetItems(string tagName)
        {
            return this.document.DocumentNode.SelectNodes("//" + tagName);
        }

        /// <summary>
        /// List of data with all items
        /// </summary>
        /// <returns>list of items for generate presentation.</returns>
        public virtual List<ppItem> GetList()
        {
            return this.Items;
        }

    }

    /// <summary>
    /// type for presentation item
    /// </summary>
    public enum ppItemType
    {
        Title,
        Text,
        Image,
        Table,
        Chart
    }

    /// <summary>
    /// item with atributes and types to get more options 
    /// </summary>
    public struct ppItem
    {
        public string Text { get; set; }
        public int Index { get; set; }
        public ppItemType Type { get; set; }
    }
}

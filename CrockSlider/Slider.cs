using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace CrockSlider
{
    public partial class Slider: UserControl
    {
        private List<SliderItem> _Items;
        [Category("Items")]
        public List<SliderItem> Items
        {
            get
            {
                return this._Items;
            }
            set
            {
                this._Items = value;
                this.Invalidate();
            }
        }

        private int activeItem = 0;
        public int ActiveItem 
        {
            get
            {
                return this.activeItem;
            }
            set
            {
                this.activeItem = value;
                this.Invalidate();
            }
        }

        private bool RadiosVisible = true;

        public int RadiosSize { get; set; }

        public Slider()
        {
            InitializeComponent();
            this._Items = new List<SliderItem>();
            this.RadiosSize = 30;
            this._Items.Clear();         
            this._Items.Add(new SliderItem()
            {
                Index = 0,
                Image = null
            });
            
            
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Y < this.Height - this.RadiosSize)
            {
                this.RadiosVisible = !this.RadiosVisible;
                // open browser with some page
                this.Invalidate();
            }
            else if (e.Y > this.Height - this.RadiosSize && this.RadiosVisible)
            {
                if ((e.X / this.RadiosSize) < this._Items.Count)
                {
                    this.activeItem = e.X / this.RadiosSize;
                    this.Invalidate();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            // draw bg up
            if(this._Items[this.activeItem].Image != null)
                g.DrawImage(this._Items[this.activeItem].Image, 0,0);
            AddRadios(g);
            //g.FillRectangle(new SolidBrush(this._Items[this.activeItem].BgColor), 0, 0, this.Width, this.Height - this.ItemSize);
            //
            // draw title


            //g.DrawString(this._Items[this.activeItem].Name, fontText, new SolidBrush(this._Items[this.activeItem].FontColor), this.Width / 2, this.Height - this.ItemSize, sFormat);
            /*
            // draw menu
            if (this.menuVisible)
            {
                foreach (SliderItem item in this._Items)
                {
                    if (item.Logo != null)
                    {
                        Bitmap b = new Bitmap(item.Logo, new Size(30, 30));
                        g.DrawImage(b, new Point(item.Index * 30, this.Height - 30));
                    }
                    //g.FillRectangle(new SolidBrush(item.BgColor), item.Index * 30, this.Height - 30, 30, 30);
                }
                //g.FillRectangle(Brushes.Red, 0, this.Height - 30, 30, 30);
                //g.FillRectangle(Brushes.Black, 30, this.Height - 30, 30, 30);
                //g.FillRectangle(Brushes.Purple, 60, this.Height - 30, 30, 30);
             
            }*/
        }

        private void AddRadios(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (RadiosVisible)
            {
                foreach(SliderItem item in this._Items)
                {
                    if (item.Index == this.activeItem)
                        g.FillEllipse(Brushes.Red, item.Index * this.RadiosSize, this.Height - this.RadiosSize - 1, this.RadiosSize, this.RadiosSize);
                    g.DrawEllipse(Pens.Black, item.Index * this.RadiosSize, this.Height - this.RadiosSize - 1, this.RadiosSize, this.RadiosSize);
                }
            }
        }

        public void AddItem(SliderItem item)
        {
            
        }

        public void AddItems(SliderItem[] items)
        {
            this._Items.AddRange(items);
        }

        public void AddItems(List<SliderItem> items)
        {
            this._Items.AddRange(items);
        }
    }

    [Serializable]
    public class SliderItem
    {
        public string Url { get; set; }
        public Bitmap Image { get; set; }
        public int Index { get; set; }
    }
}

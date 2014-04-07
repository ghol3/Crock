using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

using CrockSlider;

namespace Crock._Data
{
    class Database
    {
        private string _Version = "1.1";
        public string Version 
        {
            get
            {
                return this._Version;
            }
            set
            {
                this._Version = value;
            }
        }
        
        public Database()
        {

        }

        public void Connect()
        {

        }

        public void Disconnect()
        {

        }

        public List<SliderItem> GetSliderItems()
        {
            
            return new List<SliderItem>(){
                new SliderItem()
                {
                    Image = new Bitmap(global::Crock.Properties.Resources.slider1),
                    Index = 0,
                    Url = "dabel.html"
                },
                new SliderItem()
                {
                    Image = new Bitmap(global::Crock.Properties.Resources.slider2),
                    Index = 1,
                    Url = "dabel.html"
                },

                new SliderItem()
                {
                    Image = new Bitmap(global::Crock.Properties.Resources.slider3),
                    Index = 2,
                    Url = "dabel.html"
                },
                new SliderItem()
                {
                    Image = new Bitmap(global::Crock.Properties.Resources.slider4),
                    Index = 3,
                    Url = "barakL"
                },
            };
        }

        ~Database()
        {
            Disconnect();
        }
    }
}

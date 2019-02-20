using System;

namespace StockNet
{
    public class Crumb
    {
        public string Text
        {
            get;
            set;
        }
        public bool HasText
        {
            get
            {
                return !string.IsNullOrEmpty(this.Text);
            }
        }
        public string Link
        {
            get;
            set;
        }
        public bool HasLink
        {
            get
            {
                return !string.IsNullOrEmpty(this.Link);
            }
        }
        public string Icon
        {
            get;
            set;
        }
        public bool HasIcon
        {
            get
            {
                return !string.IsNullOrEmpty(this.Icon);
            }
        }
        public Crumb()
        {
        }
        public Crumb(string text)
        {
            this.Text = text;
        }
        public Crumb(string text, string link)
            : this(text)
        {
            this.Link = link;
        }
        public Crumb(string text, string link, string icon)
            : this(text)
        {
            this.Link = link;
            this.Icon = icon;
        }
    }
}
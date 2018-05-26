using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTEReader.Models
{
    class BookItem
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string id;

        public string Id
        {
            get { return this.id; }
            set
            {
                this.id = value;
                NotifyPropertyChanged("Id");
            }
        }

        private string title;

        public string Title
        {
            get { return this.title; }
            set
            {
                this.title = value;
                NotifyPropertyChanged("Title");
            }
        }

        private string catalog;

        public string Catalog
        {
            get { return this.catalog; }
            set
            {
                this.catalog = value;
                NotifyPropertyChanged("Catalog");
            }
        }

        private string tags;

        public string Tags
        {
            get { return this.tags; }
            set
            {
                this.tags = value;
                NotifyPropertyChanged("Tags");
            }
        }

        private string info;

        public string Info
        {
            get { return this.info; }
            set
            {
                this.info = value;
                NotifyPropertyChanged("Info");
            }
        }        

        private string image;

        public string Image
        {
            get { return this.image; }
            set
            {
                this.image = value;
                NotifyPropertyChanged("Image");
            }
        }

        private string pages;

        public string Pages
        {
            get { return this.pages; }
            set
            {
                this.pages = value;
                NotifyPropertyChanged("Pages");
            }
        }

        public BookItem(string title, string pages)
        {
            this.id = Guid.NewGuid().ToString();
            this.title = title;
            this.pages = "0";
        }

        public BookItem(string id, string title, string pages)
        {
            this.id = id;
            this.title = title;
            this.pages = "0";

        }

        public BookItem(string title, string catalog, string tags, string info, string image)
        {
            this.id = Guid.NewGuid().ToString();
            this.title = title;
            this.catalog = catalog;
            this.tags = tags;
            this.info = info;
            this.image = image;            
        }
    }
}

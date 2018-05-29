using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTEReader.Models
{
    class BookInShelf
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

        public BookInShelf(string title)
        {
            this.id = Guid.NewGuid().ToString();
            this.title = title;
            this.pages = "0";
        }

        public BookInShelf(string id, string title, string pages)
        {
            this.id = id;
            this.title = title;
            this.pages = pages;
        }
    }
}

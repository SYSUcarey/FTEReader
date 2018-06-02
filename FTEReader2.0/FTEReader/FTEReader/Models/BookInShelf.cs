using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTEReader.Models
{
    //书架上的书，即本地书籍
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

        //书本的唯一标识
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

        //书本名称
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

        //当前已阅读页数
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

        //构造函数
        public BookInShelf(string title)
        {
            this.id = Guid.NewGuid().ToString();
            this.title = title;
            this.pages = "1";
        }

        public BookInShelf(string id, string title, string pages)
        {
            this.id = id;
            this.title = title;
            this.pages = pages;
        }
    }
}

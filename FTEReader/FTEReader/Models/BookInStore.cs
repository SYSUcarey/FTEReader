using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace FTEReader.Models
{
    //书库里的书，即在线书籍
    class BookInStore
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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

        //书本种类
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

        //书本标签
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

        //书本信息
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

        //图像uri，来自api访问
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

        //显示图像
        private ImageSource source;
        public ImageSource Source
        {
            get { return this.source; }
            set
            {
                this.source = value;
                NotifyPropertyChanged("Source");
            }
        }

        //唯一标识
        private string bookId;
        public string BookId
        {
            get { return this.bookId; }
            set
            {
                this.bookId = value;
                NotifyPropertyChanged("BookId");
            }
        }

        //作者
        private string author;
        public string Author
        {
            get { return this.author; }
            set
            {
                this.author = value;
                NotifyPropertyChanged("Author");
            }
        }

        //男女区分
        private string compatibeMen;
        public string CompatibeMen
        {
            get { return this.compatibeMen; }
            set
            {
                this.compatibeMen = value;
                NotifyPropertyChanged("CompatibeMen");
            }
        }

        //当前阅读章节
        private string nowChac;
        public string NowChac
        {
            get { return this.nowChac; }
            set
            {
                this.nowChac = value;
                NotifyPropertyChanged("NowChac");
            }
        }

        public BookInStore(string title, string catalog, string tags, string info, string image, string bookId,
                            string author, string compatibeMen, string nowChac)
        {
            this.title = title;
            this.catalog = catalog;
            this.tags = tags;
            this.info = info;
            this.image = image;
            this.bookId = bookId;
            this.author = author;
            this.compatibeMen = compatibeMen;
            this.nowChac = nowChac;

            BitmapImage bitmap = new BitmapImage(new Uri(image, UriKind.Absolute));
            this.source = bitmap;
        }
    }
}


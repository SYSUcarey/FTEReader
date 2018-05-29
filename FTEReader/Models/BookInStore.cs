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

        public BookInStore(string title, string catalog, string tags, string info, string image)
        {
            this.title = title;
            this.catalog = catalog;
            this.tags = tags;
            this.info = info;
            this.image = image;

            //解决无图片的问题？？尚未解决
            if(image == "http://apis.juhe.cn/goodbook/img/68e52b5b4d6715947c73056c2c2e67a8.jpg")
            {
                this.source = new BitmapImage(new Uri("ms-appx:///Assets/default_book.png"));
            }
            else
            {
                BitmapImage bitmap = new BitmapImage(new Uri(image, UriKind.Absolute));
                this.source = bitmap;
            }
        }
    }
}


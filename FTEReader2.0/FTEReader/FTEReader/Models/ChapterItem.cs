using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTEReader.Models
{
    class ChapterItem
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        //章节数
        private int num;
        public int Num
        {
            get { return this.num; }
            set
            {
                this.num = value;
                NotifyPropertyChanged("Num");
            }
        }

        //章节名
        private string name;
        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public ChapterItem()
        {
            this.num = 0;
            this.name = "";
        }

        public ChapterItem(int num, string name)
        {
            this.num = num;
            this.name = name;
        }
    }
}

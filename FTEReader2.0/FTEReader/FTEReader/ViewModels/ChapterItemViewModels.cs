using FTEReader.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTEReader.ViewModels
{
    class ChapterItemViewModels
    {
        private ObservableCollection<Models.ChapterItem> chapterItems = new ObservableCollection<Models.ChapterItem>();
        public ObservableCollection<Models.ChapterItem> ChapterItems { get { return this.chapterItems; } }

        private static ChapterItemViewModels instance;
        private ChapterItemViewModels() { }

        //获取单例viewmodel
        public static ChapterItemViewModels GetViewModel()
        {
            if (instance == null)
            {
                instance = new ChapterItemViewModels();
            }
            return instance;
        }

        //将章节列表导入viewmodel中
        public void add(List<string> list)
        {
            
            for (int i = 0; i < list.Count; i++)
            {
                ChapterItem chapter = new ChapterItem(i + 1, list[i]);
                Debug.WriteLine(list[i]);               
                this.chapterItems.Add(chapter);
            }
        }
    }
}

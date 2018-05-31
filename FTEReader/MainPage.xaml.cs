using FTEReader.WebRequest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using FTEReader.DataBase;
using FTEReader.ViewModels;
using System.Diagnostics;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace FTEReader
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            bookInShelfViewModel = BookInShelfViewModels.GetViewModel();
            bookInStoreViewModel = BookInStoreViewModels.GetViewModel();
        }

        private BookInShelfViewModels bookInShelfViewModel;
        private BookInStoreViewModels bookInStoreViewModel;

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            int add = 100;
            int count = 0;
            while (add >= 30)
            {
                RootObject mybook = await GetBooks.GetBook(77, count, 30);
                int num = int.Parse(mybook.result.totalNum);
                //number.Text = mybook.result.totalNum;
                if (count == 0) add = num;
                int t = (add > 30) ? (30) : (add % 30);
                for (int i = 0; i < t; i++)
                {
                    string title = mybook.result.data[i].title;
                    string catalog = mybook.result.data[i].catalog;
                    string tags = mybook.result.data[i].tags;
                    string info = mybook.result.data[i].sub2;
                    string image = mybook.result.data[i].img;
                    BookDB.addToBookStore(title, catalog, tags, info, image);
                }
                add -= 30;
                count += 30;
            }                                  
        }
        
        private void type_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string type = button.Name;            
            bookInStoreViewModel.ChangeViewModel(type);
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(BookStorePage));
            }
        }

        private void menu_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = (StackPanel)e.ClickedItem;
            string to = item.Name;
            if (to == "_shelf")
            {
                shelf.Visibility = Visibility.Visible;
                types.Visibility = Visibility.Collapsed;
            }
            if (to == "_store")
            {
                shelf.Visibility = Visibility.Collapsed;
                types.Visibility = Visibility.Visible;
            }
        }

        private async void addShelf(object sender, RoutedEventArgs e)
        {
            ///
            /// 获得书本信息列表
            /// 调用函数：
            /// GetNewBook(string type, string major,string start,string limit,string tag,string gender)；
            /// 
            /// type为类型，这里数据库只收录 "hot"的图书即type恒为hot
            /// major为大分类，例如玄幻，武侠，都市等
            /// tag为小分类，玄幻下的有东方玄幻、异界大陆等
            /// gender为适度人群，"male"或者"female"
            /// 以上四个输入值具体查看分类表
            /// 
            /// start为分类下展示的书籍的起始位置
            /// limit为展示的数目，最高50
            /// 
            
            BooksObject myNewBook = await GetNewBooks.GetNewBook("hot","玄幻","东方玄幻","0","20","male");
            number.Text = myNewBook.total.ToString();
            booktitle.Text = myNewBook.books[0].title;
            string bookid = myNewBook.books[0]._id;
            BookDetailObject myBookDetail = await BookDetail.GetBookDetail(bookid);
            introduction.Text = myBookDetail.longIntro;
            ChapterObject myChapter = await Chapter.GetChapter(bookid);
            chaptertitle.Text = myChapter.mixToc.chapters[0].title;
            string link = myChapter.mixToc.chapters[0].link;

            //encoding
            link = link.Replace("/", "%2F");
            link = link.Replace("?", "%3F");
            //link = "http:%2F%2Fbook.my716.com%2FgetBooks.aspx%3Fmethod=content&bookId=633074&chapterFile=U_753547_201607012243065574_6770_1.txt";

            ChapterDetailObject myChaperDetail = await ChapterDetail.GetChapterDetail(link);
            show.Text = myChaperDetail.chapter.body;

            //show.Text = link;
            
            
            //string reallink = 
            //show.Text = link;
           //show.Text = myChaperDetail.chapter.body;
            
        }
    }
}

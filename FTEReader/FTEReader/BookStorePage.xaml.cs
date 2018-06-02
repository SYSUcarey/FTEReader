using FTEReader.DataBase;
using FTEReader.Models;
using FTEReader.ViewModels;
using FTEReader.WebRequest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace FTEReader
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class BookStorePage : Page
    {
        public BookStorePage()
        {
            ViewModel = BookInStoreViewModels.GetViewModel();
            this.InitializeComponent();
        }

        private BookInStoreViewModels ViewModel;
        private ObservableCollection<BookInStore> suggestions = new ObservableCollection<BookInStore>();

        //搜索框文本改变时，改变显示的建议
        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            suggestions.Clear();
            ObservableCollection<string[]> books = BookDB.findBookFromStore(sender.Text);
            foreach (string[] book in books)
            {
                string title = (string)book[0];
                string catalog = (string)book[1];
                string tags = (string)book[2];
                string info = (string)book[3];
                string image = (string)book[4];
                string bookId = (string)book[5];
                string author = (string)book[6];
                string compatibeMen = (string)book[7];
                string nowChac = (string)book[8];
                BookInStore item = new BookInStore(title, catalog, tags, info, image,
                                                    bookId, author, compatibeMen, nowChac);
                suggestions.Add(item);
            }
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                sender.ItemsSource = suggestions;
            }
        }

        //查询操作
        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            string queryStr = sender.Text;
            ViewModel.ChangeViewModel(queryStr);
        }

        //选择建议的触发事件
        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (args.SelectedItem == null) return;
            sender.Text = ((BookInStore)args.SelectedItem).Title;
        }

        //点击书籍，跳转到阅读页面
        private async void read_Click(object sender, ItemClickEventArgs e)
        {
            BookInStore selectedItem = (BookInStore)e.ClickedItem;

            string bookId = selectedItem.BookId;
            string nowChapter = selectedItem.NowChac;
            progressBar.Visibility = Visibility.Visible;
            progressBar.IsIndeterminate = true;
            string content = await BookService.GetChapterContent(bookId, nowChapter);
            progressBar.Visibility = Visibility.Collapsed;
            progressBar.IsIndeterminate = false;
            string type = "1";
            string[] parameter = { type, bookId, nowChapter, content };
            this.Frame.Navigate(typeof(ReadPage), parameter);
        }
    }
}

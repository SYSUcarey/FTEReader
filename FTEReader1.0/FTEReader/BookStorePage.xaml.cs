using FTEReader.DataBase;
using FTEReader.Models;
using FTEReader.ViewModels;
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
                BookInStore item = new BookInStore(title, catalog, tags, info, image);
                suggestions.Add(item);
            }
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                sender.ItemsSource = suggestions;
            }
        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            string queryStr = sender.Text;
            ViewModel.ChangeViewModel(queryStr);
        }

        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (args.SelectedItem == null) return;
            sender.Text = ((BookInStore)args.SelectedItem).Title;
        }

        private async void read_Click(object sender, ItemClickEventArgs e)
        {
            BookInStore selectedItem = (BookInStore)e.ClickedItem;



            var myblog = new Uri(@"https://www.jiumodiary.com/");
            var success = await Launcher.LaunchUriAsync(myblog);

            if (success)
            {
                // 如果你感兴趣，可以在成功启动后在这里执行一些操作。
            }
            else
            {
                // 如果你感兴趣，可以在这里处理启动失败的一些情况。
            }
            //BookInStoreViewModels
        }
    }
}

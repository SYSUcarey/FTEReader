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
using Windows.Storage.Pickers;
using Windows.Storage;
using FTEReader.Models;
using System.Collections.ObjectModel;

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
                button.Visibility = Visibility.Visible;
            }
            if (to == "_store")
            {
                shelf.Visibility = Visibility.Collapsed;
                types.Visibility = Visibility.Visible;
                button.Visibility = Visibility.Collapsed;
            }
        }

        private async void addShelf(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".txt");
            StorageFile file = await openPicker.PickSingleFileAsync();
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFolder bookFolder = await localFolder.CreateFolderAsync("books", CreationCollisionOption.OpenIfExists);
            await file.CopyAsync(bookFolder, file.Name, NameCollisionOption.ReplaceExisting);
            string title = file.DisplayName;
            string path = file.Path;
            BookInShelf bookInShelf = new BookInShelf(title, path);
            bookInShelfViewModel.AddBookItem(title, path);
        }

        private void read_Click(object sender, ItemClickEventArgs e)
        {
            BookInShelf bookInShelf = e.ClickedItem as BookInShelf;
            string title = bookInShelf.Title;
            string pages = bookInShelf.Pages;
            string[] parameter = { title, pages };
            this.Frame.Navigate(typeof(ReadPage), parameter);
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            var se = sender as Button;
            var dc = se.DataContext as BookInShelf;
            Debug.WriteLine(dc.Title);
            bookInShelfViewModel.RemoveBookItem(dc.Title);
            BookDB.removeFromBookShelf(dc.Id);
        }        
    }
}

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
using Windows.ApplicationModel.DataTransfer;
using System.Collections;

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
            initBookTypes();
            bookInShelfViewModel = BookInShelfViewModels.GetViewModel();
            bookInShelfViewModel.UpdateTile();
            bookInStoreViewModel = BookInStoreViewModels.GetViewModel();
            
        }       

        //初始化书籍的种类，用于与前端进行绑定
        private void initBookTypes()
        {
            male = new Dictionary<string, ArrayList>();
            female = new Dictionary<string, ArrayList>();
            ArrayList[] list = new ArrayList[20];
            for (int i = 0; i < 20; i++)
            {
                list[i] = new ArrayList();
            }
            male.Add("玄幻", list[0]);
            male.Add("奇幻", list[1]);
            male.Add("武侠", list[2]);
            male.Add("仙侠", list[3]);
            male.Add("都市", list[4]);
            male.Add("职场", list[5]);
            male.Add("历史", list[6]);
            male.Add("军事", list[7]);
            male.Add("游戏", list[8]);
            male.Add("竞技", list[9]);
            male.Add("科幻", list[10]);
            male.Add("灵异", list[11]);
            male.Add("同人", list[12]);
            female.Add("古代言情", list[13]);
            female.Add("现代言情", list[14]);
            female.Add("纯爱", list[15]);
            female.Add("玄幻奇幻", list[16]);
            female.Add("武侠仙侠", list[17]);
            female.Add("悬疑灵异", list[18]);
            female.Add("同人f", list[19]);
            male["玄幻"].Add("东方玄幻");
            male["玄幻"].Add("异界大陆");
            male["玄幻"].Add("异界争霸");
            male["玄幻"].Add("远古神话");
            male["奇幻"].Add("西方奇幻");
            male["奇幻"].Add("领主贵族");
            male["奇幻"].Add("亡灵异族");
            male["奇幻"].Add("魔法校园");
            male["武侠"].Add("传统武侠");
            male["武侠"].Add("新派武侠");
            male["武侠"].Add("国术武侠");
            male["仙侠"].Add("古典仙侠");
            male["仙侠"].Add("幻想修仙");
            male["仙侠"].Add("现代修仙");
            male["仙侠"].Add("洪荒封神");
            male["都市"].Add("都市生活");
            male["都市"].Add("爱情婚姻");
            male["都市"].Add("异术超能");
            male["都市"].Add("恩怨情仇");
            male["都市"].Add("青春校园");
            male["都市"].Add("现实百态");
            male["职场"].Add("娱乐明星");
            male["职场"].Add("官场沉浮");
            male["职场"].Add("商场职场");
            male["历史"].Add("穿越历史");
            male["历史"].Add("架空历史");
            male["历史"].Add("历史传记");
            male["军事"].Add("军事战争");
            male["军事"].Add("战争幻想");
            male["军事"].Add("谍战特工");
            male["军事"].Add("军旅生涯");
            male["军事"].Add("抗战烽火");
            male["游戏"].Add("游戏生涯");
            male["游戏"].Add("电子竞技");
            male["游戏"].Add("虚拟网游");
            male["游戏"].Add("游戏异界");
            male["竞技"].Add("体育竞技");
            male["竞技"].Add("篮球运动");
            male["竞技"].Add("足球运动");
            male["竞技"].Add("棋牌桌游");
            male["科幻"].Add("星际战争");
            male["科幻"].Add("时空穿梭");
            male["科幻"].Add("未来世界");
            male["科幻"].Add("古武机甲");
            male["科幻"].Add("超级科技");
            male["科幻"].Add("进化变异");
            male["科幻"].Add("末世危机");
            male["灵异"].Add("推理侦探");
            male["灵异"].Add("恐怖惊悚");
            male["灵异"].Add("悬疑探险");
            male["灵异"].Add("灵异奇谈");
            male["同人"].Add("武侠同人");
            male["同人"].Add("影视同人");
            male["同人"].Add("动漫同人");
            male["同人"].Add("游戏同人");
            male["同人"].Add("小说同人");
            female["古代言情"].Add("穿越时空");
            female["古代言情"].Add("古代历史");
            female["古代言情"].Add("古典架空");
            female["古代言情"].Add("宫闱宅斗");
            female["古代言情"].Add("经商种田");
            female["现代言情"].Add("豪门总裁");
            female["现代言情"].Add("都市生活");
            female["现代言情"].Add("婚恋情感");
            female["现代言情"].Add("商战职场");
            female["现代言情"].Add("异术超能");
            female["纯爱"].Add("古代纯爱");
            female["纯爱"].Add("现代纯爱");
            female["玄幻奇幻"].Add("玄幻异世");
            female["玄幻奇幻"].Add("奇幻魔法");
            female["武侠仙侠"].Add("武侠");
            female["武侠仙侠"].Add("仙侠");
            female["悬疑灵异"].Add("悬疑");
            female["悬疑灵异"].Add("灵异");
            female["同人f"].Add("小说同人");
            female["同人f"].Add("动漫同人");
            female["同人f"].Add("影视同人");
            female["同人f"].Add("游戏同人");
            female["同人f"].Add("耽美同人");
        }

        private Dictionary<string, ArrayList> male;
        private Dictionary<string, ArrayList> female;
        private BookInShelfViewModels bookInShelfViewModel;
        private BookInStoreViewModels bookInStoreViewModel;
        
        //点击书籍大类（男生）时，出现包含小类的菜单
        private void maleMajor_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string major = button.Name;
            Debug.WriteLine(major);
            ArrayList arrayList = male[major];
            MenuFlyout menuFlyout = new MenuFlyout();
            for (int i = 0; i < arrayList.Count; i++)
            {
                MenuFlyoutItem menuFlyoutItem = new MenuFlyoutItem();
                menuFlyoutItem.Text = (string)arrayList[i];
                menuFlyoutItem.Click += MenuFlyoutItem_Click;
                menuFlyout.Items.Add(menuFlyoutItem);
            }
            Point p = new Point(button.ActualWidth / 2.0, button.ActualHeight / 2.0);
            menuFlyout.ShowAt(button, p);
        }

        //点击书籍小类，跳转到阅读页面
        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem menuFlyoutItem = sender as MenuFlyoutItem;
            string type = menuFlyoutItem.Text;
            bookInStoreViewModel.ChangeViewModel(type);
            this.Frame.Navigate(typeof(BookStorePage));
        }

        //点击书籍大类（女生）时，出现包含小类的菜单
        private void femaleMajor_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string major = button.Name;
            ArrayList arrayList = female[major];
            MenuFlyout menuFlyout = new MenuFlyout();
            for (int i = 0; i < arrayList.Count; i++)
            {
                MenuFlyoutItem menuFlyoutItem = new MenuFlyoutItem();
                menuFlyoutItem.Text = (string)arrayList[i];
                menuFlyoutItem.Click += MenuFlyoutItem_Click;
                menuFlyout.Items.Add(menuFlyoutItem);
            }
            Point p = new Point(button.ActualWidth / 2.0, button.ActualHeight / 2.0);
            menuFlyout.ShowAt(button, p);
        }

        //点击主页面左侧菜单，切换显示书架和书籍种类
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

        //往书架添加本地txt文件
        private async void addShelf(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".txt");
            StorageFile file = await openPicker.PickSingleFileAsync();
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFolder bookFolder = await localFolder.CreateFolderAsync("books", CreationCollisionOption.OpenIfExists);
            if (file != null)
            {
                await file.CopyAsync(bookFolder, file.Name, NameCollisionOption.ReplaceExisting);
                string title = file.DisplayName;
                BookInShelf bookInShelf = new BookInShelf(title);
                bookInShelfViewModel.AddBookItem(title);
            }
        }

        //点击书架的书籍，跳转到阅读页面
        private void read_Click(object sender, ItemClickEventArgs e)
        {
            BookInShelf bookInShelf = e.ClickedItem as BookInShelf;
            string type = "0";
            string id = bookInShelf.Id;
            string title = bookInShelf.Title;
            string pages = bookInShelf.Pages;
            string[] parameter = { type, title, pages, id };
            this.Frame.Navigate(typeof(ReadPage), parameter);
        }

        //跳转到阅读页面
        private void MenuFlyoutItemRead_Click(object sender, RoutedEventArgs e)
        {
            var s = sender as FrameworkElement;
            var book = (BookInShelf)s.DataContext;
            string type = "0";
            string id = book.Id;
            string title = book.Title;
            string pages = book.Pages;
            string[] parameter = { type, title, pages, id };
            this.Frame.Navigate(typeof(ReadPage), parameter);
        }

        //删除书架中的书籍
        private void MenuFlyoutItemDelete_Click(object sender, RoutedEventArgs e)
        {
            var s = sender as FrameworkElement;
            var book = (BookInShelf)s.DataContext;
            bookInShelfViewModel.RemoveBookItem(book.Id);
        }


        private BookInShelf shareItem;
        //将书架中的书籍通过邮件分享txt文件和封面
        private void MenuFlyoutItemShare_Click(object sender, RoutedEventArgs e)
        {
            var s = sender as FrameworkElement;
            shareItem = (BookInShelf)s.DataContext;
            DataTransferManager.ShowShareUI();
        }

        //分享事件处理
        async void OnShareDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            var request = args.Request;
            var deferral = args.Request.GetDeferral();

            request.Data.Properties.Title = shareItem.Title;
            request.Data.Properties.Description = "I recommend this book to you!";
            request.Data.SetText(shareItem.Title);

            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFolder bookFolder = await localFolder.GetFolderAsync("books");
            var file = await bookFolder.GetFileAsync(shareItem.Title + ".txt");
            var photoFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/default_book.png"));

            request.Data.SetStorageItems(new List<StorageFile> { file, photoFile });
            deferral.Complete();
        }

        //导航到页面时，添加分享事件处理
        protected override void OnNavigatedTo(NavigationEventArgs e)
        { 
            DataTransferManager.GetForCurrentView().DataRequested += OnShareDataRequested;
        }

        //导航到其他页面时，取消分享事件处理
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {        
            DataTransferManager.GetForCurrentView().DataRequested -= OnShareDataRequested;
            base.OnNavigatedFrom(e);
        }
    }
}

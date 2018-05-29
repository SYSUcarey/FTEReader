using FTEReader.Models;
using FTEReader.ViewModels;
using System;
using System.Collections.Generic;
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

        private async void BookStoreListView_ItemClick(object sender, ItemClickEventArgs e)
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

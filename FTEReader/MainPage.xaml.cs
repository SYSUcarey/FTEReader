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
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            int add = 100;
            int count = 0;
            while (add >= 30)
            {
                RootObject mybook = await GetBooks.GetBook(77, count, 30);
                int num = int.Parse(mybook.result.totalNum);
                number.Text = mybook.result.totalNum;
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
    }
}

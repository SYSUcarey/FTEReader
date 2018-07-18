using FTEReader.DataBase;
using FTEReader.Models;
using FTEReader.ViewModels;
using FTEReader.WebRequest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.SpeechSynthesis;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace FTEReader
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ReadPage : Page
    {
        public bool isPlaying = false;        
        public string type;
        public string title;
        public string pages;
        public string id;
        public string bookId;
        public string nowChapter;
        public string content;
        const int wordsOnePage = 777;
        int numOfPage = 0;
        int currentPage = 1;
        Windows.Storage.StorageFile file;
        Encoding x;
        private ChapterItemViewModels chapters;
        public ReadPage()
        {
            this.InitializeComponent();
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Parchment.jpg", UriKind.Absolute));
            globe.Background = imageBrush;
        }

        //导航到其他页面时，清空目录，将当前阅读的椰树保存到viewmodel中
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (type == "0")
            {
                BookInShelfViewModels bookInShelfViewModel = BookInShelfViewModels.GetViewModel();
                bookInShelfViewModel.changePages(id, currentPage.ToString());
            }
            chapters.ChapterItems.Clear();
        }

        //导航到此页面时，根据书籍的来源（书架/书库）加载书籍内容
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            string[] book = e.Parameter as string[];
            type = book[0];
            chapters = ChapterItemViewModels.GetViewModel();
            if (type == "0")
            {
                chapters.ChapterItems.Clear();
                title = book[1];
                //Debug.WriteLine(title);
                pages = book[2];
                id = book[3];
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFolder bookFolder = await localFolder.GetFolderAsync("books");
                file = await bookFolder.GetFileAsync(title + ".txt");

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var encoding = Encoding.GetEncoding(0);
                //encoding = Encoding.UTF8;
                x = encoding;
                using (Stream stream = await file.OpenStreamForReadAsync())
                {
                    using (StreamReader reader = new StreamReader(stream, encoding, false))
                    {
                        string text = reader.ReadToEnd();
                        numOfPage = text.Length / wordsOnePage + 1;
                        currentPage = int.Parse(pages);
                        string result = text.Substring((currentPage - 1) * wordsOnePage, wordsOnePage);
                        B1.Text = result;
                    }
                }
            }
            if (type == "1")
            {                
                bookId = (string)book[1];
                try
                {
                    List<string> chapterStrs = await BookService.GetChapterList(bookId);
                    chapters.add(chapterStrs);
                    nowChapter = (string)book[2];
                    content = (string)book[3];
                    string text = content;
                    numOfPage = text.Length / wordsOnePage + 1;
                    currentPage = 1;
                    string result = text.Substring((currentPage - 1) * wordsOnePage, wordsOnePage);
                    B1.Text = result;
                }
                catch (Exception exception)
                {
                    B1.Text = "无法获得对应的书籍！";
                }
            }
        }      

        //语音播放按键处理
        private async void playButton_Click(object sender, RoutedEventArgs e)
        {
            await play();
        }

        //语音朗读
        private async Task<int> play()
        {
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            SpeechSynthesisStream stream = await synthesizer.SynthesizeTextToStreamAsync(B1.Text);
            media_element.SetSource(stream, stream.ContentType);
            isPlaying = true;
            media_element.Play();
            return 0;
        }

        //根据书籍来源（书架/书库）加载上一页
        private async void preButton_Click(object sender, RoutedEventArgs e)
        {
            preButton.IsEnabled = false;
            nextButton.IsEnabled = false;
            media_element.Stop();            
            if (type == "0")
            {
                if (currentPage == 1)
                {
                    preButton.IsEnabled = true;
                    nextButton.IsEnabled = true;
                    if (isPlaying)
                    {
                        await play();
                    }
                    return;
                }
                currentPage--;
                using (Stream stream = await file.OpenStreamForReadAsync())
                {
                    using (StreamReader reader = new StreamReader(stream, x, false))
                    {
                        string text = reader.ReadToEnd();
                        numOfPage = text.Length / wordsOnePage + 1;
                        string result = text.Substring((currentPage - 1) * wordsOnePage, wordsOnePage);
                        B1.Text = result;
                    }
                }
            }
            if (type == "1")
            {
                string result = null;
                if (currentPage == 1)
                {
                    if (nowChapter == "1")
                    {
                        MessageDialog message = new MessageDialog("已经是第一页", "提示");
                        await message.ShowAsync();
                        return;
                    }
                    int num = int.Parse(nowChapter);
                    nowChapter = (--num).ToString();
                    content = await BookService.GetChapterContent(bookId, nowChapter);
                    currentPage = content.Length / wordsOnePage + 1;
                    numOfPage = currentPage;
                    result = content.Substring((currentPage - 1) * wordsOnePage, content.Length % wordsOnePage);
                }
                else
                {
                    currentPage--;
                    result = content.Substring((currentPage - 1) * wordsOnePage, wordsOnePage);
                }
                B1.Text = result; 
            }
            preButton.IsEnabled = true;
            nextButton.IsEnabled = true;
            if (isPlaying)
            {
                await play();
            }
        }

        //根据书籍来源（书架/书库）加载下一页
        private async void nextButton_Click(object sender, RoutedEventArgs e)
        {
            media_element.Stop();
            preButton.IsEnabled = false;
            nextButton.IsEnabled = false;
            if (type == "0")
            {
                if (currentPage == numOfPage)
                {
                    preButton.IsEnabled = true;
                    nextButton.IsEnabled = true;
                    if (isPlaying)
                    {
                        await play();
                    }
                    return;
                }
                currentPage++;
                using (Stream stream = await file.OpenStreamForReadAsync())
                {
                    using (StreamReader reader = new StreamReader(stream, x, false))
                    {
                        string text = reader.ReadToEnd();
                        numOfPage = text.Length / wordsOnePage + 1;
                        string result = text.Substring((currentPage - 1) * wordsOnePage, wordsOnePage);
                        B1.Text = result;
                    }
                }
            }
            if (type == "1")
            {
                if (currentPage == numOfPage)
                {
                    int num = int.Parse(nowChapter);
                    nowChapter = (++num).ToString();
                    content = await BookService.GetChapterContent(bookId, nowChapter);                    
                    numOfPage = content.Length / wordsOnePage + 1;
                    currentPage = 1;
                    string result = content.Substring((currentPage - 1) * wordsOnePage, wordsOnePage);
                    B1.Text = result;
                }
                else
                {
                    string result;
                    currentPage++;
                    if (currentPage == numOfPage)
                    {
                        result = content.Substring((currentPage - 1) * wordsOnePage, content.Length % wordsOnePage);
                    }
                    else
                    {
                        numOfPage = content.Length / wordsOnePage + 1;
                        result = content.Substring((currentPage - 1) * wordsOnePage, wordsOnePage);
                    }
                    B1.Text = result;                    
                }                              
            }
            preButton.IsEnabled = true;
            nextButton.IsEnabled = true;
            if (isPlaying)
            {
                await play();
            }
        }

        //停止语音朗读
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            media_element.Stop();
            isPlaying = false;
        }

        //（来源为书库的书）点击目录，跳转到对应章节
        private async void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                ChapterItem selectedChapter = e.ClickedItem as ChapterItem;
                int chapterNum = selectedChapter.Num;
                nowChapter = chapterNum.ToString();
                content = await BookService.GetChapterContent(bookId, nowChapter);
                numOfPage = content.Length / wordsOnePage + 1;
                currentPage = 1;
                string result = content.Substring((currentPage - 1) * wordsOnePage, wordsOnePage);
                B1.Text = result;
            }
            catch (Exception)
            {
                MessageDialog message = new MessageDialog("无法获取对应章节！", "提示");
                await message.ShowAsync();
            }            
        }
    }
}

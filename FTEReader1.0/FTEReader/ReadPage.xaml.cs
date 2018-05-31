using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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
        const int wordsOnePage = 777;
        int numOfPage = 0;
        int currentPage = 1;
        Windows.Storage.StorageFile file;
        Encoding x;
        public ReadPage()
        {
            this.InitializeComponent();
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Parchment.jpg", UriKind.Absolute));
            globe.Background = imageBrush;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            string[] book = e.Parameter as string[];
            string title = book[0];
            Debug.WriteLine(title);
            string pages = book[1];
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
                    currentPage = 1;
                    string result = text.Substring((currentPage - 1) * wordsOnePage, wordsOnePage);
                    B1.Text = result;
                }
            }
            Select.Visibility = Visibility.Collapsed;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail; //可通过使用图片缩略图创建丰富的视觉显示，以显示文件选取器中的文件  
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".txt");


            //选取单个文件  
            file = await picker.PickSingleFileAsync();


            //文件处理  
            if (file != null)
            {
                StorageFolder applicationFolder = ApplicationData.Current.LocalFolder;
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var encoding = Encoding.GetEncoding(0);
                var dialog = new MessageDialog("txt文件的编码是？");
                dialog.Commands.Add(new UICommand("ANSI(默认)", cmd =>
                {
                    encoding = Encoding.GetEncoding(0);
                }, commandId: 0));
                dialog.Commands.Add(new UICommand("UTF-8", cmd =>
                {
                    encoding = Encoding.UTF8;
                }, commandId: 1));
                //设置默认按钮，不设置的话默认的确认按钮是第一个按钮
                dialog.DefaultCommandIndex = 0;
                dialog.CancelCommandIndex = 1;
                //获取返回值
                await dialog.ShowAsync();
                x = encoding;
                using (Stream stream = await file.OpenStreamForReadAsync())
                {
                    using (StreamReader reader = new StreamReader(stream, encoding, false))
                    {
                        string text = reader.ReadToEnd();
                        numOfPage = text.Length / wordsOnePage + 1;
                        //currentPage = ;
                        string result = text.Substring((currentPage - 1) * wordsOnePage, wordsOnePage);
                        B1.Text = result;
                    }
                }
            }
            Select.Visibility = Visibility.Collapsed;
        }

        private async void playButton_Click(object sender, RoutedEventArgs e)
        {
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            SpeechSynthesisStream stream = await synthesizer.SynthesizeTextToStreamAsync(B1.Text);
            media_element.SetSource(stream, stream.ContentType);
            media_element.Play();
        }
        private async void preButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage == 1) return;
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
        private async void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage == numOfPage) return;
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

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            media_element.Stop();
        }
    }
}

using FTEReader.DataBase;
using FTEReader.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;


namespace FTEReader.ViewModels
{
    class BookInShelfViewModels
    {
        private ObservableCollection<Models.BookInShelf> shelfItems = new ObservableCollection<Models.BookInShelf>();
        public ObservableCollection<Models.BookInShelf> ShelfItems { get { return this.shelfItems; } }

        private static BookInShelfViewModels instance;
        private BookInShelfViewModels()
        {
            ImportDB();
            UpdateTile();
        }
        public static BookInShelfViewModels GetViewModel()
        {
            if (instance == null)
            {
                instance = new BookInShelfViewModels();
            }
            return instance;
        }

        //从数据库初始化书架信息
        public void ImportDB()
        {
            shelfItems.Clear();
            ObservableCollection<string[]> books = BookDB.getBooksFromShelf();
            foreach (string[] book in books)
            {
                string id = book[0];
                string title = book[1];
                string pages = book[2];
                BookInShelf item = new BookInShelf(id, title, pages);
                shelfItems.Add(item);
            } 
        }

        //添加到书架
        public void AddBookItem(string title)
        {
            BookInShelf item = new BookInShelf(title);
            this.shelfItems.Add(item);
            BookDB.addToBookShelf(item.Id, item.Title, item.Pages);
            UpdateTile();
        }

        //从书架删除
        public void RemoveBookItem(string id)
        {
            for (int i = 0; i < ShelfItems.Count; i++)
            {
                if (ShelfItems[i].Id == id)
                {
                    BookDB.removeFromBookShelf(ShelfItems[i].Id);
                    shelfItems.Remove(ShelfItems[i]);
                }
            }
            UpdateTile();
        }

        //修改书架上书本已经阅读的页码
        public void changePages(string id, string Pages)
        {
            for (int i = 0; i < ShelfItems.Count; i++)
            {
                if (ShelfItems[i].Id == id)
                {
                    ShelfItems[i].Pages = Pages;
                }
            }
            BookDB.changePages(id, Pages);
        }

        //更新磁贴
        public void UpdateTile()
        {
            TileUpdateManager.CreateTileUpdaterForApplication().Clear();
            for (int i = 0; i < ShelfItems.Count; i++)
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(System.IO.File.ReadAllText("Tile/Tile.xml"));
                XmlNodeList textElements = document.GetElementsByTagName("text");
                foreach (var text in textElements)
                {
                    // 替换里面预设的字符串
                    if (text.InnerText.Equals("bookname"))
                    {
                        text.InnerText = ShelfItems[i].Title;
                    }
                    else if (text.InnerText.Equals("currentPage"))
                    {
                        text.InnerText = ShelfItems[i].Pages;
                    }
                }
                // Then create the tile notification
                var notification = new TileNotification(document);
                TileUpdateManager.CreateTileUpdaterForApplication().Update(notification);
                TileUpdateManager.CreateTileUpdaterForApplication().EnableNotificationQueue(true);
            }
        }
    }
}

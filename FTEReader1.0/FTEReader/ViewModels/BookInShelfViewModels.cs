using FTEReader.DataBase;
using FTEReader.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                string path = book[3];
                BookInShelf item = new BookInShelf(id, title, pages, path);
                shelfItems.Add(item);
            }
        }

        //添加到书架
        public void AddBookItem(string title, string path)
        {
            BookInShelf item = new BookInShelf(title, path);
            this.shelfItems.Add(item);
            BookDB.addToBookShelf(item.Id, item.Title, item.Pages, item.Path);
        }
        
        //从书架删除
        public void RemoveBookItem(string title)
        {
            for (int i = 0; i < ShelfItems.Count; i++)
            {
                if (ShelfItems[i].Title == title)
                {
                    BookDB.removeFromBookShelf(ShelfItems[i].Id);
                    shelfItems.Remove(ShelfItems[i]);
                }
            }
        }
    }
}

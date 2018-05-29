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
        }
        
        //从书架删除
        public void RemoveBookItem(string title)
        {
            foreach (BookInShelf item in shelfItems)
            {
                if (item.Title == title)
                {
                    BookDB.removeFromBookShelf(item.Id);
                    shelfItems.Remove(item);
                }
            }
        }
    }
}

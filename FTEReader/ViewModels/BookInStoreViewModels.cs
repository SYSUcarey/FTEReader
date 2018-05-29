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
    class BookInStoreViewModels
    {
        private ObservableCollection<Models.BookInStore> storeItems = new ObservableCollection<Models.BookInStore>();
        public ObservableCollection<Models.BookInStore> StoreItems { get { return this.storeItems; } }

        private static BookInStoreViewModels instance;
        private BookInStoreViewModels() { }

        public static BookInStoreViewModels GetViewModel()
        {
            if (instance == null)
            {
                instance = new BookInStoreViewModels();
            }
            return instance;
        }

        //搜索时切换ViewModel内容
        public void ChangeViewModel(string input)
        {
            storeItems.Clear();
            ObservableCollection<string[]> books = BookDB.findBookFromStore(input);
            foreach(string[] book in books)
            {
                string title = (string)book[0];
                string catalog = (string)book[1];
                string tags = (string)book[2];
                string info = (string)book[3];
                string image = (string)book[4];
                BookInStore item = new BookInStore(title, catalog, tags, info, image);
                storeItems.Add(item);
            }
        }
    }
}

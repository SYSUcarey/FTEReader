using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace FTEReader.DataBase
{
    //用来创建数据库
    public static class BookDB
    {
        //创建数据库的函数
        public static void LoadDatabase()
        {
            //连接数据库，若不存在则创建
            var conn = new SQLiteConnection("BookDB.db");
            //创建书库表
            string bookStore = @"CREATE TABLE IF NOT EXISTS
                                  BookStoreTable (Title TEXT,
                                                    Catalog TEXT,
                                                    Tags TEXT,
                                                    Info TEXT,
                                                    Image TEXT,
                                                    BookId TEXT,
                                                    Author TEXT,
                                                    CompatibeMen TEXT,
                                                    NowChac TEXT
                                                    );";
            using (var statement = conn.Prepare(bookStore))
            {
                statement.Step();
            }
            //创建书架表
            string bookShelf = @"CREATE TABLE IF NOT EXISTS
                                  BookShelfTable (Id TEXT PRIMARY KEY NOT NULL,
                                                    Title TEXT,
                                                    Pages TEXT
                                                    );";
            using (var statement = conn.Prepare(bookShelf))
            {
                statement.Step();
            }
        }
        //添加书本到书库
        public static void addToBookStore(string Title, string Catalog, string Tags, string Info, string Image,
                                            string bookId, string author, string compatibeMen, string nowChac)
        {
            using (var conn = new SQLiteConnection("BookDB.db"))
            {
                using (var statement = conn.Prepare("INSERT INTO BookStoreTable (Title, Catalog, Tags, Info," +
                    " Image, BookId, Author, CompatibeMen, NowChac) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)"))
                {
                    statement.Bind(1, Title);
                    statement.Bind(2, Catalog);
                    statement.Bind(3, Tags);
                    statement.Bind(4, Info);
                    statement.Bind(5, Image);
                    statement.Bind(6, bookId);
                    statement.Bind(7, author);
                    statement.Bind(8, compatibeMen);
                    statement.Bind(9, nowChac);
                    statement.Step();
                }
            }
        }
        //添加书本到书架
        public static void addToBookShelf(string Id, string Title, string Pages)
        {
            using (var conn = new SQLiteConnection("BookDB.db"))
            {
                using (var statement = conn.Prepare("INSERT INTO BookShelfTable (Id, Title, Pages) VALUES (?, ?, ?)"))
                {
                    statement.Bind(1, Id);
                    statement.Bind(2, Title);
                    statement.Bind(3, Pages);
                    statement.Step();
                }
            }
        }
        //从书架中删除
        public static void removeFromBookShelf(string Id)
        {
            using (var conn = new SQLiteConnection("BookDB.db"))
            {
                using (var statement = conn.Prepare("DELETE FROM BookShelfTable WHERE Id = ?"))
                {
                    statement.Bind(1, Id);
                    statement.Step();
                }
            }
        }
        //获取书架中书本信息
        public static ObservableCollection<string[]> getBooksFromShelf()
        {
            ObservableCollection<string[]> books = new ObservableCollection<string[]>();
            using (var conn = new SQLiteConnection("BookDB.db"))
            {
                using (var statement = conn.Prepare("SELECT Id, Title, Pages FROM BookShelfTable"))
                {
                    while (SQLiteResult.ROW == statement.Step())
                    {
                        string id = (string)statement[0];
                        string title = (string)statement[1];
                        string pages = (string)statement[2];
                        string[] book = { id, title, pages};
                        books.Add(book);
                    }
                }
            }
            return books;
        }
        //从书库中搜索满足条件的书本信息
        public static ObservableCollection<string[]> findBookFromStore(string input)
        {
            int count = 0;
            ObservableCollection<string[]> books = new ObservableCollection<string[]>();
            using (var conn = new SQLiteConnection("BookDB.db"))
            {
                using (var statement = conn.Prepare("SELECT Title, Catalog, Tags, Info, Image, BookId, Author, CompatibeMen, NowChac FROM BookStoreTable"))
                {
                    while (SQLiteResult.ROW == statement.Step() && count < 200)
                    {
                        string title = (string)statement[0];
                        string catalog = (string)statement[1];
                        string tags = (string)statement[2];
                        string info = (string)statement[3];
                        string image = (string)statement[4];
                        string bookId = (string)statement[5];
                        string author = (string)statement[6];
                        string compatibeMen = (string)statement[7];
                        string NowChac = (string)statement[8];
                        if (title.Contains(input) || catalog.Contains(input) || tags.Contains(input) || info.Contains(input))
                        {
                            string[] book = { title, catalog, tags, info, image, bookId, author, compatibeMen, NowChac };
                            if (!books.Contains(book)) books.Add(book);
                            count++;
                        }
                    }
                }
            }
            return books;
        }

        //修改书架上书本已经阅读的页码
        public static void changePages(string Id, string Pages)
        {
            using (var conn = new SQLiteConnection("BookDB.db"))
            {
                using (var statement = conn.Prepare("UPDATE BookShelfTable SET Pages = ? WHERE Id = ?"))
                {
                    statement.Bind(1, Pages);
                    statement.Bind(2, Id);
                    statement.Step();
                }
            }
        }
    }
}

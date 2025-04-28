// WbFile.cs
using System;
using System.Collections.Generic;
using System.IO;
using WSBit41JJY.Data;
using WSBit41JJY.Lib;

namespace WSBit41JJY.File
{
    internal static class WbFile
    {
        private const string BOOK_FILENAME = "books.txt";

        public static void Write_Book(List<Book> books)
        {
            StreamWriter writer = new StreamWriter(BOOK_FILENAME);
            writer.WriteLine(books.Count);

            for (int i = 0; i < books.Count; i++)
            {
                Book account = (Book)books[i];
                string temp = string.Empty;
                temp = account.Title + "@" + account.Price + "@" + account.Author + "@";
                writer.WriteLine(temp);
            }
            // using > { } 블록을 벗어나면 자동으로 Dispose() 호출
            writer.Dispose();
        }

        public static void Read_Books(List<Book> books)
        {
            StreamReader reader = new StreamReader(BOOK_FILENAME);
            int size = int.Parse(reader.ReadLine());

            for (int i = 0; i < size; i++)
            {
                string temp = reader.ReadLine();        // 번호@이름@잔액@날짜
                string[] sp = temp.Split('@');
                string title = sp[0];
                int price = int.Parse(sp[1]);
                string author = sp[2];

                books.Add(new Book(title, price, author));
            }
            // using > { } 블록을 벗어나면 자동으로 Dispose() 호출
            reader.Dispose();
        }
    }
}

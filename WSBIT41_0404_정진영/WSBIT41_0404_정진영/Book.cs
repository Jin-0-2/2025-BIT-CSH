// book.cs
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using WSBit41JJY.Lib;

namespace WSBit41JJY.Data
{
    internal class Book  : IComparable
    {
        #region 1.멤버필드, 프로퍼티(속성)
        public string Title { get; private set; }
        public int Price { get; private set; }
        public string Author { get; private set; }
        #endregion

        #region 2. 생성자
        public Book(string _title, int _price, string _author)
        {
            Title = _title;
            Price = _price;
            Author = _author;
        }

        #endregion

        #region 3. 기능 메서드

        public int CompareTo(object obj)                        // 정렬 시에 필요
        {
            Book book = obj as Book;
            if (book == null)
            {
                throw new ArgumentException("Book 개체가 아니다.");
            }
            return Title.CompareTo(book.Title);
        }
        public void Update_Price(int _price)
        {
            if (_price <= 0)
                throw new Exception("도서 가격은 0원 이상이어야 합니다.");

            Price = _price;
        }
        public void Print()
        {
            // 한줄에 출력
            Console.Write(Title + "\t");
            Console.Write(Price + "\t");
            Console.WriteLine(Author);
        }
        public void Println()
        {
            // 여러줄에 출력
            Console.WriteLine("[도서] " + Title);
            Console.WriteLine("[가격] "+ Price);
            Console.WriteLine("[저자] "+ Author);
        }
        #endregion
    }
}

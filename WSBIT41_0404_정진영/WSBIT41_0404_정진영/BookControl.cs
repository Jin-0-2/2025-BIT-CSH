// BookControl.cs
using System;
using System.Collections.Generic;
using System.Globalization;
using WSBit41JJY.Data;
using WSBit41JJY.File;
using WSBit41JJY.Lib;

namespace WSBit41JJY_0404
{
    internal class BookControl
    {
        private List<Book> books = new List<Book>();           // 계좌저장
        //private List<BookIO> accountios = new List<BookIO>();     // 거래내역 저장

        #region 0. 싱글톤 패턴
        public static BookControl singleton { get; } = null;

        static BookControl() { singleton = new BookControl(); }

        // 디폴트 생성자를 은닉(private) 시킴
        private BookControl()
        {

        }
        #endregion


        #region 1. 임시 데이터
       
        #endregion

        #region 2. 기능 메서드
        public void BookInsert()
        {
            try
            {
                Console.WriteLine("\n[책 저장]\n");

                string title = WbLib.InputString("책 이름 입력");
                int price = WbLib.InputNumber("책 가격 입력");
                string author = WbLib.InputString("저자 입력");

                //계좌 저장
                Book book = new Book(title, price, author);
                books.Add(book);

                Console.WriteLine("[책 저장성공]");

            }
            catch (Exception ex)
            {
                Console.WriteLine("[책 저장 실패] " + ex.Message);
            }
        }
        public void BookPrintAll()
        {
            Console.WriteLine("[저장 개수 : {0}개]\n", books.Count);
            for (int i = 0; i < books.Count; i++)
            {
                Book account = books[i];
                account.Print();
            }
        }

        public void SelectBook()
        {
            try
            {
                Console.WriteLine("\n[책 검색]\n");

                string title = WbLib.InputString("도서명 입력");

                Book book = TitleToBook(title);
                //계좌 정보 출력
                book.Println();
                Console.WriteLine("[책 검색 성공]");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[책 검색 실패] " + ex.Message);
            }
        }
        public void BookUpdate()
        {
            try
            {
                Console.WriteLine("\n[도서 가격 수정\n");

                string title = WbLib.InputString("도서명 입력");
                int price = WbLib.InputNumber("수정 가격 입력");

                Book book = TitleToBook(title);
                book.Update_Price(price);

                Console.WriteLine("도서 가격 수정 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[도서 가격 수정 실패] " + ex.Message);
            }
        }

        public void BookDelete()
        {
            try
            {
                Console.WriteLine("\n[도서 삭제]\n");

                string title = WbLib.InputString("삭제할 도서 명 입력");

                int idx = books.FindIndex(x => x.Title == title);
                books.RemoveAt(idx);

                Console.WriteLine("[도서 삭제 성공]");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[도서 삭제 실패] " + ex.Message);
            }
        }

        public void BookSort()
        {
            books.Sort();
            Console.WriteLine("[도서 정렬]");

        }

        #endregion

        #region 3. 내부에서 사용되는 메서드(공개 필요 X)

        private Book TitleToBook(string _title)
        {
            for (int i = 0; i < books.Count; i++)
            {
                Book book = (Book)books[i];
                if (book.Title == _title)
                    return book;
            }
            throw new Exception("없는 책입니다.");
        }

        #endregion
        #region 4. 시작/종료 메서드
        public void Init()
        {
            try
            {
                WbFile.Read_Books(books);
                Console.WriteLine("파일 로드 성공....");
            }
            catch (Exception ex)
            {
                Console.WriteLine("파일 로드 실패(최초실행).... ");
                Console.WriteLine(ex.Message);
            }
            WbLib.Pause();
        }
        public void Exit()
        {
            WbFile.Write_Book(books);
        }
        #endregion
    }
}

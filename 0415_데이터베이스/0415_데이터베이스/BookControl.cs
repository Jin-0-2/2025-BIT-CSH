using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _0415_데이터베이스
{
    internal class BookControl
    {
        #region 0. 싱글톤 패턴
        public static BookControl singleton { get; } = null;

        static BookControl() { singleton = new BookControl(); }

        // 디폴트 생성자를 은닉(private) 시킴
        private BookControl()
        {
            // logmanager = new LogManager(); 이 상황은 만들어지지 않은 BookControl의 객체를 접근함.
        }
        #endregion

        MyDB db = MyDB.singleton;

        #region 1. 기능 메서드
        public void BookInsert()
        {
            try
            {
                string name = WbLib.InputString("도서 이름");
                int pirce   = WbLib.InputNumber("도서 가격");
                string desc = WbLib.InputString("설명 입력");

                if(db.Insert_Book(name, pirce, desc) == false)
                {
                    throw new Exception("도서 추가 실패");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void BookSelect()
        {
            try
            {
                int pid = WbLib.InputNumber("도서 ID 입력");

                Book book = db.SelectBook(pid);
                if (book == null)
                    throw new Exception("도서 검색 실패");

                book.Println();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void BookSelect_Name()
        {
            try
            {
                string pname = WbLib.InputString("도서 이름 입력");
                // int pid = WbLib.InputNumber("도서 ID 입력");

                Book book = db.SelectBook_Name(pname);
                if (book == null)
                    throw new Exception("도서 검색 실패");

                book.Println();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void BookUpdate()
        {
            try
            {
                int pid   = WbLib.InputNumber("도서 ID 입력");
                int price = WbLib.InputNumber("수정할 가격 입력: ");

                if(db.Update_Book1(pid, price) ==false)
                {
                    throw new Exception("도서 수정 실패");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void BookDelete()
        {
            try
            {
                int pid = WbLib.InputNumber("삭제할 도서 ID 입력");

                if(db.Delete_Book1(pid) == false)
                {
                    throw new Exception("도서 삭제 실패");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void BookPrintAll()
        {
            try
            {
                List<Book> books = db.SelectAll();
                if(books == null)
                {
                    Console.WriteLine("없슈");
                    return;
                }

                Console.WriteLine($"저장갯수: {books.Count} 권");

                foreach (Book b in books)
                {
                    b.BookPrint();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 2. 시작/종료 메서드
        public void Init()
        {
            if (db.Connect() == false)
                return;
            Console.WriteLine("연결 성공");
        }
        public void Exit()
        {
            if (db.Close() == true)
                Console.WriteLine("DB연결종료");
        }
        #endregion

    }
}

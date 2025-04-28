using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0415_데이터베이스_Sale
{
    internal class SaleContorl
    {
        #region 0. 싱글톤 패턴
        public static SaleContorl singleton { get; } = null;

        static SaleContorl() { singleton = new SaleContorl(); }

        // 디폴트 생성자를 은닉(private) 시킴
        private SaleContorl() { }
        #endregion

        MyDB db = MyDB.singleton;

        #region 1. 기능 메서드
        public void SaleInsert()
        {
            try
            {
                string cname = WbLib.InputString("고객 이름");
                string pname = WbLib.InputString("도서 이름");
                int count = WbLib.InputNumber("도서 구매 갯수");

                if (db.Insert_Sale(cname, pname, count) == false)
                {
                    throw new Exception("판매 내역 실패");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void SaleSelect_Name()
        {
            try
            {
                string cname = WbLib.InputString("고객 이름 입력");
                // int pid = WbLib.InputNumber("도서 ID 입력");

                if(db.SelectSale_Name(cname) == false)
                    throw new Exception("구매 내역 검색");

                Console.WriteLine("구매 내역 검색 성공!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void SaleSelect_Name_del(string cname)
        {
            try
            { 
                if (db.SelectSale_Name(cname) == false)
                    throw new Exception("구매 내역 검색");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void SaleSelect_Name_Update(string cname)
        {
            try
            {
                if (db.SelectSale_Name(cname) == false)
                    throw new Exception("구매 내역 검색");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void SaleUpdate()
        {
            try
            {
                string cname = WbLib.InputString("구매 내역을 수정 할 고객 이름 입력");
                SaleSelect_Name_Update(cname);
                string pname = WbLib.InputString("수정할 도서 이름 입력");
                int count = WbLib.InputNumber("수정할 갯수");

                if (db.Update_Sale2(cname, pname, count) == false)
                {
                    throw new Exception("도서 구매 내역 수정 실패");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void SaleDelete()
        {
            try
            {
                string cname = WbLib.InputString("구매 내역을 삭제 할 고객 이름 입력");
                SaleSelect_Name_del(cname);
                string pname = WbLib.InputString("삭제할 도서 이름 입력");
                if (db.Delete_Sale2(cname, pname) == false)
                {
                    throw new Exception("도서 삭제 실패");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void SalePrintAll()
        {
            try
            {
                List<Sale> sales = db.Select_Sale_All();
                if (sales == null)
                {
                    Console.WriteLine("없슈");
                    return;
                }

                Console.WriteLine($"판매 내역: {sales.Count}");

                foreach (Sale b in sales)
                {
                    b.SalePrint();
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
                List<Book> books = db.Select_Book_All();
                if (books == null)
                {
                    Console.WriteLine("없슈");
                    return;
                }

                Console.WriteLine($"저장갯수: {books.Count} 권");

                foreach (Book b in books)
                {
                    b.BookPrint();
                }
                Console.WriteLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void CustomPrintAll()
        {
            try
            {
                List<Custom> cousts = db.Selec_Custom_All();
                if (cousts == null)
                {
                    Console.WriteLine("없슈");
                    return;
                }

                Console.WriteLine($"저장갯수: {cousts.Count} 명");

                foreach (Custom b in cousts)
                {
                    b.CustomPrint();
                }
                Console.WriteLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void BestSeller()
        {
            try
            {
                db.bestseller();
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        
        public void TotalPrice()
        {
            db.TotalPrice();
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0415_데이터베이스_Custom
{
    internal class CustomControl
    {
        #region 0. 싱글톤 패턴
        public static CustomControl singleton { get; } = null;

        static CustomControl() { singleton = new CustomControl(); }

        // 디폴트 생성자를 은닉(private) 시킴
        private CustomControl()
        {
            // logmanager = new LogManager(); 이 상황은 만들어지지 않은 CustomControl의 객체를 접근함.
        }
        #endregion

        MyDB db = MyDB.singleton;

        #region 1. 기능 메서드
        public void CustomInsert()
        {
            try
            {
                string name = WbLib.InputString("회원 이름");
                string phone = WbLib.InputString("전화 번호");
                string addr = WbLib.InputString("주소 입력");

                if (db.Insert_Custom(name, phone, addr) == false)
                {
                    throw new Exception("도서 추가 실패");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void CustomSelect()
        {
            try
            {
                int pid = WbLib.InputNumber("도서 ID 입력");

                Custom book = db.SelectCustom(pid);
                if (book == null)
                    throw new Exception("도서 검색 실패");

                book.Println();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void CustomSelect_Name()
        {
            try
            {
                string pname = WbLib.InputString("회원 이름 입력");
                // int pid = WbLib.InputNumber("도서 ID 입력");

                Custom book = db.SelectCustom_Name(pname);
                if (book == null)
                    throw new Exception("도서 검색 실패");

                book.Println();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void CustomUpdate()
        {
            try
            {
                int pid = WbLib.InputNumber("회원 ID 입력");
                string phone = WbLib.InputString("수정할 전화번호 입력: ");

                if (db.Update_Custom1(pid, phone) == false)
                {
                    throw new Exception("도서 수정 실패");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void CustomDelete()
        {
            try
            {
                int pid = WbLib.InputNumber("삭제할 회원 ID 입력");

                if (db.Delete_Custom1(pid) == false)
                {
                    throw new Exception("도서 삭제 실패");
                }
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
                List<Custom> cousts = db.SelectAll();
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

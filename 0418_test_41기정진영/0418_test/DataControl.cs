// Control.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0418_test
{
    internal class DataControl
    {
        #region 0. 싱글톤 패턴
        public static DataControl singleton { get; } = null;

        static DataControl() { singleton = new DataControl(); }

        // 디폴트 생성자를 은닉(private) 시킴
        private DataControl() { }
        #endregion

        MyDB db = MyDB.singleton;
        List<Jumsu> datas = new List<Jumsu>();

        #region 1. 기능 메서드
        public void DataInsert()
        {
            try
            {
                int stu_id  = WbLib.InputNumber("학생 아이디");
                int sub_id  = WbLib.InputNumber("과목 아이디");
                int jumsu  = WbLib.InputNumber("점수");

                if (db.Insert_Data(stu_id, sub_id, jumsu) == false)
                {
                    throw new Exception("점수 저장 실패");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DataSelect()
        {
            try
            {
                int stu_id = WbLib.InputNumber("학생 아이디");
                int sub_id = WbLib.InputNumber("과목 아이디");

                if (db.SelectData_Name(stu_id, sub_id) == false)
                    throw new Exception("점수 내역 검색 실패");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DataUpdate()
        {
            try
            {
                int stu_id = WbLib.InputNumber("학생 아이디");
                int sub_id = WbLib.InputNumber("과목 아이디");
                int jumsu = WbLib.InputNumber("수정 할 점수");

                if (db.Update_Data2(stu_id, sub_id, jumsu) == false)
                {
                    throw new Exception("점수 수정 실패");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DataDelete()
        {
            try
            {
                int stu_id = WbLib.InputNumber("학생 아이디");
                int sub_id = WbLib.InputNumber("과목 아이디");

                if (db.Delete_Data2(stu_id, sub_id) == false)
                {
                    throw new Exception("도서 삭제 실패");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DataSelect_Names()
        {
            try
            {
                int stu_id = WbLib.InputNumber("학생 아이디");
                int sub_id = WbLib.InputNumber("과목 아이디");

                if (db.SelectData_Names(stu_id, sub_id) == false)
                {
                    throw new Exception("검색 실패");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void JunsuPrintAll()
        {
            try
            {
                List<Jumsu> jumsus = db.Select_Jumsu_All();
                if (jumsus == null)
                {
                    Console.WriteLine("없슈");
                    return;
                }

                Console.WriteLine("[점수] ");

                foreach (Jumsu b in jumsus)
                {
                    Console.Write(b.ToString() + "\t");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void BestStudent()
        {
            try
            {
                string sub_name = string.Empty;

                Console.Write("과목명: ");
                sub_name = Console.ReadLine();

                db.beststu(sub_name);
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


        #region 다른 테이블 부를 때 사용
        public void StudentPrintAll()
        {
            try
            {
                List<Student> stus = db.Select_Student_All();
                if (stus == null)
                {
                    Console.WriteLine("없슈");
                    return;
                }

                Console.WriteLine("[학생] ");
                foreach (Student b in stus)
                {
                    b.Print();
                    Console.Write("\t");
                }
                Console.WriteLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void SubjectPrintAll()
        {
            try
            {
                List<Subject> cousts = db.Selec_Subject_All();
                if (cousts == null)
                {
                    Console.WriteLine("없슈");
                    return;
                }


                Console.WriteLine("[과목] ");
                foreach (Subject b in cousts)
                {
                    b.Print();
                    Console.Write("\t");
                }
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0418_test
{
    internal class Student
    {
        #region 멤버들
        public int Stu_Id { get; private set; }
        public string Stu_name { get; private set; }
        #endregion

        #region 생성자
        public Student()
        {

        }
        public Student(int id, string name)
        {
            Stu_Id = id;
            Stu_name = name;

        }
        #endregion

        public void Print()
        {
            Console.Write(Stu_Id + ":");
            Console.Write(Stu_name);
        }

        public void Println()
        {
            // 여러줄에 출력
            Console.WriteLine("[SID] " + Stu_Id);
            Console.WriteLine("[NAME] " + Stu_name);
        }

        public override string ToString()
        {
            return Stu_Id + ":" + Stu_name;
        }
    }
}

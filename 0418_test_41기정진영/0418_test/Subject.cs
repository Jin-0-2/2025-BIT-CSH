using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0418_test
{
    internal class Subject
    {
        #region 멤버들
        public int Sub_Id { get; private set; }
        public string Sub_name { get; private set; }
        #endregion

        #region 생성자
        public Subject()
        {

        }
        public Subject(int id, string name)
        {
            Sub_Id = id;
            Sub_name = name;

        }
        #endregion

        public void Print()
        {
            Console.Write(Sub_Id + ":");
            Console.Write(Sub_name);
        }

        public void Println()
        {
            // 여러줄에 출력
            Console.WriteLine("[ID] " + Sub_Id);
            Console.WriteLine("[NAME] " + Sub_name);
        }

        public override string ToString()
        {
            return Sub_Id + ":" + Sub_name;
        }
    }
}

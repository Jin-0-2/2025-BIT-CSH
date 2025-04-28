// .cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _0418_test
{
    internal class Jumsu
    {
        #region 멤버들
        public int Jumsu_Id { get; private set; }
        public int Stu_Id { get; private set; }

        public int Sub_Id { get; private set; }
        public int Jumsu_Num { get; private set; }  
        #endregion

        #region 생성자
        public Jumsu()
        {

        }
        public Jumsu(int id, int stu_id, int sub_id, int num)
        {
            Jumsu_Id = id;
            Stu_Id = stu_id;
            Sub_Id = sub_id;
            Jumsu_Num = num;
        }
        #endregion

        public void Print()
        {
            Console.Write(Jumsu_Id + "\t");
            Console.Write(Stu_Id + "\t");
            Console.Write(Sub_Id + "\t");
            Console.WriteLine(Jumsu_Num + "\t");
        }

        public void Println()
        {
            // 여러줄에 출력
            Console.WriteLine("[JID] " + Jumsu_Id);
            Console.WriteLine("[STU_ID] " + Stu_Id);
            Console.WriteLine("[SUB_ID] " + Sub_Id);
            Console.WriteLine("[JUMSU] " + Jumsu_Num);
        }

        public override string ToString()
        {
            return Jumsu_Id + "-" + Stu_Id + "-" + Sub_Id + "-" + Jumsu_Num;
        }
    }
}

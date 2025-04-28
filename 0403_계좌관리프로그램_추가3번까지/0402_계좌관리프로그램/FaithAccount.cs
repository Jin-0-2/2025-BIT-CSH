using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSBit41JJY.Lib;

namespace WSBit41JJY.Data
{
    internal class FaithAccount : Account
    {
        public FaithAccount(int number, string name, int balacne) : base(number, name, balacne + (int)(balacne*0.01), DateTime.Now)
        {

        }
        public FaithAccount(int number, string name, int balacne, DateTime ctime) : base(number, name, balacne, ctime)
        {

        }
        public override void Input_Money(int money)
        {
            money = money + (int)(money * 0.01);
            UpdateBalance(money);
        }

        public override void Print()
        {
            // 한줄에 출력
            Console.Write("[신용계좌]" + "\t");
            Console.Write(Number + "\t");
            Console.Write(Name + "\t");
            Console.Write(Balance + "\t");
            Console.Write(WbLib.Get_Date(Ctime) + "\t");
            Console.WriteLine(WbLib.Get_Time(Ctime));
        }
    }
}

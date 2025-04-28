using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSBit41JJY.Lib;

namespace WSBit41JJY.Data
{
    internal class ContriAccount : Account
    {
        public int Contribution {  get; private set; }

        public ContriAccount(int number, string name, int balacne) : base(number, name, 0, DateTime.Now)
        {
            Contribution = (int)(balacne * 0.01);
            Balance = balacne - Contribution;
        }
        public ContriAccount(int number, string name, int balacne, DateTime ctime, int contribution):base(number, name, balacne, ctime)
        {
            Contribution = contribution;
        }

        public override void Input_Money(int money)
        {
            int netMoney = money - (int)(money * 0.01);
            UpdateBalance(netMoney);
        }

        public void Add_Money(int money)
        {
            Input_Money(money);
            Contribution += (int)(money * 0.01);
        }

        public override void Print()
        {
            Console.Write("[기부계좌]" + "\t");
            Console.Write(Number + "\t");
            Console.Write(Name + "\t");
            Console.Write(Balance + "\t");
            Console.Write(WbLib.Get_Date(Ctime) + "\t");
            Console.Write(WbLib.Get_Time(Ctime) + "\t");
            Console.WriteLine("[기부액]"+ Contribution + "원");
        }

        public override void Println()
        {
            Console.WriteLine("[번호]   " + Number);
            Console.WriteLine("[이름]   " + Name);
            Console.WriteLine("[잔액]   " + Balance + "원");
            Console.WriteLine("[기부액] " + Contribution + "원");
            Console.WriteLine("[날짜]   " + WbLib.Get_Date(Ctime));
            Console.WriteLine("[시간]   " + WbLib.Get_Time(Ctime));
        }
    }
}

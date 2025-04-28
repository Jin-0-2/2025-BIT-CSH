// account.cs
using System;
using System.Collections.Generic;
using WSBit41JJY.Lib;

namespace WSBit41JJY.Data
{
    internal class NameComparer : IComparer<Account>
    {
        public int Compare(Account x, Account y)
        {
            if (x == null || y == null)
                throw new ArgumentException("비교 대상이 Account가 아닙니다.");

            return x.Name.CompareTo(y.Name); // 이름을 기준으로 정렬
        }
    }

    internal class NumberComparer : IComparer<Account>
    {
        public int Compare(Account x, Account y)
        {
            if (x == null || y == null)
                throw new ArgumentException("비교 대상이 Account가 아닙니다.");

            return x.Number.CompareTo(y.Number); // 이름을 기준으로 정렬
        }
    }
    internal class Account : IComparable
    {
        #region 1.멤버필드, 프로퍼티(속성)
        public int Number { get; private set; }
        public string Name { get; private set; }
        public int Balance { get; protected set; }
        public DateTime Ctime { get; private set; }
        #endregion

        #region 2. 생성자
        public Account(int _number, string _name, int _balance)
        {
            Number = _number;
            Name = _name;
            Balance = _balance;
            Ctime = DateTime.Now;       // 로컬타임
        }

        public Account(int _number, string _name, int _balance, DateTime _ctime)
        {
            Number = _number;
            Name = _name;
            Balance = _balance;
            Ctime = _ctime;             // 인자로 받은 타임
        }
        #endregion

        #region 3. 기능 메서드

        public int CompareTo(object obj)
        {
            Account account = obj as Account;
            if (account == null)
            {
                throw new ArgumentException("Account 개체가 아니다.");
            }
            return Name.CompareTo(account.Name);
        }
        public virtual void Input_Money(int money)
        {
            if (money <= 0)
                throw new Exception("입금액은 0원 이상이어야 합니다.");

            Balance = Balance + money;
        }
        public void Output_Money(int money)
        {
            if (money <= 0)
                throw new Exception("잘못된 금액.");
            if(Balance < money)
                throw new Exception(string.Format("잔액 부족 - 잔액 {0}, 요청금액 {1}", Balance, money));

            Balance = Balance - money;
        }

        public void UpdateBalance(int amount)
        {
            Balance += amount;
        }
        public virtual void Print()
        {
            // 한줄에 출력
            Console.Write("[일반계좌]" + "\t");
            Console.Write(Number + "\t");
            Console.Write(Name + "\t");
            Console.Write(Balance + "\t");
            Console.Write(WbLib.Get_Date(Ctime) + "\t");
            Console.WriteLine(WbLib.Get_Time(Ctime));
        }
        public virtual void Println()
        {
            // 여러줄에 출력
            Console.WriteLine("[번호] " + Number);
            Console.WriteLine("[이름] " + Name);
            Console.WriteLine("[잔액] " + Balance + "원");
            Console.WriteLine("[날짜] " + WbLib.Get_Date(Ctime));
            Console.WriteLine("[시간] " + WbLib.Get_Time(Ctime));
        }
        #endregion
    }
}

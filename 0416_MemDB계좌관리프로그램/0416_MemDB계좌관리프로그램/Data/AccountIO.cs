using System;
using WSBit41JJY.Lib;

namespace WSBit41JJY.Data
{
    internal class AccountIO
    {
        #region 1.멤버필드, 프로퍼티(속성)
        public int Aid { get; private set; }
        public int Number { get; private set; }
        public int Input { get; private set; }
        public int Output { get; private set; }
        public int Balance { get; private set; }
        public DateTime Ctime { get; private set; }
        #endregion

        #region 2. 생성자
        public AccountIO(int _aid, int _number, int _input, int _output, int _balance)
        {
            Aid = _aid;
            Number = _number;
            Input = _input;
            Output = _output;
            Balance = _balance;
            Ctime = DateTime.Now;       // 로컬타임
        }

        public AccountIO(int _aid, int _number, int _input, int _output, int _balance, DateTime _ctime)
        {
            Aid = _aid;
            Number = _number;
            Input = _input;
            Output = _output;
            Balance = _balance;
            Ctime = _ctime;             // 인자로 받은 타임
        }
        #endregion

        #region 3. 기능 메서드
        public void Print()
        {
            Console.Write(string.Format("{0,-5} {1,10}원 {2,10}원 {3,10}원", Number, Input, Output, Balance));
            Console.Write("\t{0} {1}", WbLib.Get_Date(Ctime), WbLib.Get_Time(Ctime));
            Console.WriteLine();
        }
        #endregion
    }
}
// MoiveIO.cs
using System;
using NetFlix.Lib;

namespace _0402_Netflix
{
    internal class MovieIO
    {
        #region 1.멤버필드, 프로퍼티(속성)
        /// <summary>
        /// Title    : 제목
        /// Director : 감독
        /// Number   : 관람객 수
        /// Logtime  : 관람 시간 로그 타임
        /// </summary>
        public string Title { get; private set; }
        public string Director { get; private set; }
        public int Number { get; private set; } 
        public DateTime Logtime { get; private set; }
        #endregion

        #region 2. 생성자
        public MovieIO(string _title, string _director, int _number)
        {
            Title = _title;
            Director = _director;
            Number = _number;
            Logtime = DateTime.Now;
        }

        public MovieIO(string _title, string _director, int _number, DateTime _logtime)
        {
            Title = _title;
            Director = _director;
            Number = _number;
            Logtime = _logtime;
        }
        #endregion

        #region 3. 기능 메서드
        public void Print()
        {
            Console.Write(string.Format("{0,-7} {1,10}감독 {2,10}명", Title, Director, Number));
            Console.Write("\t{0} {1}", WbLib.Get_Date(Logtime), WbLib.Get_Time(Logtime));
            Console.WriteLine();
        }
        #endregion
    }
}

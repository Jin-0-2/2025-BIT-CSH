using System;
using NetFlix.Lib;

namespace NetFlix.Data
{ 
    internal class Movie
    {
        #region 1.멤버필드, 프로퍼티(속성)
        /// <summary>
        /// Title    : 제목
        /// Director : 감독
        /// Number   : 관람객 수
        /// Genre    : 장르
        /// Realease : 개봉일
        /// </summary>
        public string Title { get; private set; }
        public string Director { get; private set; }
        public int Number { get; private set; }
        public string Genre { get; private set; }
        public string Realease { get; private set; }
        #endregion

        #region 2. 생성자
        public Movie(string title, string director, int number, string genre, string realease)
        {
            Title = title;
            Director = director;
            Number = number;
            Genre = genre;
            Realease = realease;
        }
        #endregion

        #region 3. 기능 메서드
        public void Input_Number(int number)
        {
            if (number <= 0)
                throw new Exception("관람객은 1명 이상이어야 합니다.");

            Number += number;
        }
        public void PrintInfo()
        {
            Console.Write(Title + "\t");
            Console.Write(Director + "\t");
            Console.Write(Genre + "\t");
            Console.Write(Realease+ "\t");
            Console.WriteLine(Number + "명");
        }
        public void PrintInfoln()
        {
            Console.WriteLine("[제목]   "  + Title);
            Console.WriteLine("[감독]   "  + Director);
            Console.WriteLine("[장르]   " + Genre);
            Console.WriteLine("[개봉일] " + Realease);
            Console.WriteLine("[누적 관객 수] " + Number);
        }
        #endregion
    }
}

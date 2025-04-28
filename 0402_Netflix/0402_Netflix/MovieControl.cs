// MovieControl.cs:
using System;
using NetFlix.Lib;
using System.Security.Principal;
using NetFlix.Data;
using System.Data;
using NetFlix.File;


namespace _0402_Netflix
{
    internal class MovieControl
    {
        private WbArray movies = new WbArray(10);     // 계좌리스트 저장
        private WbArray movieios;                     // 거래 내역 저장

        #region 0. 싱글톤 패턴
        public static MovieControl singleton { get; } = null;

        static MovieControl() { singleton = new MovieControl(); }

        // 디폴트 생성자를 은닉(private) 시킴
        private MovieControl()
        {
            movieios = new WbArray(100);
            // Temp();
        }
        #endregion


        #region 1. 임시 데이터
        public void Temp()
        {
            //movies.Add(new Movie("극한직업", "홍길동", 0, "코미디", (DateTime)"2025-08-11"));
        }
        #endregion

        #region 2. 기능 메서드
        public void MovieInsert()
        {
            try
            {
                Console.WriteLine("\n[영화 저장]\n");

                string title = WbLib.InputString("영화 이름 입력");
                string director = WbLib.InputString("감독 이름 입력");
                int number = 0;
                string genre = WbLib.InputString("장르 입력");
                int year = WbLib.InputNumber("개봉년도 입력(예: 2025)");
                int month = WbLib.InputNumber("개봉월 입력(예: 4)");
                int day = WbLib.InputNumber("개봉일 입력(예: 2)");
                string realease = string.Format("{0}-{1}-{2}", year, month, day);


                //영화 저장
                Movie account = new Movie(title, director, number, genre, realease);
                movies.Add(account);

                //저장 시 저장 내역
                //MovieIO accountio = new MovieIO(title, director, number);
                //movieios.Add(accountio);

                Console.WriteLine("[영화 저장 성공]");
                //Console.WriteLine("저장 내역 저장 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[영화 저장] " + ex.Message);
            }
        }
        public void MoviePrintAll()
        {
            Console.WriteLine("[영화 개수 : {0}개]", movies.Size);
            for (int i = 0; i < movies.Size; i++)
            {
                Movie movie = (Movie)movies[i];
                movie.PrintInfo();
            }
        }
        public void MovieIoPrintAll()
        {
            Console.WriteLine("[관람 Log : {0}개]", movieios.Size);
            for (int i = 0; i < movieios.Size; i++)
            {
                MovieIO movieio = (MovieIO)movieios[i];
                movieio.Print();
            }
        }
        public void SelectMovie()
        {
            try
            {
                Console.WriteLine("\n[영화 검색]\n");
                string title = WbLib.InputString("영화 이름 입력");

                Movie movie = NameToMovie(title);
                //영화 정보 출력
                movie.PrintInfoln();
                //영화 저장/관람내역 출력
                Console.WriteLine("--------------------------------------------");
                MovieIOPrint(title);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[영화 검색 실패] " + ex.Message);
            }
        }
        public void NumberInput()
        {
            try
            {
                Console.WriteLine("\n[영화 관람]\n");

                string title = WbLib.InputString("영화이름 입력");
                int number = WbLib.InputNumber("관람객 수 입력");

                Movie movie = NameToMovie(title);
                movie.Input_Number(number);

                //관람객 추가시 내역 저장
                MovieIO movieio = new MovieIO(title, movie.Director, number);
                movieios.Add(movieio);

                Console.WriteLine("관람 성공");
                Console.WriteLine("관람 내역 저장 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[관람 실패] " + ex.Message);
            }
        }
        public void MovieDelete()
        {
            try
            {
                Console.WriteLine("\n[영화 삭제]\n");

                string title = WbLib.InputString("영화 이름 입력");

                int idx = NameToIdx(title);

                //계좌 삭제
                movies.Remove(idx);

                //거래내역 전체 삭제
                MovieIODeleteAll(title);
                Console.WriteLine("삭제되었습니다.");
                Console.WriteLine("관람/저장 삭제 성공");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[삭제 실패] " + ex.Message);
            }
        }

        public void SelectMovieAll()
        {
            try
            {
                Console.WriteLine("\n[영화 검색(감독)]\n");

                string director = WbLib.InputString("감독 이름 입력");

                for (int i = 0; i < movies.Size; i++)
                {
                    Movie movie = (Movie)movies[i];
                    if (movie.Director == director)
                    {
                        movie.PrintInfoln();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[검색 실패] " + ex.Message);
            }
        }

        #endregion

        #region 3. 내부에서 사용되는 메서드(공개 필요 X)
        private int NameToIdx(string title)
        {
            for (int i = 0; i < movies.Size; i++)
            {
                Movie movie = (Movie)movies[i];
                if (movie.Title == title)
                    return i;
            }
            throw new Exception("없는 영화입니다.");
        }

        private Movie NameToMovie(string title)
        {
            for (int i = 0; i < movies.Size; i++)
            {
                Movie account = (Movie)movies[i];
                if (account.Title == title)
                    return account;
            }
            throw new Exception("없는 영화입니다.");
        }

        private void MovieIOPrint(string title)
        {
            for (int i = 0; i < movieios.Size; i++)
            {
                MovieIO movieio = (MovieIO)movieios[i];
                if (movieio.Title == title)
                {
                    movieio.Print();
                }
            }
        }

        private void MovieIODeleteAll(string title)     // 심각한 버그..!
        {
            // for (int i = 0; i < accountios.Size; i++)    최초 코드
            for (int i = movieios.Size - 1; i >= 0; i--)
            {
                MovieIO movieio = (MovieIO)movieios[i];
                if (movieio.Title == title)
                {
                    movieios.Remove(i);
                }
            }
        }
        #endregion
        #region 4. 시작/종료 메서드
        public void Init()
        {
            try
            {
                WbFile.Read_Accounts(movies);
                WbFile.Read_Accountios(movieios);
                Console.WriteLine("파일 로드 성공....");
            }
            catch (Exception ex)
            {
                Console.WriteLine("파일 로드 실패(최초실행).... ");
                Console.WriteLine(ex.Message);
            }
            WbLib.Pause();
        }
        public void Exit()
        {
            WbFile.Write_Account(movies);
            WbFile.Write_Accountio(movieios);
        }
        #endregion
    }
}

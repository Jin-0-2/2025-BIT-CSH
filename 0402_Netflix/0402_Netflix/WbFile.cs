// WbFile.cs
using System;
using NetFlix.Lib;
using System.IO;
using System.Security.Principal;
using NetFlix.Data;
using _0402_Netflix;

namespace NetFlix.File
{
    internal class WbFile
    {
        private const string ACCOUNTS_FILENAME = "movies.txt";
        private const string ACCOUNTIOS_FILENAME = "movieios.txt";

        public static void Write_Account(WbArray movies)
        {
            StreamWriter writer = new StreamWriter(ACCOUNTS_FILENAME);
            writer.WriteLine(movies.Size);

            for (int i = 0; i < movies.Size; i++)
            {
                Movie movie = (Movie)movies[i];
                string temp = string.Empty;
                temp = movie.Title + "@" + movie.Director + "@" + movie.Number + "@" + movie.Genre + "@" + movie.Realease;
                writer.WriteLine(temp);
            }
            // using > { } 블록을 벗어나면 자동으로 Dispose() 호출
            writer.Dispose();
        }

        public static void Read_Accounts(WbArray movies)
        {
            StreamReader reader = new StreamReader(ACCOUNTS_FILENAME);
            int size = int.Parse(reader.ReadLine());

            for (int i = 0; i < size; i++)
            {
                string temp = reader.ReadLine();        // 번호@이름@잔액@날짜
                string[] sp = temp.Split('@');
                string title = sp[0];
                string director = sp[1];
                int number = int.Parse(sp[2]);
                string genre = sp[3];
                string realease = sp[4];

                movies.Add(new Movie(title, director, number, genre, realease));
            }
            // using > { } 블록을 벗어나면 자동으로 Dispose() 호출
            reader.Dispose();
        }

        public static void Write_Accountio(WbArray movieios)
        {
            StreamWriter writer = new StreamWriter(ACCOUNTIOS_FILENAME);
            writer.WriteLine(movieios.Size);

            for (int i = 0; i < movieios.Size; i++)
            {
                MovieIO movieio = (MovieIO)movieios[i];
                string temp = string.Empty;
                temp = movieio.Title + "@" + movieio.Director + "@" + movieio.Number + "@" + movieio.Logtime;
                writer.WriteLine(temp);
            }
            // using > { } 블록을 벗어나면 자동으로 Dispose() 호출
            writer.Dispose();
        }

        public static void Read_Accountios(WbArray movieios)
        {
            StreamReader reader = new StreamReader(ACCOUNTIOS_FILENAME);
            int size = int.Parse(reader.ReadLine());

            for (int i = 0; i < size; i++)
            {
                string temp = reader.ReadLine();        // 번호@인풋@아웃풋@잔액@날짜
                string[] sp = temp.Split('@');
                string title = sp[0];
                string director = sp[1];
                int number = int.Parse(sp[2]);
                DateTime logtime = DateTime.Parse(sp[3]);

                movieios.Add(new MovieIO(title, director, number, logtime));
            }
            // using > { } 블록을 벗어나면 자동으로 Dispose() 호출
            reader.Dispose();
        }
    }
}

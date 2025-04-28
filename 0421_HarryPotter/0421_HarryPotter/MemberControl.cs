using System;
using System.Collections.Generic;
using System.Net;
using System.Drawing;

namespace _0421_HarryPotter
{
    internal class MemberControl
    {
        #region 0. 싱글톤 패턴
        public static MemberControl singleton { get; } = null;

        static MemberControl() { singleton = new MemberControl(); }

        // 디폴트 생성자를 은닉(private) 시킴
        private MemberControl() { }
        #endregion

        MyDB db = MyDB.singleton;

        #region 1. 기능 메서드
        public void MemberInsert_API_TO_DB()
        {
            try
            {
                List<Member> members = GetAPI.Search();
                foreach (Member member in members)
                {
                    if (db.Insert_Member(member) == false)
                    {
                        throw new Exception("멤버 저장 실패");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void New_MemberInsert()
        {
            try
            {
                string name = WbLib.InputString("이름");
                string gender = WbLib.InputString("성별 [male, female]\n");
                string sepecies = WbLib.InputString("종 [human] [half-giant] [werewolf] [cat]\n");
                string house = WbLib.InputString("기숙사 [Gryffindor] [Slytherin] [Hufflepuff] [Ravenclaw]\n");
                string birth = WbLib.InputString("생일(dd-mm-yyyy)");
                string eye_color = WbLib.InputString("눈 색깔");
                string hair_color = WbLib.InputString("머리 색깔");
                string actor = WbLib.InputString("배우 이름");
                string image = WbLib.InputString("이미지 url(없으면 null)");
                string still_alive = WbLib.InputString("생존 여부 [생존: true] [사망: false]");

                Member m = new Member(name, gender, sepecies, house, birth, eye_color, hair_color, actor, still_alive, image);

                if (db.Insert_Member(m) == false)
                {
                    throw new Exception("점수 저장 실패");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void MemberSelect()
        {
            try
            {
                string name = WbLib.InputString("이름 입력");

                if (db.SelectMember_Name(name) == false)
                    throw new Exception("이름 검색 실패");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void HouseSelect()
        {
            try
            {
                Console.WriteLine("[기숙사]");
                Console.WriteLine("[Gryffindor] [Slytherin] [Hufflepuff] [Ravenclaw]");
                string house = WbLib.InputString("기숙사 명 입력");

                List<string> names = db.SelectMember_House(house);
                if (names == null)
                {
                    Console.WriteLine("없슈");
                    return;
                }

                Console.WriteLine($"[{house}]");
                foreach (string n in names)
                {
                    Console.WriteLine(n);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void GenderSelect()
        {
            try
            {
                string gender = WbLib.InputString("성별 입력(male, female)");

                List<string> names = db.SelectMember_House(gender);
                if (names == null)
                {
                    Console.WriteLine("없슈");
                    return;
                }

                Console.WriteLine($"[{gender}]");
                foreach (string n in names)
                {
                    Console.WriteLine(n);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void SpeciesSelect()
        {
            try
            {
                Console.WriteLine("[종]");
                List<string> specieses = db.Select_PrintSpecies();
                int count = 1;
                foreach (string n in specieses)
                {
                    Console.Write(n + "\t");
                    if (count % 4 == 0)
                        Console.WriteLine();
                    count++;
                }
                Console.WriteLine();

                string species = WbLib.InputString("종 입력");

                List<string> names = db.SelectMember_Species(species);
                if (names == null)
                {
                    Console.WriteLine("없슈");
                    return;
                }

                Console.WriteLine($"[{species}]");
                foreach (string n in names)
                {
                    Console.WriteLine(n);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AliverSelect()
        {
            try
            {
                Console.WriteLine("[생존자]");
                Console.WriteLine("[생존: true] [사망: false]");
                string still_alive = WbLib.InputString("생존");

                List<string> names = db.SelectMember_Still_Alive(still_alive);
                if (names == null)
                {
                    Console.WriteLine("없슈");
                    return;
                }

                Console.WriteLine($"[{still_alive}]");
                foreach (string n in names)
                {
                    Console.WriteLine(n);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void MemberPrintAll()
        {
            try
            {
                List<string> members = db.SelectMember_Names_All();
                if (members == null)
                {
                    Console.WriteLine("없슈");
                    return;
                }

                Console.WriteLine("[Member] ");

                int count = 1;
                foreach (string m in members)
                {
                    Console.Write(m + "\t");
                    if (count % 5 == 0)
                        Console.WriteLine();
                    count++;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion

        #region 2. 시작/종료 메서드
        public void Init()
        {
            if (db.Connect() == false)
                return;
            Console.WriteLine("연결 성공");
            if (db.check() == 0)
            {
                MemberInsert_API_TO_DB();
            }
        }
        public void Exit()
        {
            if (db.Close() == true)
                Console.WriteLine("DB연결종료");
        }
        #endregion

        //private void PrintUrl()
        //{
        //    try
        //    {
        //        string url = "https://ik.imagekit.io/hpapi/harry.jpg";
        //        string localPath = "temp.jpg";

        //        // URL에서 이미지 다운로드
        //        using (WebClient client = new WebClient())
        //        {
        //            client.DownloadFile(url, localPath);
        //        }

        //        // 이미지를 Bitmap으로 로드
        //        using (Bitmap original = new Bitmap(localPath))
        //        {
        //            // ASCII 아트로 변환할 크기 설정
        //            int width = 100;
        //            int height = 40;

        //            using (Bitmap resized = new Bitmap(original, new Size(width, height)))
        //            {
        //                string chars = "@%#*+=-:. ";

        //                // 픽셀 데이터를 ASCII 문자로 변환
        //                for (int y = 0; y < resized.Height; y++)
        //                {
        //                    for (int x = 0; x < resized.Width; x++)
        //                    {
        //                        Color pixel = resized.GetPixel(x, y);
        //                        int gray = (pixel.R + pixel.G + pixel.B) / 3;
        //                        int index = gray * (chars.Length - 1) / 255;
        //                        Console.Write(chars[index]);
        //                    }
        //                    Console.WriteLine();
        //                }
        //            }
        //        }

        //        // 다운로드된 파일 삭제
        //        if (System.IO.File.Exists(localPath))
        //        {
        //            System.IO.File.Delete(localPath);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error: {ex.Message}");
        //    }
        //}

    }
}

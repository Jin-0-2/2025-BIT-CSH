// WbFile.cs
using System;
using System.IO;
using System.Security.Principal;
using JJY.Lib;
using JJY.DATA;
using System.Collections.Generic;

namespace JJY.FILE
{
    internal static class WbFile
    {
        private const string MEMBERS_FILENAME = "members.txt";
        private const string ATTENDENCE_FILENAME = "attendences.txt";

        public static void Write_Member(List<Member> members)
        {
            StreamWriter writer = new StreamWriter(MEMBERS_FILENAME);
            writer.WriteLine(members.Count);

            for (int i = 0; i < members.Count; i++)
            {
                Member account = (Member)members[i];
                string temp = string.Empty;
                temp = account.Id + "@" + account.Name + "@" + account.Phone + "@" + account.AttendenceCount + "@" + account.CreatedAt;
                writer.WriteLine(temp);
            }
            // using > { } 블록을 벗어나면 자동으로 Dispose() 호출
            writer.Dispose();
        }

        public static void Read_Members(List<Member> members)
        {
            StreamReader reader = new StreamReader(MEMBERS_FILENAME);
            int size = int.Parse(reader.ReadLine());

            for (int i = 0; i < size; i++)
            {
                string temp = reader.ReadLine();        // 번호@이름@잔액@날짜
                string[] sp = temp.Split('@');
                int id = int.Parse(sp[0]);
                string name = sp[1];
                string phone = sp[2];
                int att_count = int.Parse(sp[3]);
                DateTime ctime = DateTime.Parse(sp[4]);

                members.Add(new Member(name, id, phone, att_count, ctime));
            }
            // using > { } 블록을 벗어나면 자동으로 Dispose() 호출
            reader.Dispose();
        }

        public static void Write_Attendence(List<Attendance> attendences)
        {
            StreamWriter writer = new StreamWriter(ATTENDENCE_FILENAME);
            writer.WriteLine(attendences.Count);

            for (int i = 0; i < attendences.Count; i++)
            {
                Attendance accountio = (Attendance)attendences[i];
                string temp = string.Empty;
                temp = accountio.Id + "@" + accountio.Name + "@" + accountio.AttendenceCount + "@" + accountio.AttendenceDate;
                writer.WriteLine(temp);
            }
            // using > { } 블록을 벗어나면 자동으로 Dispose() 호출
            writer.Dispose();
        }

        public static void Read_Attendence(List<Attendance> attendences)
        {
            StreamReader reader = new StreamReader(ATTENDENCE_FILENAME);
            int size = int.Parse(reader.ReadLine());

            for (int i = 0; i < size; i++)
            {
                string temp = reader.ReadLine();        // 번호@인풋@아웃풋@잔액@날짜
                string[] sp = temp.Split('@');
                int id = int.Parse(sp[0]);
                string name = sp[1];
                int att_count = int.Parse(sp[2]);
                DateTime ctime = DateTime.Parse(sp[3]);

                attendences.Add(new Attendance(id, name, att_count, ctime));
            }
            // using > { } 블록을 벗어나면 자동으로 Dispose() 호출
            reader.Dispose();
        }
    }
}

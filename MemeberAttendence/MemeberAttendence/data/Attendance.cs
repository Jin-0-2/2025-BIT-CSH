using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJY.Lib;

namespace JJY.DATA
{
    internal class Attendance
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int AttendenceCount { get; private set; }    
        public DateTime AttendenceDate { get; private set; }

        public Attendance(int id, string name, int att_count)
        {
            Id = id;
            Name = name;
            AttendenceCount = att_count;
            AttendenceDate = DateTime.Now;
        }
        public Attendance(int id, string name, int att_count, DateTime date)
        {
            Id = id;
            Name = name;
            AttendenceCount = att_count;
            AttendenceDate = date;
        }


        public void Print()
        {
            Console.Write(string.Format("[{0}] {1}님 출석| {2}회 | {3} {4}", Name, Id, AttendenceCount, WbLib.Get_Date(AttendenceDate), WbLib.Get_Time(AttendenceDate)));
            Console.WriteLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJY.Lib;


namespace JJY.DATA
{
    internal class Member
    {
        #region 1.멤버필드, 프로퍼티(속성)
        public string Name { get; private set; }
        public int Id { get; private set; }
        public string Phone { get; private set; }
        public int AttendenceCount { get; private set; }
        public DateTime CreatedAt { get; private set; }


        #endregion

        #region 생성자
        public Member(string _name, string _phone)
        {
            Name = _name;
            Phone = _phone;
            Id = new Random().Next(1000, 9999);
            AttendenceCount = 0;
            CreatedAt = DateTime.Now;
        }

        public Member(string name, int id, string phone, int attendenceCount, DateTime createdAt)
        {
            Name = name;
            Id = id;
            Phone = phone;
            AttendenceCount = attendenceCount;
            CreatedAt = createdAt;
        }


        #endregion

        #region 기능 메서드
        public void Attendence()
        {
            AttendenceCount++;
        }
        public void Update_Phone(string phone)
        {
            Phone = phone;
        }
        public void Print()
        {
            Console.Write(Id + "\t");
            Console.Write(Name + "\t");
            Console.Write(Phone + "\t");
            Console.Write(AttendenceCount + "회\t");
            Console.WriteLine("[가입일자] : " + WbLib.Get_Date(CreatedAt) + "\t" + WbLib.Get_Time(CreatedAt));
        }

        public void Println()
        {
            Console.WriteLine("[회원번호] " + Id);
            Console.WriteLine("[이름]     " + Name);
            Console.WriteLine("[전화번호] " + Phone);
            Console.WriteLine("[가입일자] " + WbLib.Get_Date(CreatedAt) + "\t" + WbLib.Get_Time(CreatedAt));
        }
        #endregion
    }
}

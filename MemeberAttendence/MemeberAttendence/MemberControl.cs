using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJY.Lib;
using JJY.DATA;
using JJY.FILE;

namespace MemeberAttendence
{
    internal class MemberControl
    {
        private List<Member> members = new List<Member>();
        public List<Member> Members { get { return members; } }
        public int Members_Count { get { return members.Count; } }

        private List<Attendance> attendances = new List<Attendance>();
        public List<Attendance> Attendances { get { return attendances; } }
        public int Attendances_Count { get { return attendances.Count; } }

        #region 이벤트
        public event LogDel ui_InsertMember   = null;
        public event LogDel ui_Attendence     = null;
        public event LogDel ui_SelectMember   = null;
        public event LogDel ui_DeleteMember   = null;
        public event LogDel ui_UpdateMember   = null;
        public event LogDel ui_AttendenceRank = null;

        public event LogDel ui_TotalMember = null;

        LogManager logmannager = null;

        #endregion

        #region 싱글톤
        public static MemberControl singleton { get; } = null;
        static MemberControl() { singleton = new MemberControl(); }
        private MemberControl() { }
        #endregion

        #region 기능 메서드
        public void MemberInsert(string name, string phone)
        {
            try
            {
                Member member = new Member(name, phone);
                members.Add(member);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (ui_InsertMember != null)
            {
                ui_InsertMember(this, new LogArgs(LogType.Member_Insert, "회원 가입"));
                ui_TotalMember(this, new LogArgs(LogType.Member_Insert, "회원 가입"));
            }
        }
        public void Attendence(string name)
        {
            try
            {
                Member member = members.Find(members => members.Name == name);
                member.Attendence();
                Attendance attendance = new Attendance(member.Id, member.Name, member.AttendenceCount);
                attendances.Add(attendance);
                attendance.Print();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (ui_Attendence != null)
            {
                ui_Attendence(this, new LogArgs(LogType.Attendence, "회원 출석"));
                ui_TotalMember(this, new LogArgs(LogType.Attendence, "회원 출석"));
            }
        }
        public Member SelectMember(int id)
        {
            try
            {
                Member member  = members.Find(members => members.Id == id);
                if (ui_SelectMember != null)
                {
                    ui_SelectMember(this, new LogArgs(LogType.Select_Member, "회원 검색"));
                    ui_TotalMember(this, new LogArgs(LogType.Select_Member, "회원 검색"));
                    
                }
                return member;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteMember(int id)
        {
            try
            {
                Member member = members.Find(members => members.Id == id);
                members.Remove(member);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (ui_DeleteMember != null)
            {
                ui_DeleteMember(this, new LogArgs(LogType.Delete_Member, "회원 탈퇴"));
                ui_TotalMember(this, new LogArgs(LogType.Select_Member, "회원 탈퇴"));
            }
        }
        public void UpdateMember(string name, string phone)
        {
            try
            {
                Member member = members.Find(members => members.Name == name);
                member.Update_Phone(phone);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (ui_UpdateMember != null)
            {
                ui_UpdateMember(this, new LogArgs(LogType.Update_Member, "회원 탈퇴"));
                ui_TotalMember(this, new LogArgs(LogType.Update_Member, "회원 탈퇴"));
            }
        }
        public List<Member> AttendenceRank()
        {
            try
            {
                List<Member> _members = new List<Member>(members);

                if (ui_AttendenceRank != null)
                {
                    ui_AttendenceRank(this, new LogArgs(LogType.AttendenceRank, "우수 회원 정렬"));
                    ui_TotalMember(this, new LogArgs(LogType.AttendenceRank, "우수 회원 정렬"));
                }
                return _members;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            
        }
        public void MenuMemberPrint()
        {
            try
            {
                Console.WriteLine("[가입 회원 수: " + members.Count);
                foreach(Member member in members)
                {
                    member.Print();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[메뉴 가입 회원] " + ex.Message);
            }
        }

        public void MenuAttendencePrint()
        {
            try
            {
                Console.WriteLine("[출석 로그]: " + attendances.Count);
                foreach (Attendance attendance in attendances)
                {
                    attendance.Print();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[메뉴 출석 로그] " + ex.Message);
            }
        }
        #endregion

        #region 시작/종료 메서드
        public void Init()
        {
            try
            {
                logmannager = new LogManager();
                WbFile.Read_Members(members);
                WbFile.Read_Attendence(attendances);
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
            WbFile.Write_Member(members);
            WbFile.Write_Attendence(attendances);

            logmannager.Dispose();
        }
        #endregion
    }
}

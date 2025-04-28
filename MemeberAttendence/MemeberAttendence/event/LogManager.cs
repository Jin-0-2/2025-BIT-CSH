using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeberAttendence
{

    internal class LogManager : IDisposable
    {
        private StreamWriter writer = null;
        public LogManager()
        {
            MemberControl con = MemberControl.singleton;
            con.ui_InsertMember     += LogInsertMember;
            con.ui_SelectMember     += LogSelectMember;
            con.ui_DeleteMember     += LogDeleteMember;
            con.ui_UpdateMember     += LogUpdateMember;
            con.ui_Attendence       += LogAttendence;
            con.ui_AttendenceRank   += LogAttendenceRank;
            con.ui_TotalMember      += LogTotalMember;

            writer = new StreamWriter("log.txt", true);
        }

        public void Dispose()
        {
            writer.Dispose();
            GC.SuppressFinalize(this);
        }

        public void LogInsertMember(object obj, LogArgs e)
        {
            string msg = string.Format("[{0}] {1} ({2} {3})", e.Type, e.Msg, e.Time.ToShortDateString(), e.Time.ToShortDateString());
            writer.WriteLine(msg);
        }
        public void LogSelectMember(object obj, LogArgs e)
        {
            string msg = string.Format("[{0}] {1} ({2} {3})", e.Type, e.Msg, e.Time.ToShortDateString(), e.Time.ToShortDateString());
            writer.WriteLine(msg);
        }
        public void LogDeleteMember(object obj, LogArgs e)
        {
            string msg = string.Format("[{0}] {1} ({2} {3})", e.Type, e.Msg, e.Time.ToShortDateString(), e.Time.ToShortDateString());
            writer.WriteLine(msg);
        }
        public void LogUpdateMember(object obj, LogArgs e)
        {
            string msg = string.Format("[{0}] {1} ({2} {3})", e.Type, e.Msg, e.Time.ToShortDateString(), e.Time.ToShortDateString());
            writer.WriteLine(msg);
        }
        public void LogAttendence(object obj, LogArgs e)
        {
            string msg = string.Format("[{0}] {1} ({2} {3})", e.Type, e.Msg, e.Time.ToShortDateString(), e.Time.ToShortDateString());
            writer.WriteLine(msg);
        }

        public void LogAttendenceRank(object obj, LogArgs e)
        {
            string msg = string.Format("[{0}] {1} ({2} {3})", e.Type, e.Msg, e.Time.ToShortDateString(), e.Time.ToShortDateString());
            writer.WriteLine(msg);
        }

        public void LogTotalMember(object obj, LogArgs e)
        {
            string msg = string.Format("[{0}] {1} ({2} {3})", e.Type, e.Msg, e.Time.ToShortDateString(), e.Time.ToShortDateString());
            writer.WriteLine(msg);
        }
    }
}

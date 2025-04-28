using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeberAttendence
{
    internal enum LogType
    {
        Member_Insert,
        Attendence,
        Select_Member,
        Delete_Member,
        Update_Member,
        AttendenceRank
    }
    internal class LogArgs : EventArgs
    {
        public LogType Type { get; private set; }
        public string Msg { get; private set; }
        public DateTime Time { get; private set; }
        public LogArgs(LogType type, string msg)
        {
            Type = type;
            Msg = msg;
            Time = DateTime.Now;
        }
    }

    internal delegate void LogDel(object obj, LogArgs e);
}

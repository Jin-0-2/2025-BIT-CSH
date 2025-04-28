using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0407_계좌관리
{
    internal enum LogType
    {
        Insert_Account,
        Select_Account,
        Input_Account,
        Output_Account,
        Delete_Account
    }

    // 1. Eventing
    internal class LogArgs : EventArgs
    {
        public LogType Type { get; private set; }
        public string Msg { get; private set; }
        public DateTime CurrentTime { get; private set; }

        public LogArgs(LogType type, string msg)
        {
            Type = type;
            Msg = msg;
            CurrentTime = DateTime.Now;
        }
    }

    // 2. Delegate
    internal delegate void LogDel(object obj, LogArgs e);

}

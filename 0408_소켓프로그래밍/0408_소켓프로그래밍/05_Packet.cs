// 05_Packet.cs
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0408_소켓프로그래밍
{
    internal static class Packet
    {
        public const int PACKET_SHORTMESSAGE   = 1;      // flag
        public const int PACKET_SHORTMESSAGE_S = 2;      
        public const int PACKET_SHORTMESSAGE_F = 3;      


        #region Client -> Server
        public static string ShortMessage(string nickname, string msg, DateTime msgtime)
        {
            string packet = PACKET_SHORTMESSAGE + "@";
            packet += nickname + "#";
            packet += msg + "#";
            packet += msgtime;

            return packet;
        }
        #endregion


        #region Server -> Client
        public static string ShortMessageAck(bool b, string nickname, string msg, DateTime msgtime)
        {
            string packet = string.Empty;
            if (b)
                packet += Packet.PACKET_SHORTMESSAGE_S + "@";
            else
                packet += Packet.PACKET_SHORTMESSAGE_F + "@";

            packet += nickname + "#";
            packet += msg + "#";
            packet += msgtime;
            return packet;
        }
        #endregion
    }
}

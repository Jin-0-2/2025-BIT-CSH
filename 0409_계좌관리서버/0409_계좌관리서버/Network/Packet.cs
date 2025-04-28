// 05_Packet.cs
using System;
using System.Collections.Generic;
using WSBit41JJY.Data;

namespace WSBit41JJY.ServerNet
{
    internal static class Packet
    {
        public const int PACKET_INSERT_ACCOUNT           = 1;
        public const int PACKET_INSERT_ACCOUNT_ACK       = 2;
        public const int PACKET_SELECT_ACCOUNT           = 3;
        public const int PACKET_SELECT_ACCOUNT_ACK       = 4;
        public const int PACKET_INPUT_ACCOUNT            = 5;
        public const int PACKET_INPUT_ACCOUNT_ACK        = 6;
        public const int PACKET_OUTPUT_ACCOUNT           = 7;
        public const int PACKET_OUTPUT_ACCOUNT_ACK       = 8;
        public const int PACKET_DELETE_ACCOUNT           = 9;
        public const int PACKET_DELETE_ACCOUNT_ACK       = 10;
        public const int PACKET_PRINT_ALL_ACCOUNT        = 11;
        public const int PACKET_PRINT_ALL_ACCOUNT_ACK    = 12;
        public const int PACKET_INPUT_OUTPUT_ACCOUNT     = 13;
        public const int PACKET_INPUT_OUTPUT_ACCOUNT_ACK = 14;

        #region  Server -> Client
        public static string InsertAccount(bool ischeck, string info, int number)
        {
            string packet = PACKET_INSERT_ACCOUNT_ACK + "@";

            packet      += ischeck + "#";
            packet      += info + "#";
            packet      += number;

            return packet;
        }
        public static string SelectAccount(bool ischeck, string info, Account account)
        {
            string packet = PACKET_SELECT_ACCOUNT_ACK + "@";

            packet      += ischeck + "#";
            packet      += info + "#";
            packet      += account.Number + "#";
            packet      += account.Name + "#";
            packet      += account.Balance + "#";
            packet      += account.Ctime;

            return packet;
        }
        public static string InputAccount(bool ischeck, string info, int money)
        {
            string packet = PACKET_INPUT_ACCOUNT_ACK + "@";
            packet += ischeck + "#";
            packet += info + "#";
            packet += money;

            return packet;
        }
        public static string OutputAccount(bool ischeck, string info, int money)
        {
            string packet = PACKET_OUTPUT_ACCOUNT_ACK + "@";
            packet += ischeck + "#";
            packet += info + "#";
            packet += money;

            return packet;
        }
        public static string DeleteAccount(bool ischeck, string info, int number)
        {
            string packet = PACKET_DELETE_ACCOUNT_ACK + "@";
            packet += ischeck + "#";
            packet += info + "#";
            packet += number + "#";

            return packet;
        }
        public static string InputOutputAccount(bool ischeck, string info, int money)
        {
            string packet = PACKET_INPUT_OUTPUT_ACCOUNT_ACK + "@";
            packet += ischeck + "#";
            packet += info + "#";
            packet += money + "#";

            return packet;
        }
        public static string PrintAllAccount(bool ischeck, string info, List<Account> accounts)
        {
            string packet = PACKET_PRINT_ALL_ACCOUNT_ACK + "@";

            foreach(Account account in accounts)
            {
                packet += account.Number + "#";
                packet += account.Name + "#";
                packet += account.Balance + "#";
                packet += account.Ctime + "$";
            }
            packet.TrimEnd('$');        // 마지막 $ 빼주기
            return packet;
        }
        #endregion
    }
}

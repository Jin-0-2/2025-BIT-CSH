﻿// AccountControl.cs
using System;
using System.Collections.Generic;
using WSBit41JJY.ServerNet;
using WSBit41JJY.Data;
using System.Net.Sockets;
using System.Security.Policy;

namespace _0409_계좌관리서버
{
    internal class AccountControl
    {
        // network
        private const int SERVER_PORT  = 7000; // 서버 포트 번호
        private MyServer _server       = new MyServer();

        private List<Account> accounts = new List<Account>(10);                     // 계좌리스트 저장
        public List<Account> Accounts { get { return accounts; } }                  // accounts에 대한 get 프토퍼티
        public int Accounts_Count { get { return accounts.Count; } }

        private List<AccountIO> accountios = new List<AccountIO>(100);              // 거래 내역 저장
        public List<AccountIO> Accountios { get { return accountios; } }
        public int Accontios_Count { get { return accountios.Count; } }


        #region 0. 싱글톤 패턴
        public static AccountControl singleton { get; } = null;

        static AccountControl() { singleton = new AccountControl(); }

        // 디폴트 생성자를 은닉(private) 시킴
        private AccountControl()
        {
            // logmanager = new LogManager(); 이 상황은 만들어지지 않은 AccountControl의 객체를 접근함.
        }
        #endregion

        #region 1. 기능 메서드
        public void AccountInsert(Socket socket, string message)
        {
            try
            {
                // 1. 데이터 획득
                string[] sp = message.Split('#');
                int number = int.Parse(sp[0]);
                string name = sp[1];
                int balance = int.Parse(sp[2]);
                DateTime dtime = DateTime.Parse(sp[3]);

                // 2. 데이터 처리
                Account temp = accounts.Find(acc => acc.Number == number);
                if (temp != null)
                    throw new Exception("중복된 계좌번호입니다.");
                //계좌 저장
                Account account = new Account(number, name, balance, dtime);
                accounts.Add(account);

                //거래 내역 저장
                AccountIO accountio = new AccountIO(number, balance, 0, balance, dtime);
                accountios.Add(accountio);

                // 3. 데이터 전송
                string packet = Packet.InsertAccount(true, "계좌 생성 성공", number);
                _server.SendData(socket, packet);
            }
            catch (Exception ex)
            {
                string packet = Packet.InsertAccount(false, ex.Message, -1);
                _server.SendData(socket, packet);
            }
        }
        public void AccountSelect(Socket socket, string message)
        {
            try
            {
                // 1. 데이터 획득
                string[] sp = message.Split('#');
                int number = int.Parse(sp[0]);

                // 2. 데이터 처리
                Account account = accounts.Find(acc => acc.Number == number);
                if(account == null)
                    throw new Exception("계좌번호가 존재하지 않습니다.");

                // 3. 데이터 전송
                string packet = Packet.SelectAccount(true, "검색 성공", account);
                _server.SendData(socket, packet);

            }
            catch (Exception ex)
            {
                string packet = Packet.SelectAccount(false, ex.Message, new Account(-1, "", -1));
                _server.SendData(socket, packet);
            }
        }
        public void AccountInput(Socket socket, string message)
        {
            try
            {
                // 1. 데이터 획득
                string[] sp = message.Split('#');
                int number = int.Parse(sp[0]);
                int money = int.Parse(sp[1]);
                DateTime dateTime = DateTime.Parse(sp[2]);

                // 2. 데이터 처리
                Account account = accounts.Find(acc => acc.Number == number);
                if (account == null)
                    throw new Exception("계좌번호가 존재하지 않습니다.");

                account.Input_Money(money);

                // 3. 데이터 전송
                string packet = Packet.InputAccount(true, "입금 성공", money);
                _server.SendData(socket, packet);

            }
            catch (Exception ex)
            {
                string packet = Packet.InputAccount(false, ex.Message, -1);
                _server.SendData(socket, packet);
            }
        }
        public void AccountOutput(Socket socket, string message)
        {
            try
            {
                // 1. 데이터 획득
                string[] sp = message.Split('#');
                int number = int.Parse(sp[0]);
                int money = int.Parse(sp[1]);
                DateTime dateTime = DateTime.Parse(sp[2]);

                // 2. 데이터 처리
                Account account = accounts.Find(acc => acc.Number == number);
                if (account == null)
                    throw new Exception("계좌번호가 존재하지 않습니다.");

                account.Output_Money(money);

                // 3. 데이터 전송
                string packet = Packet.OutputAccount(true, "출금 성공", money);
                _server.SendData(socket, packet);

            }
            catch (Exception ex)
            {
                string packet = Packet.OutputAccount(false, ex.Message, -1);
                _server.SendData(socket, packet);
            }
        }
        public void AccountDelete(Socket socket, string message)
        {
            try
            {
                // 1. 데이터 획득
                string[] sp = message.Split('#');
                int number = int.Parse(sp[0]);
                DateTime dateTime = DateTime.Parse(sp[1]);

                // 2. 데이터 처리
                Account account = accounts.Find(acc => acc.Number == number);
                if (account == null)
                    throw new Exception("계좌번호가 존재하지 않습니다.");

                accounts.Remove(account);

                // 3. 데이터 전송
                string packet = Packet.DeleteAccount(true, "계좌 삭제 성공", number);
                _server.SendData(socket, packet);

            }
            catch (Exception ex)
            {
                string packet = Packet.DeleteAccount(false, ex.Message, -1);
                _server.SendData(socket, packet);
            }
        }
        public void AccountInputOutput(Socket socket, string message)
        {
            try
            {
                // 1. 데이터 획득
                string[] sp = message.Split('#');
                int input_number  = int.Parse(sp[0]);
                int output_number = int.Parse(sp[1]);
                int money         = int.Parse(sp[2]);
                DateTime dateTime = DateTime.Parse(sp[3]);

                // 2. 데이터 처리
                Account input_account = accounts.Find(acc => acc.Number == input_number);
                Account output_account = accounts.Find(acc => acc.Number == output_number);
                if (input_account == null || output_account == null)
                    throw new Exception("계좌번호가 존재하지 않습니다.");

                output_account.Output_Money(money);
                input_account.Input_Money(money);

                // 3. 데이터 전송
                string packet = Packet.InputOutputAccount(true, "계좌 이체 성공", money);
                _server.SendData(socket, packet);

            }
            catch (Exception ex)
            {
                string packet = Packet.InputOutputAccount(false, ex.Message, -1);
                _server.SendData(socket, packet);
            }
        }
        public void AccountPrintAll(Socket socket, string message)
        {
            try
            {
                // 1. 데이터 획득

                // 2. 데이터 처리

                // 3. 데이터 전송
                string packet = Packet.PrintAllAccount(true, "계좌 전체 리스트", accounts);
                _server.SendData(socket, packet);

            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region 2. 시작/종료 메서드
        public void Init()
        {
            if (_server.Start(SERVER_PORT, LogMessage, PacketMessage) == false)
                return;
            Console.WriteLine("서버 실행........");
        }
        public void Exit()
        {
            _server.Close();
            Console.WriteLine("서버 종료........");
        }
        #endregion

        #region 3. 네트워크 CallBack 메서드 -> 기능 메서드
        public void LogMessage(Socket sock, string message)
        {
            Console.WriteLine($"[log] {message}");
        }
        public void PacketMessage(Socket sock, string message)
        {
            //1. 패킷 수신 (굳이?)
            Console.WriteLine($"[packet] {message}");

            //2. 패킷 파싱(분석)
            string[] sp = message.Split('@');

            // 3. 분석 분할 처리
            switch (int.Parse(sp[0]))
            {
                case Packet.PACKET_INSERT_ACCOUNT:       AccountInsert(sock, sp[1]);      break;
                case Packet.PACKET_SELECT_ACCOUNT:       AccountSelect(sock, sp[1]);      break;
                case Packet.PACKET_INPUT_ACCOUNT:        AccountInput(sock, sp[1]);       break;
                case Packet.PACKET_OUTPUT_ACCOUNT:       AccountOutput(sock, sp[1]);      break;
                case Packet.PACKET_DELETE_ACCOUNT:       AccountDelete(sock, sp[1]);      break;
                case Packet.PACKET_INPUT_OUTPUT_ACCOUNT: AccountInputOutput(sock, sp[1]); break;
                case Packet.PACKET_PRINT_ALL_ACCOUNT:    AccountPrintAll(sock, sp[1]);    break;
            }
            TestPrint();
        }

        private void TestPrint()
        {
            Console.Clear();
            foreach (Account account in accounts)
            {
                account.Print();
            }
        }
        #endregion
    }
}

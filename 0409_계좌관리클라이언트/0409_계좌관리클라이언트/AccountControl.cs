// AccountControl.cs
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using WSBit41JJY.ClientNet;
using WSBit41JJY.Data;

namespace _0409_계좌관리클라이언트
{
    internal class AccountControl
    {
        // Network
        private const int SERVER_PORT = 7000; // 서버 포트 번호
        private const string SERVER_IP = "220.90.180.111"; // 서버 포트 번호
        private MyClient _client = new MyClient();

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


        #region 1. 임시 데이터
        public void Temp()
        {
            accounts.Add(new Account(1000, "홍길동", 1000));
            accountios.Add(new AccountIO(1000, 1000, 0, 1000));

            accounts.Add(new Account(1010, "김길동", 2000));
            accountios.Add(new AccountIO(1010, 2000, 0, 2000));

            accounts.Add(new Account(1020, "고길동", 3000));
            accountios.Add(new AccountIO(1020, 3000, 0, 3000));
        }
        #endregion

        #region 2. 기능 메서드 -> 응답 처리 메서드
        public void AccountInsert(int number, string name, int money)
        
        {
            try
            {
                // 데이터 저장은 서버에서 함. 보내줘야 해
                string packet = Packet.InsertAccount(number, name, money);
                // 서버에 전송
                _client.SendData(packet);
            }
            catch (Exception ex)
            {
                throw ex;       //예외 재 전송!!!!
            }
        }
        public void AccountInsert_Ack(string msg)
        {
            try
            {
                string[] sp = msg.Split('#');
                bool isbool = bool.Parse(sp[0]);
                string info = sp[1];
                int number = int.Parse(sp[2]);

                if (isbool == true)
                    Console.WriteLine($"{number} 계좌번호 저장 성공");
                else
                {
                    Console.WriteLine("계좌번호 저장 실패");
                    Console.WriteLine($"- {info}");            // 실패 원인
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AccountSelect(int number)
        {
            try
            {
                // 데이터 저장은 서버에서 함. 보내줘야 해
                string packet = Packet.SelectAccount(number);
                // 서버에 전송
                _client.SendData(packet);
            }
            catch (Exception ex)
            {
                throw ex;       //예외 재 전송!!!!
            }
        }
        public void AccountSelect_Ack(string msg)
        {
            try
            {
                string[] sp = msg.Split('#');
                bool isbool = bool.Parse(sp[0]);
                string info = sp[1];
                int number  = int.Parse(sp[2]);
                string name = sp[3];
                int balance = int.Parse(sp[4]);
                DateTime ctime = DateTime.Parse(sp[5]);

                if (isbool == true)
                {
                    Console.WriteLine($"    계좌번호 -> {number} ");
                    Console.WriteLine($"    이    름 -> {name} ");
                    Console.WriteLine($"    잔    액 -> {balance} ");
                    Console.WriteLine($"    개설일시 -> {ctime} ");
                }
                else
                {
                    Console.WriteLine("계좌번호 검색 실패");
                    Console.WriteLine($"- {info}");            // 실패 원인
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AccountInput(int number, int money)
        {
            try
            {
                string packet = Packet.InputAccount(number, money);
                _client.SendData(packet);
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }
        public void AccountInput_Ack(string msg)
        {
            try
            {
                string[] sp = msg.Split('#');
                bool isbool = bool.Parse(sp[0]);
                string info = sp[1];
                int money = int.Parse(sp[2]);

                if (isbool == true)
                    Console.WriteLine($"{money}원 입금 성공");
                else
                {
                    Console.WriteLine("입금 실패");
                    Console.WriteLine($"- {info}");            // 실패 원인
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AccountOutput(int number, int money)
        {
            try
            {
                string packet = Packet.OutputAccount(number, money);
                _client.SendData(packet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AccountOutput_Ack(string msg)
        {
            try
            {
                string[] sp = msg.Split('#');
                bool isbool = bool.Parse(sp[0]);
                string info = sp[1];
                int money = int.Parse(sp[2]);

                if (isbool == true)
                    Console.WriteLine($"{money}원 출금 성공");
                else
                {
                    Console.WriteLine("입금 실패");
                    Console.WriteLine($"- {info}");            // 실패 원인
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AccountDelete(int number)
        {
            try
            {
                string packet = Packet.DeleteAccount(number);
                _client.SendData(packet);
            }
            catch (Exception ex)
            {
                throw ex;       //예외 재 전송!!!!
            }
        }
        public void AccountDelete_Ack(string msg)
        {
            try
            {
                string[] sp = msg.Split('#');
                bool isbool = bool.Parse(sp[0]);
                string info = sp[1];
                int number = int.Parse(sp[2]);

                if (isbool == true)
                    Console.WriteLine($"{number} 계좌 삭제 성공");
                else
                {
                    Console.WriteLine("계좌 삭제 실패");
                    Console.WriteLine($"- {info}");            // 실패 원인
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AccountInputOutput(int input_number, int output_number, int transfer_ammount)
        {
            try
            {
                string packet = Packet.InputOutputAccount(input_number, output_number, transfer_ammount);
                _client.SendData(packet);
            }
            catch (Exception ex)
            {
                throw ex;       //예외 재 전송!!!!
            }
        }
        public void AccountInputOutput_Ack(string msg)
        {
            try
            {
                string[] sp = msg.Split('#');
                bool isbool = bool.Parse(sp[0]);
                string info = sp[1];
                int money = int.Parse(sp[2]);

                if (isbool == true)
                    Console.WriteLine($"{money}원 계좌 이체 성공");
                else
                {
                    Console.WriteLine("계좌 이체 실패");
                    Console.WriteLine($"- {info}");            // 실패 원인
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AccountPrintAll()
        {
            try
            {
                string packet = Packet.PrintAllAccount();
                _client.SendData(packet);
            }
            catch (Exception ex)
            {
                throw ex;       //예외 재 전송!!!!
            }
        }
        public void AccountPrintAll_Ack(string msg)
        {
            try
            {
                string[] sp = msg.Split('$');
                Console.WriteLine($"저장된 계좌 수: {sp.Length}개");
                
                foreach(string s in sp)
                {
                    string[] acc = s.Split('#');
                    int number = int.Parse(acc[0]);
                    string name = acc[1];
                    int balance = int.Parse(acc[2]);
                    DateTime ctime = DateTime.Parse(acc[3]);

                    Console.WriteLine($"{number} \t {name} \t {balance}원 \t {ctime}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public  void LogMessage1(Socket sock, string message)
        {
            Console.WriteLine($"[log] {message}");
        }
        public  void PacketMessage1(Socket sock, string message)
        {
            //1. 패킷 수신
            Console.WriteLine($"[packet] {message}");

            //2. 패킷 파싱(분석)/분할 처리
            string[] sp = message.Split('@');
            switch (int.Parse(sp[0]))
            {
                case Packet.PACKET_INSERT_ACCOUNT_ACK       : AccountInsert_Ack(sp[1]);      break;
                case Packet.PACKET_SELECT_ACCOUNT_ACK       : AccountSelect_Ack(sp[1]);      break;
                case Packet.PACKET_INPUT_ACCOUNT_ACK        : AccountInput_Ack(sp[1]);       break;
                case Packet.PACKET_OUTPUT_ACCOUNT_ACK       : AccountOutput_Ack(sp[1]);      break;
                case Packet.PACKET_DELETE_ACCOUNT_ACK       : AccountDelete_Ack(sp[1]);      break;
                case Packet.PACKET_INPUT_OUTPUT_ACCOUNT_ACK : AccountInputOutput_Ack(sp[1]); break;
                case Packet.PACKET_PRINT_ALL_ACCOUNT_ACK    : AccountPrintAll_Ack(sp[1]);    break;
            }
        }
        #endregion


        #region 2. 시작/종료 메서드
        public void Init()
        {
            if (_client.Start(SERVER_IP, SERVER_PORT, LogMessage1, PacketMessage1) == false)
            {
                Console.WriteLine("서버 연결 실패");
                return;
            }
            Console.WriteLine("서버 연결 성공........");
        }
        public void Exit()
        {
            _client.Close();
        }
        #endregion
    }
}

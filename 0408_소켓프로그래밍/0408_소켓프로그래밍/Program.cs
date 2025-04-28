using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _0408_소켓프로그래밍
{
    internal class Program
    {
        private const string SERVER_IP = "220.90.180.111"; //"127.0.0.1";
        private const int SERVER_PORT = 7000;

        //서버 -----------------------------------------------------------
        private static WB41.ServerNet.MyServer server1 = new WB41.ServerNet.MyServer();
        private static Object obj = new Object();

        #region CallBack Message
        public static void LogMessage(Socket sock, string message)
        {
            Console.WriteLine($"[log] {message}");
        }
        public static void PacketMessage(Socket sock, string message)
        {
            //1. 패킷 수신
            Console.WriteLine($"[packet] {message}");

            //2. 패킷 파싱(분석)
            string[] sp = message.Split('@');

            //3. 분석 분할 처리
            //----------------------------------------------------
            switch (int.Parse(sp[0]))
            {
                case Packet.PACKET_SHORTMESSAGE: ShortMessage(sock, sp[1]);  break;
            }

            
            
        }
        private static void ShortMessage(Socket sock, string message)
        {
            // -------------데이터 처리-------------------------
            string[] sp = message.Split('#');
            string name = sp[0];
            string msg = sp[1];
            DateTime dt = DateTime.Parse(sp[2]);

            Console.WriteLine($"[{name}] {msg} ({dt})");

            // -------------데이터 전송-------------------------
            string packet = Packet.ShortMessageAck(true, name, msg, dt);
            //a. 전송 패킷
            //b. 전송 패킷 전송
            //server1.SendData(sock, message);
            server1.SendAllData(sock, packet);
        }


        #endregion


        //1대 1통신
        static void ServerTest()
        {

            if (server1.Start(SERVER_PORT, LogMessage, PacketMessage) == false)
                return;
            Console.WriteLine("서버 실행........");

            Console.ReadLine();         //***********
            server1.Close();
            Console.WriteLine("서버 종료........");
        }
        //---------------------------------------------------------------


        //클라이언트 ----------------------------------------------------
        private static WB41.ClientNet.MyClient client1 = new WB41.ClientNet.MyClient();

        #region CallBack Message
        public static void LogMessage1(Socket sock, string message)
        {
            lock(obj)
            {
                Console.WriteLine($"[log] {message}");
            }
            Thread.Sleep(100); // 0.1초 대기

        }
        public static void PacketMessage1(Socket sock, string message)
        {
            lock (obj)
            {
                //1. 패킷 수신
                Console.WriteLine($"[packet] {message}");

                //2. 패킷 파싱(분석)/분할 처리
                string[] sp = message.Split('@');
                switch (int.Parse(sp[0]))
                {
                    case Packet.PACKET_SHORTMESSAGE_S: ShortMessageAck_S(sp[1]);  break;
                    case Packet.PACKET_SHORTMESSAGE_F: ShortMessageAck_F(sp[1]); break;
                }
            }
            Thread.Sleep(100); // 0.1초 대기
        }

        private static void ShortMessageAck_S(string message)
        {
            string[] sp = message.Split('#');
            string name = sp[0];
            string msg = sp[1];
            DateTime dt = DateTime.Parse(sp[2]);
            Console.WriteLine($"\t\t수신 => [{name}] {msg} ({dt})");
        }
        private static void ShortMessageAck_F(string message)
        {
            Console.WriteLine($"\t\t수신실패 허거덩거덩쓰;;");

        }
        #endregion 

        static string 정진영 = "조현준";
        static void ClientTest()
        {

            if (client1.Start(SERVER_IP, SERVER_PORT, LogMessage1, PacketMessage1) == false)
            {
                Console.WriteLine("서버 연결 실패");
                return;
            }
            Console.WriteLine("서버 연결 성공........");

            //전송 test
            while (true)
            {
                lock(obj)
                {
                    Console.Write(" >> ");
                    string name = 정진영;
                    string msg = Console.ReadLine();

                    string packet = Packet.ShortMessage(name, msg, DateTime.Now);
                    if (msg == string.Empty)
                        break;

                    client1.SendData(packet);
                }
                Thread.Sleep(100); // 0.1초 대기
            }

            client1.Close();
        }
        //---------------------------------------------------------------


        static void Main(string[] args)
        {
            //ServerTest();
            ClientTest();
        }
    }
}

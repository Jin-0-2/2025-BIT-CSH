// MyServer3.cs
// 다중 접속 1대 다 통신(가변 데이터 처리)
// Read: 끊어 읽기
// Send/Recv: head(고정크기) + data(가변크기) 보내기
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

// [socket -> bind -> listen]
// [accept]
// [recv -> send]
// [closesocket]


namespace _0408_소켓프로그래밍2
{
    internal class MyServer1 : IDisposable
    {
        private Socket server = null;
        private Thread work_thread = null;

        private List<Socket> clients = new List<Socket>();   // 접속한 클라이언트 리스트

        #region 1. Start 호출 시 Accpet Thread 동작
        public bool Start(int port)
        {
            try
            {
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);   // AddressFaimly : enum
                server.Bind(new IPEndPoint(IPAddress.Any, port));                                                                      // 주소 할당
                server.Listen(20);                                                                      // 대기 큐 크기 지정

                work_thread = new Thread(AcceptThread);                                                 // 스레드 생성
                work_thread.Start();                                                                    // 스레드 시작
                work_thread.IsBackground = true;                                                        // 백그라운드 스레드로 설정
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("서버 시작 실패 : " + ex.Message);
                return false;
            }
        }

        private void AcceptThread()
        {
            while (true)
            {
                try
                {
                    Socket socket = server.Accept();   // 클라이언트 연결 수락

                    // LocalEndPoint : 서버의 주소, RemoteEndPoint : 클라이언트의 주소
                    IPEndPoint ip = (IPEndPoint)socket.RemoteEndPoint;
                    Console.WriteLine("[클라이언트 접속] {0}주소, {1}포트 접속", ip.Address, ip.Port);

                    clients.Add(socket);   // 클라이언트 리스트에 추가

                    Thread tr = new Thread(WorkThread);
                    tr.IsBackground = true;
                    tr.Start(socket);                           // 스레드 시작

                }
                catch (Exception ex)
                {
                    Console.WriteLine("[접속 오류] : " + ex.Message);
                }
            }
        }
        #endregion

        #region 2. Dispose 나 Close 호출 소켓 및 스레드 종료
        ~MyServer1()
        {
            Dispose();

        }
        public void Dispose()
        {
            Close();
            GC.SuppressFinalize(this);   // 소멸자 호출 방지
        }

        public void Close()
        {
            work_thread.Abort();   // 스레드 종료
            server.Close();   // 소켓 종료
        }
        #endregion

        #region 3. 클라이언트 접속 시 통신 스레드
        private void WorkThread(object obj)
        {
            Socket sock = (Socket)obj;

            while (true)
            {
                try
                {
                    // 1. 데이터 수신
                    byte[] data = null;
                    RecvData(sock, ref data);
                    
                    // 2. 데이터 출력
                    string msg = Encoding.Default.GetString(data,0,data.Length);   // 수신한 데이터 변환
                    Console.WriteLine($"[수신] {msg}");

                    // 3. 데이터 송신
                    // SendData(sock, data);
                    SendAllData(sock, data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[recv 오류] : " + ex.Message);
                    break;
                }

            }
            IPEndPoint iP = (IPEndPoint)sock.RemoteEndPoint;
            Console.WriteLine($"[클라이언트 종료] {iP.Address} : {iP.Port}");

            sock.Close();   // 클라이언트 소켓 종료
            clients.Remove(sock);   // 클라이언트 리스트에서 제거
        }
        #endregion

        #region 4. 데이터 송 수신
        private void RecvData(Socket socket, ref byte[] data)
        {
            try
            {
                // 1. [헤더 수신] 가변 데이터 크기
                byte[] head     = new byte[4];
                int recv_data = socket.Receive(head, 0, head.Length, SocketFlags.None);
                int size        = BitConverter.ToInt32(head, 0);               // 받을 크기


                // 2. [데이터 수신] 가변 데이터를 size의 크기 만큼 수신
                int total       = 0;                // 받은 크기
                int left_data   = size;             // 남은 크기
                data            = new byte[size];

                while (total < size)
                {
                    recv_data = socket.Receive(data, total, left_data, 0);
                    if (recv_data == 0) throw new Exception("상대방 소켓 종료");
                    total += recv_data;
                    left_data -= recv_data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SendAllData(Socket sock, byte[] data)
        {
            foreach (Socket s in clients)
            {
                // if(s != sock)   // 본인 전달 X
                SendData(s, data);
            }
        }
        //BitConverter.GetBytes() // 기본 타입들을 모두 바이트로 바꿀 수 있다
        private void SendData(Socket sock, byte[] data)
        {
            try
            {
                // 1. [헤더 송신] 가변 데이터의 크기
                byte[] head     = new byte[4];
                int size        = data.Length;
                head            = BitConverter.GetBytes(size);
                int send_data   = sock.Send(head);

                // 2. [데이터 송신] 가변 데이ㅓ를 size의 크기만큼 송신
                int total       = 0;
                int left_data   = size;
                while (total < size)
                {
                    send_data   = sock.Send(data, total, left_data, SocketFlags.None);
                    total       += send_data;
                    left_data   -= send_data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
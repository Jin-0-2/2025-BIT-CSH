// 01_MyClient.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _0408_소켓프로그래밍
{
    internal class MyClient1 : IDisposable
    {
        private Socket socket = null;

        #region 1. Start  호출 시 소켓 생성 및 서버 연결, 수신 thread 실행
        public bool Start(string ip, int port)
        {
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);   // AddressFaimly : enum
                socket.Connect(new IPEndPoint(IPAddress.Parse(ip), port));                              // 서버에 접속
                Thread tr = new Thread(WorkThread);                                                     // 스레드 생성
                tr.IsBackground = true;                                                                 // 백그라운드 스레드로 설정
                tr.Start();                                                                             // 스레드 시작
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // 리시브 전용
        private void WorkThread(object obj)
        {
            byte[] data = new byte[1024];

            while (true)
            {
                try
                {
                    int ret = RecvData(data);
                    if (ret == 0)
                        throw new Exception("상대방이 소켓을 닫음");

                    // 수신 정보
                    string msg = Encoding.Default.GetString(data, 0, ret);   // 수신한 데이터 변환
                    Console.WriteLine($"[수신] {msg}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[recv 오류] : " + ex.Message);
                    break;
                }
            }
        }

        #endregion

        #region 2. Dispose 나 Close 호출 소켓 및 스레드 종료
        ~MyClient1()
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
            socket.Close();
        }
        #endregion

        #region 3. 송수신
        public void SendData(string msg)
        {
            byte[] data = Encoding.Default.GetBytes(msg);
            int ret = socket.Send(data, data.Length, SocketFlags.None);   // 서버에 데이터 송신
            Console.WriteLine($"\t\t보낸 바이트 : {ret}byte");
        }
        private int RecvData(byte[] data)
        {
            int ret = socket.Receive(data);   // 서버에서 데이터 수신
            return ret;
        }
        #endregion
    }
}

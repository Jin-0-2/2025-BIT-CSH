// NaverBook.cs
using System;
using System.Net;
using System.Text;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace _0421_OPENAPI사용
{
    internal class NaverBook
    {
        private const string CLIENT_ID = "a7VPtEYFFzgEJcvPnFre";
        private const string CLIENT_SECRET = "Qf6aS7hUtz";

        private static XmlDocument doc = null;
        public static List<Book> Search(string query)
        {
            try
            {
                string text = Sample(query);
                doc  = new XmlDocument();

                doc.LoadXml(text);                  // Load(파일 경로) > X
                Xml_Print();
                return Xml_Parsing();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        // 1. Open API 호출
        private static string Sample(string query)
        {
            //string query = "네이버 Open API"; // 검색할 문자열
            //string url = $"https://openapi.naver.com/v1/search/{book}?query=" + query;                        // JSON 결과
            string url = "https://openapi.naver.com/v1/search/book.xml?query=" + query + "&display=30";       // XML 결과
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-Naver-Client-Id", CLIENT_ID);                                                // 클라이언트 아이디
            request.Headers.Add("X-Naver-Client-Secret", CLIENT_SECRET);                                        // 클라이언트 시크릿
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string status = response.StatusCode.ToString();
            if (status == "OK")     // 정상적 반환 OK!
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                string text = reader.ReadToEnd();
                return text;
            }
            else
            {
                throw new Exception(status);
            }
        }

        // 2. XML 저장
        private static void Xml_Print()
        {
            doc.Save("Books.xml");
            // doc.Save(Console.Out); // 문서 저장
        }

        // 3. XML 데이터 파싱
        private static List<Book> Xml_Parsing()
        {
            XmlNode node = doc.SelectSingleNode("rss");         // 첫 rss요소에 접근
            XmlNode n = node.SelectSingleNode("channel");

            List<Book> books = new List<Book>();
            foreach (XmlNode item_node in n.SelectNodes("item"))
            {
                books.Add(Book.MakeBook(item_node));
            }

            return books;
        }

        // 4. 이미지 다운로드
        public static bool DownloadRemoteImageFile(string uri, string fileName)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            bool bImage = response.ContentType.StartsWith("image",
                StringComparison.OrdinalIgnoreCase);
            if ((response.StatusCode == HttpStatusCode.OK ||
                response.StatusCode == HttpStatusCode.Moved ||
                response.StatusCode == HttpStatusCode.Redirect) &&
                bImage)
            {
                using (Stream inputStream = response.GetResponseStream())
                using (Stream outputStream = File.OpenWrite(fileName))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    do
                    {
                        bytesRead = inputStream.Read(buffer, 0, buffer.Length);
                        outputStream.Write(buffer, 0, bytesRead);
                    } while (bytesRead != 0);
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

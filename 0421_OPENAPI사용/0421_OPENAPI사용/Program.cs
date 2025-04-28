// Program.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace _0421_OPENAPI사용
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //List<Book> books = NaverBook.Search("C언어");
            //int count = 0;
            //foreach (Book book in books)
            //{
            //    book.Println();
            //    string fileName = string.Format($"image_{count++}.png");
            //    NaverBook.DownloadRemoteImageFile(book.Img, fileName);
            //}

            {
                // 1. JSON 가져오기
                string json = new WebClient().DownloadString("https://hp-api.onrender.com/api/characters");

                // 30 개만
                JArray jsonArray = JArray.Parse(json);
                JArray top30 = new JArray(jsonArray.Take(30));
                string trimmedJson = JsonConvert.SerializeObject(top30);
                // 2. JSON을 XML로 변환 (루트 태그 추가 필요)
                XmlDocument xmlDoc = JsonConvert.DeserializeXmlNode("{\"characters\":" + trimmedJson + "}", "root");

                // 3. XML 출력
                Console.WriteLine(xmlDoc.OuterXml);

                // (선택) 저장하고 싶다면:
                xmlDoc.Save("harrypotter_characters.xml");

            }
    }
    }
}

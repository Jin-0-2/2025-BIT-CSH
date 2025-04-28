using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml;

namespace _0421_HarryPotter
{
    internal class GetAPI
    {
        public static List<Member> Search()
        {
            string json = new WebClient().DownloadString("https://hp-api.onrender.com/api/characters");

            // 30 개만
            //JArray jsonArray = JArray.Parse(json);
            //JArray top30 = new JArray(jsonArray.Take(30));
            //string trimmedJson = JsonConvert.SerializeObject(top30);
            // 2. JSON을 XML로 변환 (루트 태그 추가 필요)
            XmlDocument xmlDoc = JsonConvert.DeserializeXmlNode("{\"characters\":" + json + "}", "root");
            Xml_Print(xmlDoc);

            return Xml_Parsing(xmlDoc);

        }

        private static void Xml_Print(XmlDocument doc)
        {
            doc.Save("Books.xml");
            // doc.Save(Console.Out); // 문서 저장
        }
        private static List<Member> Xml_Parsing(XmlDocument xm)
        {
            XmlNode node = xm.SelectSingleNode("root");         // 첫 rss요소에 접근

            List<Member> members = new List<Member>();
            foreach(XmlNode charter in node.SelectNodes("characters"))
            {
                // string id = xm.SelectSingleNode("id").InnerText.ToString();
                string name = charter.SelectSingleNode("name").InnerText;

                string sepecies = charter.SelectSingleNode("species").InnerText;

                string gender = charter.SelectSingleNode("gender").InnerText;

                string house = charter.SelectSingleNode("house").InnerText;

                string birth = charter.SelectSingleNode("dateOfBirth").InnerText;

                string eye_color = charter.SelectSingleNode("eyeColour").InnerText;

                string hair_color = charter.SelectSingleNode("hairColour").InnerText;

                string actor = charter.SelectSingleNode("actor").InnerText;

                string still_alive = charter.SelectSingleNode("alive").InnerText;

                string image = charter.SelectSingleNode("image").InnerText;

                Member m = new Member(name, sepecies, gender, house, birth, eye_color, hair_color, actor, still_alive, image);    
                members.Add(m);
            }
            return members;
        }
    }
}

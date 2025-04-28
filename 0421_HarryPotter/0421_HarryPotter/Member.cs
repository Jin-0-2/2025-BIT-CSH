using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0421_HarryPotter
{
    internal class Member
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Gender { get; private set; }
        public string Sepecies { get; private set; }
        public string House { get; private set; }
        public string Birth {  get; private set; }
        public string Eye_color { get; private set; }
        public string Hair_color { get; private set; }
        public string Actor { get; private set; }
        public string Image { get; private set; }
        public string Still_Alive { get; private set; }

        public Member(int id, string name, string gender, string sepecies, string house, string birth, string eye_color, string hair_color, string actor, string image, string still_Alive)
        {
            Id = id;
            Name = name;
            Gender = gender;
            Sepecies = sepecies;
            House = house;
            Birth = birth;
            Eye_color = eye_color;
            Hair_color = hair_color;
            Actor = actor;
            Image = image;
            Still_Alive = still_Alive;
        }
        public Member(string name, string sepecies, string gender, string house, string birth, string eye_color, string hair_color, string actor, string image, string still_Alive)
        {
            Id = 0;
            Name = name;
            Gender = gender;
            Sepecies = sepecies;
            House = house;
            Birth = birth;
            Eye_color = eye_color;
            Hair_color = hair_color;
            Actor = actor;
            Image = image;
            Still_Alive = still_Alive;
        }


        public void Println()
        {
            Console.WriteLine($"[이름]       {Name}");
            Console.WriteLine($"[성별]       {Gender}");
            Console.WriteLine($"[종]         {Sepecies}");
            Console.WriteLine($"[기숙사]     {House}");
            Console.WriteLine($"[생일]       {Birth}");
            Console.WriteLine($"[눈 색깔]    {Eye_color}");
            Console.WriteLine($"[머리 색깔]  {Hair_color}");
            Console.WriteLine($"[배우]       {Actor}");
            Console.WriteLine($"[생존/사망]  {Still_Alive}");
            Console.WriteLine($"[이미지 url] {Image}");
        }
    }
}

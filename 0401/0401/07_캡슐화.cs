using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0401
{
    // C++ : 멤버 필드 > 생성자 & 소멸자 > *get & set 메서드* > 기능 메서드
    // C#  : 멤버 필드 > *속성(프로퍼티)* >생성자 & 소멸자 > 기능 메서드
    // get, set 메서드 = 속성
    internal class Member
    {
        #region 1. 멤버 필드 및 속성
        private string name;
        public string Name              // 속성을 만드는 기준 앞 글자를 대문자로
        {
            get { return name; }
            private set { name = value; }       // 외부에서 접근 X
        }

        private int age;
        public int Age
        {
            get { return age; }
            set                         // set메서드에서 조건을 걸어 필터링을 해줄 수 있음.
            { 
                if(value < 0)
                {
                    return;
                }
                age = value; 
            }
        }

        private char gender;
        public char Gender
        { 
            get { return gender; } 
            set 
            { 
                if(value != '남' && value != '여')
                {
                    return;
                }
                gender = value;
            } 
        }
        #endregion

        #region 2. 생성자(멤버 메서드에서 속성(프로퍼티)을 사용 권장)
        public Member(string _name, int _age, char _gender)
        {
            Name = _name;
            Age = _age;
            Gender = _gender;
        }
        #endregion

        #region 3. 기능 메서드
        public void Pirnt()
        {
            Console.WriteLine(Name + " " + Age + " " + Gender);
        }
        #endregion
    }

    internal class _07_캡슐화
    {
        
        static void Main(string[] args)
        {
            Member m = new Member("홍길동", 20, '남');
            // m.Name   = "홍길동";
            m.Age    = -10;
            m.Gender = '남';


            Console.WriteLine(m.Name);
            Console.WriteLine(m.Age);
            Console.WriteLine(m.Gender);
        }
    }
}

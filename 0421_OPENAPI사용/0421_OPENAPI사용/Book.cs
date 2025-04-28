// Book.cs
using System;
using System.Xml;

namespace _0421_OPENAPI사용
{
    internal class Book
    {
        public string Title { get; private set; }
        public string Link {  get; private set; }
        public string Img { get; private set; }
        public string Author { get; private set; }
        public int Discount { get; private set; }
        public string Publisher { get; private set; }
        public int Pubdate { get; private set; }
        public long Isbn { get; private set; }
        public string Description { get; private set; }

        public Book(string title, string link, string img, string author, int discount, string publisher, int pubdate, long isbn, string description)
        {
            Title       = title;
            Link        = link;
            Img         = img;
            Author      = author;
            Discount    = discount;
            Publisher   = publisher;
            Pubdate     = pubdate;
            Isbn        = isbn;
            Description = description;
        }
        public static Book MakeBook(XmlNode xm)
        {
            string title        = xm.SelectSingleNode("title").InnerText;

            string link         = xm.SelectSingleNode("link").InnerText;

            string image        = xm.SelectSingleNode("image").InnerText;

            string author       = xm.SelectSingleNode("author").InnerText;

            int discount        = int.Parse(xm.SelectSingleNode("discount").InnerText);

            string publisher    = xm.SelectSingleNode("publisher").InnerText;

            int pubdate         = int.Parse(xm.SelectSingleNode("pubdate").InnerText);

            long isbn            = long.Parse(xm.SelectSingleNode("isbn").InnerText);

            string description  = xm.SelectSingleNode("description").InnerText;

            return new Book(title, link, image, author, discount, publisher, pubdate, isbn, description);
        }
        public void Println()
        {
            Console.WriteLine($"[Title]     {Title}");
            Console.WriteLine($"[Link]      {Link}");
            Console.WriteLine($"[Img]       {Img}");
            Console.WriteLine($"[Author]    {Author}");
            Console.WriteLine($"[Discount]  {Discount}");
            Console.WriteLine($"[Publisher] {Publisher}");
            Console.WriteLine($"[Pubdate]   {Pubdate}");
            Console.WriteLine($"[Isbn]      {Isbn}");
            Console.WriteLine($"[Description]");
            Console.WriteLine($"{Description}");
        }


    }
}

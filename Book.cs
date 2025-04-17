using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoGestaoBiblioteca
{
    internal class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
        public List<Copy> Copies { get;private set; }

        public int TotalCopies { get; private set; }
        public int AvailableCopies { get; private set; }

        public Book(string title, string author, int publicationYear)
        {
            Title = title;
            Author = author;
            PublicationYear = publicationYear;
            Copies = new List<Copy>(); //new List<Copy> { }?
            TotalCopies = 0;
            AvailableCopies = 0;
        }

        public void SetTotalCopies()
        {
            TotalCopies = Copies.Count;
        }

        public void SetAvailableCopies()
        {
            AvailableCopies = Copies.Count(c => !c.IsLoaned);
        }

        public override string ToString()
        {
            return $"\nBook: {Title} \nAuthor: {Author} \nYear: {PublicationYear}\n" +
                $"Total copies: {TotalCopies}, Available copies: {AvailableCopies}:\n";
        }

        public Copy? FindFirstAvailableCopy(User user)
        {
           return Copies.FirstOrDefault(c => !c.IsLoaned);
        }

        
        //public enum CopyFilter
        //{
        //    All,
        //    Available,
        //    Loaned
        //}

        //public string CopiesToString(CopyFilter filter = CopyFilter.All) //options all, only available or only loaned?
        //{
        //    string text = $"Copies ({filter}):";
        //    foreach (Copy copy in Copies)
        //    {
        //        text += copy.ToString();
        //    }
        //    return text;
        //}
    }
}

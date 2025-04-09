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
        public List<Copy> Copies { get; set; }

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
            // Joana
        }

        public override string ToString()
        {
            return $"Book: {Title}, Author: {Author}, Year: {PublicationYear}," +
                $"Total copies: {TotalCopies}, Available copies: {AvailableCopies}\n";
        }
    }
}

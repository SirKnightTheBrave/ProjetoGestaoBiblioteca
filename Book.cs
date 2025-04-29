

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

        public Book(string title, string author, int publicationYear, int totalCopies = 0, int availableCopies = 0)
        {
            Title = title;
            Author = author;
            PublicationYear = publicationYear;
            Copies = new List<Copy>(); //new List<Copy> { }?
            TotalCopies = totalCopies;
            AvailableCopies = availableCopies;
        }

        public void SetTotalCopies()
        {
            TotalCopies = Copies.Count;
        }

        public void SetAvailableCopies()
        {
            AvailableCopies = Copies.Count(c => !c.IsLoaned);
        }

        public override string ToString() => $"{Title} of {Author} ({PublicationYear})";
                    //$"Total copies: {TotalCopies}, Available copies: {AvailableCopies}:\n";

        public string CopiesToString()
        {
            string text = "Copies:\n";
            if (Copies.Count == 0)
            {
                text += "No existing copies.\n";
                return text;
            }
            foreach (var copy in Copies) text += copy.ToString() + "\n";
            return text;
        }

        public Copy? FindFirstAvailableCopy(User user)
        {
           return Copies.FirstOrDefault(c => !c.IsLoaned);
        }

        public void AddCopy(Copy copy)
        {
            Copies.Add(copy);
            SetTotalCopies();
            SetAvailableCopies();
        }
    }
}

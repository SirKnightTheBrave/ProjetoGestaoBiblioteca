using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoGestaoBiblioteca
{
    internal class Copy
    {
        public string Code { get; set; }
        public Book Book { get; set; }
        public int Edition { get; set; }
        public int EditionYear { get; set; }
        public bool IsLoaned { get; set; }
        public Condition Condition { get; set; }
        public DateTime? LoanDate { get; set; }

        public Copy(string code, Book book, int edition, int editionYear, Condition condition)
        {
            Code = code;
            Book = book;
            Edition = edition;
            EditionYear = editionYear;
            IsLoaned = false; //por defeito, é falso?
            Condition = condition;
        }

        public bool Loan()
        {
            if (IsLoaned)
            {
                return false;
            }
            else
            {
                IsLoaned = true;
                return true;
            }
        }

        public bool Return()
        {
            if (!IsLoaned)
            {
                return false;
            }
            else
            {
                IsLoaned = false;
                return true;
            }
        }
    }
}

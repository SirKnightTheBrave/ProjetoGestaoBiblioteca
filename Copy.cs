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
        public int Code { get; set; }
        public Book Book { get; set; }
        public int Edition { get; set; }

        public enum CopyCondition //enums repetidos em ficheiros diferentes?
        {
            Good=1,
            Fair=2,
            Worn=3
        }
        public CopyCondition Condition { get; set; }

        public bool IsLoaned { get; set; }
        public User? LoanedTo { get; set; }
        public DateTime? LoanDate { get; set; }

        public Copy(int code, Book book, int edition, CopyCondition condition, bool isLoaned = false, User? loanedTo = null, DateTime? loanDate = null)
        {
            Code = code;
            Book = book;
            Edition = edition;
            IsLoaned = isLoaned; //por defeito, é falso?
            Condition = condition;
            LoanedTo = loanedTo;
            LoanDate = loanDate;
        }

        public bool Loan(User user)
        {
            if (IsLoaned) return false;

            IsLoaned = true;
            LoanedTo = user;
            LoanDate = DateTime.UtcNow;
           
            return true;
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
                LoanedTo = null;
                LoanDate = null;
                return true;
            }
        }

        public override string ToString()
        {
            return $"Code: {Code}, Edition: {Edition}, Condition: {Condition}, Loaned: {(IsLoaned? "True" : "False")}";
        }
    }
}

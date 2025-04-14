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

        public enum CopyCondition //enums repetidos em ficheiros diferentes?
        {
            Good,
            Fair,
            Worn
        }
        public CopyCondition Condition { get; set; }

        public bool IsLoaned { get; set; }
        public User? User { get; set; }
        public DateTime? LoanDate { get; set; }

        public Copy(string code, Book book, int edition, CopyCondition condition)
        {
            Code = code;
            Book = book;
            Edition = edition;
            IsLoaned = false; //por defeito, é falso?
            Condition = condition;
        }

        public bool Loan()
        {
            if (IsLoaned) return false;

            IsLoaned = true;
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
                return true;
            }
        }

        public void SetLoan(User user, DateTime loanDate)
        {
            User = user;
            LoanDate = loanDate;
        }

        public string GetLoanInfo()
        {
            return $"Loaned to {User.Name} on {LoanDate.Value.ToShortDateString()}.";
        }

        // public string GetCopyInfo()
        // {
           
        //     return $"Code: {Code}, Book: {Book.Title}, Edition: {Edition}, Condition: {Condition}, Loaned: {IsLoaned}";
        // }
    }
}

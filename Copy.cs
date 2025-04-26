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
        public User? LoanedTo { get; set; }
        public DateTime? LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public Copy(string code, Book book, int edition, CopyCondition condition)
        {
            Code = code;
            Book = book;
            Edition = edition;
            IsLoaned = false; //por defeito, é falso?
            Condition = condition;
        }

        public bool Loan(User user, TimeSpan loanPeriod)
        {
            if (IsLoaned) return false;

            IsLoaned = true;
            LoanedTo = user;
            LoanDate = DateTime.UtcNow;
            //new DateTime(2025, 4, 19)//teste;
            ReturnDate = LoanDate + loanPeriod;
           
            return true;
        }

        public bool Return(decimal fine)
        {
            if (!IsLoaned)
            {
                return false;
            }
            else
            {
                TimeOverdue(fine);
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

        public void TimeOverdue (decimal fine){
            if (DateTime.UtcNow>ReturnDate){
                    TimeSpan difference = DateTime.UtcNow - ReturnDate.Value;
                    double days = Math.Floor(difference.TotalDays);
                    decimal totalfine = fine*(decimal)days;
                    Console.WriteLine($"Book returned {days} days late. Total Fine: {totalfine} €");
            }
            else
            {
                Console.WriteLine("Returned on time!");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoGestaoBiblioteca
{
    internal class Library
    {
        public string Name { get; set; }
        public int MaxLoansPerUser { get; set; }
        public List<Book> Books { get; private set; }
        public List<User> Users { get; private set; }

        //TimeSpan para duração de tempo!
        public TimeSpan LoanPeriod { get; set; }

        public Library(string name, int maxLoansPerUser, TimeSpan loanPeriod)
        {
            Name = name;
            MaxLoansPerUser = maxLoansPerUser;
            Books = new List<Book>();
            Users = new List<User>();
            LoanPeriod = loanPeriod;
        }

        public bool AddBook(Book book)
        {
            var existingBook = FindBook(book.Title, book.Author);
            //se o livro já existe...
            if (existingBook != null)
            {
                book = existingBook; //garante que book aponta para o livro existente
                return false;
            }

            Books.Add(book);
            return true; //livro adicionado com sucesso!
        }

        public Book? FindBook(string title, string author)
        {
            return Books.FirstOrDefault(b =>
                b.Title.Equals(title, StringComparison.OrdinalIgnoreCase) &&
                b.Author.Equals(author, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Add a book copy
        /// </summary>
        /// <param name="copy">The book copy</param>
        /// <returns>A tuple of bools: 1st indicates if the book was added, 2nd indicates if the copy was added.</returns>
        public Tuple<bool, bool> AddCopy(Copy copy)
        {
            //adiciona o livro caso não exista!
            bool bookAdded = AddBook(copy.Book);

            AddBook(copy.Book);

            bool copyAdded;

            //se o código é único...
            if (FindCopy(copy.Code) == null)
            {
                //adiciona a cópia ao livro
                copy.Book.Copies.Add(copy);
                copyAdded = true;
            }
            //senão não adiciona a cópia ao livro porque o código é repetido
            else copyAdded = false;

            return new Tuple<bool, bool>(bookAdded, copyAdded);
        }

        public Copy? FindCopy(string code)
        {
            foreach (Book book in Books)
            {
                var copy = book.Copies.Find(c => c.Code == code);
                if (copy != null) return copy;
            }

            return null;
        }

        public string BooksToString()
        {
            string text = "Books:\n";
            foreach (var book in Books) text += book.GetInfo();return text;
        }
        

        public User? FindUser(User user){
            return Users.FirstOrDefault(u => u.Username == user.Username);//método procura o user pelo username
        }


        public User? FindUser(string username) //overload para verificar se o username existe
        {
            return Users.FirstOrDefault(u => u.Username == username);//método procura o user pelo username
        }

        public bool AddUser(User user)
        {
            User? storedUser = FindUser(user); //criação de uma variável de procura
            if (storedUser != null){//verifica a existência do username                
                return false;//caso o username exista, rompe o método e retorna falso
            }
            Users.Add(user);//
            return true; //caso o username não exista, é criado um novo user   
        }

        public string UsersToString()
        {
            string text = "Users:\n";
            foreach (var user in Users)
                text += user.GetInfo();//lista de users
            return text;
        }

        public string GetReport()
        {
            string text = "***REPORT***\n";

            foreach(var book in Books) //para cada livro
                if(book.TotalCopies != book.AvailableCopies) //se o total de cópias for diferente do número de cópias disponíveis
                    text += book.GetInfo(true); //buscar a info do livro sem as cópias
                
            return text;
        }

        

    }
}

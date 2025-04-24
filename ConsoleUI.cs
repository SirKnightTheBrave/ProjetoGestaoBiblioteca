using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Dapper;
using static System.Reflection.Metadata.BlobBuilder;
using System.Reflection;
using Org.BouncyCastle.Asn1.X509;
using System.Data;
using Mysqlx.Crud;
using System.Xml.Linq;

namespace ProjectoGestaoBiblioteca
{
    internal class ConsoleUI
    {
        public ConsoleColor DefaultForeColor { get; set; } //cor padrão do texto
        public ConsoleColor DefaultBackColor { get; set; } //cor padrão do fundo
        public Library Library { get; private set; }
        private string ConnectionString { get; set; }
        //public DBContext DBContext { get; private set; } //contexto da base de dados
        public User? LoggedUser { get; private set; } //utilizador logado

        public ConsoleUI(Library library, string connectionString, ConsoleColor defaultBackColor, ConsoleColor defaultForeColor)
        {
            Library = library;
            //DBContext = new DBContext(connectionString); //contexto da base de dados
            ConnectionString = connectionString;
            DefaultBackColor = defaultBackColor;
            DefaultForeColor = defaultForeColor;

           SelectBooksDB(); //carregar livros da BD
           SelectUsersDB(); //carregar utilizadores da BD
           SelectCopiesDB();
           Library.Books[0].AddCopy(new Copy("1001", Library.Books[0], 1, Copy.CopyCondition.Good)); //adicionar uma cópia do primeiro livro para teste
        }

        //nonquery: sem ser select? using funciona sem {}?
        public int ExecuteNonQuery(string query, object? parameters = null)
        {
            using var connection = new MySqlConnection(ConnectionString);
            connection.Open();
            return connection.Execute(query, parameters);
        }

        internal void SelectBooksDB()
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT title, author, publication_year, total_copies, available_copies FROM books", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var book = new Book
                        (
                            reader.GetString("title"),
                            reader.GetString("author"),
                            reader.GetInt32("publication_year"),
                            reader.GetInt32("total_copies"),
                            reader.GetInt32("available_copies")
                        );

                        Library.AddBook(book);
                    }
                }
            }
        }

        internal void SelectCopiesDB()
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT code, title, author, edition, `condition` FROM copies INNER JOIN books ON books.id = copies.book_id", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var book = Library.FindBook(reader.GetString("title"), reader.GetString("author"));
                        var copy = new Copy
                            (
                                reader["code"].ToString(),
                                book,
                                reader.GetInt32("edition"),
                                Enum.Parse<Copy.CopyCondition>(reader.GetString("condition")) // Parse the string to the enum
                            );

                        book.AddCopy(copy);
                    }
                }
            }
        }


        internal void SelectUsersDB()
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT name, username, password, isAdmin, address, phone FROM users", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = Library.AddUser(

                            reader.GetString("name"),
                            reader.GetString("username"),
                            reader.GetString("password"),
                            reader.GetBoolean("isAdmin"),
                            reader.GetString("address"),
                            reader.GetString("phone"),
                            true // Não fazer hash aqui, pois já está na base de dados
                        );
                        //var user = new User(
                        //    reader.GetString("name"),
                        //    reader.GetString("username"),
                        //    reader.GetString("password"),
                        //    reader.GetString("address"),
                        //    reader.GetString("phone"),
                        //    reader.GetBoolean("isAdmin")
                        //);
                    }
                }
            }
        }



        public bool InsertBookDB(Book book)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "INSERT INTO Books (Title, Author, publication_year) VALUES (@Title, @Author, @PublicationYear)";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@Author", book.Author);
                    command.Parameters.AddWithValue("@PublicationYear", book.PublicationYear);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Return true if a row was inserted
                }
            }
        }

        public bool InsertUserDB(User user)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "INSERT INTO Users (name, username, password, isAdmin, address, phone) VALUES (@Name, @Username, @password, @IsAdmin, @Address, @Phone)";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@password", user.HashedPassword);
                    command.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
                    command.Parameters.AddWithValue("@Address", user.Address);
                    command.Parameters.AddWithValue("@Phone", user.Phone);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Return true if a row was inserted
                }
            }
        }

        /// <summary>
        /// Reads a string and validates it using a provided validation function.
        /// </summary>
        /// <param name="message">The message to display when asking for input.</param>
        /// <param name="isValid">A function that takes a string and returns a boolean indicating if the input is valid.</param>
        /// <returns>A valid string.</returns>
        public static User ReadValidUser(string message, Func<string, User?> findUser, string errorMessage = "Invalid input. Please try again.")
        {
            string? input;
            User? user;

            do
            {
                Console.Write(message + " ");
                input = Console.ReadLine();
                
                if (input == null || (user = findUser(input)) == null)
                    Console.WriteLine(errorMessage);
                else
                    break;
            } while (true);

            return user;
        }

        public static T ReadValidType<T>(string message, Func<string, T?> findType, string errorMessage = "Invalid input. Please try again.")
        {
            string? input;
            T? result;

            do
            {
                Console.Write(message + " ");
                input = Console.ReadLine();

                if (input == null || (result = findType(input)) == null)
                    Console.WriteLine(errorMessage);
                else
                    break;
            } while (true);

            return result!;
        }

        private void CreateUser()
        {
            Console.WriteLine("Enter user details:");
            string name = Utils.ReadValidString("Name: ", input => !String.IsNullOrWhiteSpace(input));
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Address: ");
            string? address = Console.ReadLine();
            Console.Write("Phone: ");
            string? phone = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            Console.Write("Is Admin (true/false): ");
            bool isAdmin = bool.Parse(Console.ReadLine());

            Library.AddUser(name, username, password, isAdmin, address, phone, true);
            ExecuteNonQuery("INSERT INTO Users(name, username, password, isAdmin, address, phone) VALUES(@Name, @Username, @password, @IsAdmin, @Address, @Phone)");
        }

        public void LoginMenu()
        {
            NewConsole();
            Console.WriteLine($"***{Library.Name}***");
            Console.WriteLine($"LOGIN");

            var user = ReadValidUser("Username:", Library.FindUser,"Username not found!");
            var password = Utils.ReadValidString("Password:", user.CheckPassword);
            Console.WriteLine("Login successful!");
            LoggedUser = user; // Define o utilizador logado
            if (user.IsAdmin) // Se for admin vai par ao menu administrador
               AdminMenu();
            else
               UserMenu();  //Se for user vai para o menu de utilizador
            
        }
        public void AdminMenu()
        {
            bool flag = true;
            do
            {
                NewConsole();
                Console.WriteLine("Welcome to the Library Management System");
                Console.WriteLine("1. Register User");
                Console.WriteLine("2. View Users");
                Console.WriteLine("3. Register Books");
                Console.WriteLine("4. Add Copies");
                Console.WriteLine("5. View Books and Copies");
                Console.WriteLine("6. Loan Report");
                Console.WriteLine("7. Logout");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        // Add user logic
                        CreateUser();
                        break;
                    case "2":
                        // View user logic
                        Console.WriteLine(Library.UsersToString());

                        break;
                    case "3":
                        // Register books logic
                        Console.WriteLine("Enter book details:");
                        string title = Utils.ReadValidString("Title: ", input => !String.IsNullOrWhiteSpace(input));
                        string author = Utils.ReadValidString("Author: ", input => !String.IsNullOrWhiteSpace(input));
                        int PublicationYear =Utils.ReadInt("Publication of Year:");//"Publication Year: ", x => x < 0, "Year must be positive.");
                        Book newBook = new Book(title, author, PublicationYear);
                        Library.AddBook(newBook);
                        InsertBookDB(newBook);
                        Console.WriteLine("Book registered successfully!");

                        break;
                    case "4":
                        // Add copies logic
                        Console.WriteLine("Enter book title or author name:");
                        string? search = Console.ReadLine();
                        var foundBooks = Library.SearchBook(search);
                        Console.WriteLine(Utils.ListToString(foundBooks, "Books Found"));

                        int index = Utils.ReadInt("Choose the book index: ", 1, foundBooks.Count);
                        index--; // Adjust for 0-based index
                        var book = foundBooks[index];
                        int edition =Utils.ReadInt("Edition: ", 1);
                        string code = Utils.ReadValidString("Code: ", input => !String.IsNullOrWhiteSpace(input));
                        var conditionList = Enum.GetValues<Copy.CopyCondition>().ToList();
                        Console.WriteLine(Utils.ListToString(conditionList, "Possible conditions:"));
                        int conditionIndex = Utils.ReadInt("Choose the condition index: ", 1, conditionList.Count);
                        conditionIndex--; // Adjust for 0-based index
                        var condition = conditionList[conditionIndex];

                        book.Copies.Add(new Copy(code, book, edition, condition));



                        Console.WriteLine($"Copy of {book.Title} successfully added!");
                            
                            
                        
                        break;
                    case "5":
                        // View books logic
                        Console.WriteLine(Library.BooksToString(true));
                       
                        break;
                    case "6":
                        // Loan report logic
                        Console.WriteLine("Loan Report:");
                        Console.WriteLine(Library.GetReport());
                        break;
                    case "7":
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                  }
                Utils.WaitForKeyPress();
            } while (flag);
            LoginMenu();

        }
        public void SplashScreen()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" ██████╗ ██████╗     ██╗     ██╗██████╗ ██████╗  █████╗ ██████╗ ██╗   ██╗    ███████╗██╗   ██╗███████╗");
            Console.WriteLine("██╔════╝ ╚════██╗    ██║     ██║██╔══██╗██╔══██╗██╔══██╗██╔══██╗╚██╗ ██╔╝    ██╔════╝╚██╗ ██╔╝██╔════╝");
            Console.WriteLine("██║  ███╗ █████╔╝    ██║     ██║██████╔╝██████╔╝███████║██████╔╝ ╚████╔╝     ███████╗ ╚████╔╝ ███████╗");
            Console.WriteLine("██║   ██║ ╚═══██╗    ██║     ██║██╔══██╗██╔══██╗██╔══██║██╔══██╗  ╚██╔╝      ╚════██║  ╚██╔╝  ╚════██║");
            Console.WriteLine("╚██████╔╝██████╔╝    ███████╗██║██████╔╝██║  ██║██║  ██║██║  ██║   ██║       ███████║   ██║   ███████║");
            Console.WriteLine(" ╚═════╝ ╚═════╝     ╚══════╝╚═╝╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝       ╚══════╝   ╚═╝   ╚══════╝");
            Console.WriteLine();
            Console.ForegroundColor = DefaultForeColor;
        }

        public void NewConsole()
        {
            Console.BackgroundColor = DefaultBackColor;
            Console.Clear(); //clear console
            SplashScreen();
        }
        public void UserMenu()
        {
            bool flag = true;
            do 
            {
                NewConsole();
                Console.WriteLine("User Menu:");
                Console.WriteLine("1. View Books");
                Console.WriteLine("2. Search and Loan Book");
                Console.WriteLine("3. Return Book");
                Console.WriteLine("4. Logout");
                Console.Write("Select an option: ");
                string? choice = Console.ReadLine();
            
                switch (choice)
                {
                    case "1":
                        // View books logic
                        Console.WriteLine(Library.BooksToString());
                        break;
                    case "2":
                        //Search and Loan book logic
                        Console.WriteLine("Enter book title or author name:");
                        string? search = Console.ReadLine();
                        var foundBooks = Library.SearchBook(search);
                        Console.WriteLine(Utils.ListToString(foundBooks, "Books Found"));
                    
                        if (foundBooks?.Count > 0)
                        {
                            int index = Utils.ReadInt("Choose the book index: ", 1, foundBooks.Count);
                            index--; // Adjust for 0-based index
                            var book = foundBooks[index];
                            if (foundBooks[index].AvailableCopies > 0)
                            {
                                Library.LoanCopy(LoggedUser, book); // Loan the copy to the user
                                ExecuteNonQuery(
                                    "UPDATE books SET available_copies = available_copies - 1 WHERE title = @Title AND author = @Author",
                                    new { Title = book.Title, Author = book.Author }
                                    );
                                Console.WriteLine($"Copy of {book.Title} successfully loaned!");
                            }
                            else
                                Console.WriteLine("No available copies for this book.");
                        }
                        break;
                    case "3":
                        // Return book logic
                        if (LoggedUser.CurrentLoans.Count == 0)
                        {
                            Console.WriteLine("You have no books to return.");
                            
                        }
                        else if (LoggedUser.CurrentLoans.Count == 1)
                        {
                            var loanedCopy = LoggedUser.CurrentLoans[0];
                            Console.WriteLine("You have one copy to return:");
                            Console.WriteLine(loanedCopy.ToString());
                            Console.WriteLine("Do you want to return it? (y/n)");
                            string answer = Console.ReadLine();
                            if (answer.ToLower() == "y")
                            {
                                ExecuteNonQuery(
                                    "UPDATE books SET available_copies = available_copies + 1 WHERE title = @Title AND author = @Author",
                                    new { Title = loanedCopy.Book.Title, Author = loanedCopy.Book.Author }
                                    );
                                Library.ReturnCopy(LoggedUser, loanedCopy);
                                Console.WriteLine("Copy returned successfully!");
                            }
                            
                        }
                        else if (LoggedUser.CurrentLoans.Count > 1)
                        {
                            Console.WriteLine("You have multiple copies to return:");
                            Console.WriteLine(Utils.ListToString(LoggedUser.CurrentLoans, "Copies to return"));
                            int index = Utils.ReadInt("Choose the copy index: ", 1, LoggedUser.CurrentLoans.Count);
                            index--; // Adjust for 0-based index
                            var copyToReturn = LoggedUser.CurrentLoans[index];
                            if (copyToReturn != null)
                            {
                                Console.WriteLine($"Do you want to return {copyToReturn.Book.Title}? (y/n)");
                                string answer = Console.ReadLine();
                                if (answer.ToLower() == "y")
                                {
                                    ExecuteNonQuery(
                                        "UPDATE books SET available_copies = available_copies + 1 WHERE title = @Title AND author = @Author",
                                        new { Title = copyToReturn.Book.Title, Author = copyToReturn.Book.Author }
                                        );
                                    Library.ReturnCopy(LoggedUser, copyToReturn);
                                    Console.WriteLine("Copy returned successfully!");
                                   
                                }
                            }
                           
                        }
                        break;
                    case "4":
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                    }
                    Utils.WaitForKeyPress();
            } while (flag);

            LoginMenu();
        }
    }
}

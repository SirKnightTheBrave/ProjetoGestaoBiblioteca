using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using static System.Reflection.Metadata.BlobBuilder;

namespace ProjectoGestaoBiblioteca
{
    internal class ConsoleApp
    {
        public ConsoleColor DefaultForeColor { get; set; } //cor padrão do texto
        public ConsoleColor DefaultBackColor { get; set; } //cor padrão do fundo
        public Library Library { get; private set; }
        private string ConnectionString { get; set; }

        public ConsoleApp(Library library, string connectionString, ConsoleColor defaultBackColor, ConsoleColor defaultForeColor)
        {
            Library = library;
            ConnectionString = connectionString;
            DefaultBackColor = defaultBackColor;
            DefaultForeColor = defaultForeColor;

            SelectBooksDB();
        }

        private void SelectBooksDB()
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT title, author, publication_year FROM books", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var book = new Book
                        (
                            reader.GetString("title"),
                            reader.GetString("author"),
                            1900
                            //reader.GetInt32("publication_year")
                        );

                        Library.AddBook(book);
                    }
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

        private User CreateUser()
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

            return new User(name, username, password, isAdmin, address, phone);
        }

        public void LoginMenu()
        {
            NewConsole();
            Console.WriteLine($"***{Library.Name}***");
            Console.WriteLine($"LOGIN");

            var user = ReadValidUser("Username:", Library.FindUser,"Username not found!");
            var password = Utils.ReadValidString("Password", user.CheckPassword);
            Console.WriteLine("Login successful!");
            if (user.IsAdmin) // Se for admin vai par ao menu administrador
               AdminMenu();
            else
               UserMenu();  //Se for user vai para o menu de utilizador
            
        }
        public void AdminMenu()
        {
            do
            {
                NewConsole();
                Console.WriteLine("Welcome to the Library Management System");
                Console.WriteLine("1. Register User");
                Console.WriteLine("2. View Users");
                Console.WriteLine("3. Register Books");
                Console.WriteLine("4. View Books");
                Console.WriteLine("5. Loan Report");
                Console.WriteLine("6. Logout");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        // Add user logic
                        User newUser = CreateUser();
                        Library.AddUser(newUser);
                        break;
                    case "2":
                        // View user logic
                        Console.WriteLine(Library.UsersToString());
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "3":
                        // Register books logic
                        Console.WriteLine("Enter book details:");
                        string title = Utils.ReadValidString("Title: ", input => !String.IsNullOrWhiteSpace(input));
                        string author = Utils.ReadValidString("Author: ", input => !String.IsNullOrWhiteSpace(input));
                        int PublicationYear =Utils.ReadInt("Publication of Year:");//"Publication Year: ", x => x < 0, "Year must be positive.");
                        Book newBook = new Book(title, author, PublicationYear);
                        Library.AddBook(newBook);
                        Console.WriteLine("Book registered successfully!");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "4":
                        // View books logic
                        Console.WriteLine(Library.BooksToString());
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "5":
                        // Loan report logic
                        Console.WriteLine("Loan Report:");
                        Console.WriteLine(Library.GetReport());
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "6":
                        LoginMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            } while (true);
            

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
            Console.WriteLine("User Menu:");
            Console.WriteLine("1. View Books");
            Console.WriteLine("2. Loan Book");
            Console.WriteLine("3. Return Book");
            Console.WriteLine("4. Logout");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    // View books logic
                    break;
                case "2":
                    // Loan book logic
                    break;
                case "3":
                    // Return book logic
                    break;
                case "4":
                    LoginMenu();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}

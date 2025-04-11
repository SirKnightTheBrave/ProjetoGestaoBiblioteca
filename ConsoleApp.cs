using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoGestaoBiblioteca
{
    internal class ConsoleApp
    {
        public ConsoleColor DefaultForeColor { get; set; } //cor padrão do texto
        public ConsoleColor DefaultBackColor { get; set; } //cor padrão do fundo
        public Library Library { get; private set; }

        public ConsoleApp(Library library, ConsoleColor defaultBackColor, ConsoleColor defaultForeColor)
        {
            Library = library;
            DefaultBackColor = defaultBackColor;
            DefaultForeColor = defaultForeColor;
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
            string name = Utils.ReadValidString("Name: ", String.IsNullOrWhiteSpace);
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

            var user = ReadValidUser("Username:", Library.FindUser);
            var password = Utils.ReadValidString("Password", user.CheckPassword);
            //if (username == "admin" && password == "admin")
            //{
            //    Console.WriteLine("Login successful!");
            //    // Call the method to display the admin menu
            //    AdminMenu();
            //}
            //else if (username == "user" && password == null)
            //{
            //    Console.WriteLine("Login successful!");
            //    // Call the method to display the user menu
            //    UserMenu();
            //}
            //else
            //{
            //    Console.WriteLine("Invalid username or password.");
            //}
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
                        // Register users logic
                        break;
                    case "4":
                        // View all books logic
                        break;
                    case "5":
                        // Loan report logic
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

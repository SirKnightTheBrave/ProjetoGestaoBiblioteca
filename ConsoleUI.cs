using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoGestaoBiblioteca
{
    internal class ConsoleUI
    {
        //global? constants
        public const ConsoleColor DefaultTextColor = ConsoleColor.Gray; //default foreground color
        public const ConsoleColor DefaultBackColor = ConsoleColor.DarkBlue; //default background color

        public ConsoleUI(Library library)
        {
            Library = library;
        }

        public Library Library { get; set; }
        private User CreateUser()
        {
            Console.WriteLine("Enter user details:");
            string name = Utils.ReadValidatedString("Name: ", String.IsNullOrWhiteSpace);
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Address: ");
            string address = Console.ReadLine();
            Console.Write("Phone: ");
            string phone = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            Console.Write("Is Admin (true/false): ");
            bool isAdmin = bool.Parse(Console.ReadLine());

            return new User(name, username, address, phone, password,isAdmin);
        }

        public void LoginMenu()
        {
            Console.WriteLine("Username: ");
            var user = Library.FindUser(Console.ReadLine());
            var password = Utils.ReadValidatedString("Password", user.CheckPassword);
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
        public static void SplashScreen()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" ██████╗ ██████╗     ██╗     ██╗██████╗ ██████╗  █████╗ ██████╗ ██╗   ██╗");
            Console.WriteLine("██╔════╝ ╚════██╗    ██║     ██║██╔══██╗██╔══██╗██╔══██╗██╔══██╗╚██╗ ██╔╝");
            Console.WriteLine("██║  ███╗ █████╔╝    ██║     ██║██████╔╝██████╔╝███████║██████╔╝ ╚████╔╝ ");
            Console.WriteLine("██║   ██║ ╚═══██╗    ██║     ██║██╔══██╗██╔══██╗██╔══██║██╔══██╗  ╚██╔╝  ");
            Console.WriteLine("╚██████╔╝██████╔╝    ███████╗██║██████╔╝██║  ██║██║  ██║██║  ██║   ██║   ");
            Console.WriteLine(" ╚═════╝ ╚═════╝     ╚══════╝╚═╝╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝   ");
            Console.WriteLine();
            Console.ForegroundColor = DefaultTextColor;
        }

        public static void NewConsole()
        {
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

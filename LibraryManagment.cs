using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoGestaoBiblioteca
{
    internal class LibraryManagment
    {
    }

        public void LoginMenu()
        {
            Console.WriteLine("Username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Password: ");
            string password = Console.ReadLine();
            if (username == "admin" && password == "admin")
            {
                Console.WriteLine("Login successful!");
                // Call the method to display the admin menu
                AdminMenu();
            }
            else if (username == "user" && password == "user")
            {
                Console.WriteLine("Login successful!");
                // Call the method to display the user menu
                UserMenu();
            }
            else
            {
                Console.WriteLine("Invalid username or password.");
            }
        }
        public void AdminMenu()
        {
            Console.WriteLine("Admin Menu:");
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
                    break;
                case "2":
                    // View user logic
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
    } }

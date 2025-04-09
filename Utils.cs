using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoGestaoBiblioteca
{
    internal class Utils //short for Utilities; utility methods that perform common tasks
    {
        //global? constants
        public const ConsoleColor DefaultTextColor = ConsoleColor.Gray; //default foreground color
        public const ConsoleColor DefaultBackColor = ConsoleColor.DarkBlue; //default background color

        /// <summary>
        /// Reads a non-empty string.
        /// </summary>
        /// <param name="message">The message asking for a non-empty string.</param>
        /// <returns>A non-empty string.</returns>
        public static string ReadNonEmptyString(string message)
        {
            string? input;

            do
            {
                Console.Write(message + " ");
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) WriteError("You didn't write anything!");
                else break;
            }
            while (true);

            return input; //no need for null-forgiving operator?
        }

        /// <summary>
        /// Reads a decimal.
        /// </summary>
        /// <param name="message">The message asking for the decimal.</param>
        /// <param name="min">The min value.</param>
        /// <returns>The decimal.</returns>
        public static decimal ReadDecimal(string message, decimal? min = null)
        {
            decimal value;
            bool isValid;

            do
            {
                Console.Write(message + " ");

                isValid = decimal.TryParse(Console.ReadLine(), out value)
                    && (min == null || value >= min);

                // check what happens without () for the terniary operator
                if (!isValid) WriteError("Value must be a decimal" + (min != null ? " >= " + min : "") + "!");
                else break;

            } while (true);

            return value;
        }

        /// <summary>
        /// Reads an int.
        /// </summary>
        /// <param name="message">The message asking for the int.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>The int.</returns>
        public static int ReadInt(string message, int? min = null, int? max = null)
        {
            int value;
            bool isValid;
            do
            {
                Console.Write(message + " ");

                isValid = int.TryParse(Console.ReadLine(), out value)
                    && (min == null || value >= min)
                    && (max == null || value <= max);

                if (!isValid) WriteError("Input must be an int"
                    + (min != null && max != null ? " between " + min + " and " + max
                    : min != null ? " >= " + min
                    : max != null ? " <= " + max : "")
                    + "!");

            } while (!isValid);
            return value;
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

        /// <summary>
        /// Prints error message in red color.
        /// </summary>
        /// <param name="error">The error message.</param>
        public static void WriteError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red; //change foreground color
            Console.WriteLine(error);
            Console.ForegroundColor = DefaultTextColor; //restore the default color
        }

        public static void WriteWithEmphasis(string message)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Write(message);
            Console.ForegroundColor = DefaultTextColor;
            Console.BackgroundColor = DefaultBackColor;
        }
    }
}

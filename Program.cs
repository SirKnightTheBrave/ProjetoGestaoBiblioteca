namespace ProjectoGestaoBiblioteca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var library = new Library("CESAE LIBRARY", 1, TimeSpan.FromDays(3)); //1 aluguer por utilizador, 3 dias de aluguer

            var consoleApp = new ConsoleApp(library, ConsoleColor.DarkBlue, ConsoleColor.White);

            consoleApp.Library.AddUser(new User("admin", "admin", "password", true));

            consoleApp.LoginMenu();

        }
       
    }
}

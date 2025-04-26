namespace ProjectoGestaoBiblioteca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "server=localhost;database=library_management;user=root;password=;";
            var library = new Library("CESAE LIBRARY", 1, TimeSpan.FromDays(3), 0.10m); //1 aluguer por utilizador, 3 dias de aluguer
            var consoleApp = new ConsoleUI(library, connectionString, ConsoleColor.DarkBlue, ConsoleColor.White);
            //consoleApp.Library.AddUser(UserFactory.Create("admin", "admin", "password", true));
            //consoleApp.Library.AddUser(UserFactory.Create("user", "user", "password", false));
            consoleApp.LoginMenu();

        }
       
    }
}

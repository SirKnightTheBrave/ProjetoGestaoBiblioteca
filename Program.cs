namespace ProjectoGestaoBiblioteca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "server=localhost;database=library_management;user=root;password=;";
            var library = new Library("CESAE LIBRARY", 1, TimeSpan.FromDays(3), 0.10m); //1 aluguer por utilizador, 3 dias de aluguer, 10 cêntimos de multa por dia
            var consoleApp = new ConsoleUI(library, connectionString, ConsoleColor.DarkBlue, ConsoleColor.White);
            //consoleApp.Library.AddUser(UserFactory.Create("admin", "admin", "password", true)); //admin de teste sem integração com a BD
            //consoleApp.Library.AddUser(UserFactory.Create("user", "user", "password", false)); //user de teste sem integração com a BD
            consoleApp.LoginMenu();

        }
       
    }
}

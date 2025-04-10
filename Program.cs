namespace ProjectoGestaoBiblioteca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var library = new Library("Group 3 Library", TimeSpan.FromDays(3));
           //José
            var libraryManagment = new ConsoleUI(library);
            libraryManagment.AdminMenu();

        }
       
    }
}

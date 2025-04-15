namespace ProjectoGestaoBiblioteca
{
    static class UserFactory
    {
        // deveria estar numa classe userFactory -> possui a logica de como criar users (ver o que é a factory design pattern)
        public static User Create(string name, string username, string password, bool isAdmin = false, string? address = null, string? phone = null, bool hashPassword = true)
        {
            // hash password se não tiver hashed (registo)
            // não fazer hash se já estiver (carregamento da BD no startup)
            password = hashPassword ? password.GetHashCode().ToString() : password;
            return new User(name, username, password, isAdmin, address, phone);
        }
    }
}


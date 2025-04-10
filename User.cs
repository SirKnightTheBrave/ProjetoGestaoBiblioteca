using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoGestaoBiblioteca
{
    internal class User
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        private string Password { get; set; } //segurança, não expor a password
        public bool IsAdmin { get; set; } // Adiciona propriedade IsAdmin para verificar se é admin ou não

        public User(string name, string username, string address, string phone, string password, bool isAdmin)
        {
            Name = name;
            Username = username;
            Address = address;
            Phone = phone;
            Password = password;
            IsAdmin = isAdmin;
        }

        public string GetInfo()
        {
            return $"User: {Username}, Name: {Name}, Access level: {(IsAdmin ? "Admin" : "User")}, Address: {Address}, " +
                $"Phone: {Phone} \n";//criação de string com dados do user para a lista de users (classe library)
        }

        public bool CheckPassword(string password)
        {
            return Password == password; //verifica se a password é igual à password do user
        }


    }
}


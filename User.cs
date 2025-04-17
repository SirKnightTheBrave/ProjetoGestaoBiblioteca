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
        public string? Phone { get; set; }
        // coloquei public porque a password já está hashed / só não expôr para a interface
        public string HashedPassword { get; set; } //segurança, não expor a password
        public List<Copy>  CurrentLoans { get; private set; }
        public bool IsAdmin { get; set; } // Adiciona propriedade IsAdmin para verificar se é admin ou não
        // public bool IsBlocked { get; set; } // Adiciona propriedade IsBlocked para verificar se o user está bloqueado ou não
        
        public User(string name, string username, string hashPassword, string address, string? phone = null, bool isAdmin = false)
        {
            Name = name;
            Username = username;
            Address = address;
            Phone = phone;
            HashedPassword = hashPassword;
            IsAdmin = isAdmin;
            CurrentLoans = new List<Copy>(); // Inicializa uma lista vazia de empréstimos atuais
        }

        public override string ToString()
        {
            return $"User: {Username}, Name: {Name}, Access level: {(IsAdmin ? "Admin" : "User")}, Address: {Address}, " +
                $"Phone: {Phone} \n"; //criação de string com dados do user para a lista de users (classe library)
        }

        public bool CheckPassword(string password)
        {
            return HashedPassword == password.GetHashCode().ToString(); //verifica se a password é igual à password do user
        }

        public void AddLoan(Copy copy)
        {
            if (CurrentLoans.Count < 3) // Limite de 3 empréstimos por utilizador, pode ser alterado
            {
                CurrentLoans.Add(copy);
            }
            else
            {
                throw new InvalidOperationException("User has reached the maximum number of loans.");
            }
        }
     public void RemoveLoan(Copy copy)
        {
            if (CurrentLoans.Contains(copy))
            {
                CurrentLoans.Remove(copy);
            }
            else
            {
                throw new InvalidOperationException("User does not have this loan.");
            }
        }
        
        public void ClearLoans()
        {
            CurrentLoans.Clear();
        }

    }
}


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

        public User(string name, string username, string address, string phone)
        {
            Name = name;
            Username = username;
            Address = address;
            Phone = phone;
        }

        public override string ToString()
        {
            return $"User: {Username}, Name: {Name}, Address: {Address}," +
                $"Phone: {Phone}\n";//criação de string com dados do user para a lista de users (classe library)
        }

        
    }
}


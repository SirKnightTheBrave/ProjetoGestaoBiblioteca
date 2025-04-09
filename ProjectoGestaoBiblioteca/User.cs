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

        //Viviane
    }
}


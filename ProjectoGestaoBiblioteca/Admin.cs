using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoGestaoBiblioteca
{
    internal class Admin : User
    {
        public string Password { get; set; }

        public Admin(string name, string address, string phone, string password) : base(name, address, phone)
        {
            Password = password;
        }
    }
}


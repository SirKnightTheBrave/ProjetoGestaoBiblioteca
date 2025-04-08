using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoGestaoBiblioteca
{
    internal class Utilizador
    {
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string NumeroTelefone { get; set; }

        public Livro LivroRequisitado { get; set; } // apenas um livro por utilizador
    }
}

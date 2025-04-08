using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoGestaoBiblioteca
{
    internal class Biblioteca
    {
        public List<Utilizador> Utilizadores { get; set; }
        public List<Livro> Livros { get; set; }

        public Biblioteca(List<Utilizador>? utilizadores = null, List<Livro>? livros = null)
        {
            Utilizadores = utilizadores ?? new List<Utilizador>();
          
            Livros = livros ?? new List<Livro>();
        }

        public void 
    }
}

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectoGestaoBiblioteca
{

    public class DBContext
    {
    private string ConnectionString { get; set; }
        public DBContext(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}

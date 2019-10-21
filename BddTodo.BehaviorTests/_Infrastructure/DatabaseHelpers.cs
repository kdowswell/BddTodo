using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BddTodo.Tests._Infrastructure
{
    public static class DatabaseHelpers
    {
        public static string ConnString = "Server=(localdb)\\mssqllocaldb;Database=BddTodo_Test;Trusted_Connection=True;ConnectRetryCount=0";
    }
}

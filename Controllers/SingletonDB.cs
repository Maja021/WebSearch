using MVCLogin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCLogin.Controllers
{
    public sealed class SingletonDB
    {
        private static readonly SingletonDB instance = new SingletonDB();
        private readonly LoginDataBaseEntities db = new LoginDataBaseEntities();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static SingletonDB()
        {
        }

        private SingletonDB()
        {
        }

        public static SingletonDB Instance
        {
            get
            {
                return instance;
            }
        }

        public LoginDataBaseEntities GetDBConnection()
        {
            return db;
        }

    }
}
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealWorldApp.Models
{
    public class Register
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

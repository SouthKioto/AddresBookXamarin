using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AddresBook.Classes
{
    public class AddresData
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string nazwisko { get; set; }
        public string imie { get; set; }
        public string telefon { get; set; }
        public string email { get; set; }
    }
}

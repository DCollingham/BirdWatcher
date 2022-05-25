using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWatcher
{
    public class Bird
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }

    }
}

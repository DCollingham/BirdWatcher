using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace BirdWatcher
{
    public class Bird
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }
        public string Family { get; set; }
        public string Species { get; set; }
        public int Certainty { get; set; }
    }
}

using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace BirdWatcher
{
    public class Bird //Bird object is stored in SQLite Database
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateSpotted { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

    }
}

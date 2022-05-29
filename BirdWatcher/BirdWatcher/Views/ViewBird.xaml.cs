using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BirdWatcher.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewBird : ContentPage
    {
        public Bird Bird { get; set; }
        public ViewBird(Bird bird)
        {
            InitializeComponent();
            Bird = bird;
            FillLabels();

        }

        private void FillLabels()
        {
            ImageName.Source =  Bird.ImageUrl;
            birdName.Text = "Name: " + Bird.Name;
            birdLocation.Text = "Location: " + Bird.Location;
            birdFamily.Text = "Family: " + Bird.Family;
            birdSpecies.Text = "Species: " + Bird.Species;
            dateSpotted.Text = "Date Spotted: " + Bird.DateSpotted;
        }
    }
}
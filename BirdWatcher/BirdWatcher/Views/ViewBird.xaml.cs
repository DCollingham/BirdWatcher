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
            dateSpotted.Text = "Date Spotted: " +
            string.Format(string.Format("{0:ddd, MMM d, yyyy}", Bird.DateSpotted));
            longitude.Text = "longitude: " + Bird.Longitude;
            latitude.Text = "latitude: " + Bird.Latitude;
        }
    }
}
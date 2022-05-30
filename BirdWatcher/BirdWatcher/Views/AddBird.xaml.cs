using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using System.IO;

namespace BirdWatcher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBird : ContentPage
    {
        public string ImageFilePath { get; set; } //Set Image Path Property
        public Location BirdLocation { get; set; } // Set Location Property
        public AddBird()
        {
            InitializeComponent();
            resultImage.Source = "BirdPlaceholder.png";
        }
 

        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text) && !string.IsNullOrWhiteSpace(locationEntry.Text))
            {
                await App.Database.SaveBirdAsync(new Bird //Creates new Bird object & Saves to DB
                {
                    Name = nameEntry.Text,
                    Location = locationEntry.Text,
                    ImageUrl = ImageFilePath,
                    DateSpotted = datePicker.Date,
                    Longitude = BirdLocation.Longitude,
                    Latitude = BirdLocation.Latitude
                });

                ClearLabels();
            }
        }

        private async void AddPhoto_Clicked(object sender, EventArgs e) //Adds a photo
        {
            FileResult result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions //Asigns photo to result
            {
                Title = "Pick a photo"
            });

            if (result != null)
            {
                ImageFilePath = Path.Combine(FileSystem.AppDataDirectory, result.FullPath);
                Stream stream = await result.OpenReadAsync();
                resultImage.Source = ImageSource.FromStream(() => stream);
            }
        }

        async private void TakePhoto_Clicked(object sender, EventArgs e)
        {
            FileResult result = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
            {
                Title = "Take a photo"
            });

            if(result != null)
            {

                ImageFilePath = Path.Combine(FileSystem.AppDataDirectory, result.FullPath);
                Debug.WriteLine($"This is the path {ImageFilePath}");
                Stream stream = await result.OpenReadAsync();
                resultImage.Source = ImageSource.FromStream(() => stream);
                BirdLocation = GetLocation().Result;
            }
        }
        private void ClearLabels()
        {
            nameEntry.Text = string.Empty;
            locationEntry.Text = string.Empty;
            ImageFilePath = string.Empty;
            resultImage.Source = "BirdPlaceholder.png";
        }

        private async Task<Location> GetLocation()
        {
            Location location = null;

            try
            {
                location = await Geolocation.GetLastKnownLocationAsync();
                if(location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest(
                                    GeolocationAccuracy.Lowest, TimeSpan.FromSeconds(3)));                   
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Woops: {ex.Message}");
            }
        return location;
        }
    }
}
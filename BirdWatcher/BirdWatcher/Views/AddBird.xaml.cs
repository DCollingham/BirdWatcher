using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using System.IO;
using ExifLib;

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
            BirdLocation = GetLocation().Result; //Gets Location when loading page
            resultImage.Source = "BirdPlaceholder.png"; //Placeholder image for a bird
        }
 

        async void OnButtonClicked(object sender, EventArgs e)
        {
            //Runs if either name or location filled-in
            if (!string.IsNullOrWhiteSpace(nameEntry.Text) || !string.IsNullOrWhiteSpace(locationEntry.Text))
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

        private async void AddPhoto_Clicked(object sender, EventArgs e) //Add a photo from file
        {
            FileResult result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions //Asigns photo to result
            {
                Title = "Pick a photo"
            });

            if (result != null)
            {
                ImageFilePath = Path.Combine(FileSystem.AppDataDirectory, result.FullPath); //Gets file path
                Stream stream = await result.OpenReadAsync(); //Creates stream
                resultImage.Source = ImageSource.FromStream(() => stream); //Sets page image from stream
                SetLocation(result); //Gets location from Photo Metadata

            }
        }

        //Take photo to add to sighting
        private async void TakePhoto_Clicked(object sender, EventArgs e) 
        {
            FileResult result = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions 
            {
                Title = "Take a photo"
            });

            if(result != null)
            {
                //Gets filepath and GPS location
                ImageFilePath = Path.Combine(FileSystem.AppDataDirectory, result.FullPath);
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

        //Gets GPS location
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

        //Sets location property to Metadata location
        private async void SetLocation(FileResult result)
        {
            Stream infoStream = await result.OpenReadAsync();
            JpegInfo imageInfo = ExifReader.ReadJpeg(infoStream);
            BirdLocation.Latitude = imageInfo.GpsLatitude[0];
            BirdLocation.Longitude = imageInfo.GpsLongitude[0];
        }
    }
}
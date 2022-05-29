using System;
using System.Diagnostics;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BirdWatcher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Edit : ContentPage
    {
        public string ImageFilePath { get; set; }
        public Bird Bird { get; set; }
        private bool imageChanged = false;
        public Edit(Bird bird)
        {
            InitializeComponent();
            Bird = bird;
            FillBirdLabels();
        }


        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text) && !string.IsNullOrWhiteSpace(locationEntry.Text))
            {
                Bird.Name = nameEntry.Text;
                Bird.Location = locationEntry.Text;
                Bird.DateSpotted = datePicker.Date;

                if (imageChanged)
                {
                    Bird.ImageUrl = ImageFilePath;
                }
                _ = await App.Database.EditBird(Bird);
                await Navigation.PopModalAsync();
            }
        }

        async private void AddPhoto_Clicked(object sender, EventArgs e)
        {
            FileResult result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Pick a photo"
            });

            if (result != null)
            {
                ImageFilePath = Path.Combine(FileSystem.AppDataDirectory, result.FullPath);
                Stream stream = await result.OpenReadAsync();
                previousImage.Source = ImageSource.FromStream(() => stream);
                imageChanged = true;
            }
        }

        async private void TakePhoto_Clicked(object sender, EventArgs e)
        {
            FileResult result = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
            {
                Title = "Take a photo"
            });

            if (result != null)
            {

                ImageFilePath = Path.Combine(FileSystem.AppDataDirectory, result.FullPath);
                Debug.WriteLine($"This is the path {ImageFilePath}");
                Stream stream = await result.OpenReadAsync();
                previousImage.Source = ImageSource.FromStream(() => stream);
                imageChanged = true;
            }
        }

        private void FillBirdLabels()
        {
            nameEntry.Text = Bird.Name;
            locationEntry.Text = Bird.Location;
            previousImage.BindingContext = Bird;
            datePicker.Date = Bird.DateSpotted;

        }
    }
}
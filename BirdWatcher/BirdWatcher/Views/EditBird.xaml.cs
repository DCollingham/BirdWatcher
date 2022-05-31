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
        public string ImageFilePath { get; set; } //Filepath property
        public Bird Bird { get; set; } //Bird object

        private bool _imageChanged = false; //Changes if user adds new image
        public Edit(Bird bird)
        {
            InitializeComponent();
            Bird = bird; //Set Bird object to contructor parameter
            FillBirdLabels(); //Fill page entrys
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            //Updates bird with new information
            if (!string.IsNullOrWhiteSpace(nameEntry.Text) || !string.IsNullOrWhiteSpace(locationEntry.Text))
            {
                Bird.Name = nameEntry.Text;
                Bird.Location = locationEntry.Text;
                Bird.DateSpotted = datePicker.Date;

                if (_imageChanged)
                {
                    Bird.ImageUrl = ImageFilePath; //Changes image file path if changed
                }
                _ = await App.Database.EditBird(Bird); //Submits edit
                _ = await Navigation.PopModalAsync(); //Closes page
            }
        }

        private async void AddPhoto_Clicked(object sender, EventArgs e)
        {
            FileResult result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Pick a photo"
            });
            //Gets filepath of image
            if (result != null)
            {
                ImageFilePath = Path.Combine(FileSystem.AppDataDirectory, result.FullPath);
                Stream stream = await result.OpenReadAsync();
                previousImage.Source = ImageSource.FromStream(() => stream);
                _imageChanged = true;
            }
        }

        private async void TakePhoto_Clicked(object sender, EventArgs e)
        {
            FileResult result = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
            {
                Title = "Take a photo"
            });
            //Get filepath of image
            if (result != null)
            {
                ImageFilePath = Path.Combine(FileSystem.AppDataDirectory, result.FullPath);
                Stream stream = await result.OpenReadAsync();
                previousImage.Source = ImageSource.FromStream(() => stream);
                _imageChanged = true;
            }
        }

        //Fill entry fields with selected Bird object
        private void FillBirdLabels()
        {
            nameEntry.Text = Bird.Name;
            locationEntry.Text = Bird.Location;
            previousImage.BindingContext = Bird;
            datePicker.Date = Bird.DateSpotted;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public string imageFilePath { get; set; }

        public AddBird()
        {
            InitializeComponent();
        }
 

        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text) && !string.IsNullOrWhiteSpace(locationEntry.Text))
            {
                await App.Database.SaveBirdAsync(new Bird
                {
                    Name = nameEntry.Text,
                    Location = locationEntry.Text,
                    ImageUrl = imageFilePath
                });

                nameEntry.Text = locationEntry.Text = string.Empty;
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
                imageFilePath = Path.Combine(FileSystem.AppDataDirectory, result.FullPath);
                System.IO.Stream stream = await result.OpenReadAsync();
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

                imageFilePath = Path.Combine(FileSystem.AppDataDirectory, result.FullPath);
                Debug.WriteLine($"This is the path {imageFilePath}");         
                System.IO.Stream stream = await result.OpenReadAsync();
                resultImage.Source = ImageSource.FromStream(() => stream);
            }

        }
    }
}
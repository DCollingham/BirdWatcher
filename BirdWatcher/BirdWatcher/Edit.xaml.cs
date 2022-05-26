using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BirdWatcher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Edit : ContentPage
    {
        public string imageFilePath { get; set; }
        public Bird Bird { get; set; }

        public Edit(Bird bird)
        {
            InitializeComponent();
            Bird = bird;
            fillBirdLabels();
            Debug.WriteLine($" Bird Id: {Bird.ID}");
        }


        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text) && !string.IsNullOrWhiteSpace(locationEntry.Text))
            {
                Bird.Name = nameEntry.Text;
                Bird.Location = locationEntry.Text;
                int result = await App.Database.EditBird(Bird);

                nameEntry.Text = locationEntry.Text = string.Empty;
                Debug.WriteLine(result);
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

            if (result != null)
            {

                imageFilePath = Path.Combine(FileSystem.AppDataDirectory, result.FullPath);
                Debug.WriteLine($"This is the path {imageFilePath}");
                System.IO.Stream stream = await result.OpenReadAsync();
                resultImage.Source = ImageSource.FromStream(() => stream);
            }
        }

        private void fillBirdLabels()
        {
            nameEntry.Text = Bird.Name;
            locationEntry.Text = Bird.Location;
            previousImage.BindingContext = Bird;
        }
    }
}
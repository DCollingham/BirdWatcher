using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BirdWatcher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBird : ContentPage
    {
        public AddBird()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetBirdsAsync();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text) && !string.IsNullOrWhiteSpace(locationEntry.Text))
            {
                await App.Database.SaveBirdAsync(new Bird
                {
                    Name = nameEntry.Text,
                    Location = locationEntry.Text
                });

                nameEntry.Text = locationEntry.Text = string.Empty;
                collectionView.ItemsSource = await App.Database.GetBirdsAsync();
            }
        }

    }
}
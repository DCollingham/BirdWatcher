using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

namespace BirdWatcher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Gallery : ContentPage
    {
        public object Source { get; set; }
        public Gallery()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
       //https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/collectionview/populate-data
            base.OnAppearing();
            birdCollection.ItemsSource = await App.Database.GetBirdsAsync();
        }

        private void DeleteItem_Invoked(object sender, EventArgs e)
        {
            
            var swipeview = sender as SwipeItem;
            var id = swipeview.BindingContext;
            Database.DeleteBird(id);
            Debug.WriteLine(id);
        }
    }
}
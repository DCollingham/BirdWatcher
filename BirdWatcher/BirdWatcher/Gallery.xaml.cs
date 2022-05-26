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
using System.Collections.ObjectModel;

namespace BirdWatcher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Gallery : ContentPage
    {
        public object Source { get; set; }
        ObservableCollection<Bird> Birds;
        public Gallery()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            //https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/collectionview/populate-data
            base.OnAppearing();
            Birds = App.Database.GetBirdsObservableAsync();
            birdCollection.ItemsSource = Birds;
        }

        private void DeleteItem_Invoked(object sender, EventArgs e)
        {

            SwipeItem swipeview = sender as SwipeItem;
            Bird bird = (Bird)swipeview.CommandParameter;
            _ = App.Database.DeleteBird(bird);
            Birds.Remove(bird);
        }

        private void SwipeItem_Invoked(object sender, EventArgs e)
        {
            SwipeItem swipeview = sender as SwipeItem;
            Bird bird = (Bird)swipeview.CommandParameter;
            _ = Navigation.PushModalAsync(new NavigationPage(new Edit(bird)));
        }
    }
}
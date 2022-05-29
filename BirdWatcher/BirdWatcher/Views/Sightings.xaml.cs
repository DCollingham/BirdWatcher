using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using BirdWatcher.Views;
using System.Diagnostics;

namespace BirdWatcher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Gallery : ContentPage
    {
        public ObservableCollection<Bird> Birds { get; set; } //Create Observable Collection
        public Gallery()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing() 
        {
            base.OnAppearing(); //Executes on appearing
            Birds = await App.Database.GetBirdsObservableAsync(); //Populates Bird Collection
            birdCollection.ItemsSource = Birds; //Binds birdCollection in XAML to Birds

        }

        //Invoked by clicking delete button on swipe left
        private void DeleteItem_Invoked(object sender, EventArgs e) 
        {

            SwipeItem swipeview = sender as SwipeItem;
            Bird bird = (Bird)swipeview.CommandParameter;
            _ = App.Database.DeleteBird(bird);
            _ = Birds.Remove(bird);
        }

        private void SwipeItem_Invoked(object sender, EventArgs e)
        {
            SwipeItem swipeview = sender as SwipeItem;
            Bird bird = (Bird)swipeview.CommandParameter;
            _ = Navigation.PushModalAsync(new NavigationPage(new Edit(bird)));
        }

        private void SwipeItem_DetailsInvoked(object sender, EventArgs e)
        {
            SwipeItem swipeview = sender as SwipeItem;
            Bird bird = (Bird)swipeview.CommandParameter;
            Debug.WriteLine(bird.Name);
            _ = Navigation.PushModalAsync(new NavigationPage(new ViewBird(bird)));
        }
    }
}
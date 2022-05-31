using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using BirdWatcher.Views;
using System.Diagnostics;
using System.Collections.Generic;

namespace BirdWatcher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Gallery : ContentPage
    {
        public ObservableCollection<Bird> Birds { get; set; } //Create Observable Collection
        List<SwipeView> SwipeViews { set; get; }
        public Gallery()
        {
            InitializeComponent();
            SwipeViews = new List<SwipeView>(); //Initialises swipeview list
        }

        protected override async void OnAppearing() 
        {
            base.OnAppearing(); //Executes on appearing
            Birds = await App.Database.GetBirdsObservableAsync(); //Populates Bird Collection
            birdCollection.ItemsSource = Birds; //Binds birdCollection viewCollection to Birds OC
        }

        //Invoked by clicking delete button on swipe left
        private void DeleteItem_Invoked(object sender, EventArgs e) 
        {

            SwipeItem swipeview = sender as SwipeItem; //Gets swipeitem from sender object
            Bird bird = (Bird)swipeview.CommandParameter; //Gets bird object
            _ = App.Database.DeleteBird(bird); //Deletes object from Database
            _ = Birds.Remove(bird); //Deletes object from observable collection
        }

        private void SwipeItem_Invoked(object sender, EventArgs e)
        {
            //Get bird object
            SwipeItem swipeview = sender as SwipeItem;
            Bird bird = (Bird)swipeview.CommandParameter;
            _ = Navigation.PushModalAsync(new NavigationPage(new Edit(bird)));
        }

        private void SwipeItem_DetailsInvoked(object sender, EventArgs e)
        {
            //Get bird object
            SwipeItem swipeview = sender as SwipeItem;
            Bird bird = (Bird)swipeview.CommandParameter;
            //Push new page with bird object
            _ = Navigation.PushModalAsync(new NavigationPage(new ViewBird(bird)));
        }


        //https://stackoverflow.com/questions/62874278/how-can-i-make-xamarin-forms-close-open-swiped-views
        private void SwipeView_SwipeStarted(object sender, SwipeStartedEventArgs e)
        {

            if (SwipeViews.Count == 1)
            {
                SwipeViews[0].Close();
                _ = SwipeViews.Remove(SwipeViews[0]);
            }
        }

        private void SwipeView_SwipeEnded(object sender, SwipeEndedEventArgs e)
        {
            SwipeView swipeview = sender as SwipeView;
            SwipeViews.Add(swipeview);
        }
    }
}
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
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
            BindingContext = this;
        }

        protected async  override void OnAppearing()
        {
            //https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/collectionview/populate-data
            base.OnAppearing();
            Birds = await App.Database.GetBirdsObservableAsync();
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
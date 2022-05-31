using BirdWatcher.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BirdWatcher
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void AddEntryClicked(object sender, EventArgs e)
        {
            //New Navigation window on button clicked
            Navigation.PushModalAsync(new NavigationPage(new AddBird()));
        }

        private void Gallery_Clicked(object sender, EventArgs e)
        {
            //New Navigation window on button clicked
            _ = Navigation.PushModalAsync(new NavigationPage(new Gallery()));
        }

        private void Information_Clicked(object sender, EventArgs e)
        {
            //New Navigation window on button clicked
            _ = Navigation.PushModalAsync(new NavigationPage(new Information()));
        }
    }
}

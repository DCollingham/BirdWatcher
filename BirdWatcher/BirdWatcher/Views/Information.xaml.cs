using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BirdWatcher.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Information : ContentPage
    {
        public Information()
        {
            InitializeComponent();
        }

        public async void Handle_Tapped(object sender, EventArgs e)
        {
            //Go to link when clicked
            string url = "https://www.rspb.org.uk/birds-and-wildlife/wildlife-guides/birdwatching/";
            await Browser.OpenAsync(new Uri(url), BrowserLaunchMode.SystemPreferred);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BirdWatcher
{
    public class BirdViewModel //Non-working attempt at integrating MVVM
    {
        public ObservableCollection<Bird> Birds { get; }
        public Command LoadItemsCommand { get; }
        public BirdViewModel()
        {

            Birds = new ObservableCollection<Bird>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        private async Task ExecuteLoadItemsCommand()
        {
            try
            {
                ObservableCollection<Bird> Birds = await App.Database.GetBirdsObservableAsync();
                foreach (Bird item in Birds)
                {
                    Birds.Add(item);
                    Debug.WriteLine(item.Name);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                //IsBusy = false;
            }
        }
    }
}

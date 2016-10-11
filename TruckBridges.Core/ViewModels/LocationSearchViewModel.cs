// Created by Tim Heinz - n8683981

using MvvmCross.Core.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TruckBridges.Core.Database;
using TruckBridges.Core.Interfaces;
using TruckBridges.Core.Models;
using TruckBridges.Core.Services;

namespace TruckBridges.Core.ViewModels
{
    class LocationSearchViewModel : MvxViewModel
    {
        public System.Windows.Input.ICommand ButtonBack { get; private set; }
        public System.Windows.Input.ICommand ButtonAdd { get; private set; }
        public ICommand SelectItemCommand { get; private set; }



        private ObservableCollection<LocationAutoCompleteResult> locations;

        public ObservableCollection<LocationAutoCompleteResult> Locations
        {
            get { return locations; }
            set { SetProperty(ref locations, value); }
        }

        private string searchTerm;

        public string SearchTerm
        {
            get { return searchTerm; }
            set
            {
                SetProperty(ref searchTerm, value);
                if (searchTerm.Length > 3)
                {
                    SearchLocations(searchTerm);
                }
            }
        }

        public async void SearchLocations(string searchTerm)
        {
            LocationService locationService = new LocationService();
            Locations.Clear();
            var locationResults = await locationService.GetLocations(searchTerm);
            var bestLocationResults = locationResults.Where(location => location.Rank > 80);
            foreach (var item in bestLocationResults)
            {
                Locations.Add(item);
            }
        }

        public ICommand SelectLocationCommand { get; private set; }



        public void AddItem(LocationAutoCompleteResult item)
        {
            if (item.LocalizedName != null)
            {
                if (item.LocalizedName.Trim() != string.Empty)
                {
                    Locations.Add(item);
                }
                else
                {
                    //Note this code just removes spaces from the EditText if that is all was in them
                    //DestinationName = item.LocalizedName;
                }
            }
        }

        public LocationSearchViewModel(ISqlite sqlite)
        {
            Locations = new ObservableCollection<LocationAutoCompleteResult>();

/*            SelectLocationCommand = new
            MvxCommand<LocationAutoCompleteResult>(selectedLocation =>
            {
                var database = new LocationsDatabase(sqlite);
                database.InsertLocation(selectedLocation);
                Close(this);
            });
*/
            SelectLocationCommand = new
            MvxCommand<LocationAutoCompleteResult>(selectedLocation =>
                ShowViewModel<MapViewModel>(selectedLocation));



            ButtonBack = new MvxCommand(() =>
            {
                ShowViewModel<ScanViewModel>();
            });

            ButtonAdd = new MvxCommand(() =>
            {
                //AddItem(new DestinationItem(DestinationName));
                //RaisePropertyChanged(() => DestinationSearchItems);
            });
        }

    }
}

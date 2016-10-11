// Created by Tim Heinz - n8683981

using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using MvvmCross.Droid.Views;
using TruckBridges.Core.Models;
using TruckBridges.Core.ViewModels;

namespace TruckBridges.Droid.Views
{
    [Activity(Label = "View for MapViewModel")]
    public class MapView : MvxActivity, IOnMapReadyCallback
    {
        private GoogleMap map;
        MapViewModel vm;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MapView);
            vm = ViewModel as MapViewModel;

            // init Google Maps MapFragment
            var mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.MapViewMap) as MapFragment;
            mapFragment.GetMapAsync(this);

/*            GoogleMap map = mapFrag.Map;
            if (map != null)
            {
                // The GoogleMap object is ready to go.
                map.MapType = GoogleMap.MapTypeNormal;
                map.UiSettings.ZoomControlsEnabled = true;
                map.UiSettings.CompassEnabled = true;
                map.UiSettings.MapToolbarEnabled = true;
                map.UiSettings.MyLocationButtonEnabled = true;

                moveCamera();
            }
*/
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            vm.OnMapSetup(MoveToLocation, AddStopPin);
            map = googleMap;
            map.MyLocationEnabled = true;
            map.MyLocationChange += Map_MyLocationChange;
            //map.MapLongClick += Map_MapClick;
        }

        private void MoveToLocation(GeoLocation geoLocation, float zoom = 13)
        {
            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(new LatLng(geoLocation.Latitude, geoLocation.Longitude));
            builder.Zoom(zoom);
            var cameraPosition = builder.Build();
            var cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);

            map.MoveCamera(cameraUpdate);
        }

        private void AddStopPin(GeoLocation location)
        {
            var markerOptions = new MarkerOptions();
            markerOptions.SetPosition(new LatLng(location.Latitude, location.Longitude));
            //var min = forecast.DailyForecasts.FirstOrDefault().Temperature.Minimum;
            //var max = forecast.DailyForecasts.FirstOrDefault().Temperature.Maximum;
            //markerOptions.SetSnippet(string.Format("Min {0}{1}, Max {2}{3}", min.Value, min.Unit, max.Value, max.Unit));
            markerOptions.SetTitle(location.Locality);
            map.AddMarker(markerOptions);
        }

        private void Map_MyLocationChange(object sender, GoogleMap.MyLocationChangeEventArgs e)
        {
            map.MyLocationChange -= Map_MyLocationChange;
            var location = new GeoLocation(e.Location.Latitude, e.Location.Longitude, e.Location.Altitude);
            MoveToLocation(location);
            vm.OnMyLocationChanged(location);
        }

/*
        private void Map_MapClick(object sender, GoogleMap.MapLongClickEventArgs e)
        {
            vm.MapTapped(new GeoLocation(e.Point.Latitude, e.Point.Longitude));
        }
*/

        /*
                public void moveCamera()
                {
                    // Construct camera position from the provided location
                    LatLng location = new LatLng(-27.470846, 153.020619);
                    CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
                    builder.Target(location);
                    builder.Zoom(18);
                    builder.Bearing(155);
                    builder.Tilt(65);
                    CameraPosition cameraPosition = builder.Build();
                    CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);

                    MapFragment mapFrag = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.MapViewMap);
                    GoogleMap map = mapFrag.Map;
                    if (map != null)
                    {
                        // Set the camera to the location selected by the user
                        map.MoveCamera(cameraUpdate);
                    }
                }
        */
    }
}

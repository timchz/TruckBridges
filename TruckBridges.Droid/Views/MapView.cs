// Created by Tim Heinz - n8683981

using System.Collections.Generic;
using System.Drawing;
using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using MvvmCross.Droid.Views;
using TruckBridges.Core.Models;
using TruckBridges.Core.ViewModels;
using Android.Content.Res;
using System.IO;
//using Acr.UserDialogs;
//using MvvmCross.Platform;

namespace TruckBridges.Droid.Views
{
    [Activity(Label = "View for MapViewModel")]
    public class MapView : MvxActivity, IOnMapReadyCallback
    {
        MapViewModel vm;
        private GoogleMap map;
        private List<Marker> mapMarkers;
        private List<Polyline> mapPolylines;
        private List<Marker> bridgeMarkers;
        bool firstLocationChange = true;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MapView);
            vm = ViewModel as MapViewModel;

            // init Google Maps MapFragment
            var mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.MapViewMap) as MapFragment;
            mapFragment.GetMapAsync(this);

            mapMarkers = new List<Marker>();
            mapPolylines = new List<Polyline>();
            bridgeMarkers = new List<Marker>();
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            // configure the google map
            map = googleMap;
            map.MyLocationEnabled = true;
            map.MyLocationChange += Map_MyLocationChange;
            //map.MapLongClick += Map_MapClick;
            map.UiSettings.ZoomControlsEnabled = true;
            map.UiSettings.CompassEnabled = false;
            //map.UiSettings.MapToolbarEnabled = true;
            //map.UiSettings.MyLocationButtonEnabled = true;

            // give ViewModel access to specific local methods
            vm.OnMapSetup(MoveToLocation, SetBridgeLocations, SetNewRoute);

            // send bridge data read from json asset to ViewModel
            var bridgeJSON = ReadAsset("lowBridge_2016-04-06.json");
            vm.ReceiveBridgeData(bridgeJSON);
        }

        private void Map_MyLocationChange(object sender, GoogleMap.MyLocationChangeEventArgs e)
        {
            var location = new GeoLocation(e.Location.Latitude, e.Location.Longitude, e.Location.Altitude);
            if (firstLocationChange)
            {
                MoveToLocation(location, 13);
                firstLocationChange = false;
            }
            else
            {
                MoveToLocation(location);
            }
            vm.OnMyLocationChanged(location);

            // check if we're near a bridge
            for (var i = 0; i < bridgeMarkers.Count; i++)
            {
                Android.Locations.Location markerLocation = new Android.Locations.Location("bridge");
                markerLocation.Latitude = bridgeMarkers[i].Position.Latitude;
                markerLocation.Longitude = bridgeMarkers[i].Position.Longitude;
                if (e.Location.DistanceTo(markerLocation) < 100)
                {
                    if (!bridgeMarkers[i].IsInfoWindowShown)
                    {
                        bridgeMarkers[i].Title = "WARNING";
                        bridgeMarkers[i].Snippet = "Approaching Low Clearance Bridge";
                        bridgeMarkers[i].ShowInfoWindow();
                        //vm.OnMyLocationNearBridge(new GeoLocation(marker.Position.Latitude, marker.Position.Longitude));
                    }
                }
                else
                {
                    if (bridgeMarkers[i].IsInfoWindowShown)
                        bridgeMarkers[i].HideInfoWindow();
                }
            }
            
        }

        /*
        private void Map_MapClick(object sender, GoogleMap.MapLongClickEventArgs e)
        {
            //vm.MapTapped(new GeoLocation(e.Point.Latitude, e.Point.Longitude));
            var fakeloc = new Android.Locations.Location("Fake Location");
            fakeloc.Latitude = -27.477369;
            fakeloc.Longitude = 153.026863;
            GoogleMap.MyLocationChangeEventArgs args = new GoogleMap.MyLocationChangeEventArgs(fakeloc);

            Map_MyLocationChange(this, args);
        }
        */

        private string ReadAsset(string name)
        {
            if (name == "")
                return "";

            string content = "";

            AssetManager assets = this.Assets;
            using (StreamReader sr = new StreamReader(assets.Open(name)))
            {
                content = sr.ReadToEnd();
            }

            return content;
        }


        //--------------------------------------------------------------------
        // EXTERNAL METHODS
        //--------------------------------------------------------------------

        private void MoveToLocation(GeoLocation geoLocation, float zoom = -1)
        {
            if (geoLocation == null)
                return;

            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(new LatLng(geoLocation.Latitude, geoLocation.Longitude));
            if (zoom != -1)
            {
                builder.Zoom(zoom);
            }
            else
            {
                builder.Zoom(map.CameraPosition.Zoom);
            }

            //builder.Bearing(155);
            //builder.Tilt(65);
            var cameraPosition = builder.Build();
            var cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);

            map.MoveCamera(cameraUpdate);
        }

        private void SetBridgeLocations(List<GeoArea> locations)
        {
            foreach (var location in locations)
                AddBridge(location.Center);
        }

        private void SetNewRoute(CombinedRoute route)
        {
            RemoveAllMarkers();
            RemoveAllPolylines();
            AddWaypoints(route.waypoint);
            AddLegs(route.leg, route.snappedSpan);
        }


        //--------------------------------------------------------------------
        // SUPPORT METHODS FOR EXTERNAL METHODS
        //--------------------------------------------------------------------

        private void AddBridge(GeoLocation location)
        {
            // add marker
            var markerOptions = new MarkerOptions();
            markerOptions.SetPosition(new LatLng(location.Latitude, location.Longitude));
            markerOptions.SetTitle(location.Locality);
            markerOptions.SetIcon(BitmapDescriptorFactory.FromAsset("bridge32.png"));
            Marker marker = map.AddMarker(markerOptions);
            bridgeMarkers.Add(marker);
        }

        private void AddWaypoints(List<Waypoint> waypoints)
        {
            // marker for start of journey waypoint
            var start = waypoints[0];
            AddMarker(new GeoLocation(start.mappedPosition.latitude, start.mappedPosition.longitude, start.label),
                      BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueCyan));

            // markers for inbetween waypoints
            // (start at index 1 and continue until count-1
            var waypointCount = waypoints.Count;
            for (var i = 1; i < waypointCount - 1; i++)
            {
                var wp = waypoints[i];
                AddMarker(new GeoLocation(wp.mappedPosition.latitude, wp.mappedPosition.longitude, wp.label));
            }

            // marker for end of journey waypoint
            var end = waypoints[waypointCount-1];
            AddMarker(new GeoLocation(end.mappedPosition.latitude, end.mappedPosition.longitude, end.label),
                      BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueGreen));
        }

        private void AddLegs(List<Leg> legs, List<SnappedSpan> snappedSpan)
        {
            foreach (var span in snappedSpan)
            {
                PolylineOptions rectOptions = new PolylineOptions();
                rectOptions.InvokeWidth(7);
                rectOptions.InvokeColor(Color.Blue.ToArgb());

                foreach (var point in span.snappedPoints)
                {
                    rectOptions.Add(new LatLng(point.location.latitude, point.location.longitude));
                }

                map.AddPolyline(rectOptions);
            }

        }


        //--------------------------------------------------------------------
        // MAP DRAWING METHODS
        //--------------------------------------------------------------------

        private void AddPolyline(GeoLocation start, GeoLocation end)
        {
            PolylineOptions rectOptions = new PolylineOptions();
            rectOptions.Add(new LatLng(start.Latitude, start.Longitude));
            rectOptions.Add(new LatLng(end.Latitude,   end.Longitude));
            Polyline polyline = map.AddPolyline(rectOptions);
            mapPolylines.Add(polyline);
        }

        private void RemoveAllPolylines()
        {
            foreach (var polyline in mapPolylines)
                polyline.Remove();

            mapPolylines.Clear();
        }

        private void AddMarker(GeoLocation location, BitmapDescriptor icon = null, string snippet = "")
        {
            var markerOptions = new MarkerOptions();

            markerOptions.SetPosition(new LatLng(location.Latitude, location.Longitude));

            markerOptions.SetTitle(location.Locality);

            if (icon != null)
                markerOptions.SetIcon(icon);

            if (snippet != "")
                markerOptions.SetSnippet(snippet);

            Marker marker = map.AddMarker(markerOptions);
            mapMarkers.Add(marker);
        }

        private void RemoveAllMarkers()
        {
            foreach (var marker in mapMarkers)
                marker.Remove();

            mapMarkers.Clear();
        }

    }
}

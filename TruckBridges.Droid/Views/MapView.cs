// Created by Tim Heinz - n8683981

using Android.App;
using Android.Gms.Maps;
using Android.OS;
using MvvmCross.Droid.Views;

namespace TruckBridges.Droid.Views
{
    [Activity(Label = "View for MapViewModel")]
    public class MapView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MapView);

            // init Google Maps MapFragment
            MapFragment mapFrag = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.MapViewMap);
            GoogleMap map = mapFrag.Map;
            if (map != null)
            {
                // The GoogleMap object is ready to go.
                map.MapType = GoogleMap.MapTypeNormal;
                map.UiSettings.ZoomControlsEnabled = true;
                map.UiSettings.CompassEnabled = true;
            }
        }
    }
}
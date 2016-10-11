// Created by Tim Heinz - n8683981

using Android.App;
using Android.Gms.Common.Apis;
using Android.Gms.Location.Places;
using Android.OS;
using MvvmCross.Droid.Views;


namespace TruckBridges.Droid.Views
{
    [Activity(Label = "View for LocationSearchViewModel")]
    public class LocationSearchView : MvxActivity
    {
        private GoogleApiClient mGoogleApiClient;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LocationSearchView);

/*            mGoogleApiClient = new GoogleApiClient
                           .Builder(Application.Context)
                           .AddApi(Places.GEO_DATA_API)
                           .AddApi(Places.PLACE_DETECTION_API)
                           //.EnableAutoManage(this, this)
                           .Build();
*/
        }
    }
}

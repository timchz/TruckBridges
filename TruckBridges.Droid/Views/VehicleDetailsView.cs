// Created by Tim Heinz - n8683981

using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;

namespace TruckBridges.Droid.Views
{
    [Activity(Label = "View for VehicleDetailsViewModel")]
    public class VehicleDetailsView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.VehicleDetailsView);
        }
    }
}

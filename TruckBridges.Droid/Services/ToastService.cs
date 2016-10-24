// Created by Ronak Patel - n9315144

using Android.App;
using Android.Widget;
using TruckBridges.Core.Interfaces;

namespace TruckBridges.Droid.Services
{
    public class ToastService : IToast
    {
        public void Show(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}

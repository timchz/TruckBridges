using Android.Content;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using MvvmCross.Platform;
using TruckBridges.Core.Interfaces;
//using TruckBridges.Droid.Database;
using TruckBridges.Droid.Services;
using ZXing.Mobile;
//using Acr.UserDialogs;

namespace TruckBridges.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override void InitializeFirstChance()
        {
            Mvx.LazyConstructAndRegisterSingleton<IGeoCoder, GeoCoderService>();
            Mvx.LazyConstructAndRegisterSingleton<IMobileBarcodeScanner, MobileBarcodeScanner>();
            base.InitializeFirstChance();
        }
    }
}

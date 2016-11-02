// Created by Ronak Patel - n9315144

using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmCross.Platform;
using MvvmCross.Core.ViewModels;
using ZXing.Mobile;
using TruckBridges.Core.Interfaces;
using TruckBridges.Core.Models;

namespace TruckBridges.Core.ViewModels
{
    public class ScanViewModel : MvxViewModel
    {
        public ICommand ScanOnceCommand { get; private set; }
        public async void ScanOnce()
        {
            var result = await scanner.Scan();
            OnResult(result);
        }

        public void OnResult(ZXing.Result result)
        {
            var barcode = result.Text;
            //Mvx.Resolve<IToast>().Show(string.Format("Bar code = {0}", barcode));

            var vehicleDetails = new VehicleDetails();
            vehicleDetails.ParseQRCode(barcode);
            ShowViewModel<VehicleDetailsViewModel>(vehicleDetails);
        }
        public IMobileBarcodeScanner scanner;
        public System.Windows.Input.ICommand ButtonCancel { get; private set; }

        public ScanViewModel(IMobileBarcodeScanner scanner)
        {
            this.scanner = scanner;

            ScanOnceCommand = new MvxCommand(ScanOnce);

            ButtonCancel = new MvxCommand(() =>
            {
                ShowViewModel<ScanViewModel>();
            });
        }

    }

}

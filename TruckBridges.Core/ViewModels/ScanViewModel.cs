// Created by Ronak Patel - n9315144

using System.Windows.Input;
using ZXing.Mobile;
using MvvmCross.Core.ViewModels;
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
            if (result != null)
            {
                var barcode = result.Text;

                var vehicleDetails = new VehicleDetails();

                // proceed if the QR code is valid
                if (vehicleDetails.ParseQRCode(barcode) == true)
                    ShowViewModel<VehicleDetailsViewModel>(vehicleDetails);
            }
        }
        public IMobileBarcodeScanner scanner;
        public System.Windows.Input.ICommand ButtonCancel { get; private set; }

        public ScanViewModel(IMobileBarcodeScanner scanner)
        {
            this.scanner = scanner;

            ScanOnceCommand = new MvxCommand(ScanOnce);
        }

    }

}

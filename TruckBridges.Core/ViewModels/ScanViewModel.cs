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
        private ObservableCollection<string> barcodes = new ObservableCollection<string>();
        public ObservableCollection<string> Barcodes
        {
            get { return barcodes; }
            set { SetProperty(ref barcodes, value); }
        }
        public ICommand ScanOnceCommand { get; private set; }
        public async void ScanOnce()
        {
            var result = await scanner.Scan();
            OnResult(result);
        }

        public ICommand ScanContinuouslyCommand { get; private set; }
        public void ScanContinuously()
        {
            var options = new MobileBarcodeScanningOptions();
            options.UseNativeScanning = true;
            scanner.ScanContinuously(OnResult);
        }

        public void OnResult(ZXing.Result result)
        {
            var barcode = result.Text;
//            Barcodes.Add(barcode);
            Mvx.Resolve<IToast>().Show(string.Format("Bar code = {0} added to list", barcode));

            var vehicleDetails = new VehicleDetails();
            vehicleDetails.ParseQRCode(barcode);
            ShowViewModel<VehicleDetailsViewModel>(vehicleDetails);
        }
        public IMobileBarcodeScanner scanner;
        public ICommand GenerateQRCodeCommand { get; private set; }
        public System.Windows.Input.ICommand ButtonCancel { get; private set; }
        public System.Windows.Input.ICommand ButtonMenu { get; private set; }
        public System.Windows.Input.ICommand ButtonDone { get; private set; }

        public ScanViewModel(IMobileBarcodeScanner scanner)
        {
            this.scanner = scanner;

            GenerateQRCodeCommand = new MvxCommand<string>(selectedBarcode =>
            {
                //TODO geneerate the QR Code
            });

            ScanOnceCommand = new MvxCommand(ScanOnce);
            ScanContinuouslyCommand = new MvxCommand(ScanContinuously);


            ButtonCancel = new MvxCommand(() =>
            {
                ShowViewModel<MapViewModel>();
            });

            ButtonMenu = new MvxCommand(() =>
            {
                ShowViewModel<ScanmenuViewModel>();
            });

            ButtonDone = new MvxCommand(() =>
            {
                ShowViewModel<VehicleDetailsViewModel>();
            });
        }

    }

}

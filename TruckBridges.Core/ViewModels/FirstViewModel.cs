using Final.Core.Interfaces;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZXing.Mobile;

namespace Final.Core.ViewModels
{
    public class FirstViewModel 
        : MvxViewModel
    {
        private ObservableCollection<string> barcodes = new
  ObservableCollection<string>();
        public ObservableCollection<string> Barcodes
        {
            get { return barcodes; }
            set { SetProperty(ref barcodes, value); }
        }        public ICommand GenerateQRCodeCommand { get; private set; }
        public ICommand ScanOnceCommand { get; private set; }
        public ICommand ScanContinuouslyCommand { get; private set; }
        public IMobileBarcodeScanner scanner;


        public FirstViewModel(IMobileBarcodeScanner scanner)
        {
            this.scanner = scanner;
                GenerateQRCodeCommand = new MvxCommand<string>(selectedBarcode =>
            {
                //TODO geneerate the QR Code
            });
            ScanOnceCommand = new MvxCommand(ScanOnce);
            ScanContinuouslyCommand = new MvxCommand(ScanContinuously);
        }
        public async void ScanOnce()
        {
            var result = await scanner.Scan();
            OnResult(result);
        }
        public void ScanContinuously()
        {
            var options = new MobileBarcodeScanningOptions();
            options.UseNativeScanning = true;
            scanner.ScanContinuously(OnResult);
        }


        public void OnResult(ZXing.Result result)
        {
            var barcode = result.Text;
            Barcodes.Add(barcode);
            Mvx.Resolve<IToast>().Show(string.Format("Bar code = {0} added to list",
           barcode));
        }




    }
}

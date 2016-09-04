// Created by Ronak Patel - n
// Edited by Tim Heinz - n8683981

using MvvmCross.Core.ViewModels;

namespace TruckBridges.Core.ViewModels
{
    public class ScanViewModel
        : MvxViewModel
    {
        public System.Windows.Input.ICommand ButtonCancel { get; private set; }
        public System.Windows.Input.ICommand ButtonMenu { get; private set; }
        public System.Windows.Input.ICommand ButtonDone { get; private set; }

        public ScanViewModel()
        {
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

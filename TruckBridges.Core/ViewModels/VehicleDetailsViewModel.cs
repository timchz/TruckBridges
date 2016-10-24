// Created by Roslin Punnoose - n9319751

using MvvmCross.Core.ViewModels;
using TruckBridges.Core.Models;

namespace TruckBridges.Core.ViewModels
{
    public class VehicleDetailsViewModel : MvxViewModel
    {
        public VehicleDetails vehicleDetails { get; set; }
        public void Init(VehicleDetails details)
        {
            if (details != null)
                vehicleDetails = details;
            else
                vehicleDetails = new VehicleDetails();
        }

        public System.Windows.Input.ICommand ButtonCancel { get; private set; }
        public System.Windows.Input.ICommand ButtonConfirm { get; private set; }

        public VehicleDetailsViewModel()
        {
            ButtonCancel = new MvxCommand(() =>
            {
                ShowViewModel<ScanViewModel>();
            });

            ButtonConfirm = new MvxCommand(() =>
            {
                ShowViewModel<MapViewModel>();
            });
        }
    }
}

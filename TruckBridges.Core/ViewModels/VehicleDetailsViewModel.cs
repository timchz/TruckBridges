// Created by Tim Heinz - n8683981

using MvvmCross.Core.ViewModels;

namespace TruckBridges.Core.ViewModels
{
    public class VehicleDetailsViewModel
        : MvxViewModel
    {
        private double sliderValueClearance;
        public double SliderValueClearance
        {
            get { return sliderValueClearance; }
            set
            {
                SetProperty(ref sliderValueClearance, value);
            }
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
                ShowViewModel<LocationSearchViewModel>();
            });
        }

    }


}

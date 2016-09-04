// Created by Tim Heinz - n8683981

using MvvmCross.Core.ViewModels;

namespace TruckBridges.Core.ViewModels
{
    public class MapViewModel
        : MvxViewModel
    {
        public System.Windows.Input.ICommand ButtonFinish { get; private set; }
        public System.Windows.Input.ICommand ButtonMenu { get; private set; }

        public MapViewModel()
        {
            ButtonFinish = new MvxCommand(() =>
            {
                ShowViewModel<ScanViewModel>();
            });

            ButtonMenu = new MvxCommand(() =>
            {
            });
        }

    }
}

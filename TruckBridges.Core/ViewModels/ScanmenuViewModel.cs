// Created by Roslin Punnoose - n9319751
// Edited by Tim Heinz - n8683981

using MvvmCross.Core.ViewModels;

namespace TruckBridges.Core.ViewModels
{
	public class ScanmenuViewModel
		: MvxViewModel
	{
        public System.Windows.Input.ICommand ButtonScan { get; private set; }
        public System.Windows.Input.ICommand ButtonHistory { get; private set; }
        public System.Windows.Input.ICommand ButtonSettings { get; private set; }
        public System.Windows.Input.ICommand ButtonHelp { get; private set; }

        public ScanmenuViewModel()
        {
            ButtonScan = new MvxCommand(() =>
            {
                ShowViewModel<ScanViewModel>();
            });

            ButtonHistory = new MvxCommand(() =>
            {
            });

            ButtonSettings = new MvxCommand(() =>
            {
            });

            ButtonHelp = new MvxCommand(() =>
            {
            });
        }
    }

}
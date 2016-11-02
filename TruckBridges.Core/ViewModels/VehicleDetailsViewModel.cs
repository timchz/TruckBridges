// Created by Roslin Punnoose - n9319751

using System.Collections.Generic;
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

        private double heightSliderValue;
        public double HeightSliderValue
        {
            get { return heightSliderValue; }
            set
            {
                SetProperty(ref heightSliderValue, value / 20.0);
            }
        }

        private List<HMCItem> hmcItems;
        public List<HMCItem> HMCItems
        {
            get { return hmcItems; }
            set { hmcItems = value; RaisePropertyChanged(() => HMCItems); }
        }

        private HMCItem selectedHMC;
        public HMCItem SelectedHMC
        {
            get { return selectedHMC; }
            set
            {
                selectedHMC = value;
                RaisePropertyChanged(() => SelectedHMC);
            }
        }

        public System.Windows.Input.ICommand ButtonCancel { get; private set; }
        public System.Windows.Input.ICommand ButtonConfirm { get; private set; }

        public VehicleDetailsViewModel()
        {
            hmcItems = new List<HMCItem>
            {
                new HMCItem("None"),
                new HMCItem("Pollutant", "harmfulToWater"),
                new HMCItem("Flammable", "flammable"),
                new HMCItem("Corrosive", "corrosive")
            };
            selectedHMC = new HMCItem("None");

            ButtonCancel = new MvxCommand(() =>
            {
                ShowViewModel<ScanViewModel>();
            });

            ButtonConfirm = new MvxCommand(() =>
            {
                vehicleDetails.ExtraHeight = HeightSliderValue;
                vehicleDetails.HazardousMaterialClass = SelectedHMC.ApiValue;
                vehicleDetails.Calculate();
                ShowViewModel<MapViewModel>(vehicleDetails);
            });
        }
    }
}

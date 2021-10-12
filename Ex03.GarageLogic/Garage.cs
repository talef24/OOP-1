using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private const int k_NumOfMinutsInHour = 60;
        private readonly Dictionary<string, GarageClient> r_VehiclsCurrentlyInTheGarage;
        private VehicleFactory m_VehicleFactory;

        public VehicleFactory VehiclsFactory
        {
            get { return m_VehicleFactory; }
        }

        public Garage()
        {
            r_VehiclsCurrentlyInTheGarage = new Dictionary<string, GarageClient>();
            m_VehicleFactory = new VehicleFactory();
        }

        private class GarageClient
        {
            private string m_VehicleOwnerName;
            private string m_VehicleOwnerPhoneNumber;
            private Vehicle m_Vehicle;
            private eVehicleStatusInGarage m_StatusOfVehicleInTheGarage;

            internal Vehicle Vehicle
            {
                get { return m_Vehicle; }
            }

            internal eVehicleStatusInGarage StatusInTheGarage
            {
                get { return m_StatusOfVehicleInTheGarage; }
                set { m_StatusOfVehicleInTheGarage = value; }
            }

            internal GarageClient(string i_VehicleOwnerName, string i_VehicleOwnerPhoneNumber, Vehicle i_vehicle)
            {
                m_VehicleOwnerName = i_VehicleOwnerName;
                m_VehicleOwnerPhoneNumber = i_VehicleOwnerPhoneNumber;
                m_Vehicle = i_vehicle;
                m_StatusOfVehicleInTheGarage = eVehicleStatusInGarage.InRepair;
            }

            public override string ToString()
            {
                return string.Format(
 @"Vehicle owner name: {0}
Phone number: {1}
Status in the garage: {2}
{3}",
 m_VehicleOwnerName,
 m_VehicleOwnerPhoneNumber,
 m_StatusOfVehicleInTheGarage.ToString(),
 m_Vehicle.ToString());
            } 
        }

        public bool IsVehicleInTheGarage(string i_LicenseNumber)
        {
            return r_VehiclsCurrentlyInTheGarage.ContainsKey(i_LicenseNumber);
        }

        public void SetStatusToExistedClient(string i_LicenseNumber)
        {
            r_VehiclsCurrentlyInTheGarage[i_LicenseNumber].StatusInTheGarage = eVehicleStatusInGarage.InRepair;
        }

        public void EnterNewVehicleToTheGarge(string i_VehicleOwnerName, string i_VehicleOwnerPhoneNumber, Vehicle i_VehicleToEnterToGarage)
        {
            r_VehiclsCurrentlyInTheGarage.Add(i_VehicleToEnterToGarage.LicenseNumber, new GarageClient(i_VehicleOwnerName, i_VehicleOwnerPhoneNumber, i_VehicleToEnterToGarage));
        }

        public List<string> GetListOfLicenseNumbersOfCurrentVehiclesInTheGarage(string i_StatusInGarage)
        {
            bool getAllLicense;
            List<string> licenseNumbers = new List<string>();
            eVehicleStatusInGarage garageStatusToCheck = (eVehicleStatusInGarage)Enum.Parse(typeof(eVehicleStatusInGarage), i_StatusInGarage);

            getAllLicense = !Enum.IsDefined(typeof(eVehicleStatusInGarage), garageStatusToCheck);

            foreach (KeyValuePair<string, GarageClient> item in r_VehiclsCurrentlyInTheGarage)
            {
                if (item.Value.StatusInTheGarage == garageStatusToCheck || getAllLicense)
                {
                    licenseNumbers.Add(item.Key);
                }
            }

            return licenseNumbers;
        }

        public void ChangeStatusOfVehicleInTheGarage(string i_LicenseNumberOfVehicleToChangeStatus, string i_NewStatus)
        {
            eVehicleStatusInGarage garageStatus = ParseUtils.EnumParse<eVehicleStatusInGarage>(i_NewStatus, " it is not a status in the Garage");
            r_VehiclsCurrentlyInTheGarage[i_LicenseNumberOfVehicleToChangeStatus].StatusInTheGarage = garageStatus;
        }

        public void InflatingAirPressureToMaximum(string i_LicenseNumberOfVehicleToInflateItWheels)
        {
            foreach (Wheel wheel in r_VehiclsCurrentlyInTheGarage[i_LicenseNumberOfVehicleToInflateItWheels].Vehicle.Wheels)
            {
                wheel.InflatingToMaximum();
            }
        }

        public void RefuelingGasVehicle(string i_LicenseNumberOfVehicleToRefuel, string i_FuelQuantityToRefuel, string i_FuelType)
        {
            float amountOfFuelToAdd = ParseUtils.Parse<float>(i_FuelQuantityToRefuel, " is not a valid amount of fuel");
            eFuelType fuelType = ParseUtils.EnumParse<eFuelType>(i_FuelType, " is not a valid type of fuel");

            Vehicle vehicle = r_VehiclsCurrentlyInTheGarage[i_LicenseNumberOfVehicleToRefuel].Vehicle;
            GasEngine gasEngine = vehicle.Engine as GasEngine;

            if (gasEngine != null)
            {
                gasEngine.Refueling(amountOfFuelToAdd, fuelType);
            }
            else
            {
                throw new ArgumentException(i_LicenseNumberOfVehicleToRefuel + " doesn't have a gas engine");
            }
        }

        public void ChargeElectricVehicle(string i_LicenseNumberOfVehicleToCharge, string i_NumOfHoursToAddToBattery)
        {
            float numOfMinutesToCharge = ParseUtils.Parse<float>(i_NumOfHoursToAddToBattery, " is not a valid number of minutes");
            Vehicle vehicle = r_VehiclsCurrentlyInTheGarage[i_LicenseNumberOfVehicleToCharge].Vehicle;

            if (vehicle.Engine is ElectricEngine)
            {
                ElectricEngine electricEngine = vehicle.Engine as ElectricEngine;
                electricEngine.BatteryCharging(numOfMinutesToCharge / k_NumOfMinutsInHour);
            }
            else
            {
                throw new ArgumentException(" doesn't have an elctric engine");
            }
        }

        public static bool IsValidChoiceFromNumOfOptions(int i_NumOfOptions, string i_userChoice)
        {
            int numOfChoice;

            return int.TryParse(i_userChoice, out numOfChoice) &&
                 numOfChoice <= i_NumOfOptions &&
                 numOfChoice > 0;
        }

        public static List<string> GetGrageStatuses()
        {
            return new List<string>(Enum.GetNames(typeof(eVehicleStatusInGarage)));
        }

        public string GetFullDataOfGarageClient(string i_LicenseNumberOfVehicleToShowData)
        {
            return r_VehiclsCurrentlyInTheGarage[i_LicenseNumberOfVehicleToShowData].ToString();
        }

        public bool IsElectricVehicle(string i_LicenseNumber)
        {
            return r_VehiclsCurrentlyInTheGarage[i_LicenseNumber].Vehicle.Engine is ElectricEngine;
        }

        public bool IsGasVehicle(string i_LicenseNumber)
        {
            return r_VehiclsCurrentlyInTheGarage[i_LicenseNumber].Vehicle.Engine is GasEngine;
        }
    }
}

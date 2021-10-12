using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected const int k_Precent = 100;
        private readonly int r_TotalWheelsNumber;
        private readonly string r_ModelName;
        private readonly string r_LicenseNumber;
        private List<Wheel> m_Wheels;
        private float m_MaxWheelsAirPressure;
        private Engine m_Engine;

        internal Vehicle(string i_ModelName, string i_LicenseNumber, float i_MaxAirPressure, int i_NumOfWheels, Engine i_EngineType)
        {
            this.m_MaxWheelsAirPressure = i_MaxAirPressure;
            this.r_TotalWheelsNumber = i_NumOfWheels;
            this.r_ModelName = i_ModelName;
            this.r_LicenseNumber = i_LicenseNumber; 
            this.m_Engine = i_EngineType;
        }

        internal string LicenseNumber
        {
            get { return this.r_LicenseNumber; }
        }

        internal List<Wheel> Wheels
        {
            get { return m_Wheels; }
        }

        internal Engine Engine
        {
            get { return this.m_Engine; }
        }

        public static List<string> BasicProperties()
        {
            List<string> basicProperties = new List<string>();
            basicProperties.Add("Model name");
            return basicProperties;
        }

        public List<string> WheelProperties()
        {
            string currentAirPressureProperty = string.Empty;
            string currentAirPressurePropertyUpdatedAccordingToVehicle = string.Empty;
            List<string> wheelProperties = Wheel.Properties();

            currentAirPressureProperty = wheelProperties[1];
            currentAirPressurePropertyUpdatedAccordingToVehicle = string.Format("{0} (A number between 0 to {1})", currentAirPressureProperty, this.m_MaxWheelsAirPressure);
            wheelProperties[1] = currentAirPressurePropertyUpdatedAccordingToVehicle;

            return wheelProperties;
        }

        public abstract List<string> UniqueProperties
        {
            get;           
            set;
        }

        public void SetVehicleWheels(string i_ProducerName, float i_CurrentAirPressure)
        {
            List<Wheel> vehicleWheels = new List<Wheel>(this.r_TotalWheelsNumber);
            Wheel currentWheelToAdd = null;

            for (int i = 0; i < this.r_TotalWheelsNumber; i++)
            {
                currentWheelToAdd = new Wheel(i_ProducerName, i_CurrentAirPressure, this.m_MaxWheelsAirPressure);
                vehicleWheels.Add(currentWheelToAdd);
            }

            this.m_Wheels = vehicleWheels;
        }

        public override string ToString()
        {
            return string.Format(
@"License Number: {0}
Model name: {1}
{2}
{3}
Precent of energy left in engine: {4}%",
r_LicenseNumber,
r_ModelName,
m_Wheels[0].ToString(),
m_Engine.ToString(),
this.m_Engine.RemainingEnergyInPrecent.ToString()); 
        }
    }
}

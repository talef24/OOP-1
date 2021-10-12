using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private const int k_NumOfWheels = 16;
        private const float k_MaxAirPressureInWheels = 28;
        internal const eFuelType k_FuelType = eFuelType.Soler;
        internal const float k_FuelTankCapacity = 120.0F;
        private bool m_TransportsHazardousMaterials;
        private float m_CargoVolume;

        internal Truck(string i_ModelName, string i_LicenseNumber, Engine i_EngineType) : base(i_ModelName, i_LicenseNumber, k_MaxAirPressureInWheels, k_NumOfWheels, i_EngineType)
        {
        }

        private void setCargoVolume(float i_InputOfCargoVolume)
        {
            if (i_InputOfCargoVolume < 0)
            {
                throw new ValueOutOfRangeException("cargo volume", 0);
            }
            else
            {
                m_CargoVolume = i_InputOfCargoVolume;
            }
        }

        public override List<string> UniqueProperties
        {
            get
            {
                List<string> propeties = new List<string>();
                propeties.Add("Does transports hazardous materials (True or False)");
                propeties.Add("Cargo volume");
                propeties.Add(Engine.Properties);

                return propeties;
            }

            set
            {
                m_TransportsHazardousMaterials = ParseUtils.Parse<bool>(value[0], " is not a valid value");
                this.setCargoVolume(ParseUtils.Parse<float>(value[1], " is not a valid volume for truck cargo"));
                Engine.CurrentOrRemaining = ParseUtils.Parse<float>(value[2], " is not valid amount of energy");
            }
        }

        public override string ToString()
        {
            return string.Format(
@"{0}
Does transports hazardous materials:{1}
Cargo volume:{2}",
base.ToString(),
m_TransportsHazardousMaterials.ToString(),
m_CargoVolume.ToString());
        }
    }
}

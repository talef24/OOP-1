using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private const int k_NumOfWheels = 4;
        private const float k_MaxAirPressureInWheels = 32;
        internal const eFuelType k_FuelType = eFuelType.Octan96;
        internal const float k_FuelTankCapacity = 60.0F;
        internal const float k_MaxBatteryTimeCapacity = 2.1F;
        private const int k_MinNumOfDoors = 2;
        private const int k_MaxNumOfDoors = 5;
        private eCarColor m_Color;
        private eCarNumOfDoors m_NumOfDoors;

        internal Car(string i_ModelName, string i_LicenseNumber, Engine i_EngineType) : base(i_ModelName, i_LicenseNumber, k_MaxAirPressureInWheels, k_NumOfWheels, i_EngineType)
        {
        }

        public override List<string> UniqueProperties
        {
            get
            {
                List<string> propeties = new List<string>();
                propeties.Add(string.Format("Color: ({0})", getCarColors()));
                propeties.Add(string.Format("Number of doors (integer between {0}-{1})", k_MinNumOfDoors, k_MaxNumOfDoors));
                propeties.Add(Engine.Properties);

                return propeties;
            }

            set
            {
                this.m_Color = ParseUtils.EnumParse<eCarColor>(value[0], " is not a valid color for car");
                this.m_NumOfDoors = ParseUtils.EnumParse<eCarNumOfDoors>(value[1], " is not a valid quantity of doors for car");
                Engine.CurrentOrRemaining = ParseUtils.Parse<float>(value[2], " is not valid quantity of energy");
            }
        }    

        public override string ToString()
        {
            return string.Format(
@"{0}
Color: {1}
Number of doors: {2}",
base.ToString(),
m_Color.ToString(),
m_NumOfDoors.ToString());
        }

        private string getCarColors()
        {
            return string.Join("/ ", Enum.GetNames(typeof(eCarColor)));
        }
    }
}

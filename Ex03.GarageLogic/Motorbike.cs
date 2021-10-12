using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Motorbike : Vehicle
    {
        private const int k_NumOfWheels = 2;
        private const float k_MaxAirPressureInWheels = 30;
        internal const eFuelType k_FuelType = eFuelType.Octan95;
        internal const float k_FuelTankCapacity = 7.0f;
        internal const float k_MaxBatteryTimeCapacity = 1.2F;
        
        private eTypeOfMotorbikeLicense m_TypeOfLicense;
        private int m_EngineVolumeInCC;

        private eTypeOfMotorbikeLicense LisenceType
        {
            get { return m_TypeOfLicense; }
            set { m_TypeOfLicense = value; }
        }

        internal Motorbike(string i_ModelName, string i_LicenseNumber, Engine i_EngineType) : base(i_ModelName, i_LicenseNumber, k_MaxAirPressureInWheels, k_NumOfWheels, i_EngineType)
        {
        }

        private void setEngineVolume(int i_InputOfEngineVolume)
        {
            if (i_InputOfEngineVolume < 0)
            {
                throw new ValueOutOfRangeException("engine volume", 0);
            }
            else
            {
                m_EngineVolumeInCC = i_InputOfEngineVolume;
            }
        }

        public override List<string> UniqueProperties
        {
            get
            {
                List<string> propeties = new List<string>();
                propeties.Add(string.Format("License type ({0})", getLisenceTypes()));
                propeties.Add("Engine volume in CC");
                propeties.Add(Engine.Properties);

                return propeties;
            }

            set
            {
                LisenceType = ParseUtils.EnumParse<eTypeOfMotorbikeLicense>(value[0], " is not a Type of motorbike license");
                this.setEngineVolume(ParseUtils.Parse<int>(value[1], " is not a valid cacpacity for motorbike engine"));
                Engine.CurrentOrRemaining = ParseUtils.Parse<float>(value[2], " is not a valid quantity of energy");
            }
        }

        public override string ToString()
        {
            return string.Format(
@"{0}
Lisence type: {1}
Engine capacity: {2}",
base.ToString(),
m_TypeOfLicense.ToString(),
m_EngineVolumeInCC.ToString());
        }

        private string getLisenceTypes()
        {
            return string.Join("/ ", Enum.GetNames(typeof(eTypeOfMotorbikeLicense)));
        }
    }
}

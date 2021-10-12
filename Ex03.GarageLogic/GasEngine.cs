using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GasEngine : Engine
    {
        private eFuelType m_FuelType;

        internal GasEngine(eFuelType i_FuelType, float i_MaxFuelQuantityInCC) : base(i_MaxFuelQuantityInCC)
        {
            this.m_FuelType = i_FuelType;
        }

        internal void Refueling(float i_FuelQuantityToAdd, eFuelType i_FuelType)
        {
            if(this.m_FuelType == i_FuelType)
            {
                FillEnergy(i_FuelQuantityToAdd);
            } 
            else
            {
                throw new ArgumentException(i_FuelType.ToString() + " is not matching the vehicle fuel type");
            }
        }

        public static List<string> GetPossibleFuelTypes()
        {
            return new List<string>(Enum.GetNames(typeof(eFuelType)));
        }

        internal override string Properties
        {
            get { return "Current quantity of fuel in liters"; }
        }

        internal override float CurrentOrRemaining 
        { 
            set { CurrentEneregyQuantity = value; } 
        }

        public override string ToString()
        {
            return string.Format(
@"Current quantity of fuel in liters: {0}
Type of fuel: {1}",
base.ToString(),
m_FuelType.ToString());
        }
    }
}

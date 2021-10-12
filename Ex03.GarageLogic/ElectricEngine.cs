using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class ElectricEngine : Engine
    {
        private float m_RemainingBatteryTimeInHours;

        internal ElectricEngine(float i_MaxEneregyQuantity) : base(i_MaxEneregyQuantity)
        {
        }

        internal void BatteryCharging(float i_NumOfHoursToAddToBattery)
        {
            FillEnergy(i_NumOfHoursToAddToBattery);
        }
        
        internal override string Properties
        {
            get { return "Current remaining battery time (in hours)"; }
        }

        internal override float CurrentOrRemaining
        {
            set
            {
                this.m_RemainingBatteryTimeInHours = value;
                CurrentEneregyQuantity = MaxEnergyQuantity - value;
            }
        }

        public override string ToString()
        {
            return "Current remaining battery time (in hours): " + base.ToString();
        }
    }
}

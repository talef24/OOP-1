using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        private readonly float r_MaxEneregyQuantity;
        protected const int k_Precent = 100;
        private float m_CurrentEneregyQuantity;
        private float m_RemainingEnergyInPrecent;

        internal Engine(float i_MaxEneregyQuantity)
        {
            this.r_MaxEneregyQuantity = i_MaxEneregyQuantity;
        }

        internal float RemainingEnergyInPrecent
        {
            get { return this.m_RemainingEnergyInPrecent; }
        }

        internal float MaxEnergyQuantity
        {
            get { return this.r_MaxEneregyQuantity; }
        }
        
        internal float CurrentEneregyQuantity
        {
            get { return this.m_CurrentEneregyQuantity; }
            set
            {
                if (value > 0 && value <= this.r_MaxEneregyQuantity)
                {
                    this.m_CurrentEneregyQuantity = value;
                    this.updateRemainingEnergyInPrecent();
                }
                else
                {
                    throw new ValueOutOfRangeException("eneregy in the engine", 0, r_MaxEneregyQuantity);
                }
            }
        }

        internal abstract float CurrentOrRemaining { set; }

        private void updateRemainingEnergyInPrecent()
        {
            this.m_RemainingEnergyInPrecent = (this.m_CurrentEneregyQuantity / this.r_MaxEneregyQuantity) * k_Precent;
        }
        
        internal void FillEnergy(float i_EneregyQuantityToAdd)
        {
            float EnergyQuantityAfterInflating = this.m_CurrentEneregyQuantity + i_EneregyQuantityToAdd;
            this.CurrentEneregyQuantity = EnergyQuantityAfterInflating;
        }

        public override string ToString()
        {
            return m_CurrentEneregyQuantity.ToString();
        }

        internal abstract string Properties { get; }
    }
}

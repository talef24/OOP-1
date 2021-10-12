using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;
        private string m_Field;

        public ValueOutOfRangeException(string i_Field, float i_Minvalue, float i_Maxvalue)
            : base(string.Format("The value of the {0} should be between {1} to {2}", i_Field, i_Minvalue, i_Maxvalue))
        {
            m_MaxValue = i_Maxvalue;
            m_MinValue = i_Minvalue;
            m_Field = i_Field;
        }

        public ValueOutOfRangeException(string i_Field, float i_Minvalue)
            : base(string.Format("The value of the {0} should be above {1}", i_Field, i_Minvalue))
        {
            m_MinValue = i_Minvalue;
            m_Field = i_Field;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ParseUtils
    {
        public static T EnumParse<T>(string i_StrToParse, string i_ExceptionMsg)
        {
            try
            {
                T enumValueToReturn = (T)Enum.Parse(typeof(T), i_StrToParse, false);
                if (Enum.IsDefined(typeof(T), enumValueToReturn))
                {
                    return enumValueToReturn;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                throw new FormatException(i_StrToParse + i_ExceptionMsg);
            }
        }

        public static T Parse<T>(string i_StrToParse, string i_ExceptionMsg)
        {
            try
            {
                return (T)Convert.ChangeType(i_StrToParse, typeof(T));
            }
            catch (Exception)
            {
                throw new FormatException(i_StrToParse + i_ExceptionMsg);
            }
        }
    }
}

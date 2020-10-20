using System;
using System.Collections.Generic;
using System.Text;
using AccountManagement.Core;

namespace AccountManagement.Test
{
    public class ExceptionUtils
    {
        public static bool ShouldThrowException(Action<object> action, 
                    Type exceptionType, string message)
        {
            try
            {
                action(null);
                return false; // Exception not thrown
            }
            catch (Exception ex)
            {
                if (ex.GetType() != exceptionType)
                {
                    return false;
                }

                if (!ex.Message.Contains(message))
                {
                    return false;
                }

                return true;
            }
        }

        public static bool ShouldNotThrowException(Action<object> action)
        {
            try
            {
                action(null);
                return true; // Exception not thrown
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}

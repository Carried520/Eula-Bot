using System;
using System.Collections.Generic;
using System.Text;

namespace Discord_Bot.Utils
{
    class WeekDay
    {
        public  string Day()
        {
            String returnDay = "";
            returnDay = DateTime.Now.DayOfWeek.ToString();
            return returnDay;
            
        }
    }
}

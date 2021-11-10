using System;

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

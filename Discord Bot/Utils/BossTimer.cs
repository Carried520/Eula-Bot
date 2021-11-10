using System;
using System.Collections.Generic;

namespace Discord_Bot.Utils
{
    class BossTimer
    {
        public Dictionary<string, DateTime[]> getBoss(){

            var Monday = new Dictionary<string, DateTime[]> { 
                ["Karanda and Kutum"] = new DateTime[] { DateTime.Parse("0:15") },
                ["Karanda"] = new DateTime[] { DateTime.Parse("2:00") },
                ["Kzarka"] = new DateTime[] { DateTime.Parse("5:00"), DateTime.Parse("9:00"), DateTime.Parse("22:15") },
                ["Offin"] = new DateTime[] { DateTime.Parse("12:00") },
                ["Kutum"] = new DateTime[] { DateTime.Parse("16:00") },
                ["Nouver"] = new DateTime[] { DateTime.Parse("19:00") }



            };
            var Tuesday = new Dictionary<string, DateTime[]>
            {
                ["Karanda"] = new DateTime[] { DateTime.Parse("0:15"), DateTime.Parse("19:00") },
                ["Kutum"] = new DateTime[] { DateTime.Parse("2:00") , DateTime.Parse("12:00") },
                ["Kzarka"] = new DateTime[] { DateTime.Parse("5:00") },
                ["Nouver"] = new DateTime[] { DateTime.Parse("9:00") , DateTime.Parse("16:00") },
                ["Garmoth"] = new DateTime[] { DateTime.Parse("22:15") },
                



            };
            var Wednesday = new Dictionary<string, DateTime[]>
            {
                ["Kutum and Kzarka"] = new DateTime[] { DateTime.Parse("0:15")},
                ["Karanda"] = new DateTime[] { DateTime.Parse("2:00"), DateTime.Parse("9:00") },
                ["Kzarka"] = new DateTime[] { DateTime.Parse("5:00") },
                ["Kutum and Offin"] = new DateTime[] {  DateTime.Parse("16:00") },
                ["Vell"] = new DateTime[] { DateTime.Parse("19:00") },
                ["Karanda and Kzarka"] = new DateTime[] { DateTime.Parse("22:15") },
                ["Quint and Muraka"] = new DateTime[] { DateTime.Parse("23:15") },
            };


            var Thursday = new Dictionary<string, DateTime[]>
            {

                ["Nouver"] = new DateTime[] { DateTime.Parse("0:15") , DateTime.Parse("5:00") , DateTime.Parse("12:00") },
                ["Kutum"] = new DateTime[] { DateTime.Parse("2:00"), DateTime.Parse("9:00") , DateTime.Parse("19:00") },
                ["Kzarka"] = new DateTime[] { DateTime.Parse("15:00")},
                ["Garmoth"] = new DateTime[] { DateTime.Parse("22:15") },
            };




            var Friday = new Dictionary<string, DateTime[]>
            {

                ["Karanda and Kzarka"] = new DateTime[] { DateTime.Parse("0:15") },
                ["Nouver"] = new DateTime[] { DateTime.Parse("2:00"), DateTime.Parse("16:00") },
                ["Karanda"] = new DateTime[] { DateTime.Parse("5:00"), DateTime.Parse("12:00") },
                ["Kutum"] = new DateTime[] { DateTime.Parse("9:00") },
                ["Kzarka"] = new DateTime[] { DateTime.Parse("19:00") },
                ["Kutum and Kzarka"] = new DateTime[] { DateTime.Parse("22:15") },


            };

            var Saturday = new Dictionary<string, DateTime[]>
            {

                ["Karanda"] = new DateTime[] { DateTime.Parse("0:15") },
                ["Offin"] = new DateTime[] { DateTime.Parse("2:00")},
                ["Nouver"] = new DateTime[] { DateTime.Parse("5:00"), DateTime.Parse("12:00") },
                ["Kutum"] = new DateTime[] { DateTime.Parse("9:00") },
                ["Quint and Muraka"] = new DateTime[] { DateTime.Parse("16:00") },
                ["Karanda and Kzarka"] = new DateTime[] { DateTime.Parse("19:00") },


            };

            var Sunday = new Dictionary<string, DateTime[]>
            {

                ["Kutum and Nouver"] = new DateTime[] { DateTime.Parse("0:15") },
                ["Kzarka"] = new DateTime[] { DateTime.Parse("1:00"), DateTime.Parse("12:00") },
                ["Kutum"] = new DateTime[] { DateTime.Parse("5:00") },
                ["Nouver"] = new DateTime[] { DateTime.Parse("9:00") },
                ["Vell"] = new DateTime[] { DateTime.Parse("16:00") },
                ["Garmoth"] = new DateTime[] { DateTime.Parse("19:00") },
                ["Kzarka and Nouver"] = new DateTime[] { DateTime.Parse("22:15") }


            };
            var boss = new Dictionary<string, DateTime[]>();

            string weekday = new WeekDay().Day();
            switch(weekday)
            {
                case "Monday":
                    if(DateTime.Now>DateTime.Parse("23:15"))
                    {
                        boss = Tuesday;
                    }
                    else
                    {
                        boss = Monday;
                    }
                   
                    break;
                case "Tuesday":
                    if (DateTime.Now > DateTime.Parse("23:15"))
                    {
                        boss = Wednesday;
                    }
                    else
                    {
                        boss = Tuesday;
                    }
                    break;
                case "Wednesday":
                    if (DateTime.Now > DateTime.Parse("23:15"))
                    {
                        boss = Thursday;
                    }
                    else
                    {
                        boss = Wednesday;
                    }
                    break;
                case "Thursday":
                    if (DateTime.Now > DateTime.Parse("23:15"))
                    {
                        boss = Friday;
                    }
                    else
                    {
                        boss = Thursday;
                    }
                    break;
                case "Friday":
                    if (DateTime.Now > DateTime.Parse("23:15"))
                    {
                        boss = Saturday;
                    }
                    else
                    {
                        boss = Friday;
                    }
                    break;
                case "Saturday":
                    if (DateTime.Now > DateTime.Parse("23:15"))
                    {
                        boss = Sunday;
                    }
                    else
                    {
                        boss = Saturday;
                    }
                    break;
                case "Sunday":
                    if (DateTime.Now > DateTime.Parse("23:15"))
                    {
                        boss = Monday;
                    }
                    else
                    {
                        boss = Sunday;
                    }
                    break;
                    
                        
            }
            return boss;
        }

    }
}

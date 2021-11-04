using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Discord_Bot.Utils
{
    class BossSchedule
    {
        public List <string> Bosses()
        {
            string warn = "";
            string rev = "";
            var finalArray = new List<string>();
            var boss = new List<List<string>>();

            Dictionary<string, DateTime[]> BossTimer = new BossTimer().getBoss();
            var now = DateTime.Now;
            foreach(var key in BossTimer.Keys)
            {
                List<string> arr = new List<string>();
                foreach(var value in BossTimer[key])
                {

                    if(now>DateTime.Parse("23:15"))
                    {
                        var NextDay = DateTime.Today.AddDays(1);
                        var Tommorow = new DateTime(NextDay.Year, NextDay.Month, NextDay.Day, value.Hour, value.Minute, value.Second);
                        var diff = (Tommorow - now);
                        if (diff.TotalMinutes > 0)
                        {
                            var minutes = Convert.ToInt32(diff.Minutes);
                            var hours = Convert.ToInt32(diff.Hours);
                            
                            arr.Add(key);
                            arr.Add(hours.ToString());
                            arr.Add(minutes.ToString());
                            boss.Add(arr);
                        }
                        
                    }
                    else{

                        var diff = value - now;
                        if (diff.TotalMinutes > 0)
                        {
                            var minutes = Convert.ToInt32(diff.Minutes);
                            var hours = Convert.ToInt32(diff.Hours);
                            
                            arr.Add(key);
                            arr.Add(hours.ToString());
                            arr.Add(minutes.ToString());
                            boss.Add(arr);
                        }

                    }
                    
                    
                   
                }
               
                



            }

            boss.Sort((o1, o2) => int.Parse(o1[1]).CompareTo(int.Parse(o2[1])));
            
            if (!boss.Any())
            {
                rev = "No boss today anymore";
                finalArray.Add(rev);
            }
            else
            {
                rev = $"{boss[0][0]} {boss[0][1]}:{boss[0][2]}";
                finalArray.Add(rev);
                if (Convert.ToInt32(boss[0][1]) == 0 && Convert.ToInt32(boss[0][2]) < 11)
                {
                    warn = $"{boss[0][2]}";
                }
                finalArray.Add(warn);
            }
            
            return finalArray;
        }
    }
}

using System;
using System.Collections.Generic;

namespace Discord_Bot.Commands.Activities.ActivityMaker
{
  public  class ClassStats
    {

        public  Dictionary<string,string> GetStats(string ClassName)
        {
            var ClassDict = new Dictionary<string, string>
            {
                { "Classname", ClassName },
                { "Ap", Convert.ToString(50) },
                { "Dp", Convert.ToString(50) }
            };
            return ClassDict;
            
        }
    }
}

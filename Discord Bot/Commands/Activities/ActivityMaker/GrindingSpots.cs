using System;
using System.Collections.Generic;
using System.Text;

namespace Discord_Bot.Commands.Activities.ActivityMaker
{
    class GrindingSpots
    {
        public int[] ReturnSpotStats(string SpotName)
        {
            int[] Stats = new int[4];
            switch (SpotName)
            {


                


                case "aakman":
                    Stats[0] = (int)Aakman.Ap;
                    Stats[1] = (int)Aakman.Dp;
                    Stats[2] = (int)Aakman.AvgTrash;
                    Stats[3] = (int)Aakman.TrashValue;
                    break;
                case "se":
                    Stats[0] = (int)StarsEnd.Ap;
                    Stats[1] = (int)StarsEnd.Dp;
                    Stats[2] = (int)StarsEnd.AvgTrash;
                    Stats[3] = (int)StarsEnd.TrashValue;
                    break;
                case "sycraia":
                    Stats[0] = (int)Sycraia.Ap;
                    Stats[1] = (int)Sycraia.Dp;
                    Stats[2] = (int)Sycraia.AvgTrash;
                    Stats[3] = (int)Sycraia.TrashValue;
                    break;
                case "orcs":
                    Stats[0] = (int)Orcs.Ap;
                    Stats[1] = (int)Orcs.Dp;
                    Stats[2] = (int)Orcs.AvgTrash;
                    Stats[3] = (int)Orcs.TrashValue;
                    break;
                case "grassbeetles":
                    Stats[0] = (int)GrassBeetle.Ap;
                    Stats[1] = (int)GrassBeetle.Dp;
                    Stats[2] = (int)GrassBeetle.AvgTrash;
                    Stats[3] = (int)GrassBeetle.TrashValue;
                    break;

                case "imps":

                    Stats[0] = (int)Imps.Ap;
                    Stats[1] = (int)Imps.Dp;
                    Stats[2] = (int)Imps.AvgTrash;
                    Stats[3] = (int)Imps.TrashValue;
                    break;

            }
            return Stats;
        }


        
        enum GrassBeetle
        {
            Ap = 50 ,
            Dp = 50 ,
            AvgTrash = 1000,
            TrashValue = 5000
        }
        enum Imps
        {
            Ap = 100,
            Dp = 100,
            AvgTrash = 3000,
            TrashValue = 5500
        }

        

        enum Aakman
        {
            Ap = 150,
            Dp = 200,
            AvgTrash = 10000,
            TrashValue = 7500

        }
        enum StarsEnd
        {
            Ap = 200,
            Dp = 250,
            AvgTrash = 10000,
            TrashValue = 18500
        }

        enum Sycraia
        {
            Ap = 250 ,
            Dp = 300 ,
            AvgTrash = 15000,
            TrashValue = 15000
            
        }
        enum Orcs
        {
            Ap = 300 ,
            Dp = 350 , 
            AvgTrash = 20000,
            TrashValue = 18000
        }


        
    }
}

using DSharpPlus.CommandsNext;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Activities.ActivityMaker
{
    class ShopItems
    {


        public class ShopData
        {

            public ulong Id { get; set; }
            public List<string> items { get; set; }

        }

        public class CharData
        {

            public ulong Id { get; set; }
            public string Class { get; set; }
            public string Name { get; set; }
            public int Ap { get; set; }
            public int Dp { get; set; }
            public long Silver { get; set; }
            

        }

        enum Rosar : long
        {
            Cost = 100000000,
            Ap = 50,
            Dp = 0
        }


        enum Vangertz : long
        {
            Cost = 150000000,
            Ap = 0,
            Dp = 50
        }


        enum Lemoria: long
        {
            Cost = 200000000,
            Ap = 25,
            Dp = 50
        }

        enum Blackstar : long{
            Cost = 13000000000,
            Ap = 50,
            Dp = 0
        }
        enum DimTree : long
        {
            Cost = 2000000000,
            Ap = 0,
            Dp = 100
        }
        enum FallenGod : long
        {
            Cost = 50000000000,
            Ap = 0,
            Dp = 200
        }
        enum Kutum : long
        {
            Cost = 20000000000,
            Ap = 25,
            Dp = 50
        }
        




        public Task <long[]> GetItemStatsAsync(string ItemName)
        {
            long[] Stats = new long[3];
            switch (ItemName)
            {
                case "blackstar":
                    Stats[0]= (long)Blackstar.Cost;
                    Stats[1] = (long)Blackstar.Ap;
                    Stats[2] = (long)Blackstar.Dp;
                    break;

                case "dimtree":
                    Stats[0] = (long)DimTree.Cost;
                    Stats[1] = (long)DimTree.Ap;
                    Stats[2] = (long)DimTree.Dp;
                    break;
                case "fallengod":
                    Stats[0] = (long)FallenGod.Cost;
                    Stats[1] = (long)FallenGod.Ap;
                    Stats[2] = (long)FallenGod.Dp;
                    break;
                case "kutum":
                    Stats[0] = (long)Kutum.Cost;
                    Stats[1] = (long)Kutum.Ap;
                    Stats[2] = (long)Kutum.Dp;
                    break;
                case "rosar":
                    Stats[0] = (long)Rosar.Cost;
                    Stats[1] = (long)Rosar.Ap;
                    Stats[2] = (long)Rosar.Dp;
                    break;
                case "vangertz":
                    Stats[0] = (long)Vangertz.Cost;
                    Stats[1] = (long)Vangertz.Ap;
                    Stats[2] = (long)Vangertz.Dp;
                    break;
                case "lemoria":
                    Stats[0] = (long)Lemoria.Cost;
                    Stats[1] = (long)Lemoria.Ap;
                    Stats[2] = (long)Lemoria.Dp;
                    break;
                    
            }
            return Task.FromResult(Stats);

        }




        public async Task<string> InsertShopItemAsync(CommandContext ctx,ulong id,string item)
        {
            string outcome = "";
            long cost = 0;
            int ap = 0;
            int dp = 0;
            switch (item)
            {
                case "blackstar":
                    cost = (long)Blackstar.Cost;
                    ap = (int)Blackstar.Ap;
                    dp = (int)Blackstar.Dp;
                    break;

                case "dimtree":
                    cost = (long)DimTree.Cost;
                    ap = (int)DimTree.Ap;
                    dp = (int)DimTree.Dp;
                    break;
                case "fallengod":
                    cost = (long)FallenGod.Cost;
                    ap = (int)FallenGod.Ap;
                    dp = (int)FallenGod.Dp;
                    break;
                case "kutum":
                    cost = (long)Kutum.Cost;
                    ap = (int)Kutum.Ap;
                    dp = (int)Kutum.Dp;
                    break;

                case "rosar":
                    cost = (long)Rosar.Cost;
                    ap = (int)Rosar.Ap;
                    dp = (int)Rosar.Dp;
                    break;
                case "vangertz":
                    cost = (long)Vangertz.Cost;
                    ap = (int)Vangertz.Ap;
                    dp = (int)Vangertz.Dp;
                    break;
                case "lemoria":
                    cost = (long)Lemoria.Cost;
                    ap = (int)Lemoria.Ap;
                    dp = (int)Lemoria.Dp;
                    break;

            }


            var client = new MongoClient(Config.Get("uri"));
            var database = client.GetDatabase("Activity");
            var collection = database.GetCollection<CharData>("Class");
            var filter = Builders<CharData>.Filter.Eq("_id", id);
            var match = collection.Find(filter).FirstOrDefaultAsync().Result;


          
            var Charcollection = database.GetCollection<ShopData>("Shop");
            var Charfilter = Builders<ShopData>.Filter.Eq("_id", id);
            var Charmatch = Charcollection.Find(Charfilter).FirstOrDefaultAsync().Result;
            var ListOfItems = new List<string>();
            

            if (match != null)
            {
                if (match.Silver < cost)
                {
                    outcome = ("You dont have money for the item");
                    

                }
                else
                {
                    var SilverLeft = match.Silver - cost;
                    var NewAp = match.Ap + ap;
                    var NewDp = match.Dp + dp;



                    if (Charmatch == null)
                    {
                        ListOfItems.Add(item);
                        

                        await Charcollection.InsertOneAsync(new ShopData { Id = id, items = ListOfItems });

                        var updated = Builders<CharData>.Update.Set("Silver", SilverLeft).Set("Ap", NewAp).Set("Dp", NewDp);
                        await collection.UpdateOneAsync(filter, updated);
                        outcome = $"{item} bought";
                    }
                    else
                    {
                        ListOfItems = Charmatch.items;
                        if (ListOfItems.Contains(item))
                        {
                            await ctx.RespondAsync("Item already bought");
                            
                        }
                        else
                        {

                            ListOfItems.Add(item);
                            var updated2 = Builders<ShopData>.Update.Set("items", ListOfItems);
                            var updated = Builders<CharData>.Update.Set("Silver", SilverLeft).Set("Ap", NewAp).Set("Dp", NewDp);
                            await collection.UpdateOneAsync(filter,updated);
                            await Charcollection.UpdateOneAsync(Charfilter, updated2);
                            await ctx.RespondAsync($"{item} bought");
                        }
                    }

                   
                }


            }
            else
            {
                outcome = "Character not registered";
            }

            return outcome;


        }
        }
}

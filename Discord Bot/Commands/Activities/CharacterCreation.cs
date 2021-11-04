using Discord_Bot.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discord_Bot.Commands.Activities
{

    class CharacterCreation : BaseCommandModule
    {


        public class CharData
        {

            public ulong Id { get; set; }
            public string Class { get; set; }
            public string Name { get; set; }
            public int Ap { get; set; }
            public int Dp { get; set; }
            public int Silver { get; set; }
            

        }
       


        [Command("CreateChar")]
        [Description("Create char")]
        [Category("activity")]
        public async Task CreateChar(CommandContext ctx , string classname,string charname)
        {
            
            var id = ctx.Member.Id;
            
            string[] ClassArray = { "warrior","ranger","sorceress","berserker","tamer","musa","maehwa","valkyrie","kunoichi","ninja","wizard","witch",
            "mystic","striker","lahn","archer","dk","shai","guardian","hashashin","nova","sage","corsair"};
     
            if (ClassArray.Contains(classname, StringComparer.OrdinalIgnoreCase))
            {
               string  DbInput = char.ToUpper(classname[0])+classname.Substring(1);



                var client = new MongoClient(Config.Get("uri"));
                var database = client.GetDatabase("Activity");
                var collection = database.GetCollection<CharData>("Class");
                var filter = Builders<CharData>.Filter.Eq("_id", id);
                var Items= new List<string>();
                var match =  await collection.Find(filter).FirstOrDefaultAsync() ;
                if (match == null)
                {
                    await collection.InsertOneAsync(new CharData { Id = id, Class = DbInput,Name = charname,Ap = 0,Dp = 0,Silver = 0});
                    await ctx.RespondAsync($"{DbInput} Character created. Welcome {charname} ");
                }
                else
                {
                    
                    var updated = Builders<CharData>.Update.Set("Class", DbInput);
                    await collection.UpdateOneAsync(filter, updated);
                    await ctx.RespondAsync($"Class updated to {DbInput}");
                }

            }
            else
            {
                await ctx.RespondAsync("Not a valid class");
                return;
            }
           

            


        }
    }
}

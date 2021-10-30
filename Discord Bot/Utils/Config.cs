using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Discord_Bot
{
    class Config
    {

        public static string Get (string token)
        {

            String json = File.ReadAllText("appsettings.json");
            JsonDocument Deserialize = JsonDocument.Parse(json);
            JsonElement root = Deserialize.RootElement;
            string results = root.GetProperty(token).GetString();


            return results;
        }
    }
}

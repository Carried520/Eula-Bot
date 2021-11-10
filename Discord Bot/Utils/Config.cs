﻿using System;
using System.IO;
using System.Text.Json;

namespace Discord_Bot
{
    class Config
    {

        public static string Get (string token)
        {
            token = token.ToLower();
            String json = File.ReadAllText("appsettings.json");
            JsonDocument Deserialize = JsonDocument.Parse(json);
            JsonElement root = Deserialize.RootElement;
            string results = root.GetProperty(token).GetString();


            return results;
        }
    }
}

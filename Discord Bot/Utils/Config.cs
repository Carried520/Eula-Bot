using System;
using System.Collections.Generic;
using System.Text;

namespace Discord_Bot
{
    class Config
    {
        public static string get (string token)
        {

            DotNetEnv.Env.TraversePath().Load();
            String dotenv = Environment.GetEnvironmentVariable(token);
            return dotenv;
        }
    }
}

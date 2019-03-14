using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using SODA;

namespace ConsoleApp1
{
    class Group7
    {
        static void Main(string[] args)
        {

            /*
             * Assisted by code from https://dev.socrata.com/foundry/data.cityofnewyork.us/fhrw-4uyv
             * using the SODA library install command with NuGent in Visual Studio- Install-Package CSM.SodaDotNet
             * The goal is to use the Npgsql and the Soda.NET library to download the data automatically and load it into our PostgresDB
             * queried DB - https://data.cityofnewyork.us
             * our API KEY - PVGjhHLj8Svy7Ryz0uJgW9IBh
             */
            var client = new SodaClient("https://data.cityofnewyork.us", "PVGjhHLj8Svy7Ryz0uJgW9IBh");

            /*
             * IMPORTANT: Since the website has been updated, the .NET library has been updated
             * and SodaClient.GetResource no longer allows for generic typing, it requires either
             * <Dictionary<string,object>> (the dictionary collection found in System.Collections.Generic
             * or a user defined class <MyClass>
             */
            var dataset = client.GetResource<Dictionary<string,object>>("fhrw-4uyv");

 
            var soql = new SoqlQuery().Select("*").Where("created_date > 2019-03-12T00:00:00.000");

            var results = dataset.Query<Dictionary<string, object>>(soql);
          
        }

        /*
         * For eventual code cleanup - will move the query code from main to here
         */ 
         /*
        public void loadDB()
        {
            var client = new SodaClient("https://data.cityofnewyork.us", "PVGjhHLj8Svy7Ryz0uJgW9IBh");

        }
        */
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using SODA;

namespace ConsoleApp1
{
    class Group7
    {
        static void Main(string[] args)
        {

            /*
             * Assisted by code from https://dev.socrata.com/foundry/data.cityofnewyork.us/fhrw-4uyv
             * using the SODA library install command with NuGet in Visual Studio- Install-Package CSM.SodaDotNet
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
            //var dataset = client.GetResource<Json311>("fhrw-4uyv");

            /*
             * IMPORTANT: this library has a dependancy, install in NuGet Install-Package System.Security.Permissions
             * This line will otherwise throw an error
             * usage: Where("created_date > \"timestamp\" "); 
             * Timestamp (or other literal) must be in quotes or else it gets treated as a variable which causes the
             * SoqlQuery to throw a bad request exception
             */
            var soql = new SoqlQuery().Select("*").Limit(50);//.Where("created_date > \"2019-03-12\" ");

            /*
             * Results saves the data as System.Collections.Generic.List according to .GetType()
             * and saved as IEnumerable according to visual studio
             */ 
            var results = dataset.Query<Dictionary<string, object>>(soql);
            //var results = dataset.Query<Json311>(soql);

            Console.WriteLine(results.GetType());

            /*
             * Currently throws error: sequence contains no elements
             */
            int SizeOfList;
            void Start(){
                SizeOfList = results.Count();
            }
            Start();
            Console.WriteLine(SizeOfList);
            Console.ReadKey();

           /* foreach (Dictionary<string, object> kvp in results)
            {
                Console.WriteLine("key is {0}, Value is {1}", kvp.Keys, kvp.Values);
            }
            Console.ReadKey();
            */

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

    class Json311
    {
        public string closed_date { get; set;}
        public string agency { get; set; }
        public string complaint_type { get; set; }
        public string descriptor { get; set; }
        public string location_type { get; set; }
        public string incident_zip { get; set; }
        public string incident_address { get; set; }
        public string street_name { get; set; }
        public string cross_street_1 { get; set; }
        public string cross_street_2 { get; set; }
        public string intersection_street_1 { get; set; }
        public string intersection_street_2 { get; set; }
        public string address_type { get; set; }
        public string city { get; set; }
        public string landmark { get; set; }
        public string facility_type { get; set; }
        public string status { get; set; }
        public string due_date { get; set; }
        public string resolution_description { get; set; }
        public string resoltuion_action_updated_date { get; set; }
        public string community_board { get; set; }
        public string bbl { get; set; }
        public string borough { get; set; }
        public string x_coordinate_state_plane { get; set; }
        public string y_coordinate_state_plane { get; set; }
        public string open_data_channel_type { get; set; }
        public string park_facility_name { get; set; }
        public string park_borough { get; set; }
        public string vehicle_type { get; set; }
        public string taxi_company_borough { get; set; }
        public string taxi_pick_up_location { get; set; }
        public string bridge_highway_name { get; set; }
        public string bridge_highway_direction { get; set; }
        public string road_ramp { get; set; }
        public string bridge_highway_segment { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string location_city { get; set; }
        public string location { get; set; }
        public string location_zip { get; set; }
        public string location_state { get; set; }
    }
}

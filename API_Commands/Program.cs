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
            ManageDB test = new ManageDB();
            var results = test.LoadDB();
        }

    }

    /*
     * Class for managing the Database
     * created for code clean-up and easier readability
     */ 
    class ManageDB
    {
        public ManageDB() {}


        public IEnumerable<Dictionary<string,object>> LoadDB()
        {

            /*
             * Assisted by code from https://dev.socrata.com/foundry/data.cityofnewyork.us/fhrw-4uyv
             * using the SODA library install command with NuGet in Visual Studio- Install-Package CSM.SodaDotNet
             * The goal is to use the Npgsql and the Soda.NET library to download the data automatically and load it into our PostgresDB
             * queried DB - https://data.cityofnewyork.us
             * our API KEY - PVGjhHLj8Svy7Ryz0uJgW9IBh
             */
            SODA.SodaClient client = new SodaClient("https://data.cityofnewyork.us", "PVGjhHLj8Svy7Ryz0uJgW9IBh");

            /*
             * IMPORTANT: Since the website has been updated, the .NET library has been updated
             * and SodaClient.GetResource no longer allows for generic typing, it requires either
             * <Dictionary<string,object>> (the dictionary collection found in System.Collections.Generic
             * or a user defined class <MyClass>
             */
            SODA.Resource<Dictionary<string,object>> dataset = client.GetResource<Dictionary<string, object>>("fhrw-4uyv");
            //SODA.Resource<Json311> dataset = client.GetResource<Json311>("fhrw-4uyv"); //testing our serializable json class


            ManageDB test = new ManageDB();
            SODA.SoqlQuery soql = test.GetQueryDate();

            /*
             * Results saves the data as System.Collections.Generic.List according to .GetType()
             * and saved as IEnumerable with they dype Dictionary<string,object> according to visual studio
             * 
             * this field remains a var type as we are working on changing it to our user defined class
             */
            var results = dataset.Query<Dictionary<string, object>>(soql);

            /*
             * there are elements in the list, now just need to access them
             * test to check if there really are elements in the list
             */
            int SizeOfList;
            test.TestIEnum(ref results, out SizeOfList);
            Console.WriteLine(SizeOfList);

            Console.WriteLine();
            Console.ReadKey();

            return results;
        }


        /*
         * For Better readablity and enables the program to more easily make changes to the query
         * As we can later write another function instead of just continually changing this on         
         * this also allows a neater place to stick the DateTime variable that will automaticall set the date
         */ 
        public SODA.SoqlQuery GetQueryDate()
        {
            /*
             * Added so that we do not need to keep manually changing the date stamp in the query 
             * today is the time in nanoseconds, date translates it to a usable string in the correct format
             * for the query
             */
            DateTime today = DateTime.Today;
            String date = today.ToString("yyyy-mm-dd");

            /*
             * Working on resolving issue where DateTime returns the month as 00
             */ 
            Console.WriteLine(date);

            /*
             * IMPORTANT: this library has a dependancy, install in NuGet Install-Package System.Security.Permissions
             * This line will otherwise throw an error
             * usage: Where("created_date > \"timestamp\" "); 
             * Timestamp (or other literal) must be in quotes or else it gets treated as a variable which causes the
             * SoqlQuery to throw a bad request exception
             */
            SODA.SoqlQuery soql = new SoqlQuery().Select("*").Limit(10);

            /*
             * Having an issue with the query we want, using the Limit query (above) to test functionality of the code
             */ 
            //SODA.SoqlQuery soql = new SoqlQuery().Select("*").Where("created_date > \"" + date + "T00:00:00.000\" ");


            return soql;
        }

        /*
         * Internal test to make sure the query returned data 
         */ 
        public void TestIEnum(ref IEnumerable<Dictionary<string, object>> testDB, out int SizeOfList)
        {
            SizeOfList = testDB.Count();
        }
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

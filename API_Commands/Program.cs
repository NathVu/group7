using System;
using System.Collections.Generic;
using System.Linq;
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

    
     /// <summary> Class for managing the Database
     /// created for code clean-up and easier readability 
     /// </summary>
    class ManageDB
    {
        public ManageDB() {}


        /// <summary>
        /// Assisted by code from https://dev.socrata.com/foundry/data.cityofnewyork.us/fhrw-4uyv
        /// queried DB - https://data.cityofnewyork.us
        /// API KEY - PVGjhHLj8Svy7Ryz0uJgW9IBh
        /// loadDB connects to the database, sends the query and then returns the data
        /// </summary>
        /// <returns>Returns the dataset of the query to main</returns>
        public IEnumerable<Dictionary<string,object>> LoadDB()
        {

            
            ///<remarks>
            ///This requires the SODA library 
            ///It can be installed from NuGet in Visual studio using 
            ///Install-Package CSM.SodaDotNet
            ///</remarks>
             
            SODA.SodaClient client = new SodaClient("https://data.cityofnewyork.us", "PVGjhHLj8Svy7Ryz0uJgW9IBh");


             ///<remarks>
             /// The documentation on the web is outdated.
             /// The .NET library has been updated to no longer allow generic typing. 
             /// You must use either Dictionary(String,Object) - use <> but not allowed in XML comments
             /// OR a user-defined json serializable class
             ///</remarks>
            SODA.Resource<Dictionary<string,object>> dataset = client.GetResource<Dictionary<string, object>>("fhrw-4uyv");

            ///<remarks>
            ///This is a test for our user defined serializable class
            ///It may be implemented in later versions 
            ///</remarks>
            //SODA.Resource<Json311> dataset = client.GetResource<Json311>("fhrw-4uyv"); //testing our serializable json class

            ///<summary>
            /// instantiate our object and get our query from GetQueryDate()
            /// </summary>
            ManageDB test = new ManageDB();
            SODA.SoqlQuery soql = test.GetQueryDate();


            ///<summary>
            /// Query sends our query to our pre-defined location and returns an IEnumerable which we assign to results
            /// results now contains the results of our query
            /// 
            /// This field for now remains dynamically typed in case we decide to change to our user defined class
            /// </summary>
            var results = dataset.Query<Dictionary<string, object>>(soql);



            ///<summary>
            /// Testing to make sure that our query returned results
            ///</summary>
            int SizeOfList;
            test.TestIEnum(ref results, out SizeOfList);
            Console.WriteLine(SizeOfList);

            Console.WriteLine();
            Console.ReadKey();

            return results;
        }




        /// <summary>
        /// This function was written to increase readability of the code
        /// It gets the current data (and yesterdays date) so that the code does not need to be updated every day
        /// and composes a query object (SODA.SoqlQuery) to reurn to LoadDB to query the dataset
        /// Utilizes the DateTime type to get todays date
        /// </summary>
        /// <returns>our Query in their defined type - SODA.SoqlQuery </returns>
        public SODA.SoqlQuery GetQueryDate()
        {
            
            DateTime today = DateTime.Today;
            String year = today.Year.ToString();


            ///<remarks>
            /// Their query requires typing with a 0 in front of the month so we 
            /// check the formatting and add a 0 if necessary
            /// </remarks>
            String month;
            if (today.Month / 10 != 0)
            {
                month = today.Month.ToString();
            }
            else
            {
                month = "0" + today.Month.ToString();
            }
            String day = today.Day.ToString();
            String yday = (today.Day - 1).ToString();
        

            ///<remarks>
            ///The date field in the Query needs to be of type
            ///yyyy-mm-dd + T + hh:mm:ss.nnn (hours, minutes, seconds and nanoseconds.
            ///We format the data here to be in the correct type as the ToString method provided by 
            ///the DateTime class does not allow for formatting in this manner
            /// </remarks>
            String date= "\"" + year + "-" + month + "-" + day + "T00:00:00.000\"";
            String ydate = "\"" + year + "-" + month + "-" + yday + "T00:00:00.000\"";
 
            ///<remarks>
            /// just a quick test to make sure the date is in the format we want
            /// </remarks>
            Console.WriteLine(date);

            ///<remarks>
            /// A test case just to test that the connection is working
            /// will be removed in later revisions
            /// </remarks>
            //SODA.SoqlQuery soql = new SoqlQuery().Select("*").Limit(10);

           ///<remarks>
           /// for increased readability and easier formatting
           /// </remarks>
            string cdate = "created_date";

            ///<remarks>
            /// Usage of creating a new SoqlQuery - 
            /// Note: TimeStamp (or other literal) must be in quotes or else it gets treated like a variable which will throw an error
            /// Field + "function" + "comparison type"
            /// Select can be used to select specific fields but we want all data associated with the calls
            /// </remarks>
            SODA.SoqlQuery soql = new SoqlQuery().Select("*").Where(cdate + ">" + ydate);


            return soql;
        }
        

        /// <summary>
        /// Internal Test to make sure the query is returning data and not an empty set
        /// </summary>
        /// <param name="testDB"> The database we want to test</param>
        /// <param name="SizeOfList"> An out paramted - returns the number of data items in the database</param>
        public void TestIEnum(ref IEnumerable<Dictionary<string, object>> testDB, out int SizeOfList)
        {
            SizeOfList = testDB.Count();
        }
    }

    
     /// <summary>
     /// Template for our seriablizable class to potentially replace Dictionary(string, object) (same <> notation, see reason above)
     /// Will be removed if Dictionary proves to be more useful and useable
     /// </summary>
    class Json311
    {
        public string unique_key { get; set; }
        public string created_date { get; set; }
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

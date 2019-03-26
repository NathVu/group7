﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using SODA;
using Json311;

namespace ConsoleApp1
{
    class Group7
    {
        static void Main(string[] args)
        {
            DataFormat test = new DataFormat();
            Dictionary<string, object>[] rarr = test.getData();
            test.parseData(rarr);
            Console.ReadKey();

            /*
             * Temp Code: holding here until incorporated into our program 
             * var val = rarr[0];
             * Console.WriteLine(val.Values.Count());
             * foreach (KeyValuePair<string, object> iterate in val)
             * {
             *   Console.WriteLine(iterate.Key + " " + iterate.Value.GetType());
             * }
             * Console.ReadKey();
             * 
             */

        }

    }




    /// <summary>
    ///  A class to take the data recieved from the API Query
    ///  And format it usiing our user defined class to 
    /// </summary>
    class DataFormat
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataset"> recieves out dataset, formatted in getData into an array of Dictionary objects</param>
        /// <returns>The data parsed into our user created class</returns>
        public List<Json311.Json311> parseData(Dictionary<string, object>[] dataset)
        {
            List<Json311.Json311> dataList = new List<Json311.Json311>();
            

            for (int i = 0; i < dataset.Length; i++)
            {
                Json311.Json311 dItem = new Json311.Json311(dataset[i]);
                //if(dItem.Created_date > )
                dataList.Add(dItem);
                Console.WriteLine("working: " + i);
            }

            return dataList;
        }

        /// <summary>
        /// Manages our Database function call and gets back the Data as they return us
        /// And converts it to an array which we return
        /// </summary>
        /// <returns>A Dictionary Array which we can read through</returns>
        public Dictionary<string, object>[] getData()
        {
            ManageDB test = new ManageDB();
            IEnumerable<Dictionary<string, object>> results = test.LoadDB();

            /// <remarks>
            /// Allows us to read the data
            /// We convert the dara into an array so it is not in an interface and we are able 
            /// to read the data that our query returned 
            /// </remarks>
            Dictionary<string, object>[] results_arr = results.ToArray();
            Dictionary<string, object> val = results_arr[0];
            Console.WriteLine(val.Values.Count());
            foreach (KeyValuePair<string, object> iterate in val)
            {
                Console.WriteLine(iterate.Key.ToString() + " " + iterate.Value.GetType() + " " + iterate.Value);
            }
            Console.ReadKey();
            return results_arr;
        }
    }





    /// <summary> Class for managing the Database
    /// created for code clean-up and easier readability 
    /// </summary>
    class ManageDB
    {
        public ManageDB() { }

        /// <summary>
        /// Assisted by code from https://dev.socrata.com/foundry/data.cityofnewyork.us/fhrw-4uyv
        /// queried DB - https://data.cityofnewyork.us
        /// API KEY - PVGjhHLj8Svy7Ryz0uJgW9IBh
        /// loadDB connects to the database, sends the query and then returns the data
        /// </summary>
        /// <returns>Returns the dataset of the query to main</returns>
        public IEnumerable<Dictionary<string, object>> LoadDB()
        {

            /// <remarks>
            /// This requires the SODA library 
            /// It can be installed from NuGet in Visual studio using 
            /// Install-Package CSM.SodaDotNet
            /// </remarks>>   
            SODA.SodaClient client = new SodaClient("https://data.cityofnewyork.us", "PVGjhHLj8Svy7Ryz0uJgW9IBh");

            /// <remarks>
            /// The documentation on the web is outdated.
            /// The .NET library has been updated to no longer allow generic typing. 
            /// You must use either Dictionary(String,Object) - use <> but not allowed in XML comments
            /// OR a user-defined json serializable class - their documentation does not explain how to do this
            /// well enough, however so we are sticking with the Generic Collection specified 
            /// </remarks>>
            SODA.Resource<Dictionary<string, object>> dataset = client.GetResource<Dictionary<string, object>>("fhrw-4uyv");

            /// <summary>
            /// instantiate our object and get our query from GetQueryDate()
            /// </summary>
            ManageDB test = new ManageDB();
            SODA.SoqlQuery soql = test.GetQueryDate();

            /// <summary>
            /// Query sends our query to our pre-defined location and returns an IEnumerable which we assign to results
            /// results now contains the results of our query
            /// </summary>
            IEnumerable<Dictionary<string, object>> results = dataset.Query<Dictionary<string, object>>(soql);

            /// <summary>
            /// Testing to make sure that our query returned results
            /// Will be changed to throw an error instead of printing a value 
            /// </summary>
            int SizeOfList;
            test.TestIEnum(ref results, out SizeOfList);
            Console.WriteLine(SizeOfList);
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
            /// <remarks>
            /// Get Date and Time of system now so we do not have to keep changing the code
            /// </remarks>
            DateTime today = DateTime.Now;
            String year = today.Year.ToString();

            /// <remarks>
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

            /// <remarks>
            /// Since the data is only updated every day for the day before in part and fully for 
            /// 2 days before we get the dates for yesterday and the day before for filtering
            /// </remarks>
            String day = (today.Day - 1).ToString();
            String yday = (today.Day - 2).ToString();

            /// <remarks>
            ///The date field in the Query needs to be of type
            ///yyyy-mm-dd + T + hh:mm:ss.nnn (hours, minutes, seconds and nanoseconds.
            ///We format the data here to be in the correct type as the ToString method provided by 
            ///the DateTime class does not allow for formatting in this manner
            /// </remarks>
            String date = "\"" + year + "-" + month + "-" + day + "T00:00:00.000\"";
            String ydate = "\"" + year + "-" + month + "-" + yday + "T00:00:00.000\"";

            /// <remarks>
            /// A test case just to test that the connection is working
            /// will be removed in later revisions
            /// </remarks>
            //SODA.SoqlQuery soql = new SoqlQuery().Select("*").Limit(10);

            /// <remarks>
            /// for increased readability and easier formatting
            /// </remarks>
            string cdate = "created_date";

            /// <remarks>
            /// Usage of creating a new SoqlQuery - 
            /// Note: TimeStamp (or other literal) must be in quotes or else it gets treated like a variable which will throw an error
            /// Field + "function" + "comparison type"
            /// Select can be used to select specific fields but we want all data associated with the calls
            /// Hour compensates for my estimated time the database is updated with new info
            /// Will be updated again when we find out when exactly the data is updated every day
            /// </remarks>
            SODA.SoqlQuery soql;
            if (today.Hour > 19)
            {
                soql = new SoqlQuery().Select("*").Where(cdate + ">" + date);
            }
            else
            {
                soql = new SoqlQuery().Select("*").Where(cdate + ">" + ydate);
            }

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

}




    

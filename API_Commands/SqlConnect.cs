using System;
using System.Collections.Generic;
using Json311;
using NetTopologySuite.Geometries;
using Newtonsoft.Json.Linq;
using Npgsql;
using NpgsqlTypes;

namespace PgsqlDriver
{

    /// <summary>
    ///  Class to connect our program to our postgres DB and load our data into it
    /// </summary>
    class SqlConnect
    {
        /// <remarks> 
        /// Documentation for how to use is https://www.npgsql.org/doc/copy.html
        /// Copy command - list number of columns and then write them all 
        /// Will require 1 very large write function to accomodate all of our data types and values and then will go through the list and update it with all the new values 
        /// One huge iterator through the loop 
        /// </remarks>
        
        /// <summary>
        /// Establishes a connection between the program and our database (which will hopefully be hosted on azure 
        /// </summary>
        public String Connect()
        {
            String user = Authenticate(out String pass);
            Console.Write("Testing Connection: ");
            String connString = "Host=localhost;Username=" + user + ";Password=" + pass + ";Database=group7";
            try
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    this.CheckConnection(conn);
                    conn.Close();
                    return connString;
                }
            }    catch(Npgsql.PostgresException)
                 {
                    Console.WriteLine("Your username and password do not match, try again");
                    this.Connect();
                }

            return "failed";
            
        }

        /// <summary>
        /// make sure a connection has been established to the database
        /// Will also attempt to reconnect if the connection is checked and is no longer working
        /// Althogh connection is dropped when you leave scope since we are still inside the function
        /// when we make this call the connection stays open
        /// </summary>
        /// <param name="conn">The connection we are testing</param>
        public void CheckConnection(NpgsqlConnection conn)
        {
            if(conn.State == System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Connection Established");
            }
            else
            {
                Console.WriteLine("Connection Dropped, Please re-enter credentials");
                this.Connect();
            }
        }

        /// <summary>
        /// Responsible for user authentication to the psql database 
        /// </summary>
        /// <param name="pass">An out param to return the password to the calling function</param>
        /// <returns>Returns the username to the calling function</returns>
        private String Authenticate(out String pass)
        {
            String user = this.GetUser();
            pass = this.GetPassword(user);
            return user;
        }

        /// <summary>
        /// Get username for the database we are connecting to 
        /// and require that user enter a real username
        /// </summary>
        /// <returns>The username entered</returns>
        public String GetUser()
        {
            String user = "";
            Console.Write("Enter Username: ");
            while (user == "" || string.IsNullOrWhiteSpace(user))
            {
                user = Console.ReadLine();
            }
            return user;
            
        }
        
        /// <summary>
        /// Get the Password for the User specified to connect to our postgresDB
        /// </summary>
        /// <param name="user">The username we are getting the password for</param>
        /// <returns>returns the password to the calling function</returns>
        public String GetPassword(String user)
        {
            Console.Write("Enter Password for " + user + ": ");
            /// <remarks> 
            /// This is to make sure the password is not visible as it is typed 
            /// </remarks>
            String pass = null;
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                pass += key.KeyChar;
            }
            Console.WriteLine();
            return pass;
        }


        /// <summary>
        /// Our import command to import the data for the day into our pgsql database
        /// </summary>
        /// <param name="dataset">Our current DataSet to load into our DB</param>
        public void Import(List<Json311.Json311> dataset, String connString)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                this.CheckConnection(conn);
                conn.TypeMapper.UseJsonNet();

                /// <remarks>
                /// postgres does not support nullable datetimes so we check the DateTime fields and
                /// write a null if the value is null
                /// if not we cast the nullable datetime to datetime and then write that
                /// </remarks>
                using (var writer = conn.BeginBinaryImport("COPY calls FROM STDIN (FORMAT BINARY)"))
                {
                    foreach (Json311.Json311 entry in dataset)
                    {
                        writer.StartRow();
                        writer.Write(entry.Unique_key);

                        if(entry.Created_date==null)
                        {
                            writer.WriteNull();
                        }
                        else
                        {
                            NpgsqlDateTime cdate = Convert.ToDateTime(entry.Created_date);
                            writer.Write(cdate);
                        }


                        if (entry.Closed_date == null)
                        {
                            writer.WriteNull();
                        }
                        else
                        {
                            NpgsqlDateTime cdate = Convert.ToDateTime(entry.Closed_date);
                            writer.Write(cdate);
                        }
                        writer.Write(entry.Agency);
                        writer.Write(entry.Agency_name);
                        writer.Write(entry.Complaint_type);
                        writer.Write(entry.Descriptor);
                        writer.Write(entry.Location_type);
                        writer.Write(entry.Incident_zip);
                        writer.Write(entry.Incident_address);
                        writer.Write(entry.Street_name);
                        writer.Write(entry.Cross_street_1);
                        writer.Write(entry.Cross_street_2);
                        writer.Write(entry.Intersection_street_1);
                        writer.Write(entry.Intersection_street_2);
                        writer.Write(entry.Address_type);
                        writer.Write(entry.City);
                        writer.Write(entry.Landmark);
                        writer.Write(entry.Facility_type);
                        writer.Write(entry.Status);

                        
                        if(entry.Due_date == null)
                        {
                            writer.WriteNull();
                        }
                        else
                        {
                            NpgsqlDateTime dDate = Convert.ToDateTime(entry.Due_date);
                            writer.Write(dDate);
                        }
                        
                        writer.Write(entry.Resolution_description);

                        
                        if (entry.Resolution_action_updated_date == null)
                        {
                            writer.WriteNull();
                        }
                        else
                        {
                            NpgsqlDateTime rDate = Convert.ToDateTime(entry.Resolution_action_updated_date);
                            writer.Write(rDate);
                        }
                        
                        writer.Write(entry.Community_board);
                        writer.Write(entry.Bbl);
                        writer.Write(entry.Borough);
                        writer.Write(entry.X_coordinate_state_plane);
                        writer.Write(entry.Y_coordinate_state_plane);
                        writer.Write(entry.Open_data_channel_type);
                        writer.Write(entry.Park_facility_name);
                        writer.Write(entry.Park_borough);
                        writer.Write(entry.Vehicle_type);
                        writer.Write(entry.Taxi_company_borough);
                        writer.Write(entry.Taxi_pick_up_location);
                        writer.Write(entry.Bridge_highway_name);
                        writer.Write(entry.Bridge_highway_direction);
                        writer.Write(entry.Road_ramp);
                        writer.Write(entry.Bridge_highway_segment);
                        writer.Write(entry.Latitude);
                        writer.Write(entry.Longitude);
                        writer.Write(entry.Location_city);


                        if (entry.Location == null)
                        {
                            writer.WriteNull();
                        }
                        else
                        {
                            var dat = entry.Location.SelectToken("coordinates");
                            double x = (double)dat[0];
                            double y = (double)dat[1];
                            Point nPoint = new Point(x, y);
                            Console.WriteLine(nPoint);
                        }
                        writer.Write(entry.Location_zip);
                        writer.Write(entry.Location_state);
                    }
                    writer.Complete();
                }
                Console.WriteLine("Success?");
                conn.Close();
                //NpgsqlCommand update = new NpgsqlCommand("UPDATE checktime SET curr_up_date=" + DateTime.Now, conn);
                //update.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Checks the date to see if we need to make a new API call for today
        /// </summary>
        /// <param name="conn">recieves the connection to our database as a parameter</param>
        public void CheckDate(String connString)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                this.CheckConnection(conn);
                using (NpgsqlCommand checkDate = new NpgsqlCommand("SELECT * FROM checktime", conn))
                using (NpgsqlDataReader reader = checkDate.ExecuteReader())
                {
                    try
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader.GetDate(0));
                        }
                    }
                    catch (Exception a) { Console.WriteLine(a); }
                }
                conn.Close();
            }
        }




        /// <summary>
        /// This is just to check the types, the actual table has already been created
        /// This code will remain in case we need to create a new database 
        /// </summary>
        /// <returns>Returns the SQL command to create a table</returns>
        public NpgsqlCommand TableNew()
        {
            NpgsqlCommand checkTable = new NpgsqlCommand(
                    "CREATE TABLE IF NOT EXISTS calls311 (" +
                    "unique_key varchar," +
                    "created_date date," +
                    "closed_date date," +
                    "agency varchar," +
                    "agency_name varchar, " +
                    "complaint_type varchar," +
                    "descriptor varchar," +
                    "location_type varchar," +
                    "incident_zip varchar," +
                    "incident_address varchar," +
                    "street_name varchar," +
                    "cross_street_1 varchar," +
                    "cross_street_2 varchar," +
                    "intersection_street_1 varchar," +
                    "intersection_street_2 varchar," +
                    "address_type varchar, " +
                    "city varchar, " +
                    "landmark varchar," +
                    "facility_type varchar," +
                    "status varchar," +
                    "due_date date," +
                    "resolution_description varchar," +
                    "resolution_action_updated_date date," +
                    "community_board varchar," +
                    "bbl varchar, " +
                    "borough varchar, " +
                    "x_coordinate_state_plane varchar, " +
                    "y_coordinate_state_plane varchar," +
                    "open_data_channel_type varchar," +
                    "park_facility_name varchar," +
                    "park_borough varchar," +
                    "vehicle_type varchar," +
                    "taxi_company_borough varchar," +
                    "taxi_pick_up_location varchar, " +
                    "bridge_highway_name varchar," +
                    "bridge_highway_direction varchar, " +
                    "road_ramp varchar," +
                    "bridge_highway_segment varchar," +
                    "latitude varchar, " +
                    "longitude varchar," +
                    "location_city varchar," +
                    //"location point," +
                    "location_zip varchar, " +
                    "location_state varchar);"
                );

            return checkTable;
        }
       
    }
        

}

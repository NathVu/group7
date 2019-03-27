using System;
using System.Collections.Generic;
using System.Text;
using Json311;
using Npgsql;

namespace PgsqlDriver
{
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
        /// <param name="password"></param>
        /// <param name="username"></param>
        public NpgsqlConnection Connect(String password, String username)
        {
            string connString = "Host=localhost;Username=" + username + ";Password=" + password + ";Database=group7";
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();


                return conn;
            }
        }

        /// <summary>
        /// Our import command to import the data for the day into our pgsql database
        /// </summary>
        /// <param name="dataset"></param>
        public void Import(List<Json311.Json311> dataset, NpgsqlConnection conn)
        {
            foreach(Json311.Json311 entry in dataset)
            {

            }
        }

        public NpgsqlCommand TableNew()
        {
            NpgsqlCommand checkTable = new NpgsqlCommand(
                    "CREATE TABLE IF NOT EXISTS calls311 (" +
                    "varchar unique_key," +
                    "date created_date," +
                    "date closed_date," +
                    "varchar agency," +
                    "varchar agency_name, " +
                    "varchar complaint_type," +
                    "varchar descriptor," +
                    "varchar location_type," +
                    "varchar incident_zip," +
                    "varchar incident_address," +
                    "varchar street_name," +
                    "varchar cross_street_1," +
                    "varchar cross_street_2," +
                    "varchar intersection_street_1," +
                    "varchar intersection_street_2," +
                    "varchar address_type, " +
                    "varchar city, " +
                    "varchar landmark," +
                    "varchar facility_type" +
                    "varchar status," +
                    "date due_date," +
                    "varchar resolution_action_updated_date," +
                    "date resolution_action_updated_date," +
                    "varchar community_board," +
                    "varchar bbl, " +
                    "varchar borough, " +
                    "varchar x_coordinate_state_plane, " +
                    "varchar y_coordinate_state_plane," +
                    "varchar open_data_channel_type," +
                    "varchar park_facility_name," +
                    "varchar park_borough," +
                    "varchar vehicle_type," +
                    "varchar taxi_company_borough," +
                    "varchar taxi_pick_up_location, " +
                    "varchar bridge_highway_name," +
                    "varchar bridge_highway_direction, " +
                    "varchar road_ramp," +
                    "varchar bridge_highway_segment," +
                    "varchar latitude, " +
                    "varchar longitude," +
                    "varchar location_city," +
                    //"point location," +
                    "varchar location_zip, " +
                    "varchar location_state);"
                );

            return checkTable;
        }
       
    }
        

}

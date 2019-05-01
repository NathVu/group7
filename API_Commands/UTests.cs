using System;
using NUnit.Framework;
using Json311;
using Npgsql;

namespace UTests
{
    [TestFixture]
    public class Tests
    {

        /// <summary>
        /// Test to see how the database reacts with null values inserted
        /// </summary>
        [Test]
        public void JsonNull()
        {
            /// <remarks>
            /// Since data is often missing all fields are initialized to null if they do not have another value;
            /// </remarks>
            Json311.Json311 test = new Json311.Json311();
            SqlConnect DBTest = new SqlConnect();
            /// <remarks>
            /// Uses a test user who has only insert and select privelege in testtable
            /// </remarks>
            String connString = "Host=127.0.0.1;Port=5433;Username=test;Password=test;Database=postgres";
            NpgsqlConnection conn = new NpgsqlConnection();
            Assert.AreEqual(conn.State, System.Data.ConnectionState.Open);


        }
    }
}
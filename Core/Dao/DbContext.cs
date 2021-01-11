using GrowDataApp.Core.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrowDataApp.Core.Dao
{
    public class DbContext
    {
        public string ConnectionString { get; set; }

        public DbContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        /// <summary>
        /// Saves given data item.
        /// </summary>
        /// <param name="growDataItem">Data item to be saved</param>
        /// <returns>Saved data item.</returns>
        public GrowDataItem Save(GrowDataItem growDataItem)
        {
            using(MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception while opening mysql connection: " + ex.Message);
                    return growDataItem;
                }

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "insert into grow_data_item(timestamp, " +
                    "soil_temperature, air_temperature, " +
                    "soil_humidity, air_humidity)" +
                    "values(@timestamp, @soilTemperature, @airTemperature, @soilHumidity, @airHumidity)";
                cmd.Parameters.AddWithValue("timestamp", growDataItem.Timestamp);
                cmd.Parameters.AddWithValue("soilTemperature", growDataItem.SoilTemperature);
                cmd.Parameters.AddWithValue("airTemperature", growDataItem.AirTemperature);
                cmd.Parameters.AddWithValue("soilHumidity", growDataItem.SoilHumidity);
                cmd.Parameters.AddWithValue("airHumidity", growDataItem.AirHumidity);
                cmd.ExecuteNonQuery();
            }

            return growDataItem;
        }

        /// <summary>
        /// Returns all records in DB.
        /// </summary>
        /// <returns>Collection of records in DB.</returns>
        public IEnumerable<GrowDataItem> FindAll()
        {
            List<GrowDataItem> res = new List<GrowDataItem>();

            using(MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                } catch (Exception ex)
                {
                    Console.WriteLine("Exception while opening mysql connection: " + ex.Message);
                    return res;
                }

                MySqlCommand cmd = new MySqlCommand("select * from grow_data_item", conn);

                using(MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        res.Add(ConvertFromReader(reader));
                    }
                }
            }

            return res;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        private GrowDataItem ConvertFromReader(MySqlDataReader reader)
        {
            return new GrowDataItem()
            {
                Timestamp = DateTime.Parse(reader["timestamp"].ToString()),
                SoilTemperature = float.Parse(reader["soil_temperature"].ToString()),
                AirTemperature = float.Parse(reader["air_temperature"].ToString()),
                SoilHumidity = float.Parse(reader["soil_humidity"].ToString()),
                AirHumidity = float.Parse(reader["air_humidity"].ToString()),
            };
        }
    }
}

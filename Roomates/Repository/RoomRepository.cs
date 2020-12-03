using Microsoft.Data.SqlClient;
using Roomates.Models;
using System.Collections.Generic;

namespace Roomates.Repository
{
    /// <summary>
    ///  This class is responsible for interacting with Room data.
    ///  It inherits from the BaseRepository class so that it can use the BaseRepository's Connection property
    /// </summary>
    public class RoomRepository : BaseRepository
    {
        /// <summary>
        ///  When new RoomRespository is instantiated, pass the connection string along to the BaseRepository
        /// </summary>
        public RoomRepository(string connectionString) : base(connectionString) { }

        // ...We'll add some methods shortly...


        public List<Room> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Name, MaxOccupancy FROM Room";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Room> rooms = new List<Room>();

                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int IdValue = reader.GetInt32(idColumnPosition);

                        int nameColumnPosition = reader.GetOrdinal("Name");
                        string nameValue = reader.GetString(nameColumnPosition);

                        int maxOccupancyColumnPosition = reader.GetOrdinal("MaxOccupancy");
                        int maxOccupancy = reader.GetInt32(maxOccupancyColumnPosition);

                        Room room = new Room
                        {
                            Id = IdValue,
                            Name = nameValue,
                            MaxOccupancy = maxOccupancy,
                        };
                        rooms.Add(room);
                    }
                    reader.Close();
                    return rooms;
                }
            }
        }

    
    }
}
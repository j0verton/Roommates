using Microsoft.Data.SqlClient;
using Roommates.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Roommates.Repository
{
    public class RoommateRepository : BaseRepository
    {
        public RoommateRepository(string connectionString) : base(connectionString) { }
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true";

        public List<Roommate> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, FirstName, LastName FROM Roommate";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Roommate> roommates = new List<Roommate>();

                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int IdValue = reader.GetInt32(idColumnPosition);

                        int nameColumnPosition = reader.GetOrdinal("FirstName");
                        string nameValue = reader.GetString(nameColumnPosition);

                        int lNameColumnPosition = reader.GetOrdinal("LastName");
                        string lNameValue = reader.GetString(lNameColumnPosition);

                        Roommate roommate = new Roommate
                        {
                            Id = IdValue,
                            FirstName = nameValue,
                            LastName = lNameValue,
                        };
                        roommates.Add(roommate);
                    }
                    reader.Close();
                    return roommates;
                }
            }
        }


        public Roommate GetById(int id)
        { 
                using (SqlConnection conn = Connection)
                    {
                        conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT r.FirstName, r.LastName, r.RentPortion, r.RoomId FROM Roommate r WHERE r.Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    Roommate roommate = null;
                    if (reader.Read())
                    {
                        RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);

                    roommate = new Roommate
                        {
                            Id = id,
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            Room = roomRepo.GetById(reader.GetInt32(reader.GetOrdinal("RoomId")))

                        };
                    }
                    reader.Close();
                    return roommate;
                }
            }
        }

                //public void Insert(Roommate roommate) 
                //{
                //    using (SqlConnection conn = Connection)
                //    {
                //        conn.Open();
                //        using (SqlCommand cmd = conn.CreateCommand())
                //        { 
                //        cmd.CommandText = @"INSERT INTO Chore (FirstName, LastName, RentPortion, MovedInDate, RoomId) 
                //                                     OUTPUT INSERTED.Id 
                //                                     VALUES (@FirstName, @LastName, @RentPortion, @MovedInDate, @RoomId)";
                //            cmd.Parameters.AddWithValue("@FirstName", roommate.FirstName);
                //            cmd.Parameters.AddWithValue("@LastName", roommate.LastName);
                //            cmd.Parameters.AddWithValue("@RentPortion", roommate.RentPortion);
                //            cmd.Parameters.AddWithValue("@MoveInDate", roommate.MovedInDate);
                //            cmd.Parameters.AddWithValue("@RoomId", roommate.Room);

                //        }

                //    }

                //}


            }
}

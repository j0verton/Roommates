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

        public Roommate GetById(int id)
        { 
                using (SqlConnection conn = Connection)
                    {
                        conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT r.FirstName, r.RentPortion, Room.id, Room.Name FROM Roommate r JOIN Room ON Room.id = r.RoomId WHERE r.Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    Roommate roommate = null;
                    if (reader.Read())
                    {

                        roommate = new Roommate
                        {
                            Id = id,
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            Room = RoomRepository.GetById(reader.GetInt32(reader.GetOrdinal("RoomId")))

                        };
                    reader.Close();
                    return roommate;
                    }
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

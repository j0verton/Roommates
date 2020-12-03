using Microsoft.Data.SqlClient;
using Roomates.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Roomates.Repository
{
    public class RoomateRepository : BaseRepository
    {
        public RoomateRepository(string connectionString) : base(connectionString) { }

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

                            //what am i doing here use getbyId to get the room?
                           // Room = new Room
                        };
                    reader.Close();
                        return roommate;
                    }
                }
            }
        }

                //public void Insert(Roommate roomate) 
                //{
                //    using (SqlConnection conn = Connection)
                //    {
                //        conn.Open();
                //        using (SqlCommand cmd = conn.CreateCommand())
                //        { 
                //        cmd.CommandText = @"INSERT INTO Chore (FirstName, LastName, RentPortion, MovedInDate, RoomId) 
                //                                     OUTPUT INSERTED.Id 
                //                                     VALUES (@FirstName, @LastName, @RentPortion, @MovedInDate, @RoomId)";
                //            cmd.Parameters.AddWithValue("@FirstName", roomate.FirstName);
                //            cmd.Parameters.AddWithValue("@LastName", roomate.LastName);
                //            cmd.Parameters.AddWithValue("@RentPortion", roomate.RentPortion);
                //            cmd.Parameters.AddWithValue("@MoveInDate", roomate.MovedInDate);
                //            cmd.Parameters.AddWithValue("@RoomId", roomate.Room);

                //        }

                //    }

                //}


            }
}

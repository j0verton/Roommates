using Microsoft.Data.SqlClient;
using Roomates.Models;
using Roommates.Models;
using System;
using System.Collections.Generic;
using System.Text;
namespace Roommates.Repository
{
    class ChoreRepository : BaseRepository
    {
        public ChoreRepository(string connectionString) : base(connectionString) { }

        public List<Chore> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Name FROM Chore";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Chore> chores = new List<Chore>();
                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int IdValue = reader.GetInt32(idColumnPosition);

                        int nameColumnPosition = reader.GetOrdinal("Name");
                        string nameValue = reader.GetString(nameColumnPosition);

                        Chore chore = new Chore
                        {
                            Id = IdValue,
                            Name = nameValue,
                        };
                        chores.Add(chore);
                    }
                    reader.Close();
                    return chores;
                }
            }    
        }

        public Chore GetById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Name FROM Chore WHERE Id = @id";
                        cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    Chore chore = null;

                        if (reader.Read())
                    {
                        chore = new Chore
                        {
                            Id = id,
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                        }; 
                            }
                    reader.Close();
                    return chore;
                }
            }
        }


        public void Insert(Chore chore)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                { 
                cmd.CommandText = @"INSERT INTO Chore (Name) 
                                             OUTPUT INSERTED.Id 
                                             VALUES (@name)";
                    cmd.Parameters.AddWithValue("@name", chore.Name);
                    int id = (int)cmd.ExecuteScalar();

                    chore.Id = id;

                }
            }
        }

        public List<Chore> GetUnassignedChores() 
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT c.Id, c.Name, rc.RoommateId
                        FROM Chore c
                        LEFT JOIN RoommateChore rc ON rc.ChoreId = c.Id
                        WHERE rc.RoommateId is NULL";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Chore> unassignedChores = new List<Chore>();

                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int IdValue = reader.GetInt32(idColumnPosition);

                        int nameColumnPosition = reader.GetOrdinal("Name");
                        string nameValue = reader.GetString(nameColumnPosition);

                        Chore chore = new Chore
                        {
                            Id = IdValue,
                            Name = nameValue,
                        };
                        unassignedChores.Add(chore);
                    }
                    reader.Close();
                    return unassignedChores;
                }
            
            
            }

        }

        public void AssignChore(int roommateId, int choreId) 
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO RoommateChore (RoommateId, ChoreId) 
                                            OUTPUT INSERTED.Id 
                                            VALUES (@roommateid, @choreid)";
                    cmd.Parameters.AddWithValue("@roommateid", roommateId);
                    cmd.Parameters.AddWithValue("@choreid", choreId);
                    int id = (int)cmd.ExecuteScalar();

                    //RoommateChore.Id = id;

                }
            }
        }

        public List<ChoreCount> GetChoreCounts()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT r.FirstName, COUNT(rc.Id) AS Count
                        FROM RoommateChore rc
                        JOIN Roommate r on rc.RoommateId = r.Id
                        GROUP BY r.FirstName";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<ChoreCount> counts = new List<ChoreCount>();

                    while (reader.Read())
                    {
                        //int idColumnPosition = reader.GetOrdinal("Id");
                        //int IdValue = reader.GetInt32(idColumnPosition);

                        int nameColumnPosition = reader.GetOrdinal("FirstName");
                        string nameValue = reader.GetString(nameColumnPosition);

                        int countColumnPosition = reader.GetOrdinal("Count");
                        int CountValue = reader.GetInt32(countColumnPosition);

                        ChoreCount choreCount = new ChoreCount
                        {
                            //Id = IdValue,
                            Name = nameValue,
                            Count = CountValue,
                        };
                        counts.Add(choreCount);

                    }
                    reader.Close();
                    return counts;
                }
            }
        }

        public void Update(Chore chore)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Chore
                                    SET Name = @name,
                                    WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@name", chore.Name);
                    cmd.Parameters.AddWithValue("@id", chore.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id )
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"DELETE FROM Chore WHERE Id = @id
                                        Delete FROM chore WHERE";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}


using Microsoft.Data.SqlClient;
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

        //public List<Roomates.Models.ChoreCount> GetChoreCounts()
        //{ 
        
        
        //}


    }
}


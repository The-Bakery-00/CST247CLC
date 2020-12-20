using MilestoneCST247.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MilestoneCST247.Services.Data
{
    public class ScoreDAO
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MineSweepr;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public List<GameStats> GetAllScores()
        {
            string query = $"SELECT [G].[ID], [G].[GRIDID], [G].[CLICKS], [G].[USERID] FROM [dbo].[games] AS G";
            List<GameStats> scores = new List<GameStats>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand(query, con);
                try
                {
                    comm.Connection.Open();

                    SqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        GameStats score = new GameStats
                        {
                            Id = (int)reader["ID"],
                            Gridid = (int)reader["GRIDID"],
                            Clicks = (int)reader["CLICKS"],
                            Userid = (int)reader["USERID"]
                        };
                        scores.Add(score);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                scores.Sort();
                return scores.Where(m => m.Clicks > 0).ToList();
            }
        }
    }
}
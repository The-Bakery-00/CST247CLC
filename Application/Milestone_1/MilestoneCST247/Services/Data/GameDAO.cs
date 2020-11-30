using MilestoneCST247.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MilestoneCST247.Services.Data
{
    public class GameDAO
    {

        // way to connect to the DB
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MineSweepr;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public Grid findGrid(User user)
        {
            Grid g = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string queryString = "SELECT * FROM dbo.grids WHERE USERID=@id";

                    // create the command and parameter objects

                    SqlCommand command = new SqlCommand(queryString, connection);

                    // Set query parameters and their values
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int, 11).Value = user.Id; 

                    // open the database and run the command

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int ID = int.Parse(reader["ID"].ToString());
                        int rows = int.Parse(reader["ROWS"].ToString());
                        int cols = int.Parse(reader["COLS"].ToString());
                        int USER_ID = int.Parse(reader["USERID"].ToString());
                        Boolean GAMEOVER = Boolean.Parse(reader["GAMEOVER"].ToString());
                    
                        g = new Grid(ID, rows, cols, USER_ID, GAMEOVER);
                        g.Cells = new Cell[cols, rows];
                    }

                    // Close the connection
                    connection.Close();


                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            if (g != null)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {

                        // Setup SELECT query with parameters
                        string queryString = "SELECT * FROM dbo.cells WHERE GRIDID=@id";
                        // create the command and parameter objects

                        SqlCommand command = new SqlCommand(queryString, connection);

                        // Set query parameters and their values
                        command.Parameters.Add("@id", System.Data.SqlDbType.Int, 11).Value = g.Id; /////////////////////////////////////// *******this is a test but this needs to be g.Id*** //////////////////////////////


                        // open the database and run the command

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int ID = int.Parse(reader["ID"].ToString());
                            int x = int.Parse(reader["X"].ToString());
                            int y = int.Parse(reader["Y"].ToString());
                            Boolean bomb = Boolean.Parse(reader["BOMB"].ToString());
                            Boolean visited = Boolean.Parse(reader["VISITED"].ToString());
                            int live = int.Parse(reader["LIVENEIGHBORS"].ToString());
                            int gridId = int.Parse(reader["GRIDID"].ToString());

                            Cell c = new Cell(x, y);
                            c.Id = ID;
                            c.Bomb = bomb;
                            c.Visited = visited;
                            c.LiveNeighbors = live;
                            g.Cells[x, y] = c;

                        }

                        // Close the connection
                        connection.Close();

                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

            
            return g;
        }


        public void createGrid(Grid grid)
        {

            int gridID = -1;

            try
            {
                // Setup INSERT query with parameters
                string queryString = "INSERT INTO dbo.grids (ROWS, COLS, USERID, GAMEOVER) " +
                    "VALUES (@Rows, @Cols, @User_ID, @GameOver) SELECT SCOPE_IDENTITY()";

                // Create connection and command
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    // create the command and parameter objects

                    SqlCommand command = new SqlCommand(queryString, connection);

                    // Set query parameters and their values
                    command.Parameters.Add("@Rows", System.Data.SqlDbType.Int, 11).Value = grid.Rows;
                    command.Parameters.Add("@Cols", System.Data.SqlDbType.Int, 11).Value = grid.Cols;
                    command.Parameters.Add("@User_ID", System.Data.SqlDbType.Int, 11).Value = grid.Userid;
                    command.Parameters.Add("@GameOver", System.Data.SqlDbType.Bit).Value = grid.GameOver;
                    
                    // open the database and run the command

                    connection.Open();
                    gridID = Convert.ToInt32(command.ExecuteScalar());

                    // Close the connection
                    connection.Close();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            try
            {
                // Setup INSERT query with parameters
                string queryString = "INSERT INTO dbo.cells (X, Y, BOMB, VISITED, LIVENEIGHBORS, GRIDID) " +
                    "VALUES (@x, @y, @bomb, @visited, @live, @grid)";

                // Create connection and command
                for (int y = 0; y < grid.Rows; y++)
                {
                    for (int x = 0; x < grid.Cols; x++)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            SqlCommand command = new SqlCommand(queryString, connection);

                            // Set query parameters and their values
                            command.Parameters.Add("@x", System.Data.SqlDbType.Int, 11).Value = grid.Cells[x, y].X;
                            command.Parameters.Add("@y", System.Data.SqlDbType.Int, 11).Value = grid.Cells[x, y].Y;
                            command.Parameters.Add("@bomb", System.Data.SqlDbType.Bit).Value = grid.Cells[x, y].Bomb;
                            command.Parameters.Add("@visited", System.Data.SqlDbType.Bit).Value = grid.Cells[x, y].Visited;
                            command.Parameters.Add("@live", System.Data.SqlDbType.Int, 11).Value = grid.Cells[x, y].LiveNeighbors;
                            command.Parameters.Add("@grid", System.Data.SqlDbType.Int, 11).Value = gridID;

                            // Open the connection, execute INSERT, and close the connection
                            connection.Open();
                            int rows = command.ExecuteNonQuery();
                            connection.Close();



                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



        }



        public void updateGrid(Grid grid)
        {

            try
            {
                // Setup INSERT query with parameters

                string queryString = "UPDATE dbo.grids SET ROWS = @Rows, COLS = @Cols, USERID = @User_ID, GAMEOVER = @GameOver WHERE ID=@id";

                // Create connection and command
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    // Set query parameters and their values
                    command.Parameters.Add("@Rows", System.Data.SqlDbType.Int, 11).Value = grid.Rows;
                    command.Parameters.Add("@Cols", System.Data.SqlDbType.Int, 11).Value = grid.Cols;
                    command.Parameters.Add("@User_ID", System.Data.SqlDbType.Int, 11).Value = grid.Userid;
                    command.Parameters.Add("@GameOver", System.Data.SqlDbType.Bit).Value = grid.GameOver;
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int, 11).Value = grid.Id;

                    // Open the connection, execute INSERT, and close the connection
                    connection.Open();
                    command.ExecuteNonQuery();

                    connection.Close();



                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            try
            {
                // Setup INSERT query with parameters

                string queryString = "UPDATE dbo.cells SET X = @x, Y = @y, BOMB = @bomb, VISITED = @visited, LIVENEIGHBORS = @live, " +
                    "GRIDID = @grid WHERE ID=@id";



                // Create connection and command
                for (int y = 0; y < grid.Rows; y++)
                {
                    for (int x = 0; x < grid.Cols; x++)
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            SqlCommand command = new SqlCommand(queryString, connection);

                            // Set query parameters and their values
                            command.Parameters.Add("@x", System.Data.SqlDbType.Int, 11).Value = grid.Cells[x, y].X;
                            command.Parameters.Add("@y", System.Data.SqlDbType.Int, 11).Value = grid.Cells[x, y].Y;
                            command.Parameters.Add("@bomb", System.Data.SqlDbType.Bit).Value = grid.Cells[x, y].Bomb;
                            command.Parameters.Add("@visited", System.Data.SqlDbType.Bit).Value = grid.Cells[x, y].Visited;
                            command.Parameters.Add("@live", System.Data.SqlDbType.Int, 11).Value = grid.Cells[x, y].LiveNeighbors;
                            command.Parameters.Add("@grid", System.Data.SqlDbType.Int, 11).Value = grid.Id;
                            command.Parameters.Add("@id", System.Data.SqlDbType.Int, 11).Value = grid.Cells[x, y].Id;
                            // Open the connection, execute INSERT, and close the connection
                            connection.Open();
                            int rows = command.ExecuteNonQuery();
                            connection.Close();

                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



        }




        public void deleteGrid(User user)
        {

            try
            {
                string queryString = "DELETE FROM dbo.grids WHERE USERID=@Id";


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.AddWithValue("@Id", user.Id);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }

    }
}
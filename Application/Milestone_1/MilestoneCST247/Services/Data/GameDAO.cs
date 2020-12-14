using MilestoneCST247.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MilestoneCST247.Services.Data
{
    public class GameDAO
    {

        // way to connect to the DB **Will be different with each DB**
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Minesweeper;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public Grid findGrid(User user)
        {
            // creat a grid instance and assign it to null so you can pull new accurate data, if no value exist then null will be returned and new grid will be made
            Grid g = null;

            try
            {
                // establish a connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // query string that will be passed and executed in DB  
                    string queryString = "SELECT * FROM dbo.grids WHERE USERID=@id";

                    // create the command and parameter objects

                    SqlCommand command = new SqlCommand(queryString, connection);

                    // Set query parameters and their values, this helps prevent from attacks
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int, 11).Value = user.Id;

                    // open the database, establsih connection and execute query
                    connection.Open();

                    // Using a DataReader see if query returns any rows
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int ID = int.Parse(reader["ID"].ToString());
                        int rows = int.Parse(reader["ROWS"].ToString());
                        int cols = int.Parse(reader["COLS"].ToString());
                        int USER_ID = int.Parse(reader["USERID"].ToString());
                        Boolean GAMEOVER = Boolean.Parse(reader["GAMEOVER"].ToString());
                        int clicks = int.Parse(reader["CLICKS"].ToString());

                        g = new Grid(ID, rows, cols, USER_ID, GAMEOVER, clicks);
                        g.Cells = new Cell[cols, rows];
                    }

                    // Close the connection
                    connection.Close();
                }

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Catch Exception in GameDAO method findGrid() in trying to pull or set user DB info, + exception message = " + e);

            }

            // if a grid exist you will now pull its cells, if none exist g will be passed as a null where g will be created in createGrid()
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
                        command.Parameters.Add("@id", System.Data.SqlDbType.Int, 11).Value = g.Id;


                        // open the database and run the command

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            // create an object that will be assigned to the vlaue pulled from DB, if value is null program will stop and notifity you, else value will be assigned properly and continue
                            Object o = new Object();

                            o = reader["ID"];
                            int ID = new int();
                            // is value is not null assign to correctly and continue
                            if (o != null)
                            {
                                ID = int.Parse(reader["ID"].ToString());
                            }
                            // else the program will exist and give post description in the consol
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("Null value in GameDAO/findGrid() while pulling value from table cells, colum ID");

                            }

                            o = reader["X"];
                            int x = new int();
                            // is value is not null assign to correctly and continue
                            if (o != null)
                            {
                                x = int.Parse(reader["X"].ToString());
                            }
                            // else the program will exist and give post description in the consol
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("Null value in GameDAO/findGrid() while pulling value from table cells, colum X");

                            }

                            o = reader["Y"];
                            int y = new int();
                            // is value is not null assign to correctly and continue
                            if (o != null)
                            {
                                y = int.Parse(reader["Y"].ToString());
                            }
                            // else the program will exist and give post description in the consol
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("Null value in GameDAO/findGrid() while pulling value from table cells, colum Y");

                            }

                            o = reader["BOMB"];
                            Boolean bomb = new Boolean();
                            // is value is not null assign to correctly and continue
                            if (o != null)
                            {
                                bomb = Boolean.Parse(reader["BOMB"].ToString());
                            }
                            // else the program will exist and give post description in the consol
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("Null value in GameDAO/findGrid() while pulling value from table cells, colum BOMB");

                            }

                            o = reader["VISITED"];
                            Boolean visited = new Boolean();
                            // is value is not null assign to correctly and continue
                            if (o != null)
                            {
                                visited = Boolean.Parse(reader["VISITED"].ToString());
                            }
                            // else the program will exist and give post description in the consol
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("Null value in GameDAO/findGrid() while pulling value from table cells, colum VISITED");

                            }


                            o = reader["LIVENEIGHBORS"];
                            int live = new int();
                            // is value is not null assign to correctly and continue
                            if (o != null)
                            {
                                live = int.Parse(reader["LIVENEIGHBORS"].ToString());
                            }
                            // else the program will exist and give post description in the consol
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("Null value in GameDAO/findGrid() while pulling value from table cells, colum LIVENEIGHBORS");

                            }

                            o = reader["GRIDID"];
                            int gridId = new int();
                            // is value is not null assign to correctly and continue
                            if (o != null)
                            {
                                gridId = int.Parse(reader["GRIDID"].ToString());
                            }
                            // else the program will exist and give post description in the consol
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("Null value in GameDAO/findGrid() while pulling value from table cells, colum GRIDID");

                            }

                            // create new cell and pass the newly pulled values to that cell, then assign said cell to the correct x,y in cells list
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
                    System.Diagnostics.Debug.WriteLine("Catch Exception in GameDAO method findGrid() in pulling cell section, + exception message = " + e);
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
                string query = "INSERT INTO dbo.grids (ROWS, COLS, USERID, GAMEOVER, CLICKS) " +
                    "VALUES (@Rows, @Cols, @User_ID, @GameOver, @clicks) SELECT SCOPE_IDENTITY()";

                // Create connection and command
                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    // Set query parameters and their values
                    cmd.Parameters.Add("@Rows", SqlDbType.Int, 11).Value = grid.Rows;
                    cmd.Parameters.Add("@Cols", SqlDbType.Int, 11).Value = grid.Cols;
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int, 11).Value = grid.Userid;
                    cmd.Parameters.Add("@GameOver", SqlDbType.Bit).Value = grid.GameOver;
                    cmd.Parameters.Add("@clicks", SqlDbType.Int, 11).Value = grid.Clicks;


                    // Open the connection, execute INSERT, and close the connection
                    cn.Open();
                    gridID = Convert.ToInt32(cmd.ExecuteScalar());

                    cn.Close();



                }

            }
            catch (SqlException e)
            {
                // TODO: should log exception and then throw a custom exception
                throw e;
            }


            try
            {
                // Setup INSERT query with parameters
                string query = "INSERT INTO dbo.cells (X, Y, BOMB, VISITED, LIVENEIGHBORS, GRIDID) " +
                    "VALUES (@x, @y, @bomb, @visited, @live, @grid)";

                // Create connection and command
                for (int y = 0; y < grid.Rows; y++)
                {
                    for (int x = 0; x < grid.Cols; x++)
                    {
                        using (SqlConnection cn = new SqlConnection(connectionString))
                        using (SqlCommand cmd = new SqlCommand(query, cn))
                        {
                            // Set query parameters and their values
                            cmd.Parameters.Add("@x", SqlDbType.Int, 11).Value = grid.Cells[x, y].X;
                            cmd.Parameters.Add("@y", SqlDbType.Int, 11).Value = grid.Cells[x, y].Y;
                            cmd.Parameters.Add("@bomb", SqlDbType.Bit).Value = grid.Cells[x, y].Bomb;
                            cmd.Parameters.Add("@visited", SqlDbType.Bit).Value = grid.Cells[x, y].Visited;
                            cmd.Parameters.Add("@live", SqlDbType.Int, 11).Value = grid.Cells[x, y].LiveNeighbors;
                            cmd.Parameters.Add("@grid", SqlDbType.Int, 11).Value = gridID;

                            // Open the connection, execute INSERT, and close the connection
                            cn.Open();
                            int rows = cmd.ExecuteNonQuery();
                            cn.Close();



                        }
                    }
                }

            }
            catch (SqlException e)
            {
                // TODO: should log exception and then throw a custom exception
                throw e;
            }



        }



        public void updateGrid(Grid grid)
        {

            try
            {
                // Setup INSERT query with parameters

                string query = "UPDATE dbo.grids SET ROWS = @Rows, COLS = @Cols, USERID = @User_ID, GAMEOVER = @GameOver, CLICKS = @clicks WHERE ID=@id";

                // Create connection and command
                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    // Set query parameters and their values
                    cmd.Parameters.Add("@Rows", SqlDbType.Int, 11).Value = grid.Rows;
                    cmd.Parameters.Add("@Cols", SqlDbType.Int, 11).Value = grid.Cols;
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int, 11).Value = grid.Userid;
                    cmd.Parameters.Add("@GameOver", SqlDbType.Bit).Value = grid.GameOver;
                    cmd.Parameters.Add("@id", SqlDbType.Int, 11).Value = grid.Id;
                    cmd.Parameters.Add("@clicks", SqlDbType.Int, 11).Value = grid.Clicks;

                    // Open the connection, execute INSERT, and close the connection
                    cn.Open();
                    cmd.ExecuteNonQuery();

                    cn.Close();



                }

            }
            catch (SqlException e)
            {
                // TODO: should log exception and then throw a custom exception
                throw e;
            }


            try
            {
                // Setup INSERT query with parameters

                string query = "UPDATE dbo.cells SET X = @x, Y = @y, BOMB = @bomb, VISITED = @visited, LIVENEIGHBORS = @live, " +
                    "GRIDID = @grid WHERE ID=@id";



                // Create connection and command
                for (int y = 0; y < grid.Rows; y++)
                {
                    for (int x = 0; x < grid.Cols; x++)
                    {
                        using (SqlConnection cn = new SqlConnection(connectionString))
                        using (SqlCommand cmd = new SqlCommand(query, cn))
                        {
                            // Set query parameters and their values
                            cmd.Parameters.Add("@x", SqlDbType.Int, 11).Value = grid.Cells[x, y].X;
                            cmd.Parameters.Add("@y", SqlDbType.Int, 11).Value = grid.Cells[x, y].Y;
                            cmd.Parameters.Add("@bomb", SqlDbType.Bit).Value = grid.Cells[x, y].Bomb;
                            cmd.Parameters.Add("@visited", SqlDbType.Bit).Value = grid.Cells[x, y].Visited;
                            cmd.Parameters.Add("@live", SqlDbType.Int, 11).Value = grid.Cells[x, y].LiveNeighbors;
                            cmd.Parameters.Add("@grid", SqlDbType.Int, 11).Value = grid.Id;
                            cmd.Parameters.Add("@id", SqlDbType.Int, 11).Value = grid.Cells[x, y].Id;
                            // Open the connection, execute INSERT, and close the connection
                            cn.Open();
                            int rows = cmd.ExecuteNonQuery();
                            cn.Close();

                        }
                    }
                }

            }
            catch (SqlException e)
            {
                // TODO: should log exception and then throw a custom exception
                throw e;
            }



        }




        public void deleteGrid(User user)
        {

            try
            {
                string query = "DELETE FROM dbo.grids WHERE USERID=@Id ";


                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Id", user.Id);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
            catch (SqlException e)
            {
                // TODO: should log exception and then throw a custom exception
                throw e;
            }


        }

        //checks if game results have already been published 
        public Boolean gridPublished(Grid g)
        {
            bool result = false;

            try
            {
                // Setup SELECT query with parameters
                string query = "SELECT * FROM dbo.games WHERE ID=@id";

                // Create connection and command
                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    // Set query parameters and their values
                    cmd.Parameters.Add("@id", SqlDbType.Int, 11).Value = g.Id;

                    // Open the connection
                    cn.Open();

                    // Using a DataReader see if query returns any rows
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        result = true;
                    else
                        result = false;

                    // Close the connection
                    cn.Close();
                }

            }
            catch (SqlException e)
            {
                // TODO: should log exception and then throw a custom exception
                throw e;
            }

            // Return result of finder
            return result;
        }


        //saves game stats to db table 'games'
        public void publishGrid(Grid g)
        {
            try
            {
                // Setup INSERT query with parameters
                string query = "INSERT INTO dbo.games (GRIDID, USERID, CLICKS) " +
                    "VALUES (@Gridid, @Userid, @Clicks)";

                // Create connection and command
                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    // Set query parameters and their values
                    cmd.Parameters.Add("@Gridid", SqlDbType.Int, 11).Value = g.Id;
                    cmd.Parameters.Add("@Userid", SqlDbType.Int, 11).Value = g.Userid;
                    cmd.Parameters.Add("@Clicks", SqlDbType.Int, 11).Value = g.Clicks;

                    // Open the connection, execute INSERT, and close the connection
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();

                }

            }
            catch (SqlException e)
            {
                // TODO: should log exception and then throw a custom exception
                throw e;
            }
        }


        //returns all game stats in a list
        public List<PublishedGame> getAllStats()
        {
            List<PublishedGame> games = new List<PublishedGame>();

            try
            {
                // Setup SELECT query with parameters
                string query = "SELECT * FROM dbo.games";

                // Create connection and command
                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {

                    // Open the connection
                    cn.Open();

                    // Using a DataReader see if query returns any rows
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int ID = int.Parse(reader["ID"].ToString());
                        int gridid = int.Parse(reader["GRIDID"].ToString());
                        int userid = int.Parse(reader["USERID"].ToString());
                        int clicks = int.Parse(reader["CLICKS"].ToString());

                        games.Add(new PublishedGame(ID, gridid, userid, clicks));
                    }

                    // Close the connection
                    cn.Close();
                }

            }
            catch (SqlException e)
            {
                throw e;
            }

            return games;
        }


        //test db connectivity
        public Boolean testService()
        {
            List<PublishedGame> games = new List<PublishedGame>();

            try
            {
                // Setup SELECT query with parameters
                string query = "SELECT 1";

                // Create connection and command
                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {

                    // Open the connection
                    cn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return true;
                    }
                    // Close the connection
                    cn.Close();
                }

            }
            catch (SqlException e)
            {
                return false;
            }

            return false;
        }




    }


}
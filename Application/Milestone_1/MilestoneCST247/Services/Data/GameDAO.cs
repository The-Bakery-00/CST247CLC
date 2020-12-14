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

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
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
                            System.Diagnostics.Debug.WriteLine("Null value in GameDAO/findGrid() while pulling value from table grids, colum ID");

                        }

                        o = reader["ROWS"];
                        int rows = new int();
                        // is value is not null assign to correctly and continue
                        if (o != null)
                        {
                            rows = int.Parse(reader["ROWS"].ToString());
                        }
                        // else the program will exist and give post description in the consol
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("Null value in GameDAO/findGrid() while pulling value from table grids, colum ROWS");

                        }

                        o = reader["COLS"];
                        int cols = new int();
                        // is value is not null assign to correctly and continue
                        if (o != null)
                        {
                            cols = int.Parse(reader["COLS"].ToString());
                        }
                        // else the program will exist and give post description in the consol
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("Null value in GameDAO/findGrid() while pulling value from table grids, colum COLS");

                        }

                        o = reader["USERID"];
                        int USER_ID = new int();
                        // is value is not null assign to correctly and continue
                        if (o != null)
                        {
                            USER_ID = int.Parse(reader["USERID"].ToString());
                        }
                        // else the program will exist and give post description in the consol
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("Null value in GameDAO/findGrid() while pulling value from table grids, colum USERID");

                        }

                        o = reader["GAMEOVER"];
                        Boolean GAMEOVER = new Boolean();
                        // is value is not null assign to correctly and continue
                        if (o != null)
                        {
                            GAMEOVER = Boolean.Parse(reader["GAMEOVER"].ToString());
                        }
                        // else the program will exist and give post description in the consol
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("Null value in GameDAO/findGrid() while pulling value from table grids, colum GAMEOVER");

                        }
                        
                    
                        // create new grid and pass values into it, then create new cells list in which values will be assigned below
                        g = new Grid(ID, rows, cols, USER_ID, GAMEOVER);
                        g.Cells = new Cell[cols, rows];
                    }

                    // Close the connection
                    connection.Close();
                    System.Diagnostics.Debug.WriteLine("Youre in gameDAO findGrid() after connecting to grid DB");

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
                // DELETE query to delete old grid 
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

    // REST FUNCTION to return player stats

}
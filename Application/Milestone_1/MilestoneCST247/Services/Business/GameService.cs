using MilestoneCST247.Models;
using MilestoneCST247.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MilestoneCST247.Services.Business
{
    public class GameService
    {
        /** Game Service class **/


        //returns grid for userS
        public Grid findGrid(User user)
        {
            //System.Diagnostics.Debug.WriteLine("Loading grid for: "+user.Email);

            GameDAO gameDAO = new GameDAO();

            return gameDAO.findGrid(user);

        }

        //deletes grid from db
        public void removeGrid(User user)
        {
            GameDAO gameDAO = new GameDAO();

            gameDAO.deleteGrid(user);

        }

        public void publishGrid(Grid g)
        {

            GameDAO gameDAO = new GameDAO();

            //updates grid in db

            if (!gameDAO.gridPublished(g))
                gameDAO.publishGrid(g);

        }

        public List<PublishedGame> getAllGames()
        {
            GameDAO gameDAO = new GameDAO();

            //saves game stats to db
            return gameDAO.getAllStats();
        }

        public Boolean testService()
        {
            GameDAO gameDAO = new GameDAO();

            //saves game stats to db
            return gameDAO.testService();
        }

        //activates cell in grid
        public void activateCell(Grid g, int X, int Y)
        {

            //loop through cells and find what needs to be revealed
            //reveal these cells on the grid
            //update grid to db

            GameDAO gameDAO = new GameDAO();

            Cell c = g.Cells[X, Y];

            c.Visited = true;


            //checks if cell is a bomb
            if (c.Bomb)
            {

                //revelals all of grid
                for (int y = 0; y < g.Rows; y++)
                {
                    for (int x = 0; x < g.Cols; x++)
                    {
                        g.Cells[x, y].Visited = true;
                    }
                }
                g.GameOver = true;

                System.Diagnostics.Debug.WriteLine("Hit bomb at: " + X + ", " + Y);
            }
            else
            {
                //recursively reveals neighboring cells that have no bombs around them
                if (c.LiveNeighbors == 0)
                    revealSurroundingCells(g, c.X, c.Y);

                //checks if game has been won
                if (gameWon(g))
                {

                    //reveals whole grid
                    for (int y = 0; y < g.Rows; y++)
                    {
                        for (int x = 0; x < g.Cols; x++)
                        {
                            g.Cells[x, y].Visited = true;
                        }
                    }

                    g.GameOver = true;
                }

            }

            //updates grid in db
            gameDAO.updateGrid(g);

        }


        private Boolean gameWon(Grid g)
        {
            //loops through every cell and checks
            //if there's still an unvisited cell that
            //isn't a bomb
            for (int y = 0; y < g.Rows; y++)
            {
                for (int x = 0; x < g.Cols; x++)
                {
                    if (!g.Cells[x, y].Visited && !g.Cells[x, y].Bomb)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void revealSurroundingCells(Grid g, int x, int y)
        {
            //calls revealnextCell function on every coordinate surrounding the target cell
            RevealNextCell(g, x - 1, y - 1);
            RevealNextCell(g, x - 1, y);
            RevealNextCell(g, x - 1, y + 1);
            RevealNextCell(g, x + 1, y);
            RevealNextCell(g, x, y - 1);
            RevealNextCell(g, x, y + 1);
            RevealNextCell(g, x + 1, y - 1);
            RevealNextCell(g, x + 1, y + 1);
        }

        private void RevealNextCell(Grid g, int x, int y)
        {

            //checks if cell is in bounds
            if (!(x >= 0 && x < g.Cols && y >= 0 && y < g.Rows)) return;

            //checks if cell is visited
            if (g.Cells[x, y].Visited) return;

            //checks if cell has any bombs around it
            if (g.Cells[x, y].LiveNeighbors == 0)
            {
                //sets cell to visited and calls recursive function to cycle through its neighbors
                g.Cells[x, y].Visited = true;
                revealSurroundingCells(g, x, y);
            }

            //checks if cell isn't a bomb
            else if (!g.Cells[x, y].Bomb)
            {
                //sets cell to visited
                g.Cells[x, y].Visited = true;
            }

        }


        public Grid createGrid(Controller c, int width, int height)
        {
            User user = (User)c.Session["user"];

            Grid grid = new Grid(-1, width, height, user.Id, false, 0);
            Cell[,] cells = new Cell[width, height];

            //intitialize cells
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    cells[x, y] = new Cell(x, y);
                }
            }

            //activate cells
            Random rand = new Random();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (rand.Next(0, 100) <= 9)
                    {
                        cells[x, y].Bomb = true;
                        cells[x, y].LiveNeighbors = 9;
                        for (int neighborX = -1; neighborX <= 1; neighborX++)
                        {
                            for (int neighborY = -1; neighborY <= 1; neighborY++)
                            {
                                if (neighborX == 0 && neighborY == 0)
                                {

                                }
                                else if (x + neighborX >= 0 && x + neighborX < width && y + neighborY >= 0 && y + neighborY < height)
                                {
                                    cells[x + neighborX, y + neighborY].LiveNeighbors++;
                                }

                            }
                        }

                    }
                }
            }
            grid.Cells = cells;



            //pass Grid with populated cells to dao query

            GameDAO gameDAO = new GameDAO();

            gameDAO.createGrid(grid);


            return grid;
        }

    }
}
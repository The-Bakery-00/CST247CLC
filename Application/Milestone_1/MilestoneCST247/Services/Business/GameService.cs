﻿using MilestoneCST247.Models;
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

        //returns grid for user
        public Grid findGrid(Controller c)
        {
            User user = (User)c.Session["user"];

            GameDAO gameDAO = new GameDAO();

            return gameDAO.findGrid(user);

        }

        //deletes grid from db
        public void removeGrid(Controller c)
        {
            User user = (User)c.Session["user"];

            GameDAO gameDAO = new GameDAO();

            gameDAO.deleteGrid(user);

        }

        //activates cell in grid
        public void activateCell(Grid g, int X, int Y)
        {

            // this will make every cell that has been click on as actice and show its value, will them push updated cells and grid to DB

            GameDAO gameDAO = new GameDAO();

            g.Cells[X, Y].Visited = true;  


            if (g.Cells[X, Y].Bomb)
            {
                for (int y = 0; y < g.Rows; y++)
                {
                    for (int x = 0; x < g.Cols; x++)
                    {
                        g.Cells[x, y].Visited = true;
                    }
                }
                System.Diagnostics.Debug.WriteLine("Hit bomb at: " + X + ", " + Y);
            }
            else
            {
                if (g.Cells[X, Y].LiveNeighbors == 0)
                    revealSurroundingCells(g, g.Cells[X, Y].X, g.Cells[X, Y].Y);

            }


            gameDAO.updateGrid(g);

        }

        private void revealSurroundingCells(Grid g, int x, int y)
        {
            //will check cells around cell that was clicked and reveal them
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

            //check is cell is out of the limits
            if (!(x >= 0 && x < g.Cols && y >= 0 && y < g.Rows)) return;

            //has the cell been visited already?
            if (g.Cells[x, y].Visited) return;

            //will check is cell around contains a bomb
            if (g.Cells[x, y].LiveNeighbors == 0)
            {
                //cell is marked as visited and recusively calls itself with neighnor cell
                g.Cells[x, y].Visited = true;
                revealSurroundingCells(g, x, y);
            }

            //is cell a bomb?
            else if (!g.Cells[x, y].Bomb)
            {
                g.Cells[x, y].Visited = true;
            }

        }


        public Grid createGrid(Controller c, int width, int height)
        {
            User user = (User)c.Session["user"];

            Grid grid = new Grid(-1, width, height, user.Id, false);
            Cell[,] cells = new Cell[width, height];

            //creates cells
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    cells[x, y] = new Cell(x, y);
                }
            }

            //use rand to see if cell will be bomb or now
            Random rand = new Random();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (rand.Next(0, 100) <= 10)
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



            //this will send the new grid and cells to gameDAO

            GameDAO gameDAO = new GameDAO();

            gameDAO.createGrid(grid);

            
            return grid;
        }

    }
}
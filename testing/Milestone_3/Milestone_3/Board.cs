using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestone_3
{
    abstract class Grid
    {
        // variables
        protected int rows;
        protected int columns;
        protected Cell[,] cells;


        // default constructor
        public Grid(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            cells = new Cell[columns, rows];
            generateCells();
            activateCells();
        }

        // initiailizes all the cells in the 2d cell array
        private void generateCells()
        {
            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    cells[x, y] = new Cell();
                    cells[x, y].setCollumn(x);
                    cells[x, y].setRow(y);
                }
            }
        }

        private void activateCells()
        {
            Random rand = new Random();
            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {

                    if (rand.Next(0, 100) <= 20)
                    {
                        cells[x, y].setLive(true);
                        cells[x, y].setLiveNeighbors(9);
                        for (int neighborX = -1; neighborX <= 1; neighborX++)
                        {
                            for (int neighborY = -1; neighborY <= 1; neighborY++)
                            {
                                if (neighborX == 0 && neighborY == 0)
                                {

                                }
                                else if (x + neighborX >= 0 && x + neighborX < columns && y + neighborY >= 0 && y + neighborY < rows)
                                {
                                    cells[x + neighborX, y + neighborY].incerementLiveNeighbors();
                                }

                            }
                        }
                    }
                }
            }
        }

        // displays the grid in the console
        public virtual void reveal()
        {

            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (cells[x, y].isVisited())
                    {
                        if (cells[x, y].isLive())
                        {
                            Console.Write("| * ");
                        }
                        else
                        {
                            Console.Write("| " + cells[x, y].getLiveNeighbors() + " ");
                        }
                    }
                    else
                    {
                        // not visited
                        if (cells[x, y].isLive())
                        {
                            Console.Write("| * ");
                        }
                        else
                        {
                            Console.Write("| " + cells[x, y].getLiveNeighbors() + " ");
                        }
                    }
                }
                Console.WriteLine("|");
            }

        }
    }
}

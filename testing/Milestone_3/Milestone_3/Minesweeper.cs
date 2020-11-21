using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestone_3
{
    class Minesweeper : Grid
    {
        private bool play;

        public Minesweeper(int rows, int columns) : base(rows, columns)
        {
            play = true;
        }

        public override void reveal()
        {
            // overrides the base class reveal function
            // and reveals minesweep grid

            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    // loop that iterates through every cell

                    if (cells[x, y].isVisited())
                    {
                        if (cells[x, y].isLive())
                        {
                            Console.Write("| * ");
                        }
                        else
                        {
                            if (cells[x, y].getLiveNeighbors() > 0)
                            {
                                Console.Write("| " + cells[x, y].getLiveNeighbors() + " ");
                            }
                            else
                            {
                                Console.Write("| ~ ");
                            }
                        }
                    }
                    else
                    {
                        Console.Write("| ? ");
                    }
                }
                Console.WriteLine("|");
            }
        }

        public void playGame()
        {
            //calls the reveal grid function and starts game using a while loop

            reveal();

            while (play)
            {
                //prompts user for input
                Console.WriteLine("Enter row " + 0 + "-" + (rows - 1) + ": ");
                int rowSelected = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter collumn " + 0 + "-" + (columns - 1) + ": ");
                int columnselected = int.Parse(Console.ReadLine());

                // is input is valid
                if (rowSelected > 0 && rowSelected < rows && columnselected > 0 && columnselected < columns)
                {
                    Console.Clear();
                    // has cell been visited
                    if (!cells[columnselected, rowSelected].isVisited())
                    {
                        // does the cell have a bomb or is 'live'
                        if (cells[columnselected, rowSelected].isLive())
                        {
                            // nested loop to go through every cell and set it to visited
                            for (int x = 0; x < columns; x++)
                            {
                                for (int y = 0; y < rows; y++)
                                {
                                    cells[x, y].setVisited(true);
                                }
                            }
                            // revels entire grid and ends game loop
                            reveal();
                            Console.WriteLine("Cell has a mine, game over!");
                            play = false;
                        }
                        else
                        {
                            cells[columnselected, rowSelected].setVisited(true);
                            reveal();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Cell already visited");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid cell entered");

                }


            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestone_3
{
    class Cell
    {
        //class member variables
        private int column;
        private int row;
        private Boolean visited;
        private Boolean live;
        private int liveNeighbors;


        public Cell()
        {
            //default constructor
            column = -1;
            row = -1;
            visited = false;
            live = false;
            liveNeighbors = 0;
        }

        //getters/setters
        public int getColumn()
        {
            return column;
        }

        public int getRow()
        {
            return row;
        }

        public Boolean isVisited()
        {
            return visited;
        }

        public Boolean isLive()
        {
            return live;
        }

        public int getLiveNeighbors()
        {
            return liveNeighbors;
        }

        public void setCollumn(int col)
        {
            column = col;
        }

        public void setRow(int r)
        {
            row = r;
        }

        public void setVisited(Boolean v)
        {
            visited = v;
        }

        public void setLive(Boolean l)
        {
            live = l;
        }

        public void setLiveNeighbors(int ln)
        {
            liveNeighbors = ln;
        }
        public void incerementLiveNeighbors()
        {
            liveNeighbors++;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MilestoneCST247.Models
{
    public class Grid
    {
        private int id;
        private int rows;
        private int cols;
        private int userid;
        private Boolean gameOver;
        private Cell[,] cells;
        private int clicks;

        public Grid(int id, int rows, int cols, int userid, bool gameOver, int clicks)
        {
            this.id = id;
            this.rows = rows;
            this.cols = cols;
            this.userid = userid;
            this.gameOver = gameOver;
            this.Clicks = clicks;
        }

        public int Id { get => id; set => id = value; }
        public int Rows { get => rows; set => rows = value; }
        public int Cols { get => cols; set => cols = value; }
        public int Userid { get => userid; set => userid = value; }
        public bool GameOver { get => gameOver; set => gameOver = value; }
        public Cell[,] Cells { get => cells; set => cells = value; }
        public int Clicks { get => clicks; set => clicks = value; }
    }
}
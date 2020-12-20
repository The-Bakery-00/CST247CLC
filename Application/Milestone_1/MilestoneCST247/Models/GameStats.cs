using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MilestoneCST247.Models
{
    public class GameStats : IComparable
    {
        [DataMember]
        public int Id { get; set; }
        public int Gridid { get; set; }
        public int Userid { get; set; }
        public int Clicks { get; set; }

        public GameStats ()
        {
            this.Id = 0;
            this.Gridid = 0;
            this.Userid = 0;
            this.Clicks = 0;
        }

        public int id { get => id; set => id = value; }
        public int gridid { get => gridid; set => gridid = value; }
        public int userid { get => userid; set => userid = value; }
        public int clicks { get => clicks; set => clicks = value; }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            GameStats p2 = (GameStats)obj;
            if (this.Clicks < p2.Clicks)
                return 1;
            if (this.Clicks > p2.Clicks)
                return -1;
            else
                return 0;
        }

    }
}
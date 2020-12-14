using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MilestoneCST247.Models
{
    public class GameStats
    {
        [DataMember]
        int id;
        int gridid;
        int userid;
        int clicks;

        public GameStats (int id, int gridid, int userid, int clicks)
        {
            this.id = id;
            this.gridid = gridid;
            this.userid = userid;
            this.clicks = clicks;
        }

        public int Id { get => id; set => id = value; }
        public int Gridd { get => gridid; set => gridid = value; }
        public int Userid { get => userid; set => userid = value; }
        public int Clicks { get => clicks; set => clicks = value; }


    }
}
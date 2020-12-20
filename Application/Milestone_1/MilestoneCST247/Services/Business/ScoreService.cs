using MilestoneCST247.Models;
using MilestoneCST247.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MilestoneCST247.Services.Business
{
    public class ScoreService
    {
        public List<GameStats> GetAllScores()
        {
            ScoreDAO service = new ScoreDAO();
            return service.GetAllScores();
        }
    }
}
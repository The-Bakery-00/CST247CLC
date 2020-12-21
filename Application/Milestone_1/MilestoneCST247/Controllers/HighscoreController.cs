using MilestoneCST247.Models;
using MilestoneCST247.Services.Data;
using MilestoneCST247.Services.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MilestoneCST247.Controllers
{
    public class HighscoreController : Controller
    {
        private readonly ILogger logger;

        public HighscoreController(ILogger logger)
        {
            this.logger = logger;
        }

        // GET: Highscore
        [CustomAuthorization]
        public ActionResult Index()
        {
            logger.Info("HighscoreController Entered.");
            try
            {
                ScoreDAO s = new ScoreDAO();
                User user = (User)Session["User"];
                Tuple<User, List<GameStats>> tuple = new Tuple<User, List<GameStats>>(user, s.GetAllScores().Take(5).ToList());
                logger.Info("Returned list of Highscores for HighscoreController");
                return View("Highscore", tuple);
            }
            catch
            {
                logger.Info("Error returning list of Highscores from HighscoreController");
                return View("Error");
            }
        }
    }
}

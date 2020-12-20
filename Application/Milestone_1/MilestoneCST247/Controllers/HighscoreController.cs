using MilestoneCST247.Models;
using MilestoneCST247.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MilestoneCST247.Controllers
{
    public class HighscoreController : Controller
    {
        // GET: Highscore
        [CustomAuthorization]
        public ActionResult Index()
        {
            try
            {
                ScoreDAO s = new ScoreDAO();
                User user = (User)Session["User"];
                Tuple<User, List<GameStats>> tuple = new Tuple<User, List<GameStats>>(user, s.GetAllScores().Take(5).ToList());
                return View("Highscore", tuple);
            }
            catch
            {
                return View("Error");
            }
        }
    }
}

using MilestoneCST247.Models;
using MilestoneCST247.Services.Business;
using MilestoneCST247.Services.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MilestoneCST247.Controllers
{
    public class GameController : Controller
    {
        private readonly ILogger logger;

        public GameController(ILogger logger)
        {
            this.logger = logger;
        }
        // GET: Game
        [CustomAuthorization]
        public ActionResult Index()
        {
            UserService userService = new UserService();
            logger.Info("Entered into the GameController");
            //check if user is logged in
            if (userService.loggedIn(this))
            {

                //create game service object
                GameService gameService = new GameService();

                //load grid for current user that is logged in
                User user = (User)Session["user"];
                Grid g = gameService.findGrid(user);

                //check if user has an existing grid saved in db
                if (g != null)
                {

                }
                else
                {
                    //generate a grid for user
                    g = gameService.createGrid(this, 10, 10);
                }


                //return game board view with newly generated or loaded grid for user
                logger.Info("GameController returned the Game view Successful");
                return View("Game", g);

            }

            else
            {
                //Error no user is currently logged in
                Error e = new Error("Cant access page! You are not Logged in! Please Log in!.");
                logger.Info("There was an failed attempt to enter the GameController");
                return View("Error", e);
            }
        }


        //cell click form handle
        [HttpPost]
        public ActionResult activateCell(String id, String x, String y)
        {

            UserService userService = new UserService();

            //check if user is logged in
            if (userService.loggedIn(this))
            {
                GameService gameService = new GameService();

                //load user grid from DB
                User user = (User)Session["user"];
                Grid g = gameService.findGrid(user);
                g.Clicks++;
                //activate cell that was passed in from game view
                gameService.activateCell(g, int.Parse(x), int.Parse(y));
                logger.Info("activateCell() method fired successful");
                // AJAX Partial view update
                return PartialView("GameBoard", g);
            }
            else
            {
                //user not logged in
                Error e = new Error("Cant access page! You are not Logged in! Please Log in!.");
                logger.Info("There was a failed attempt during activateCell()");
                return View("Error", e);
            }
        }


        [HttpGet]
        public ActionResult resetGrid()
        {
            //deletes grid from DB

            GameService gameService = new GameService();
            User user = (User)Session["user"];
            gameService.removeGrid(user);
            logger.Info("Game has been reset from the GameController");
            //returns view
            return Index();

        }
        [HttpGet]
        public ActionResult publishGrid()
        {
            //publishes game results to db

            //create userservice
            UserService userService = new UserService();


            //check if user is logged in
            if (userService.loggedIn(this))
            {
                GameService gameService = new GameService();

                //load user grid from db
                User user = (User)Session["user"];
                Grid g = gameService.findGrid(user);

                //call service function to publish stats
                gameService.publishGrid(g);
                logger.Info("Game has been published with publishGrid()");
                //return same view
                return Index();


            }
            else
            {
                //user not logged in
                Error e = new Error("You must be logged in to access this page.");
                logger.Info("There was and error submitting the highscore with publishGrid()");
                return View("Error", e);
            }
        }



    }
}
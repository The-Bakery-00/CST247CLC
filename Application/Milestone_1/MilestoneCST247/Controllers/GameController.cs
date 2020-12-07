using MilestoneCST247.Models;
using MilestoneCST247.Services.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MilestoneCST247.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index()
        {
            //create user service
            UserService userService = new UserService();

            //check if user is logged in
            if (userService.loggedIn(this))
            {

                //create game service object
                GameService gameService = new GameService();

                //load grid for user
                User user = (User)Session["user"];
                Grid g = gameService.findGrid(user);

                //check if user has an existing grid saved in db
                if (g != null)
                {
                    //grid exists for user


                    /*if (g.GameOver)
                    {
                        //regenerate new grid
                    }*/

                }
                else
                {
                    //generate a grid for user
                    g = gameService.createGrid(this, 10, 10);
                }


                //return game board view with grid model
                return View("Game", g);

            }

            else
            {
                //user isn't logged in
                Error e = new Error("You must be logged in to access this page.");

                return View("Error", e);
            }
        }




        //cell click form handle
        [HttpPost]
        public ActionResult activateCell(String id, String x, String y)
        {

            //create userservice
            UserService userService = new UserService();


            //check if user is logged in
            if (userService.loggedIn(this))
            {
                //update cell components
                GameService gameService = new GameService();

                //load user grid from db
                User user = (User)Session["user"];
                Grid g = gameService.findGrid(user);
                g.Clicks += 1;
                //activate cell logic
                gameService.activateCell(g, int.Parse(x), int.Parse(y));

                //return same view
                //return Index();

                return PartialView("GameBoard", g);


            }
            else
            {
                //user not logged in
                Error e = new Error("You must be logged in to access this page.");

                return View("Error", e);
            }
        }


        [HttpGet]
        public ActionResult resetGrid()
        {
            //deletes grid from db

            GameService gameService = new GameService();
            User user = (User)Session["user"];
            gameService.removeGrid(user);

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

                //return same view
                return Index();


            }
            else
            {
                //user not logged in
                Error e = new Error("You must be logged in to access this page.");

                return View("Error", e);
            }
        }



    }
}
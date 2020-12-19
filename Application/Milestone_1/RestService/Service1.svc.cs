using MilestoneCST247.Models;
using MilestoneCST247.Services.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IGameService
    {

        List<PublishedGame> games = new List<PublishedGame>();
        Boolean serverTest = false;

        public Service1()
        {
            // create a list of games

            GameService service = new GameService();

            serverTest = service.testService();

            games = service.getAllGames();
        }


        public StatsDTO GetAllStats()
        {
            //

            // return list of games
            // Note: normally this would call a Business Service and DAO to get this information

            StatsDTO dto;

            //check if data server is up
            if (!serverTest)
            {
                //return error 
                dto = new StatsDTO(1, "Data Server down", null);
            }
            else if (!games.Any())
            {
                //return error 
                dto = new StatsDTO(-1, "No Games found", null);

            }
            else
            {
                //return with data
                dto = new StatsDTO(0, "OK", games);

            }


                return dto;

            }

            public string GetData(int value)
            {
                return string.Format("You entered: {0}", value);
            }

            public CompositeType GetDataUsingDataContract(CompositeType composite)
            {
                if (composite == null)
                {
                    throw new ArgumentNullException("composite");
                }
                if (composite.BoolValue)
                {
                    composite.StringValue += "Suffix";
                }
                return composite;
            }
        }
    }


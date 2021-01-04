using Gremlin.Net.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineTravelGraph
{
    class Program
    {
        private static string hostname = "emmanueldev.gremlin.cosmos.azure.com";// https://emmanueldev.documents.azure.com:443/
        private static int port = 443;
        private static string authKey = "NxjAXAYS3qOCQv2ZKK6iDk7DJL8op5EiMCe7cLPVPAEBWg0FS3j8BnVdNwPxx6DedD2Asdna86GaGi3viesYeA==";
        private static string database = "my-airline-db";
        private static string collection = "Airline1";
        static async Task Main(string[] args)
        {
            var gremlinServer = new GremlinServer(
               hostname, port,
               enableSsl: true,
               username: "/dbs/" + database + "/colls/" + collection,
               password: authKey);

            var airLineRoutes = AirLine.LoadData();

            var constructqueries = Util.ConstructFlightRoute(airLineRoutes);

            await Util.ExecuteGraphQueriesAsync(gremlinServer, constructqueries);

            for (int i = 0; i < airLineRoutes.Count(); i++)
            {
                var adjacentCount = await Util.ExecuteGraphQueriesAsync(gremlinServer, 
                    new GremlinQuery($"Count adjecent cities", GremlinQuery.CalculateCountOfAdjacentCities(airLineRoutes[i].Arrival)));

                Console.WriteLine($"For {airLineRoutes[i].Arrival}, the count is =>  {adjacentCount}");
            }
        }
    }
}

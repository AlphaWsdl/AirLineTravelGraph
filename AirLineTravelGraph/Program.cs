using Gremlin.Net.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineTravelGraph
{
    class Program
    {
        private static string hostname = "emmacosmos.gremlin.cosmos.azure.com";
        private static int port = 443;
        private static string authKey = "bEtnhLU5vuwh28rmSTvxWQNZiKBAznXM94kbrST4MgfwQpPnJaosvbIN1i5U6dc3enmJffR3CTBpqO48xacZYw==";
        private static string database = "AirlineGremlin";
        private static string collection = "AirLine1";
        static async Task Main(string[] args) // write exram and apply optimization
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

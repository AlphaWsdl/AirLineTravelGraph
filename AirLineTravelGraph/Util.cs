using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;
//using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AirLineTravelGraph
{
    class Util
    {
        public static IEnumerable<GremlinQuery> ConstructFlightRoute(List<AirLine> airLinesRoutes)
        {
            var queriesForVertex = new List<GremlinQuery>();
            var queriesForEdges = new List<GremlinQuery>();

            var queries = new List<GremlinQuery>();

            var vertexCount = airLinesRoutes.Count;

        //    queriesForVertex.Add(new GremlinQuery($"Add vertex => {airLinesRoutes[0].Departure}", GremlinQuery.SetInitialVertexStatement(airLinesRoutes[0])));

            for (var i = 0; i < vertexCount; ++i)
            {
                queriesForVertex.Add(new GremlinQuery($"Add vertex => {airLinesRoutes[i].Departure}", GremlinQuery.SetVertexStatement(airLinesRoutes[i])));
            }

            queries.Insert(0, new GremlinQuery("Drop existing Graph", "g.V().drop()"));

            queriesForEdges.Add(new GremlinQuery($"Airline flies from City 1 to City 2",
             GremlinQuery.SetEdgeStatement("City 1", "City 2")));

            queriesForEdges.Add(new GremlinQuery($"Airline flies from City 2 to City 3",
               GremlinQuery.SetEdgeStatement("City 2", "City 3")));

            queriesForEdges.Add(new GremlinQuery($"Airline flies from City 3 to CIty 4",
                GremlinQuery.SetEdgeStatement("City 3", "City 4")));

            queriesForEdges.Add(new GremlinQuery($"Airline flies from City 3 to City 5",
                GremlinQuery.SetEdgeStatement("City 3", "City 5")));

            queriesForEdges.Add(new GremlinQuery($"Airline flies from City 3 to City 6",
                GremlinQuery.SetEdgeStatement("City 3", "City 6")));

            queriesForEdges.Add(new GremlinQuery($"Airline flies from City 7 to City 8",
                GremlinQuery.SetEdgeStatement("City 7", "City 8")));

            queriesForEdges.Add(new GremlinQuery($"Airline flies from City 8 to City 7",
                GremlinQuery.SetEdgeStatement("City 8", "City 7")));

            queriesForEdges.Add(new GremlinQuery($"Airline flies from City 7 to City 9",
                GremlinQuery.SetEdgeStatement("City 7", "City 9")));

            queriesForEdges.Add(new GremlinQuery($"Airline flies from City 9 to City 10",
               GremlinQuery.SetEdgeStatement("City 9", "City 10")));

            queries.Add(new GremlinQuery("Partitioning", "g.withStrategies(PartitionStrategy.build().partitionKey('partitionKey').readPartitions('arrival').create()).V()"));


            queries.AddRange(queriesForVertex);
            queries.AddRange(queriesForEdges);


            
            return queries;

        }



        public static async Task ExecuteGraphQueriesAsync(GremlinServer gremlinServer, IEnumerable<GremlinQuery> queries)
        {
            using (var gremlinClient = new GremlinClient(gremlinServer, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
            {
                foreach (var query in queries)
                {
                    try
                    {
                        Console.Write($"Executing: {query.Description}... ");
                        var xx = await gremlinClient.SubmitAsync<dynamic>(query.Statement);
                        Console.WriteLine("Ok");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        break;
                    }
                }
            }
        }


        public static async Task<string> ExecuteGraphQueriesAsync(GremlinServer gremlinServer, GremlinQuery query)
        {
            using (var gremlinClient = new GremlinClient(gremlinServer, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
            {
                string count = string.Empty;
                
                try
                {
                    Console.Write($"Executing: {query.Description}... ");
                    var count1 = await gremlinClient.SubmitAsync<dynamic>(query.Statement);

                    var result = JsonConvert.DeserializeObject<List<string>>(JsonConvert.SerializeObject(count1));
                    count = result[0];
                    Console.WriteLine("Ok");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                return count;
            }
        }

    }
}

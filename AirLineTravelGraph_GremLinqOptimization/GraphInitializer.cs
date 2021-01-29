using AirLineTravelGraph_GremLinqOptimization.Models;
using ExRam.Gremlinq.Core;
using ExRam.Gremlinq.Providers.WebSocket;
using Microsoft.Extensions.Logging;
using System;
using static ExRam.Gremlinq.Core.GremlinQuerySource;

namespace AirLineTravelGraph_GremLinqOptimization
{

    public class GraphInitializer
    {
        public static object SetUpConfig()
        {
            return g.ConfigureEnvironment(env => env
                .UseLogger(LoggerFactory
                        .Create(builder => builder
                            .AddFilter(__ => true)
                            //    .AddConsole()
                            )
                        .CreateLogger("Queries")))
                .ConfigureEnvironment(env => env.UseModel(GraphModel
                        .FromBaseTypes<Vertex, Edge>(lookup => lookup
                            .IncludeAssembliesOfBaseTypes())
                        //For CosmosDB, we exclude the 'PartitionKey' property from being included in updates.
                        .ConfigureProperties(model => model
                            .ConfigureElement<Vertex>(conf => conf
                                .IgnoreOnUpdate(x => x.PartitionKey))))
                    //Disable query logging for a noise free console output.
                    //Enable logging by setting the verbosity to anything but None.
                    .ConfigureOptions(options => options
                        .SetValue(WebSocketGremlinqOptions.QueryLogLogLevel, LogLevel.None))
                    .UseCosmosDb(builder => builder
                        .At(new Uri("wss://emmacosmos.gremlin.cosmos.azure.com:443/"), "AirlineGremlin2", "Airline1")
                        .AuthenticateBy("bEtnhLU5vuwh28rmSTvxWQNZiKBAznXM94kbrST4MgfwQpPnJaosvbIN1i5U6dc3enmJffR3CTBpqO48xacZYw==")
                        .ConfigureWebSocket(_ => _
                            .ConfigureGremlinClient(client => client
                                .ObserveResultStatusAttributes((requestMessage, statusAttributes) =>
                                {
                                   //Uncomment to log request charges for CosmosDB.
                                   //if (statusAttributes.TryGetValue("x-ms-total-request-charge", out var requestCharge))
                                   //    env.Logger.LogInformation($"Query {requestMessage.RequestId} had a RU charge of {requestCharge}.");
                               })))));
        }
    }
}

using AirLineTravelGraph_GremLinqOptimization.Models;
using ExRam.Gremlinq.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineTravelGraph_GremLinqOptimization
{
    class Program
    {
        private readonly IGremlinQuerySource _g;
        public Program()
        {
            _g = (IGremlinQuerySource)GraphInitializer.SetUpConfig();
        }

        private static async Task Main()
        {
            Console.WriteLine("Started");
            await new Program().Run();
            Console.WriteLine("\nFinished");
        }


        public async Task Run()
        {
            await CreateVertices(AirPlane.LoadAirPlaneInfos());
            await ConnectVertices();
            await CalculateVertexAdjacency();
        }


        private async Task CreateAVertex(AirPlane airplane)
        {
            await _g.AddV(airplane).FirstAsync(); ;
        }


        private async Task CreateVertices(List<AirPlane> airplanes)
        {
            await _g.V().Drop();

            Task[] tasks = new Task[airplanes.Count];
            for (int i = 0; i < airplanes.Count; i++)
            {
                tasks[i] = CreateAVertex(airplanes[i]);
            }
            Task.WaitAll(tasks);
        }


        private async Task CreateVertexRelationShip(string vertex1Id, string vertex2Id)
        {
            await _g.V(vertex1Id)
             .AddE<Flies>()
             .To(__ => __.V(vertex2Id))
             .FirstAsync();
        }


        public async Task ConnectVertices()
        {
            Console.WriteLine("Connecting Vertices");

            var tasks = new List<Task>();

            tasks.Add(CreateVertexRelationShip("Terminal 1", "Terminal 2"));
            tasks.Add(CreateVertexRelationShip("Terminal 2", "Terminal 3"));
            tasks.Add(CreateVertexRelationShip("Terminal 3", "Terminal 4"));
            tasks.Add(CreateVertexRelationShip("Terminal 3", "Terminal 5"));
            tasks.Add(CreateVertexRelationShip("Terminal 3", "Terminal 6"));
            tasks.Add(CreateVertexRelationShip("Terminal 7", "Terminal 8"));
            tasks.Add(CreateVertexRelationShip("Terminal 8", "Terminal 7"));
            tasks.Add(CreateVertexRelationShip("Terminal 7", "Terminal 9"));
            tasks.Add(CreateVertexRelationShip("Terminal 9", "Terminal 10"));

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine("Finished Connecting Vertices");
        }


        public async Task CalculateVertexAdjacency()
        {
            Console.WriteLine("Calculating Adjacency of Vertices");

            var tasks = new List<Task<int>>();
            long result = 0;

            result = _g.V("Terminal 1").Both().Count().GetAwaiter().GetResult().FirstOrDefault();
            Console.WriteLine($"For City 1, the count is =>  { result}");

            result = _g.V("Terminal 2").Both().Count().GetAwaiter().GetResult().FirstOrDefault();
            Console.WriteLine($"For City 2, the count is =>  { result}");

            result = _g.V("Terminal 3").Both().Count().GetAwaiter().GetResult().FirstOrDefault();
            Console.WriteLine($"For City 3, the count is =>  { result}");

            result = _g.V("Terminal 4").Both().Count().GetAwaiter().GetResult().FirstOrDefault();
            Console.WriteLine($"For City 4, the count is =>  { result}");

            result = _g.V("Terminal 5").Both().Count().GetAwaiter().GetResult().FirstOrDefault();
            Console.WriteLine($"For City 5, the count is =>  { result}");

            result = _g.V("Terminal 6").Both().Count().GetAwaiter().GetResult().FirstOrDefault();
            Console.WriteLine($"For City 6, the count is =>  { result}");

            result = _g.V("Terminal 7").Both().Count().GetAwaiter().GetResult().FirstOrDefault();
            Console.WriteLine($"For City 7, the count is =>  { result}");

            result = _g.V("Terminal 8").Both().Count().GetAwaiter().GetResult().FirstOrDefault();
            Console.WriteLine($"For City 8, the count is =>  { result}");

            result = _g.V("Terminal 9").Both().Count().GetAwaiter().GetResult().FirstOrDefault();
            Console.WriteLine($"For City 9, the count is =>  { result}");

            result = _g.V("Terminal 10").Both().Count().GetAwaiter().GetResult().FirstOrDefault();
            Console.WriteLine($"For City 10, the count is =>  { result}");
        }
    }
}
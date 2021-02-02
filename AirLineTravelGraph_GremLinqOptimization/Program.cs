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
            //await CalculateVertexAdjacency();
            await CalculateVertexAdjacency_Update();
        }


        private async Task CreateAVertex(AirPlane airplane)
        {
            await _g.AddV(airplane).FirstAsync(); ;
        }


        private async Task CreateVertices(List<AirPlane> airplanes)
        {
            await _g.V().Drop();

            List<Task> tasks = new List<Task>();
            for (int i = 0; i < airplanes.Count; i++)
            {
                tasks.Add(CreateAVertex(airplanes[i]));
            }
            Task.WaitAll(tasks.ToArray());
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

            var tasks = new List<Task>
            {
                CreateVertexRelationShip("Terminal 1", "Terminal 2"),
                CreateVertexRelationShip("Terminal 2", "Terminal 3"),
                CreateVertexRelationShip("Terminal 3", "Terminal 4"),
                CreateVertexRelationShip("Terminal 3", "Terminal 5"),
                CreateVertexRelationShip("Terminal 3", "Terminal 6"),
                CreateVertexRelationShip("Terminal 7", "Terminal 8"),
                CreateVertexRelationShip("Terminal 8", "Terminal 7"),
                CreateVertexRelationShip("Terminal 7", "Terminal 9"),
                CreateVertexRelationShip("Terminal 9", "Terminal 10")
            };

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine("Finished Connecting Vertices");
        }


        public async Task CalculateVertexAdjacency()
        {
            Console.WriteLine("Calculating Adjacency of Vertices");

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

        public async Task CalculateVertexAdjacency_Update()  
        {
            Console.WriteLine("Calculating Adjacency of Vertices");

            List<long> result = new List<long>();

            result = _g.V().Both().Count().GetAwaiter().GetResult().ToList();

            var entityGroups = await _g
               .V()
               .Group(g => g
                   .ByKey(__ => __.Id())
                   .ByValue(__ => __.Both().Count()))
               .FirstAsync();

            foreach (var entityGroup in entityGroups.OrderBy(i => i.Key))
            {
                Console.WriteLine($"For {(entityGroup.Key).ToString().Replace("Terminal", "City")}, the count is =>  { (entityGroup.Value) }");
            }

            var minCount = entityGroups.OrderBy(i => i.Value).FirstOrDefault().Value;
            var maxCount = entityGroups.OrderByDescending(i => i.Value).FirstOrDefault().Value;

            Console.WriteLine($"\nThe MAXIMUM adjecent route count is {maxCount}");
            Console.WriteLine($"The MINIMUM adjecent route count is {minCount}");
        }
    }
}
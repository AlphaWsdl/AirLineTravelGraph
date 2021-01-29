using ExRam.Gremlinq.Core.GraphElements;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirLineTravelGraph_GremLinqOptimization.Models
{
    public class Vertex : IVertex
    {
        public object? Id { get; set; }
        public string? Label { get; set; }
        public string PartitionKey { get; set; } = "PartitionKey";
    }
}
